//
// PascalString.cs
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
using System.Text;
namespace Claunia.RsrcFork
{
    /// <summary>
    /// Contains convertes between .NET and Pascal strings. Only ASCII supported right now.
    /// </summary>
    public static class PascalString
    {
        /// <summary>
        /// Converts an ASCII Pascal string to a .NET string.
        /// </summary>
        /// <returns>The .NET string.</returns>
        /// <param name="PStr">The ASCII Pascal string.</param>
        public static string GetString(byte[] PStr)
        {
            if(PStr == null || PStr[0] >= PStr.Length)
                return null;

            return Encoding.ASCII.GetString(PStr, 1, PStr[0]);
        }

        /// <summary>
        /// Converts a .NET string to an ASCII Pascal string.
        /// </summary>
        /// <returns>The ASCII Pascal string.</returns>
        /// <param name="str">The .NET string.</param>
        public static byte[] GetBytes(string str)
        {
            if(str == null)
                return new byte[1];
            
            byte[] str_b = Encoding.ASCII.GetBytes(str);
            byte[] PStr;

            if(str_b.Length >= 256)
                PStr = new byte[256];
            else
                PStr = new byte[str_b.Length + 1];

            Array.Copy(str_b, 0, PStr, 1, PStr.Length - 1);
            PStr[0] = (byte)(PStr.Length - 1);

            return PStr;
        }
    }
}

