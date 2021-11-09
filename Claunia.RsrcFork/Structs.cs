//
// Structs.cs
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
// THE SOFTWARE.

namespace Claunia.RsrcFork;

internal struct ResourceHeader
{
    /// <summary>Offset from start of resource fork to resource data</summary>
    public int ResourceDataOff;
    /// <summary>Offset from start of resource fork to resource map</summary>
    public int ResourceMapOff;
    /// <summary>Length of resource data</summary>
    public int ResourceDataLen;
    /// <summary>Length of resource map</summary>
    public int ResourceMapLen;
}

internal struct ResourceMap
{
    /// <summary>Copy of the resource fork header</summary>
    public ResourceHeader Header;
    /// <summary>Reserved for handle to next resource map</summary>
    public uint HandleToNextMap;
    /// <summary>Reserved for file reference number</summary>
    public ushort FileRefNo;
    /// <summary>Resource fork attributes</summary>
    public ushort Attributes;
    /// <summary>Offset from start of resource map to resource type list</summary>
    public short TypeListOff;
    /// <summary>Offset from start of resource map to resource name list</summary>
    public short NameListOff;
    /// <summary>Number of types in the map minus 1</summary>
    public ushort NumberOfTypes;
}

internal struct ResourceTypeListItem
{
    /// <summary>Resource type</summary>
    public uint Type;
    /// <summary>Number of resources of this type minus 1</summary>
    public ushort Resources;
    /// <summary>Offset from beginning of resource type list to reference list</summary>
    public short ReferenceOff;
}

internal struct ResourceTypeReferenceListItem
{
    /// <summary>Resource ID</summary>
    public short Id;
    /// <summary>Offset from beginning of resource name list to resource name. -1 if it does not have a name</summary>
    public short NameOff;
    /// <summary>Resource attributes</summary>
    public byte Attributes;
    /// <summary>Offset from beginning of resource data to resource. First byte is <see cref="Attributes" />.</summary>
    public int DataOff;
    /// <summary>Reserved for handle to resource</summary>
    public uint Handle;
}