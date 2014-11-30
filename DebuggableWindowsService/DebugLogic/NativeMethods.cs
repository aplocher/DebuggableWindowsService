using System.Runtime.InteropServices;

namespace DebuggableWindowsService.DebugLogic
{
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FreeConsole();
    }
}