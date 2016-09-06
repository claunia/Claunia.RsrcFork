//
// Resource.cs
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
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Claunia.RsrcFork
{
    public class Resource
    {
        readonly Dictionary<short, string> resourceNames;
        readonly Dictionary<short, ResourceData> resources;
        readonly Dictionary<short, byte[]> resourceCache;
        readonly Stream rsrcStream;
        readonly List<short> ids;

        struct ResourceData
        {
            public long offset;
            public long length;
        }

        /// <summary>
        /// Initializates the specified resource type
        /// </summary>
        /// <param name="stream">Stream where the resources of this reside.</param>
        /// <param name="resources">How many resource of this type are.</param>
        /// <param name="referenceListOff">Offset from start of stream to reference list for this type.</param>
        /// <param name="resourceNameOffset">Offset from start of stream to resource name list.</param>
        /// <param name="resourceDataOffset">Offset from start of stream to resource data.</param>
        /// <param name="OSType">Resource type.</param>
        internal Resource(Stream stream, ushort resources, long referenceListOff, long resourceNameOffset, long resourceDataOffset, uint OSType)
        {
            rsrcStream = stream;
            Dictionary<short, ResourceTypeReferenceListItem> referenceLists = new Dictionary<short, ResourceTypeReferenceListItem>();
            resourceNames = new Dictionary<short, string>();
            this.resources = new Dictionary<short, ResourceData>();
            ids = new List<short>();
            byte[] tmp;

            rsrcStream.Seek(referenceListOff + 2, SeekOrigin.Begin);
            for(int i = 0; i < resources; i++)
            {
                ResourceTypeReferenceListItem item = new ResourceTypeReferenceListItem();
                tmp = new byte[2];
                rsrcStream.Read(tmp, 0, 2);
                item.id = BitConverter.ToInt16(tmp.Reverse().ToArray(), 0);
                rsrcStream.Read(tmp, 0, 2);
                item.nameOff = BitConverter.ToInt16(tmp.Reverse().ToArray(), 0);
                tmp = new byte[4];
                rsrcStream.Read(tmp, 0, 4);
                item.attributes = tmp[0];
                tmp[0] = 0;
                item.dataOff = BitConverter.ToInt32(tmp.Reverse().ToArray(), 0);
                rsrcStream.Read(tmp, 0, 4);
                item.handle = BitConverter.ToUInt32(tmp.Reverse().ToArray(), 0);

                referenceLists.Add(item.id, item);
                ids.Add(item.id);
            }

            foreach(ResourceTypeReferenceListItem item in referenceLists.Values)
            {
                string name;
                ResourceData data = new ResourceData();

                if(item.nameOff == -1)
                    name = null;
                else
                {
                    rsrcStream.Seek(resourceNameOffset + item.nameOff, SeekOrigin.Begin);
                    byte nameLen = (byte)rsrcStream.ReadByte();
                    byte[] nameBytes = new byte[nameLen];
                    rsrcStream.Read(nameBytes, 0, nameLen);
                    // TODO: Use MacRoman
                    name = Encoding.ASCII.GetString(nameBytes);
                }

                rsrcStream.Seek(resourceDataOffset + item.dataOff, SeekOrigin.Begin);
                tmp = new byte[4];
                rsrcStream.Read(tmp, 0, 4);
                data.offset = rsrcStream.Position;
                data.length = BitConverter.ToUInt32(tmp.Reverse().ToArray(), 0);

                resourceNames.Add(item.id, name);
                this.resources.Add(item.id, data);
            }

            resourceCache = new Dictionary<short, byte[]>();
        }

        /// <summary>
        /// Gets the name of the specified resource id, or null if there is no name
        /// </summary>
        /// <returns>The name.</returns>
        /// <param name="id">Identifier.</param>
        public string GetName(short id)
        {
            string name;
            return resourceNames.TryGetValue(id, out name) ? name : null;
        }

        /// <summary>
        /// Gets the resource contents
        /// </summary>
        /// <returns>The resource.</returns>
        /// <param name="id">Identifier.</param>
        public byte[] GetResource(short id)
        {
            byte[] resource;

            if(resourceCache.TryGetValue(id, out resource))
                return resource;

            ResourceData data;

            if(!resources.TryGetValue(id, out data))
                return null;

            resource = new byte[data.length];
            rsrcStream.Seek(data.offset, SeekOrigin.Begin);
            rsrcStream.Read(resource, 0, resource.Length);

            resourceCache.Add(id, resource);

            return resource;
        }

        public long GetLength(short id)
        {
            ResourceData data;
            return !resources.TryGetValue(id, out data) ? 0 : data.length;
        }

        public short[] GetIds()
        {
            return ids.ToArray();
        }

        public bool ContainsId(short id)
        {
            return ids.Contains(id);
        }
    }
}

