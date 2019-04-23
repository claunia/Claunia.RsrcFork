//
// Program.cs
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

using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Claunia.RsrcFork.CLI
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Claunia.RsrcFork.CLI");
            Console.WriteLine("© 2016 Natalia Portillo");
            Console.WriteLine();

            if(args.Length != 2 && args.Length != 4)
            {
                Usage();
                return;
            }

            if(args.Length == 2)
            {
                if(args[0] != "-l")
                {
                    Usage();
                    return;
                }

                if(!File.Exists(args[1]))
                {
                    Console.WriteLine("Specified file does not exist.");
                    return;
                }

                FileStream rsrcStream = new FileStream(args[1], FileMode.Open, FileAccess.Read);

                ResourceFork rsrc = new ResourceFork(rsrcStream);

                foreach(uint type in rsrc.GetTypes())
                {
                    byte[] typeBytes = BitConverter.GetBytes(type);
                    typeBytes = typeBytes.Reverse().ToArray();
                    string typeName = Types.GetName(type);
                    if(typeName != null)
                        Console.WriteLine("\"{0}\" (0x{1:X8}) - {2}", Encoding.ASCII.GetString(typeBytes), type,
                                          typeName);
                    else Console.WriteLine("\"{0}\" (0x{1:X8})", Encoding.ASCII.GetString(typeBytes), type);

                    Resource rez = rsrc.GetResource(type);
                    Console.WriteLine("{0,6}{1,8}\t{2}", "ID",     "Length",   "Name");
                    Console.WriteLine("{0,6}{1,8}--{2}", "------", "--------", "----------------");
                    foreach(short id in rez.GetIds())
                        if(rez.GetName(id) != null)
                            Console.WriteLine("{0,6}{1,8}\t\"{2}\"", id, rez.GetLength(id), rez.GetName(id));
                        else Console.WriteLine("{0,6}{1,8}",         id, rez.GetLength(id));

                    Console.WriteLine();
                }

                return;
            }

            if(args.Length == 4)
            {
                if(args[0] != "-x")
                {
                    Usage();
                    return;
                }

                if(args[1] != "-o")
                {
                    Usage();
                    return;
                }

                if(Directory.Exists(args[2]))
                {
                    Console.WriteLine("Output directory exists, not proceeding");
                    return;
                }

                if(!File.Exists(args[3]))
                {
                    Console.WriteLine("Specified file does not exist.");
                    return;
                }

                FileStream rsrcStream = new FileStream(args[3], FileMode.Open, FileAccess.Read);
                FileStream outStream;

                ResourceFork rsrc = new ResourceFork(rsrcStream);

                foreach(uint type in rsrc.GetTypes())
                {
                    byte[] typeBytes = BitConverter.GetBytes(type);
                    typeBytes = typeBytes.Reverse().ToArray();

                    Directory.CreateDirectory(Path.Combine(args[2], Encoding.ASCII.GetString(typeBytes)));

                    Resource rez = rsrc.GetResource(type);
                    foreach(short id in rez.GetIds())
                    {
                        outStream =
                            new
                                FileStream(Path.Combine(args[2], Encoding.ASCII.GetString(typeBytes), string.Format("{0}", id)),
                                           FileMode.Create, FileAccess.ReadWrite);
                        byte[] rez_b = rez.GetResource(id);
                        outStream.Write(rez_b, 0, rez_b.Length);
                        outStream.Close();
                    }
                }
            }
        }

        public static void Usage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("\tClaunia.RsrcFork.CLI.exe -l resourcefork.bin - Lists resources in resource fork file");
            Console.WriteLine("\t\tor");
            Console.WriteLine("\tClaunia.RsrcFork.CLI.EXE -x -o output.dir resourcefork.bin - Extracts all resources to output.dir");
        }
    }
}