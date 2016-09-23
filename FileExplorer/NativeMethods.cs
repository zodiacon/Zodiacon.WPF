using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer {
    [SuppressUnmanagedCodeSecurity]
    static class NativeMethods {
        [DllImport("shell32", CharSet = CharSet.Unicode)]
        public static extern IntPtr ExtractIcon(IntPtr hReserved, string filename, uint index);

        [DllImport("user32")]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHSTOCKICONINFO {
            public uint cbSize;
            public IntPtr hIcon;
            public int iSysIconIndex;
            public int iIcon;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szPath;
        }

        [Flags]
        public enum SHGSI : uint {
            SHGSI_ICONLOCATION = 0,
            SHGSI_ICON = 0x000000100,
            SHGSI_SYSICONINDEX = 0x000004000,
            SHGSI_LINKOVERLAY = 0x000008000,
            SHGSI_SELECTED = 0x000010000,
            SHGSI_LARGEICON = 0x000000000,
            SHGSI_SMALLICON = 0x000000001,
            SHGSI_SHELLICONSIZE = 0x000000004
        }

        [DllImport("Shell32", SetLastError = false)]
        public static extern Int32 SHGetStockIconInfo(int siid, SHGSI uFlags, ref SHSTOCKICONINFO psii);
    }
}
