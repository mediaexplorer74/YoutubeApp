using System;
using System.Runtime.InteropServices;

namespace LibVLCSharp.Shared
{
    public class PlatformHelper
    {
        public static bool IsWindows
        {
            get => true;
        }

        public static bool IsLinux
        {
            get => false;
        }

        public static bool IsMac
        {
            get => false; // no easy way to detect Mac platform host at runtime under net471
        }

        public static bool IsX64BitProcess => IntPtr.Size == 8;
    }
}