//
// Types.cs
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
using System.Linq;

namespace Claunia.RsrcFork
{
    public static class Types
    {
        public static string GetName(uint OSType)
        {
            switch(OSType) {
                case 0x61637462: // "actb"
                    return "Alert colour table";
                case 0x61637572: // "acur"
                    return "Animated cursor";
                case 0x41444253: // "ADBS"
                    return "Apple Desktop Bus service routine";
                case 0x616C6973: // "alis"
                    return "Alias";
                case 0x414C5254: // "ALRT"
                    return "Alert template";
                case 0x4150504C: // "APPL"
                    return "Application list (Desktop file)";
                case 0x6174706C: // "atpl"
                    return "AppleTalk resource";
                case 0x626C6B78: // "blkx"
                    return "UDIF block chunks";
                case 0x626D6170: // "bmap"
                    return "Bitmap graphic";
                case 0x424E444C: // "BNDL"
                    return "Bundle linking FREF and ICN# to provide correct icon on Desktop";
                case 0x424E444E: // "BNDN"
                    return "Data configured in big-endian form";
                case 0x424F4F4C: // "BOOL"
                    return "Boolean word";
                case 0x626F6F74: // "boot"
                    return "Copy of boot blocks";
                case 0x43414348: // "CACH"
                    return "RAM cache control code";
                case 0x63637462: // "cctb"
                    return "Control colour table";
                case 0x43444546: // "CDEF"
                    return "Control definition";
                case 0x63646576: // "cdev"
                    return "Control device code (control panel)";
                case 0x63667267: // "cfrg"
                    return "Code fragment dictionary for PowerPC code";
                case 0x43484152: // "CHAR"
                    return "ASCII character";
                case 0x6369636E: // "cicn"
                    return "Large screen icon, colour";
                case 0x434C5554: // "CLUT"
                    return "Color look-up table";
                case 0x636C7574: // "clut"
                    return "Color look-up table";
                case 0x434E544C: // "CNTL"
                    return "Control template";
                case 0x434D444F: // "CMDO"
                    return "MPW or A/UX Comando data";
                case 0x434D4E55: // "CMNU"
                    return "Menu with command ID numbers";
                case 0x636D6E75: // "cmnu"
                    return "MacApp temporary menu resource";
                case 0x434F4445: // "CODE"
                    return "Application code segment";
                case 0x434F4C52: // "COLR"
                    return "QuickDraw RGB colour template";
                case 0x63727372: // "crsr"
                    return "Cursor, colour";
                case 0x63746162: // "ctag"
                    return "Cache table containing list of possible cache sizes";
                case 0x43545923: // "CTY#"
                    return "City list";
                case 0x43555253: // "CURS"
                    return "Cursor, black & white with mask";
                case 0x44415441: // "DATA"
                    return "Data";
                case 0x44415445: // "DATE"
                    return "System date and time";
                case 0x64637462: // "dctb"
                    return "Dialog colour table";
                case 0x4449434C: // "DICL"
                    return "Used by MacWorkstation";
                case 0x4449544C: // "DITL"
                    return "Dialog item list";
                case 0x444C4F47: // "DLOG"
                    return "Dialog template";
                case 0x44525652: // "DRVR"
                    return "Driver";
                case 0x44534154: // "DSAT"
                    return "Default startup alert table";
                case 0x65727273: // "errs"
                    return "MacApp error string";
                case 0x4642544E: // "FBTN"
                    return "MiniFinder button";
                case 0x46434D54: // "FCMT"
                    return "Finder comment";
                case 0x66637462: // "fctb"
                    return "Font colour table";
                case 0x46444952: // "FDIR"
                    return "MiniFinder button directory ID";
                case 0x66696E66: // "finf"
                    return "Font information";
                case 0x464B4559: // "FKEY"
                    return "Function Key code";
                case 0x666C6423: // "fld#"
                    return "Folder names list";
                case 0x464C5452: // "FLTR"
                    return "Declare Filtered Template with comment";
                case 0x666D6E75: // "fmnu"
                    return "Finder menu";
                case 0x464D5452: // "FMTR"
                    return "Format record";
                case 0x464F424A: // "FOBJ"
                    return "Folder information";
                case 0x464F4E44: // "FOND"
                    return "Font family descriptor";
                case 0x464F4E54: // "FONT"
                    return "Font description, bitmap";
                case 0x66505254: // "fPRT"
                    return "Print Catalogue defaults for Finder";
                case 0x46524546: // "FREF"
                    return "File reference for BNDL used to identify correct icon";
                case 0x46525356: // "FRSV"
                    return "ROM font resources";
                case 0x46574944: // "FWID"
                    return "Font width table";
                case 0x67616D61: // "gama"
                    return "Gamma table giving color correction for screen";
                case 0x474E524C: // "GNRL"
                    return "NBP timeout and retry info for AppleTalk";
                case 0x68646C67: // "hdlg"
                    return "Balloon Help for dialog box items";
                case 0x68666472: // "hfdr"
                    return "Balloon Help for application icon in Finder";
                case 0x686D6E75: // "hmnu"
                    return "Balloon Help for menus in application";
                case 0x686F7672: // "hovr"
                    return "Balloon Help that overrides Finder help";
                case 0x68726374: // "hrct"
                    return "Balloon Help for rectangles in windows";
                case 0x6877696E: // "hwin"
                    return "Bundles together the hrct and hdlg resources for a window";
                case 0x69636C34: // "icl4"
                    return "Large Desktop icon, 4-bit colour";
                case 0x69636C38: // "icl8"
                    return "Large Desktop icon, 8-bit colour";
                case 0x69636D74: // "icmt"
                    return "Installer comment";
                case 0x69636E73: // "icns"
                    return "Universal icon, all sizes";
                case 0x49434E23: // "ICN#"
                    return "Large Desktop icon, black & white with mask";
                case 0x49434F4E: // "ICON"
                    return "Large application icon, black & white";
                case 0x69637334: // "ics4"
                    return "Small Desktop icon, 4-bit colour";
                case 0x69637338: // "ics8"
                    return "Small Desktop icon, 8-bit colour";
                case 0x69637323: // "ics#"
                    return "Small Desktop icon, black and white with mask";
                case 0x69637462: // "ictb"
                    return "Dialog items colour table";
                case 0x696E6262: // "inbb"
                    return "Installer boot block";
                case 0x696E646D: // "indm"
                    return "Installer default map";
                case 0x696E6661: // "infa"
                    return "Installer file atom";
                case 0x696E6673: // "infs"
                    return "Installer file spec";
                case 0x696E706B: // "inpk"
                    return "Installer package";
                case 0x696E7261: // "inra"
                    return "Installer resource atom";
                case 0x696E7363: // "insc"
                    return "Installer script";
                case 0x494E4954: // "INIT"
                    return "Startup resource (extension, control panel or System file)";
                case 0x69746C30: // "itl0"
                    return "International date and time format, now obsolete (INTL ID=0)";
                case 0x49544C31: // "ITL1"
                    return "International long date format";
                case 0x69746C31: // "itl1"
                    return "International names of days and months (INTL ID=1)";
                case 0x69746C32: // "itl2"
                    return "International Utilities string comparison hooks";
                case 0x69746C34: // "itl4"
                    return "International Tokenise tables & Localisation code";
                case 0x69746C35: // "itl5"
                    return "International Character Set encoding";
                case 0x69746C62: // "itlb"
                    return "International Utilities Package Script bundles";
                case 0x69746C63: // "itlc"
                    return "International Script Manager configuration";
                case 0x69746C6D: // "itlm"
                    return "Sorting order for Script, language and region";
                case 0x69746C6B: // "itlk"
                    return "International exception dictionary for KCHR";
                case 0x494E544C: // "INTL"
                    return "International formatting information";
                case 0x4B434150: // "KCAP"
                    return "Physical keyboard layout used by Keycaps DA";
                case 0x4B434852: // "KCHR"
                    return "Keyboard character software ASCII mapping";
                case 0x4B455943: // "KEYC"
                    return "Old keyboard layout";
                case 0x6B696E64: // "kind"
                    return "File description";
                case 0x4B4D4150: // "KMAP"
                    return "Hardware keyboard mapping";
                case 0x6B73636E: // "kscn"
                    return "Keyboard and script system icon";
                case 0x4B535750: // "KSWP"
                    return "Keyboard script swapping table";
                case 0x4C41594F: // "LAYO"
                    return "Layout resource";
                case 0x4C444546: // "LDEF"
                    return "List definition";
                case 0x6C6D656D: // "lmem"
                    return "Finder low memory globals";
                case 0x4C4E444E: // "LNDN"
                    return "Data in little-endian format";
                case 0x4C4E4743: // "LNGC"
                    return "Mac OS language code";
                case 0x6D616368: // "mach"
                    return "Matches machine to CDEV control panel (control panel)";
                case 0x4D414353: // "MACS"
                    return "Version number for System and Finder files";
                case 0x4D424446: // "MBDF"
                    return "Default menu definition";
                case 0x4D424152: // "MBAR"
                    return "Menu bar, contains set of MENU IDs";
                case 0x6D636B79: // "mcky"
                    return "Mouse tracking speed presets for Mouse control panel";
                case 0x6D636F64: // "mcod"
                    return "MacroMaker information";
                case 0x6D637462: // "mctb"
                    return "Menu colour table";
                case 0x4D444154: // "MDAT"
                    return "Modification date and time";
                case 0x6D646374: // "mdct"
                    return "MacroMaker information";
                case 0x4D444546: // "MDEF"
                    return "Menu definition";
                case 0x6D656D21: // "mem!"
                    return "MacApp memory usage";
                case 0x4D454E55: // "MENU"
                    return "Menu contents & style";
                case 0x6D696E66: // "minf"
                    return "Macro info for MacroMaker";
                case 0x6D697471: // "mitq"
                    return "Internal memory needs for colour Make Inverse Table";
                case 0x6D6E7462: // "mntb"
                    return "MacApp menu table, relating command number to menu item";
                case 0x4D6F6F56: // "MooV"
                    return "Movie";
                case 0x6D707063: // "mppc"
                    return "MPP configuration resource";
                case 0x6D737472: // "mstr"
                    return "Finder substitute file storing Open and Quit strings";
                case 0x4E425043: // "NBPC"
                    return "AppleTalk NBP configuration";
                case 0x4E464E54: // "NFNT"
                    return "New Font Numbering Table bitmap font, indexed via Font & Style menus";
                case 0x6E637473: // "ncts"
                    return "List of constants";
                case 0x6E726374: // "nrct"
                    return "Rectangle position list";
                case 0x6F70656E: // "open"
                    return "Openable file types";
                case 0x5041434B: // "PACK"
                    return "High-level software packages";
                case 0x50415041: // "PAPA"
                    return "Printer name, type and zone";
                case 0x50415420: // "PAT "
                    return "Black & white QuickDraw pattern, 8 by 8 pixels";
                case 0x50415423: // "PAT#"
                    return "Black & white QuickDraw pattern list";
                case 0x50444546: // "PDEF"
                    return "Printer definition";
                case 0x50494354: // "PICT"
                    return "Picture";
                case 0x706C7374: // "plst"
                    return "UDIF partition list";
                case 0x706C7474: // "pltt"
                    return "Colour palette";
                case 0x706E6F74: // "pnot"
                    return "Preview notification";
                case 0x504E5420: // "PNT "
                    return "QuickDraw Point";
                case 0x504F5354: // "POST"
                    return "PostScript data";
                case 0x70706174: // "ppat"
                    return "Colour pixel pattern";
                case 0x70706363: // "ppcc"
                    return "PPC browser communications";
                case 0x70707423: // "ppt#"
                    return "List of ppat patterns";
                case 0x50524546: // "PREF"
                    return "Preferences";
                case 0x50524330: // "PRC0"
                    return "Default printer page setup defaults";
                case 0x50524333: // "PRC3"
                    return "Print record";
                case 0x50524543: // "PREC"
                    return "Printer driver's private data";
                case 0x50534150: // "PSAP"
                    return "String";
                case 0x50544348: // "PTCH"
                    return "ROM patch code";
                case 0x71727363: // "qrsc"
                    return "Query resource used in Sytem 7.0";
                case 0x52454354: // "RECT"
                    return "QuickDraw rectangle";
                case 0x7265736C: // "resl"
                    return "Resident MacApp segments";
                case 0x52474E43: // "RGNC"
                    return "Mac OS System Region Code";
                case 0x524F7623: // "ROv#"
                    return "List of ROM resource overrides";
                case 0x52534944: // "RSID"
                    return "Signed resource ID integer";
                case 0x53435043: // "SCPC"
                    return "MacOS system script code";
                case 0x7363726E: // "scrn"
                    return "Screen configuration for Monitors control panel";
                case 0x73656721: // "seg!"
                    return "MacApp memory management";
                case 0x53455244: // "SERD"
                    return "RAMSerial Driver";
                case 0x73666E23: // "sfn#"
                    return "System Font list";
                case 0x73666E74: // "sfnt"
                    return "Spline or scalable font";
                case 0x5349434E: // "SICN"
                    return "Small application icon, black & white";
                case 0x53495A45: // "SIZE"
                    return "Finder size information";
                case 0x736E6420: // "snd "
                    return "Sound";
                case 0x736E7468: // "snth"
                    return "Sound synthesiser resource";
                case 0x53545220: // "STR "
                    return "String in Pascal format";
                case 0x53545223: // "STR#"
                    return "String list in Pascal format";
                case 0x7374796C: // "styl"
                    return "Style information for text used by TextEdit";
                case 0x7379737A: // "sysz"
                    return "System Heap request";
                case 0x54455854: // "TEXT"
                    return "Unlabelled text string";
                case 0x746C7374: // "tlst"
                    return "Title list";
                case 0x544D504C: // "TMPL"
                    return "ResEdit template";
                case 0x544E414D: // "TNAM"
                    return "Type name";
                case 0x76696577: // "view"
                    return "MacApp view resource";
                case 0x76657273: // "vers"
                    return "Version";
                case 0x77637462: // "wctb"
                    return "Window colour table";
                case 0x57444546: // "WDEF"
                    return "Window definition";
                case 0x57494E44: // "WIND"
                    return "Window template";
                case 0x77737472: // "wstr"
                    return "String used by qrsc";
                case 0x58434D44: // "XCMD"
                    return "HyperCard external command";
                case 0x5846434E: // "XFCN"
                    return "HyperCard external function";
                default:
                    return null;
            }
        }

        public static string GetName(string OSType)
        {
            if(OSType.Length != 4)
                return null;

            byte[] typB = Encoding.ASCII.GetBytes(OSType);
            uint type = BitConverter.ToUInt32(typB.Reverse().ToArray(), 0);

            return GetName(type);
        }
    }
}

