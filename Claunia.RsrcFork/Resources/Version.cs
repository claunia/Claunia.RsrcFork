﻿//
// Version.cs
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
using System.Linq;
using Claunia.RsrcFork;

namespace Resources;

/// <summary>This class handles the "VERS" resource fork</summary>
public class Version
{
    /// <summary>Known development stages</summary>
    public enum DevelopmentStage : byte
    {
        /// <summary>Pre-alpha.</summary>
        PreAlpha = 0x20,
        /// <summary>Alpha.</summary>
        Alpha = 0x40,
        /// <summary>Beta.</summary>
        Beta = 0x60,
        /// <summary>Final release.</summary>
        Final = 0x80
    }

    /// <summary>Initializes a new instance of the <see cref="T:Resources.Version" /> class.</summary>
    /// <param name="resource">Byte array containing the "VERS" resource.</param>
    public Version(byte[] resource)
    {
        byte[] tmpShort = new byte[2];

        MajorVersion      = BCDToNumber(resource[0]);
        MinorVersion      = BCDToNumber(resource[1]);
        DevStage          = (DevelopmentStage)resource[2];
        PreReleaseVersion = BCDToNumber(resource[3]);
        Array.Copy(resource, 4, tmpShort, 0, 2);
        RegionCode = BitConverter.ToUInt16(tmpShort.Reverse().ToArray(), 0);
        byte[] tmpStr = new byte[resource[6] + 1];
        Array.Copy(resource, 6, tmpStr, 0, tmpStr.Length);
        VersionString = PascalString.GetString(tmpStr);
        byte[] tmpMsg = new byte[resource[6 + tmpStr.Length] + 1];
        Array.Copy(resource, 6 + tmpStr.Length, tmpMsg, 0, tmpMsg.Length);
        VersionMessage = PascalString.GetString(tmpMsg);
    }

    /// <summary>Gets the OSTYPE of this resource.</summary>
    /// <value>The OSTYPE.</value>
    public static uint OSType { get; } = 0x76657273;

    /// <summary>Gets a byte array with the "VERS" resource contained by this instance.</summary>
    /// <returns>The "VERS" resource.</returns>
    public byte[] GetBytes()
    {
        byte[] tmpShort = BitConverter.GetBytes(RegionCode).Reverse().ToArray();
        byte[] tmpStr   = PascalString.GetBytes(VersionString);
        byte[] tmpMsg   = PascalString.GetBytes(VersionMessage);
        byte[] vers     = new byte[6 + tmpStr.Length + tmpMsg.Length];

        vers[0] = NumberToBCD(MajorVersion);
        vers[1] = NumberToBCD(MinorVersion);
        vers[2] = (byte)DevStage;
        vers[3] = NumberToBCD(PreReleaseVersion);
        Array.Copy(tmpShort, 0, vers, 4, 2);
        Array.Copy(tmpStr, 0, vers, 6, tmpStr.Length);
        Array.Copy(tmpMsg, 0, vers, 6 + tmpStr.Length, tmpMsg.Length);

        return vers;
    }

    byte BCDToNumber(byte bcd) => Convert.ToByte($"{bcd:X2}", 10);

    byte NumberToBCD(byte number)
    {
        if(number >= 100)
            number = 99;

        return Convert.ToByte($"{number:D2}", 16);
    }

    #region On-disk structure
    /// <summary>Major version.</summary>
    public byte MajorVersion;
    /// <summary>Minor version.</summary>
    public byte MinorVersion;
    /// <summary>Development stage.</summary>
    public DevelopmentStage DevStage;
    /// <summary>Pre-release version.</summary>
    public byte PreReleaseVersion;
    /// <summary>Region code.</summary>
    public ushort RegionCode;
    /// <summary>Version string.</summary>
    public string VersionString;
    /// <summary>Version message.</summary>
    public string VersionMessage;
    #endregion
}