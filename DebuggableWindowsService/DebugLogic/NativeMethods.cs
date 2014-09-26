using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DebuggableWindowsService.DebugLogic
{
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
    }
}
