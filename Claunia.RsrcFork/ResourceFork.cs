//
// ResourceFork.cs
//
// Author:
//       Natalia Portillo <claunia@claunia.com>
//
// Copyright (c) 2016 © Claunia.com
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

namespace Claunia.RsrcFork
{
    public class ResourceFork
    {
        Stream rsrcStream;
        ResourceHeader header;
        ResourceMap map;
        Dictionary<uint, ResourceTypeListItem> resourceTypeList;
        Dictionary<uint, Resource> resourceCache;
        List<uint> osTypes;

        /// <summary>
        /// Initializates a resource fork using a byte array as backend
        /// </summary>
        /// <param name="buffer">Buffer.</param>
        public ResourceFork(byte[] buffer)
        {
            rsrcStream = new MemoryStream(buffer, false);
            Init();
        }

        /// <summary>
        /// Initializates a resource fork using a stream as backed
        /// </summary>
        /// <param name="stream">Stream.</param>
        public ResourceFork(Stream stream)
        {
            rsrcStream = stream;
            Init();
        }

        ~ResourceFork()
        {
            if(rsrcStream != null)
                rsrcStream.Close();
        }

        public void Init()
        {
            header = new ResourceHeader();
            byte[] tmp = new byte[4];
            rsrcStream.Seek(0, SeekOrigin.Begin);
            rsrcStream.Read(tmp, 0, 4);
            header.resourceDataOff = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);
            rsrcStream.Read(tmp, 0, 4);
            header.resourceMapOff = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);
            rsrcStream.Read(tmp, 0, 4);
            header.resourceDataLen = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);
            rsrcStream.Read(tmp, 0, 4);
            header.resourceMapLen = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);

            if(header.resourceDataOff <= 0 || header.resourceDataOff <= 0 || header.resourceDataLen <= 0 || header.resourceMapLen <= 0)
                throw new InvalidCastException("Not a resource fork");

            if(header.resourceDataOff + header.resourceDataLen > rsrcStream.Length ||
               header.resourceMapOff + header.resourceMapLen > rsrcStream.Length)
                throw new InvalidCastException("Not a resource fork");

            map = new ResourceMap();
            map.header = new ResourceHeader();
            rsrcStream.Seek(header.resourceMapOff, SeekOrigin.Begin);
            rsrcStream.Read(tmp, 0, 4);
            map.header.resourceDataOff = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);
            rsrcStream.Read(tmp, 0, 4);
            map.header.resourceMapOff = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);
            rsrcStream.Read(tmp, 0, 4);
            map.header.resourceDataLen = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);
            rsrcStream.Read(tmp, 0, 4);
            map.header.resourceMapLen = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);

            if(map.header.resourceDataOff != header.resourceDataOff ||
               map.header.resourceDataLen != header.resourceDataLen ||
               map.header.resourceMapOff != header.resourceMapOff ||
               map.header.resourceMapLen != header.resourceMapLen)
                throw new InvalidCastException("Header copy is not same as header.");

            rsrcStream.Read(tmp, 0, 4);
            map.handleToNextMap = BitConverter.ToUInt32(tmp.Reverse().ToArray(), 0);
            tmp = new byte[2];
            rsrcStream.Read(tmp, 0, 2);
            map.fileRefNo = BitConverter.ToUInt16(tmp.Reverse().ToArray(), 0);
            rsrcStream.Read(tmp, 0, 2);
            map.attributes = BitConverter.ToUInt16(tmp.Reverse().ToArray(), 0);
            rsrcStream.Read(tmp, 0, 2);
            map.typeListOff = BitConverter.ToInt16(tmp.Reverse().ToArray(), 0);
            rsrcStream.Read(tmp, 0, 2);
            map.nameListOff = BitConverter.ToInt16(tmp.Reverse().ToArray(), 0);

            // Number of types is part of the resource type list not of the map

            rsrcStream.Seek(header.resourceMapOff + map.typeListOff, SeekOrigin.Begin);
            rsrcStream.Read(tmp, 0, 2);
            map.numberOfTypes = BitConverter.ToUInt16(tmp.Reverse().ToArray(), 0);

            resourceTypeList = new Dictionary<uint, ResourceTypeListItem>();
            osTypes = new List<uint>();

            for(int i = 0; i <= map.numberOfTypes; i++) {
                ResourceTypeListItem typeList = new ResourceTypeListItem();
                tmp = new byte[4];
                rsrcStream.Read(tmp, 0, 4);
                typeList.type = BitConverter.ToUInt32(tmp.Reverse().ToArray(), 0);
                tmp = new byte[2];
                rsrcStream.Read(tmp, 0, 2);
                typeList.resources = BitConverter.ToUInt16(tmp.Reverse().ToArray(), 0);
                rsrcStream.Read(tmp, 0, 2);
                typeList.referenceOff = BitConverter.ToInt16(tmp.Reverse().ToArray(), 0);

                resourceTypeList.Add(typeList.type, typeList);
                osTypes.Add(typeList.type);
            }

            resourceCache = new Dictionary<uint, Resource>();
        }

        /// <summary>
        /// Gets the resources with indicated type
        /// </summary>
        /// <returns>The resource.</returns>
        /// <param name="OSType">OSType.</param>
        public Resource GetResource(uint OSType)
        {
            Resource rsrc;

            if(resourceCache.TryGetValue(OSType, out rsrc))
               return rsrc;

            ResourceTypeListItem typeList;
            if(!resourceTypeList.TryGetValue(OSType, out typeList))
                return null;

            rsrc = new Resource(rsrcStream, (ushort)(typeList.resources + 1), header.resourceMapOff + map.typeListOff + typeList.referenceOff - 2, header.resourceMapOff + map.nameListOff, header.resourceDataOff, OSType);

            resourceCache.Add(OSType, rsrc);

            return rsrc;
        }

        /// <summary>
        /// Gets all OSTypes stored in this resource fork
        /// </summary>
        /// <returns>The types.</returns>
        public uint[] GetTypes()
        {
            return osTypes.ToArray();
        }

        /// <summary>
        /// Returns true if the specified OSType is present in this resource fork.
        /// </summary>
        /// <param name="OSType">OSType.</param>
        public bool ContainsKey(uint OSType)
        {
            return resourceTypeList.ContainsKey(OSType);
        }
    }
}

