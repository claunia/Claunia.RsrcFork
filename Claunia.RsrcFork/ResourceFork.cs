//
// ResourceFork.cs
//
// Author:
//       Natalia Portillo <claunia@claunia.com>
//
// Copyright (c) 2016-2019 Natalia Portillo
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Claunia.RsrcFork;

/// <summary>This class represents a resource fork.</summary>
public class ResourceFork
{
    readonly Stream                        _rsrcStream;
    ResourceHeader                         _header;
    ResourceMap                            _map;
    List<uint>                             _osTypes;
    Dictionary<uint, Resource>             _resourceCache;
    Dictionary<uint, ResourceTypeListItem> _resourceTypeList;

    /// <summary>Initializates a resource fork using a byte array as backend.</summary>
    /// <param name="buffer">Buffer.</param>
    public ResourceFork(byte[] buffer)
    {
        _rsrcStream = new MemoryStream(buffer, false);
        Init();
    }

    /// <summary>Initializates a resource fork using a stream as backed.</summary>
    /// <param name="stream">Stream.</param>
    public ResourceFork(Stream stream)
    {
        _rsrcStream = stream;
        Init();
    }

    /// <summary>Cleans up this instances and closes the underlying stream.</summary>
    ~ResourceFork() => _rsrcStream?.Dispose();

    /// <summary>Initializes this instance.</summary>
    void Init()
    {
        _header = new ResourceHeader();
        byte[] tmp = new byte[4];
        _rsrcStream.Seek(0, SeekOrigin.Begin);
        _rsrcStream.Read(tmp, 0, 4);
        _header.ResourceDataOff = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);
        _rsrcStream.Read(tmp, 0, 4);
        _header.ResourceMapOff = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);
        _rsrcStream.Read(tmp, 0, 4);
        _header.ResourceDataLen = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);
        _rsrcStream.Read(tmp, 0, 4);
        _header.ResourceMapLen = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);

        if(_header.ResourceDataOff <= 0 ||
           _header.ResourceMapOff  <= 0 ||
           _header.ResourceDataLen <= 0 ||
           _header.ResourceMapLen  <= 0)
            throw new InvalidCastException("Not a resource fork");

        if(_header.ResourceDataOff + _header.ResourceDataLen > _rsrcStream.Length ||
           _header.ResourceMapOff  + _header.ResourceMapLen  > _rsrcStream.Length)
            throw new InvalidCastException("Not a resource fork");

        _map = new ResourceMap
        {
            Header = new ResourceHeader()
        };

        _rsrcStream.Seek(_header.ResourceMapOff, SeekOrigin.Begin);
        _rsrcStream.Read(tmp, 0, 4);
        _map.Header.ResourceDataOff = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);
        _rsrcStream.Read(tmp, 0, 4);
        _map.Header.ResourceMapOff = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);
        _rsrcStream.Read(tmp, 0, 4);
        _map.Header.ResourceDataLen = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);
        _rsrcStream.Read(tmp, 0, 4);
        _map.Header.ResourceMapLen = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);

        if(_map.Header.ResourceDataOff != _header.ResourceDataOff ||
           _map.Header.ResourceDataLen != _header.ResourceDataLen ||
           _map.Header.ResourceMapOff  != _header.ResourceMapOff  ||
           _map.Header.ResourceMapLen  != _header.ResourceMapLen)
            throw new InvalidCastException("Header copy is not same as header.");

        _rsrcStream.Read(tmp, 0, 4);
        _map.HandleToNextMap = BitConverter.ToUInt32(tmp.Reverse().ToArray(), 0);
        tmp                  = new byte[2];
        _rsrcStream.Read(tmp, 0, 2);
        _map.FileRefNo = BitConverter.ToUInt16(tmp.Reverse().ToArray(), 0);
        _rsrcStream.Read(tmp, 0, 2);
        _map.Attributes = BitConverter.ToUInt16(tmp.Reverse().ToArray(), 0);
        _rsrcStream.Read(tmp, 0, 2);
        _map.TypeListOff = BitConverter.ToInt16(tmp.Reverse().ToArray(), 0);
        _rsrcStream.Read(tmp, 0, 2);
        _map.NameListOff = BitConverter.ToInt16(tmp.Reverse().ToArray(), 0);

        // Number of types is part of the resource type list not of the map

        _rsrcStream.Seek(_header.ResourceMapOff + _map.TypeListOff, SeekOrigin.Begin);
        _rsrcStream.Read(tmp, 0, 2);
        _map.NumberOfTypes = BitConverter.ToUInt16(tmp.Reverse().ToArray(), 0);

        _resourceTypeList = new Dictionary<uint, ResourceTypeListItem>();
        _osTypes          = new List<uint>();

        for(int i = 0; i <= _map.NumberOfTypes; i++)
        {
            ResourceTypeListItem typeList = new();
            tmp = new byte[4];
            _rsrcStream.Read(tmp, 0, 4);
            typeList.Type = BitConverter.ToUInt32(tmp.Reverse().ToArray(), 0);
            tmp           = new byte[2];
            _rsrcStream.Read(tmp, 0, 2);
            typeList.Resources = BitConverter.ToUInt16(tmp.Reverse().ToArray(), 0);
            _rsrcStream.Read(tmp, 0, 2);
            typeList.ReferenceOff = BitConverter.ToInt16(tmp.Reverse().ToArray(), 0);

            _resourceTypeList.Add(typeList.Type, typeList);
            _osTypes.Add(typeList.Type);
        }

        _resourceCache = new Dictionary<uint, Resource>();
    }

    /// <summary>Gets the resources with indicated type</summary>
    /// <returns>The resource.</returns>
    /// <param name="OSType">OSType.</param>
    public Resource GetResource(uint OSType)
    {
        if(_resourceCache.TryGetValue(OSType, out Resource rsrc))
            return rsrc;

        if(!_resourceTypeList.TryGetValue(OSType, out ResourceTypeListItem typeList))
            return null;

        rsrc = new Resource(_rsrcStream, (ushort)(typeList.Resources + 1),
                            _header.ResourceMapOff + _map.TypeListOff + typeList.ReferenceOff - 2,
                            _header.ResourceMapOff + _map.NameListOff, _header.ResourceDataOff, OSType);

        _resourceCache.Add(OSType, rsrc);

        return rsrc;
    }

    /// <summary>Gets all OSTypes stored in this resource fork</summary>
    /// <returns>The types.</returns>
    public uint[] GetTypes() => _osTypes.ToArray();

    /// <summary>Returns true if the specified OSType is present in this resource fork.</summary>
    /// <param name="OSType">OSType.</param>
    public bool ContainsKey(uint OSType) => _resourceTypeList.ContainsKey(OSType);
}