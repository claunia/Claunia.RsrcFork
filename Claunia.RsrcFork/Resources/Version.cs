//
// Version.cs
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
using System.Linq;
using Claunia.RsrcFork;

namespace Resources
{
    public class Version
    {
        static uint ostype = 0x76657273;

        #region On-disk structure
        public byte MajorVersion;
        public byte MinorVersion;
        public DevelopmentStage DevStage;
        public byte PreReleaseVersion;
        public ushort RegionCode;
        public string VersionString;
        public string VersionMessage;
        #endregion

        public enum DevelopmentStage : byte
        {
            PreAlpha = 0x20,
            Alpha = 0x40,
            Beta = 0x60,
            Final = 0x80
        }

        public Version(byte[] resource)
        {
            byte[] tmpShort, tmpStr, tmpMsg;

            tmpShort = new byte[2];

            MajorVersion = BCDToNumber(resource[0]);
            MinorVersion = BCDToNumber(resource[1]);
            DevStage = (DevelopmentStage)resource[2];
            PreReleaseVersion = BCDToNumber(resource[3]);
            Array.Copy(resource, 4, tmpShort, 0, 2);
            RegionCode = BitConverter.ToUInt16(tmpShort.Reverse().ToArray(), 0);
            tmpStr = new byte[resource[6] + 1];
            Array.Copy(resource, 6, tmpStr, 0, tmpStr.Length);
            VersionString = PascalString.GetString(tmpStr);
            System.Console.WriteLine("{0} == {1}, \"{2}\"", resource[6], tmpStr.Length, VersionString);
            tmpMsg = new byte[resource[6 + tmpStr.Length] + 1];
            Array.Copy(resource, 6 + tmpStr.Length, tmpMsg, 0, tmpMsg.Length);
            VersionMessage = PascalString.GetString(tmpMsg);
        }

        public byte[] GetBytes()
        {
            byte[] tmpShort, tmpStr, tmpMsg;
            tmpShort = BitConverter.GetBytes(RegionCode).Reverse().ToArray();
            tmpStr = PascalString.GetBytes(VersionString);
            tmpMsg = PascalString.GetBytes(VersionMessage);

            byte[] vers = new byte[6 + tmpStr.Length + tmpMsg.Length];

            vers[0] = NumberToBCD(MajorVersion);
            vers[1] = NumberToBCD(MinorVersion);
            vers[2] = (byte)DevStage;
            vers[3] = NumberToBCD(PreReleaseVersion);
            Array.Copy(tmpShort, 0, vers, 4, 2);
            Array.Copy(tmpStr, 0, vers, 6, tmpStr.Length);
            Array.Copy(tmpMsg, 0, vers, 6 + tmpStr.Length, tmpMsg.Length);

            return vers;
        }

        public static uint OSType {
            get {
                return ostype;
            }
        }

        byte BCDToNumber(byte bcd)
        {
            return Convert.ToByte(string.Format("{0:X2}", bcd), 10);
        }

        byte NumberToBCD(byte number)
        {
            if(number >= 100)
                number = 99;

            return Convert.ToByte(string.Format("{0:D2}", number), 16);
        }
    }
}

