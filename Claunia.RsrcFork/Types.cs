//
// Types.cs
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
using System.Linq;
using System.Text;

namespace Claunia.RsrcFork
{
    /// <summary>
    ///     This class contains static methods for OSTYPE handling.
    /// </summary>
    public static class Types
    {
        /// <summary>
        ///     Gets a descriptive name of a resource from its OSTYPE.
        /// </summary>
        /// <returns>The name corresponding to the specified OSTYPE.</returns>
        /// <param name="OSType">The OSTYPE.</param>
        public static string GetName(uint OSType)
        {
            switch(OSType)
            {
                case 0x2E58534C: // ".XSL"
                    return "XML style sheet";
                case 0x33444D46: // "3DMF"
                    return "3DMF";
                case 0x61637462: // "actb"
                    return "Alert colour table";
                case 0x61637572: // "acur"
                    return "Animated cursor";
                case 0x41444253: // "ADBS"
                    return "Apple Desktop Bus service routine";
                case 0x6164696F: // "adio"
                    return "Audio Code";
                case 0x61656474: // "aedt"
                    return "Apple Event Dispatch Template";
                case 0x61657465: // "aete"
                    return "Apple Event Terminology";
                case 0x61657574: // "aeut"
                    return "Apple Event User Terminology";
                case 0x41494E49: // "AINI"
                    return "AppleTalk Initializaiton Code";
                case 0x616C6973: // "alis"
                    return "Alias";
                case 0x414C5254: // "ALRT"
                    return "Alert template";
                case 0x616C7278: // "alrx"
                    return "Alert Extension";
                case 0x4150504C: // "APPL"
                    return "Application list (Desktop file)";
                case 0x61736364: // "ascd"
                    return "AppleShare Code";
                case 0x4153646C: // "ASdl"
                    return "AppleScript Delimiters";
                case 0x41536531: // "ASe1"
                    return "AppleScript String Escapes";
                case 0x41536532: // "ASe2"
                    return "AppleScript Identifier Escapes";
                case 0x41536E66: // "ASnf"
                    return "AppleScript Number Formats";
                case 0x41536E70: // "ASnp"
                    return "AppleScript Number Parts";
                case 0x4153746C: // "AStl"
                    return "AppleScript Style Map";
                case 0x4153746E: // "AStn"
                    return "AppleScript Style Names";
                case 0x6174706C: // "atpl"
                    return "AppleTalk resource";
                case 0x6263656D: // "bcem"
                    return "DiskCopy Block Chunks";
                case 0x62636D23: // "bcm#"
                    return "Diskcopy Segments";
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
                case 0x426F6F74: // "Boot"
                    return "DOS Boot Blocks";
                case 0x6274626C: // "btbl"
                    return "Bootable Machines Table";
                case 0x43414348: // "CACH"
                    return "RAM cache control code";
                case 0x63617262: // "carb"
                    return "Carbon Direct Launch";
                case 0x63617264: // "card"
                    return "Video Board Name";
                case 0x434349AA: // "CCI™"
                    return "Extension Manager String";
                case 0x63637462: // "cctb"
                    return "Control colour table";
                case 0x63646369: // "cdci"
                    return "Coder/Decoder Information";
                case 0x43444546: // "CDEF"
                    return "Control definition";
                case 0x63646576: // "cdev"
                    return "Control device code (control panel)";
                case 0x43464947: // "CFIG"
                    return "Apple Software Restore Settings";
                case 0x63666D74: // "cfmt"
                    return "Apple System Profiler Formatting Configuration";
                case 0x63667267: // "cfrg"
                    return "Code fragment directory for PowerPC code";
                case 0x43484152: // "CHAR"
                    return "ASCII character";
                case 0x63687772: // "chwr"
                    return "Apple System Profiler Characters Width";
                case 0x6369636E: // "cicn"
                    return "Large screen icon, colour";
                case 0x63494647: // "cIFG"
                    return "Modem Configuration";
                case 0x636C6F63: // "cloc"
                    return "Connection Manager Localization Code";
                case 0x434C5554: // "CLUT"
                    return "Color look-up table";
                case 0x636C7574: // "clut"
                    return "Color look-up table";
                case 0x436D7072: // "Cmpr"
                    return "Apple FM Radio Compression Types";
                case 0x434E544C: // "CNTL"
                    return "Control template";
                case 0x434D444F: // "CMDO"
                    return "MPW or A/UX Comando data";
                case 0x636D6D20: // "cmm "
                    return "CMM";
                case 0x434D4E55: // "CMNU"
                    return "Menu with command ID numbers";
                case 0x636D6E75: // "cmnu"
                    return "MacApp temporary menu resource";
                case 0x434F4445: // "CODE"
                    return "Application code segment";
                case 0x434F4C52: // "COLR"
                    return "QuickDraw RGB colour template";
                case 0x636F6C77: // "colw"
                    return "Apple System Profiler Column Widths";
                case 0x436F6E66: // "Conf"
                    return "Apple Video Player Configuration";
                case 0x63707570: // "cpup"
                    return "CPU plugin";
                case 0x63707574: // "cput"
                    return "Apple Software Restore Supported Machines";
                case 0x63727372: // "crsr"
                    return "Cursor, colour";
                case 0x63727970: // "cryp"
                    return "File Sharing Code";
                case 0x63736372: // "cscr"
                    return "Connection Manager Script";
                case 0x6353756D: // "cSum"
                    return "Apple Software Restore Checksums";
                case 0x63746162: // "ctag"
                    return "Cache table containing list of possible cache sizes";
                case 0x43545923: // "CTY#"
                    return "City list";
                case 0x43555253: // "CURS"
                    return "Cursor, black & white with mask";
                case 0x6376616C: // "cval"
                    return "Connection Manager Validation Code";
                case 0x44415441: // "DATA"
                    return "Data";
                case 0x44415445: // "DATE"
                    return "System date and time";
                case 0x4462646C: // "Dbdl"
                    return "AppleScript Dialect";
                case 0x64637462: // "dctb"
                    return "Dialog colour table";
                case 0x44447276: // "DDrv"
                    return "Driver";
                case 0x64667462: // "dftb"
                    return "Dialog Font Table";
                case 0x4449434C: // "DICL"
                    return "Used by MacWorkstation";
                case 0x64696D67: // "dimg"
                    return "Dictionary Manager";
                case 0x4449544C: // "DITL"
                    return "Dialog item list";
                case 0x444C4746: // "DLGF"
                    return "Dialog Font List";
                case 0x646C6778: // "dlgx"
                    return "Dialog Extension";
                case 0x444C4F47: // "DLOG"
                    return "Dialog template";
                case 0x44525652: // "DRVR"
                    return "Driver";
                case 0x44534154: // "DSAT"
                    return "Default startup alert table";
                case 0x64736967: // "dsig"
                    return "DiskCopy Signature";
                case 0x45325479: // "E2Ty"
                    return "File Extension to OSType Table";
                case 0x65636667: // "ecfg"
                    return "Ethernet Configuration";
                case 0x45466E74: // "EFnt"
                    return "Encoding to Font Table";
                case 0x656E6574: // "enet"
                    return "Ethernet";
                case 0x65727273: // "errs"
                    return "MacApp error string";
                case 0x45737472: // "Estr"
                    return "Error String";
                case 0x4578636C: // "Excl"
                    return "StuffIt! Exclusion Table";
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
                case 0x464C4123: // "FLA#"
                    return "Folder Load Action Application Types";
                case 0x666C6423: // "fld#"
                    return "Folder names list";
                case 0x464C5452: // "FLTR"
                    return "Declare Filtered Template with comment";
                case 0x666D3344: // "fm3D"
                    return "Apple FM Radio 3D Look";
                case 0x666D6170: // "fmap"
                    return "Finder Mapping";
                case 0x666D6E75: // "fmnu"
                    return "Finder menu";
                case 0x464D5452: // "FMTR"
                    return "Format record";
                case 0x666E696E: // "fnin"
                    return "List of Fonts";
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
                case 0x46726571: // "Freq"
                    return "Apple Video Player List of Frequencies";
                case 0x46525356: // "FRSV"
                    return "ROM font resources";
                case 0x4654494E: // "FTIN"
                    return "Filesystem Translator Initialization Code";
                case 0x66747023: // "ftp#"
                    return "FTP List";
                case 0x66766577: // "fvew"
                    return "Finder View";
                case 0x46574944: // "FWID"
                    return "Font width table";
                case 0x67616D61: // "gama"
                    return "Gamma table giving color correction for screen";
                case 0x474E524C: // "GNRL"
                    return "NBP timeout and retry info for AppleTalk";
                case 0x68616D6D: // "hamm"
                    return "Hamming Decode Bytes";
                case 0x68646C67: // "hdlg"
                    return "Balloon Help for dialog box items";
                case 0x68666472: // "hfdr"
                    return "Balloon Help for application icon in Finder";
                case 0x686D6E75: // "hmnu"
                    return "Balloon Help for menus in application";
                case 0x486E7452: // "HntR"
                    return "Printer Hints";
                case 0x686F7672: // "hovr"
                    return "Balloon Help that overrides Finder help";
                case 0x68726374: // "hrct"
                    return "Balloon Help for rectangles in windows";
                case 0x68746D6C: // "html"
                case 0x48544D4C: // "HTML"
                    return "HTML";
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
                case 0x696C3332: // "il32"
                    return "Large 32-bit Icon";
                case 0x696D6170: // "imap"
                    return "Creator Name Mapping";
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
                case 0x69736476: // "isdv"
                    return "InputSprocket Device Configuration";
                case 0x6973656C: // "isel"
                    return "inputsproject Button Configuration";
                case 0x69737276: // "isrv"
                    return "Icon Mapping Table";
                case 0x69746C23: // "itl#"
                    return "Internationalization List";
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
                case 0x6A706567: // "jpeg"
                case 0x4A504547: // "JPEG"
                    return "JPEG";
                case 0x4B434150: // "KCAP"
                    return "Physical keyboard layout used by Keycaps DA";
                case 0x4B434852: // "KCHR"
                    return "Keyboard character software ASCII mapping";
                case 0x6B637323: // "kcs#"
                    return "Keyboard/Script Small Icon & Mask";
                case 0x6B637334: // "kcs4"
                    return "Keyboard/Script Small 4-bit Icon";
                case 0x6B637338: // "kcs8"
                    return "keyboard/script small 8-bit icon";
                case 0x4B455943: // "KEYC"
                    return "Old keyboard layout";
                case 0x6B657973: // "keys"
                    return "IR Remote Codes";
                case 0x6B696E64: // "kind"
                    return "File description";
                case 0x4B4D4150: // "KMAP"
                    return "Hardware keyboard mapping";
                case 0x6B73636E: // "kscn"
                    return "Keyboard and script system icon";
                case 0x4B535750: // "KSWP"
                    return "Keyboard script swapping table";
                case 0x6C386D6B: // "l8mk"
                    return "Large 8-bit Icon & Mask";
                case 0x6C616E67: // "lang"
                    return "Language Code List";
                case 0x4C41594F: // "LAYO"
                    return "Layout resource";
                case 0x4C444546: // "LDEF"
                    return "List Definition Function";
                case 0x6C646573: // "ldes"
                    return "List Control Descriptor";
                case 0x6C696220: // "lib "
                    return "Library";
                case 0x4C696269: // "Libi"
                case 0x6C696269: // "libi"
                    return "Shared Library Information";
                case 0x4C696272: // "Libr"
                case 0x6C696272: // "libr"
                    return "Shared Library Classes";
                case 0x4C704C62: // "LpLb"
                    return "LeapLib";
                case 0x6C6D656D: // "lmem"
                    return "Low Memory Globals";
                case 0x6C6D6772: // "lmgr"
                    return "Link Access Protocol Manager";
                case 0x4C4E444E: // "LNDN"
                    return "Data in little-endian format";
                case 0x4C4E4743: // "LNGC"
                    return "Mac OS language code";
                case 0x6C6F6323: // "loc#"
                    return "Location Coordinates";
                case 0x6C737472: // "lstr"
                    return "Finder File Label";
                case 0x6C746C6B: // "ltlk"
                    return "LocalTalk Code";
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
                case 0x4D636D64: // "Mcmd"
                    return "PowerPlant Menu Commands";
                case 0x6D636F64: // "mcod"
                    return "MacroMaker information";
                case 0x6D637462: // "mctb"
                    return "Menu colour table";
                case 0x6D637673: // "mcvs"
                    return "MacCVS Version Resource";
                case 0x4D444154: // "MDAT"
                    return "Modification date and time";
                case 0x6D646374: // "mdct"
                    return "MacroMaker information";
                case 0x4D444546: // "MDEF"
                    return "Menu definition";
                case 0x6D656D21: // "mem!"
                    return "MacApp Memory Utilization";
                case 0x4D454E55: // "MENU"
                    return "Menu contents & style";
                case 0x6D696D65: // "mime"
                    return "MIME table";
                case 0x6D696E66: // "minf"
                    return "Macro info for MacroMaker";
                case 0x6D697471: // "mitq"
                    return "Queue Sizes for MakeITable";
                case 0x4D4D4150: // "MMAP"
                    return "Mouse Tracking Code";
                case 0x4D6D6170: // "Mmap"
                    return "Message Pool Map";
                case 0x6D6E7462: // "mntb"
                    return "MacApp menu table, relating command number to menu item";
                case 0x6D6F646D: // "modm"
                    return "Modem Details";
                case 0x4D6F6F56: // "MooV"
                    return "Movie";
                case 0x6D707063: // "mppc"
                    return "MPP configuration resource";
                case 0x6D737472: // "mstr"
                    return "Finder substitute file storing Open and Quit strings";
                case 0x6D747632: // "mtv2"
                    return "Apple Video Player Sintonization Information";
                case 0x4E425043: // "NBPC"
                    return "AppleTalk NBP configuration";
                case 0x4E434F44: // "NCOD"
                    return "File System Translator Code";
                case 0x6E647276: // "ndrv"
                    return "Driver";
                case 0x4E464E54: // "NFNT"
                    return "New Font Numbering Table bitmap font, indexed via Font & Style menus";
                case 0x6E637473: // "ncts"
                    return "List of constants";
                case 0x6E726374: // "nrct"
                    return "Rectangle position list";
                case 0x4F467074: // "OFpt"
                    return "OpenFirmware Patch";
                case 0x6F70656E: // "open"
                    return "Openable file types";
                case 0x5041434B: // "PACK"
                    return "ROM extension code package";
                case 0x50415041: // "PAPA"
                    return "Printer Access Protocol Address";
                case 0x5061726D: // "Parm"
                    return "Apple FM Radio Application Parameters";
                case 0x50415420: // "PAT "
                    return "Black & white QuickDraw pattern, 8 by 8 pixels";
                case 0x50415423: // "PAT#"
                    return "Black & white QuickDraw pattern list";
                case 0x70646174: // "pdat"
                    return "Print Record Data";
                case 0x50444546: // "PDEF"
                    return "Printer definition";
                case 0x50494354: // "PICT"
                    return "Picture";
                case 0x504C474E: // "PLGN"
                    return "PrintingLib Plugin";
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
                case 0x50506F62: // "PPob"
                    return "PowerPlant Object";
                case 0x70707423: // "ppt#"
                    return "List of ppat patterns";
                case 0x50524543: // "PREC"
                    return "Printer Driver Record";
                case 0x70726563: // "prec"
                    return "GX PostScript Procedure Set";
                case 0x50524546: // "PREF"
                    return "Preferences";
                case 0x50524330: // "PRC0"
                    return "Default printer page setup defaults";
                case 0x50524333: // "PRC3"
                    return "Print record";
                case 0x50534150: // "PSAP"
                    return "String";
                case 0x70736C74: // "pslt"
                    return "NuBus Pseudo Slots";
                case 0x70746368: // "ptch"
                    return "General System Patches";
                case 0x50544348: // "PTCH"
                    return "ROM Specific Patches";
                case 0x70766920: // "pvi "
                    return "MIME type table";
                case 0x71727363: // "qrsc"
                    return "Query resource used in System 7.0";
                case 0x72616D72: // "ramr"
                    return "Apple System Profiler RAM Report Configuration";
                case 0x52454354: // "RECT"
                    return "QuickDraw rectangle";
                case 0x7265736C: // "resl"
                    return "Resident MacApp segments";
                case 0x72676220: // "rgb "
                    return "Red/Green/Blue Color";
                case 0x52474E43: // "RGNC"
                    return "Mac OS System Region Code";
                case 0x5269646C: // "Ridl"
                    return "PowerPlant Resource ID List";
                case 0x726F7574: // "rout"
                    return "Folder Drop Routing Info";
                case 0x524F7623: // "ROv#"
                    return "Override ROM Code List";
                case 0x524F7672: // "ROvr"
                    return "Override ROM Code";
                case 0x72707477: // "rptw"
                    return "Apple System Profiler View Widths";
                case 0x52534944: // "RSID"
                    return "Signed resource ID integer";
                case 0x73386D6B: // "s8mk"
                    return "Small 8-bit Icon & Mask";
                case 0x7363656E: // "scen"
                    return "Theme Wallpapers";
                case 0x53435043: // "SCPC"
                    return "MacOS system script code";
                case 0x73636F64: // "scod"
                    return "System Code";
                case 0x7363726E: // "scrn"
                    return "Screen configuration for Monitors control panel";
                case 0x7363737A: // "scsz"
                    return "Script Sizes";
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
                case 0x53494723: // "SIG#"
                    return "Processes to Quit";
                case 0x73697A65: // "size"
                    return "Apple Software Restore Size";
                case 0x53495A45: // "SIZE"
                    return "Finder size information";
                case 0x53656C66: // "Self"
                    return "Self-extracting Decompressor Code";
                case 0x53696D75: // "Simu"
                    return "Apple FM Radio Simulated Buttons";
                case 0x736C6472: // "sldr"
                    return "Aperture Slider";
                case 0x736E6420: // "snd "
                    return "Sound";
                case 0x736E7468: // "snth"
                    return "Sound synthesiser resource";
                case 0x53545220: // "STR "
                    return "String in Pascal format";
                case 0x53545223: // "STR#"
                    return "String list in Pascal format";
                case 0x73747269: // "stri"
                    return "Component Info String";
                case 0x7374726E: // "strn"
                    return "Component Info Name";
                case 0x7374796C: // "styl"
                    return "Style information for text used by TextEdit";
                case 0x7379737A: // "sysz"
                    return "System Heap Size Increment";
                case 0x74616223: // "tab#"
                    return "Tab Group Name List";
                case 0x54455854: // "TEXT"
                    return "Unlabelled text string";
                case 0x74686E67: // "thng"
                    return "Component Information";
                case 0x746C7374: // "tlst"
                    return "Title list";
                case 0x544D504C: // "TMPL"
                    return "ResEdit template";
                case 0x544E414D: // "TNAM"
                    return "Type name";
                case 0x74726923: // "tri#"
                    return "Phosphor Calibration Values";
                case 0x74737464: // "tstd"
                    return "Apple Video Player Television Standards";
                case 0x74544F50: // "tTOP"
                    return "Apple Video Player TeleText Options";
                case 0x74766172: // "tvar"
                    return "Theme Variants";
                case 0x54787472: // "Txtr"
                    return "PowerPlant Text Traits";
                case 0x75666F78: // "ufox"
                    return "File System Translator Module Name";
                case 0x75726C20: // "url "
                    return "URL";
                case 0x56454E44: // "VEND"
                    return "PCI Vendor Name List";
                case 0x76656E64: // "vend"
                    return "Driver Creator";
                case 0x76657273: // "vers"
                    return "Version";
                case 0x76696577: // "view"
                    return "MacApp view resource";
                case 0x766E6964: // "vnid"
                    return "FireWire Vendor IDs";
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
                case 0x786D6E75: // "xmnu"
                    return "Extented Menu";
                case 0x7A697661: // "ziva"
                    return "ZiVA microcode";
                case 0x5F36384B: // "_68K"
                    return "MC680x0 Code";
                case 0x5F505043: // "_PPC"
                    return "PowerPC Code";
                default: return null;
            }
        }

        /// <summary>
        ///     Gets a descriptive name of a resource from its OSTYPE.
        /// </summary>
        /// <returns>The name corresponding to the specified OSTYPE.</returns>
        /// <param name="OSType">The OSTYPE.</param>
        public static string GetName(string OSType)
        {
            if(OSType.Length != 4) return null;

            byte[] typB = Encoding.ASCII.GetBytes(OSType);
            uint   type = BitConverter.ToUInt32(typB.Reverse().ToArray(), 0);

            return GetName(type);
        }
    }
}