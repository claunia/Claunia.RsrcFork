//
// Resource.cs
//
// Author:
//       Natalia Portillo <claunia@claunia.com>
//
// Copyright (c) 2016-2021 Natalia Portillo
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
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Claunia.RsrcFork;

/// <summary>This class represents a resource type.</summary>
public class Resource
{
    readonly List<short>                     _ids;
    readonly Dictionary<short, byte[]>       _resourceCache;
    readonly Dictionary<short, string>       _resourceNames;
    readonly Dictionary<short, ResourceData> _resources;
    readonly Stream                          _rsrcStream;

    /// <summary>Initializates the specified resource type.</summary>
    /// <param name="stream">Stream where the resources of this reside.</param>
    /// <param name="resources">How many resource of this type are.</param>
    /// <param name="referenceListOff">Offset from start of stream to reference list for this type.</param>
    /// <param name="resourceNameOffset">Offset from start of stream to resource name list.</param>
    /// <param name="resourceDataOffset">Offset from start of stream to resource data.</param>
    /// <param name="OSType">Resource type.</param>
    internal Resource(Stream stream, ushort resources, long referenceListOff, long resourceNameOffset,
                      long resourceDataOffset, uint OSType)
    {
        _rsrcStream = stream;
        Dictionary<short, ResourceTypeReferenceListItem> referenceLists = new();
        _resourceNames = new Dictionary<short, string>();
        _resources     = new Dictionary<short, ResourceData>();
        _ids           = new List<short>();
        byte[] tmp;

        _rsrcStream.Seek(referenceListOff + 2, SeekOrigin.Begin);

        for(int i = 0; i < resources; i++)
        {
            ResourceTypeReferenceListItem item = new();
            tmp = new byte[2];
            _rsrcStream.Read(tmp, 0, 2);
            item.Id = BitConverter.ToInt16(tmp.Reverse().ToArray(), 0);
            _rsrcStream.Read(tmp, 0, 2);
            item.NameOff = BitConverter.ToInt16(tmp.Reverse().ToArray(), 0);
            tmp          = new byte[4];
            _rsrcStream.Read(tmp, 0, 4);
            item.Attributes = tmp[0];
            tmp[0]          = 0;
            item.DataOff    = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);
            _rsrcStream.Read(tmp, 0, 4);
            item.Handle = BitConverter.ToUInt32(tmp.Reverse().ToArray(), 0);

            referenceLists.Add(item.Id, item);
            _ids.Add(item.Id);
        }

        foreach(ResourceTypeReferenceListItem item in referenceLists.Values)
        {
            string       name;
            ResourceData data = new();

            if(item.NameOff == -1)
                name = null;
            else
            {
                _rsrcStream.Seek(resourceNameOffset + item.NameOff, SeekOrigin.Begin);
                byte   nameLen   = (byte)_rsrcStream.ReadByte();
                byte[] nameBytes = new byte[nameLen];
                _rsrcStream.Read(nameBytes, 0, nameLen);

                // TODO: Use MacRoman
                name = Encoding.ASCII.GetString(nameBytes);
            }

            _rsrcStream.Seek(resourceDataOffset + item.DataOff, SeekOrigin.Begin);
            tmp = new byte[4];
            _rsrcStream.Read(tmp, 0, 4);
            data.Offset = _rsrcStream.Position;
            data.Length = BitConverter.ToUInt32(tmp.Reverse().ToArray(), 0);

            _resourceNames.Add(item.Id, name);
            _resources.Add(item.Id, data);
        }

        _resourceCache = new Dictionary<short, byte[]>();
    }

    /// <summary>Gets the name of the specified resource id, or null if there is no name.</summary>
    /// <returns>The name.</returns>
    /// <param name="id">Identifier.</param>
    public string GetName(short id) => _resourceNames.TryGetValue(id, out string name) ? name : null;

    /// <summary>Gets the resource contents.</summary>
    /// <returns>The resource.</returns>
    /// <param name="id">Identifier.</param>
    public byte[] GetResource(short id)
    {
        if(_resourceCache.TryGetValue(id, out byte[] resource))
            return resource;

        if(!_resources.TryGetValue(id, out ResourceData data))
            return null;

        resource = new byte[data.Length];
        _rsrcStream.Seek(data.Offset, SeekOrigin.Begin);
        _rsrcStream.Read(resource, 0, resource.Length);

        _resourceCache.Add(id, resource);

        return resource;
    }

    /// <summary>Gets the length of the resource specified by ID.</summary>
    /// <returns>The length.</returns>
    /// <param name="id">Resource identifier.</param>
    public long GetLength(short id) => !_resources.TryGetValue(id, out ResourceData data) ? 0 : data.Length;

    /// <summary>Gets the IDs of all the resources contained by this instance.</summary>
    /// <returns>The identifiers.</returns>
    public short[] GetIds() => _ids.ToArray();

    /// <summary>Checks if the resource specified by ID is contained by this instance.</summary>
    /// <returns><c>true</c>, if the resource is contained in this instance, <c>false</c> otherwise.</returns>
    /// <param name="id">Resource identifier.</param>
    public bool ContainsId(short id) => _ids.Contains(id);

    struct ResourceData
    {
        public long Offset;
        public long Length;
    }
}