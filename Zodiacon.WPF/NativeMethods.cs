using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Zodiacon.WPF {
    [SuppressUnmanagedCodeSecurity]
    static class NativeMethods {
        [DllImport("user32")]
        public static extern bool MessageBeep(uint type);
    }
}
