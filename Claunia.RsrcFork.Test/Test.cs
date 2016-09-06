//
// Test.cs
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

using System.IO;
using NUnit.Framework;
using Resources;

namespace Claunia.RsrcFork.Test
{
    [TestFixture]
    public class Test
    {
        readonly byte[] DC42_Vers = { 0x06, 0x40, 0x80, 0x00, 0x00, 0x00, 0x09, 0x44, 0x69, 0x73, 0x6B, 0x20, 0x43, 0x6F, 0x70, 0x79, 0x1C, 0x34, 0x2E, 0x32, 0x2C,
            0x20, 0x64, 0x61, 0x74, 0x61, 0x20, 0x63, 0x68, 0x65, 0x63, 0x6B, 0x73, 0x75, 0x6D, 0x3D, 0x24, 0x41, 0x35, 0x31, 0x34, 0x37, 0x46, 0x37, 0x45 };
        readonly byte[] DC42_Str = { 0x09, 0x44, 0x69, 0x73, 0x6B, 0x20, 0x43, 0x6F, 0x70, 0x79 };

        [Test]
        public void TestDC42()
        {
            FileStream dc42Stream = new FileStream("Samples/DiskCopy42.rsrc", FileMode.Open, FileAccess.Read);
            ResourceFork rsrc = new ResourceFork(dc42Stream);

            uint[] types = rsrc.GetTypes();
            Assert.AreEqual(0x53545220, types[0]);
            Assert.AreEqual(0x76657273, types[1]);

            Resource str_ = rsrc.GetResource(0x53545220);
            Assert.IsNotNull(str_);

            short[] str_Ids = str_.GetIds();
            Assert.AreEqual(-16396, str_Ids[0]);
            Assert.AreEqual(10, str_.GetLength(-16396));

            byte[] str1 = str_.GetResource(-16396);
            Assert.AreEqual(DC42_Str, str1);

            string strStr = str_.GetName(-16396);
            Assert.IsNull(strStr);

            Resource vers = rsrc.GetResource(0x76657273);
            Assert.IsNotNull(vers);
            short[] versIds = vers.GetIds();
            Assert.AreEqual(1, versIds[0]);
            Assert.AreEqual(45, vers.GetLength(1));

            byte[] vers1 = vers.GetResource(1);
            Assert.AreEqual(DC42_Vers, vers1);

            string versStr = vers.GetName(1);
            Assert.IsNull(versStr);

            Version versDec = new Version(vers1);
            Assert.AreEqual(6, versDec.MajorVersion);
            Assert.AreEqual(40, versDec.MinorVersion);
            Assert.AreEqual(Version.DevelopmentStage.Final, versDec.DevStage);
            Assert.AreEqual(0, versDec.PreReleaseVersion);
            Assert.AreEqual(0, versDec.RegionCode);
            Assert.AreEqual("Disk Copy", versDec.VersionString);
            Assert.AreEqual("4.2, data checksum=$A5147F7E", versDec.VersionMessage);
        }

        [Test]
        public void TestDC6()
        {
            FileStream dc6Stream = new FileStream("Samples/DiskCopy6.rsrc", FileMode.Open, FileAccess.Read);
            ResourceFork rsrc = new ResourceFork(dc6Stream);

            uint[] types = rsrc.GetTypes();
            Assert.AreEqual(0x53545220, types[0]);
            Assert.AreEqual(0x6263656D, types[1]);
            Assert.AreEqual(0x62636D23, types[2]);
            Assert.AreEqual(0x76657273, types[3]);

            Resource str_ = rsrc.GetResource(0x53545220);
            Assert.IsNotNull(str_);

            short[] str_Ids = str_.GetIds();
            Assert.AreEqual(-16396, str_Ids[0]);
            Assert.AreEqual(10, str_.GetLength(-16396));

            byte[] str1 = str_.GetResource(-16396);
            Assert.AreEqual(DC42_Str, str1);

            string strStr = str_.GetName(-16396);
            Assert.IsNull(strStr);

            Resource vers = rsrc.GetResource(0x76657273);
            Assert.IsNotNull(vers);
            short[] versIds = vers.GetIds();
            Assert.AreEqual(1, versIds[0]);
            Assert.AreEqual(66, vers.GetLength(1));

            string versStr = vers.GetName(1);
            Assert.IsNull(versStr);

            Resource bcem = rsrc.GetResource(0x6263656D);
            Assert.IsNotNull(bcem);
            short[] bcemIds = bcem.GetIds();
            Assert.AreEqual(128, bcemIds[0]);
            Assert.AreEqual(224, bcem.GetLength(128));
            string bcemStr = bcem.GetName(128);
            Assert.IsNull(bcemStr);

            Resource bceS = rsrc.GetResource(0x6263656D);
            Assert.IsNotNull(bceS);
            short[] bceSIds = bceS.GetIds();
            Assert.AreEqual(128, bceSIds[0]);
            Assert.AreEqual(224, bcem.GetLength(128));
            string bceSStr = bceS.GetName(128);
            Assert.IsNull(bceSStr);

            byte[] vers1 = vers.GetResource(1);
            Version versDec = new Version(vers1);
            Assert.AreEqual(6, versDec.MajorVersion);
            Assert.AreEqual(40, versDec.MinorVersion);
            Assert.AreEqual(Version.DevelopmentStage.Final, versDec.DevStage);
            Assert.AreEqual(0, versDec.PreReleaseVersion);
            Assert.AreEqual(0, versDec.RegionCode);
            Assert.AreEqual("6.4 Disk Copy", versDec.VersionString);
            Assert.AreEqual("Mac? OS HFS 1680K disk image\rCRC28: $07213FB7", versDec.VersionMessage);
        }

        [Test]
        public void TestSMI()
        {
            FileStream smiStream = new FileStream("Samples/SelfMountingImage.rsrc", FileMode.Open, FileAccess.Read);
            ResourceFork rsrc = new ResourceFork(smiStream);

            uint[] types = rsrc.GetTypes();
            Assert.AreEqual(29, types.Length);
            Assert.AreEqual(0x424E444C, types[0]);
            Assert.AreEqual(0x434F4445, types[1]);
            Assert.AreEqual(0x44415441, types[2]);
            Assert.AreEqual(0x4449544C, types[3]);
            Assert.AreEqual(0x444C4F47, types[4]);
            Assert.AreEqual(0x44525652, types[5]);
            Assert.AreEqual(0x46524546, types[6]);
            Assert.AreEqual(0x49434E23, types[7]);
            Assert.AreEqual(0x4D505352, types[8]);
            Assert.AreEqual(0x4D574242, types[9]);
            Assert.AreEqual(0x50415420, types[10]);

            Resource code = rsrc.GetResource(0x434F4445);
            Assert.IsNotNull(code);
            short[] codeIds = code.GetIds();
            Assert.AreEqual(0, codeIds[0]);
            Assert.AreEqual(1, codeIds[1]);
            Assert.AreEqual(35130, code.GetLength(1));
            string codeStr = code.GetName(1);
            Assert.AreEqual("First Segment", codeStr); //"First Segment"

            Resource bndl = rsrc.GetResource(0x424E444C);
            Assert.IsNotNull(bndl);
            short[] bndlIds = bndl.GetIds();
            Assert.AreEqual(128, bndlIds[0]);
            Assert.AreEqual(28, bndl.GetLength(128));
            string bndlStr = bndl.GetName(128);
            Assert.IsNull(bndlStr);

            Resource vers = rsrc.GetResource(0x76657273);
            Assert.IsNotNull(vers);
            byte[] vers1 = vers.GetResource(1);
            Version versDec = new Version(vers1);
            Assert.AreEqual(1, versDec.MajorVersion);
            Assert.AreEqual(10, versDec.MinorVersion);
            Assert.AreEqual(Version.DevelopmentStage.Beta, versDec.DevStage);
            Assert.AreEqual(6, versDec.PreReleaseVersion);
            Assert.AreEqual(0, versDec.RegionCode);
            Assert.AreEqual("1.1b6", versDec.VersionString);
            Assert.AreEqual("1.1b6, Copyright 1997-2001 Apple Computer, Inc.", versDec.VersionMessage);
        }

        [Test]
        public void TestUDIF()
        {
            FileStream udifStream = new FileStream("Samples/UDIF.rsrc", FileMode.Open, FileAccess.Read);
            ResourceFork rsrc = new ResourceFork(udifStream);

            uint[] types = rsrc.GetTypes();
            Assert.AreEqual(2, types.Length);
            Assert.AreEqual(0x626C6B78, types[0]);
            Assert.AreEqual(0x706C7374, types[1]);

            Resource blkx = rsrc.GetResource(0x626C6B78);
            Assert.IsNotNull(blkx);
            short[] blkxIds = blkx.GetIds();
            Assert.AreEqual(0, blkxIds[0]);
            Assert.AreEqual(524, blkx.GetLength(0));
            string blkxStr = blkx.GetName(0);
            Assert.AreEqual("Whole Device (Apple_XXX : 0)", blkxStr); //"First Segment"

            Resource plst = rsrc.GetResource(0x706C7374);
            Assert.IsNotNull(plst);
            short[] plstIds = plst.GetIds();
            Assert.AreEqual(0, plstIds[0]);
            Assert.AreEqual(1544, plst.GetLength(0));
            string plstStr = plst.GetName(0);
            Assert.IsNull(plstStr);

            Resource vers = rsrc.GetResource(0x76657273);
            Assert.IsNull(vers);
        }
    }
}

