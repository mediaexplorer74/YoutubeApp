using LibVLCSharp.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace LibVLCSharp.Shared
{
    public static class Core
    {
        struct Native
        {
            [DllImport(Constants.Kernel32, CharSet = CharSet.Unicode, SetLastError = true)]
            internal static extern IntPtr LoadPackagedLibrary(string dllToLoad, uint reserved = 0);

            [DllImport(Constants.Kernel32, SetLastError = true)]
            internal static extern ErrorModes SetErrorMode(ErrorModes uMode);

            [DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "libvlc_get_version")]
            internal static extern IntPtr LibVLCVersion();
        }

   static IntPtr _libvlcHandle;

        /// <summary>
        /// Load the native libvlc library (if necessary, depending on platform)
        /// <para/> Ensure that you installed the VideoLAN.LibVLC.[YourPlatform] package in your target project
        /// <para/> This will throw a <see cref="VLCException"/> if the native libvlc libraries cannot be found or loaded.
        /// <para/> It may also throw a <see cref="VLCException"/> if the LibVLC and LibVLCSharp major versions do not match.
        /// See https://code.videolan.org/videolan/LibVLCSharp/blob/master/VERSIONING.md for more info about the versioning strategy.
        /// </summary>
        /// <param name="libvlcDirectoryPath">The path to the directory that contains libvlc and libvlccore
        /// No need to specify unless running netstandard 1.1, or using custom location for libvlc
        /// <para/> This parameter is NOT supported on Linux, use LD_LIBRARY_PATH instead.
        /// </param>
        public static void Initialize(string libvlcDirectoryPath = null)
        {
            DisableMessageErrorBox();
            InitializeUWP();

        }

        /// <summary>
        /// Disable error dialogs in case of dll loading failures on older Windows versions.
        /// <para/>
        /// This is mostly to fix Windows XP support (https://code.videolan.org/videolan/LibVLCSharp/issues/173),
        /// though it may happen under other conditions (broken plugins/wrong ABI).
        /// <para/>
        /// As libvlc may load additional plugins later in the lifecycle of the application, 
        /// we should not unset this on exiting <see cref="Initialize(string)"/>
        /// </summary>
        static void DisableMessageErrorBox()
        {
            if (!PlatformHelper.IsWindows) return;

            var oldMode = Native.SetErrorMode(ErrorModes.SYSTEM_DEFAULT);
            Native.SetErrorMode(oldMode | ErrorModes.SEM_FAILCRITICALERRORS | ErrorModes.SEM_NOOPENFILEERRORBOX);
        }

        static void InitializeUWP()
        {
            _libvlcHandle = Native.LoadPackagedLibrary(Constants.LibraryName);
            if (_libvlcHandle == IntPtr.Zero)
            {
                Debug.WriteLine($"[ex] VLC Core: Failed to load {Constants.LibraryName}{Constants.WindowsLibraryExtension}, ");
                throw new VLCException(
                    $"Failed to load {Constants.LibraryName}{Constants.WindowsLibraryExtension}, " +
                    $"error {Marshal.GetLastWin32Error()}. Please make sure that this library, " +
                    $"{Constants.CoreLibraryName}{Constants.WindowsLibraryExtension} and the plugins are copied to the `AppX` folder. For that, you can reference the `VideoLAN.LibVLC.WindowsRT` NuGet package.");
            }
        }


    }

    internal static class Constants
    {
        internal const string LibraryName = "libvlc";
        internal const string CoreLibraryName = "libvlccore";

        /// <summary>
        /// The name of the folder that contains the per-architecture folders
        /// </summary>
        internal const string LibrariesRepositoryFolderName = "libvlc";

        internal const string Msvcrt = "msvcrt";
        internal const string Libc = "libc";
        internal const string libSystem = "libSystem";
        internal const string Kernel32 = "kernel32";
        internal const string libX11 = "libX11";
        internal const string WindowsLibraryExtension = ".dll";
        internal const string MacLibraryExtension = ".dylib";
    }

    internal static class ArchitectureNames
    {
        internal const string Win64 = "win-x64";
        internal const string Win86 = "win-x86";
        internal const string Winrt64 = "winrt-x64";
        internal const string Winrt86 = "winrt-x86";
        internal const string WinrtArm = "winrt-arm";

        internal const string Lin64 = "linux-x64";
        internal const string LinArm = "linux-arm";

        internal const string MacOS64 = "osx-x64";
    }

    [Flags]
    internal enum ErrorModes : uint
    {
        SYSTEM_DEFAULT = 0x0,
        SEM_FAILCRITICALERRORS = 0x0001,
        SEM_NOALIGNMENTFAULTEXCEPT = 0x0004,
        SEM_NOGPFAULTERRORBOX = 0x0002,
        SEM_NOOPENFILEERRORBOX = 0x8000
    }
}