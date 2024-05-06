using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LibVLCSharp.Shared.Helpers
{
    internal static class MarshalUtils
    {
        internal struct Native
        {

            [DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "libvlc_free", SetLastError = true)]
            public static extern void LibVLCFree(IntPtr ptr);

            const string Write = "w";

            [DllImport(Constants.libSystem, EntryPoint = "vsprintf", CallingConvention = CallingConvention.Cdecl)]
            public static extern int vsprintf_apple(IntPtr buffer, IntPtr format, IntPtr args);

            [DllImport(Constants.Libc, EntryPoint = "vsprintf", CallingConvention = CallingConvention.Cdecl)]
            public static extern int vsprintf_linux(IntPtr buffer, IntPtr format, IntPtr args);

            [DllImport(Constants.Msvcrt, EntryPoint = "vsprintf", CallingConvention = CallingConvention.Cdecl)]
            public static extern int vsprintf_windows(IntPtr buffer, IntPtr format, IntPtr args);
        }

        internal static int vsprintf(IntPtr buffer, IntPtr format, IntPtr args)
        {
            if (PlatformHelper.IsWindows)
                return Native.vsprintf_windows(buffer, format, args);
            else if (PlatformHelper.IsMac)
                return Native.vsprintf_apple(buffer, format, args);
            else if (PlatformHelper.IsLinux)
                return Native.vsprintf_linux(buffer, format, args);
            return -1;
        }

        /// <summary>
        /// Helper for libvlc_new
        /// </summary>
        /// <param name="options">libvlc options, an UTF16 string array turned to UTF8 string pointer array</param>
        /// <param name="create">the create function call</param>
        /// <returns></returns>
        internal static IntPtr CreateWithOptions(string[] options, Func<int, IntPtr[], IntPtr> create)
        {
            var utf8Args = default(IntPtr[]);
            try
            {
                utf8Args = options.ToUtf8();
                return create(utf8Args.Length, utf8Args);
            }
            finally
            {
                foreach (var arg in utf8Args)
                {
                    if (arg != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(arg);
                    }
                }
            }
        }

        /// <summary>
        /// Generic marshalling function to retrieve structs from a libvlc linked list
        /// </summary>
        /// <typeparam name="T">Internal struct type</typeparam>
        /// <typeparam name="TU">publicly facing struct type</typeparam>
        /// <param name="getRef">Native libvlc call: retrieve collection start pointer from parent reference</param>
        /// <param name="retrieve">Retrieve the internal struct by marshalling the native pointer</param>
        /// <param name="create">Create a publicly facing struct from the internal struct values</param>
        /// <param name="next">Access next element in the list</param>
        /// <param name="releaseRef">Native libvlc call: release resources allocated with the getRef call</param>
        /// <returns>An array of publicly facing struct types</returns>
        internal static TU[] Retrieve<T, TU>(Func<IntPtr> getRef, Func<IntPtr, T> retrieve,
            Func<T, TU> create, Func<T, IntPtr> next, Action<IntPtr> releaseRef)
            where T : struct
            where TU : struct
        {
            var nativeRef = IntPtr.Zero;

            try
            {
                nativeRef = getRef();
                if (nativeRef == IntPtr.Zero)
                {
                    return Array.Empty<TU>();
                }

                var resultList = new List<TU>();
                IntPtr nextRef = nativeRef;
                T structure;
                TU obj;

                while (nextRef != IntPtr.Zero)
                {
                    structure = retrieve(nextRef);
                    obj = create(structure);
                    resultList.Add(obj);
                    nextRef = next(structure);
                }
                return resultList.ToArray();
            }
            finally
            {
                if (nativeRef != IntPtr.Zero)
                {
                    releaseRef(nativeRef);
                    nativeRef = IntPtr.Zero;
                }
            }
        }

        /// <summary>
        /// Generic marshalling function to retrieve structs from libvlc by reading from unmanaged memory with offsets
        /// This supports uint libvlc signatures.
        /// </summary>
        /// <typeparam name="T">Internal struct type</typeparam>
        /// <typeparam name="TU">publicly facing struct type</typeparam>
        /// <param name="nativeRef">native reference of the parent</param>
        /// <param name="getRef">Native libvlc call: retrieve collection start pointer from parent reference</param>
        /// <param name="retrieve">Retrieve the internal struct by marshalling the native pointer</param>
        /// <param name="create">Create a publicly facing struct from the internal struct values</param>
        /// <param name="releaseRef">Native libvlc call: release the array allocated with the getRef call with the given element count</param>
        /// <returns>An array of publicly facing struct types</returns>
        internal static TU[] Retrieve<T, TU>(IntPtr nativeRef, ArrayOut getRef, Func<IntPtr, T> retrieve,
            Func<T, TU> create, Action<IntPtr, uint> releaseRef)
            where T : struct
            where TU : struct
        {
            var arrayPtr = IntPtr.Zero;
            uint count = 0;

            try
            {
                count = getRef(nativeRef, out arrayPtr);
                if(count == 0)
                {
                    return Array.Empty<TU>();
                }

                var resultList = new List<TU>();
                T structure;

                for (var i = 0; i < count; i++)
                {
                    var ptr = Marshal.ReadIntPtr(arrayPtr, i * IntPtr.Size);
                    structure = retrieve(ptr);
                    var managedStruct = create(structure);
                    resultList.Add(managedStruct);
                }

                return resultList.ToArray();
            }
            finally
            {
                if (arrayPtr != IntPtr.Zero)
                {
                    releaseRef(arrayPtr, count);
                    arrayPtr = IntPtr.Zero;
                }
            }
        }

        /// <summary>
        /// Generic marshalling function to retrieve structs from libvlc by reading from unmanaged memory with offsets
        /// This supports ulong libvlc signatures.
        /// </summary>
        /// <typeparam name="T">Internal struct type</typeparam>
        /// <typeparam name="TU">publicly facing struct type</typeparam>
        /// <param name="nativeRef">native reference of the parent</param>
        /// <param name="getRef">Native libvlc call: retrieve collection start pointer from parent reference</param>
        /// <param name="retrieve">Retrieve the internal struct by marshalling the native pointer</param>
        /// <param name="create">Create a publicly facing struct from the internal struct values</param>
        /// <param name="releaseRef">Native libvlc call: release the array allocated with the getRef call with the given element count</param>
        /// <returns>An array of publicly facing struct types</returns>
        internal static TU[] Retrieve<T, TU>(IntPtr nativeRef, ArrayLongOut getRef, Func<IntPtr, T> retrieve,
            Func<T, TU> create, Action<IntPtr, ulong> releaseRef)
            where T : struct
            where TU : struct
        {
            var arrayPtr = IntPtr.Zero;
            ulong countLong = 0;

            try
            { 
                countLong = getRef(nativeRef, out arrayPtr);
                var count = (int)countLong;

                if (count == 0)
                {
                    return Array.Empty<TU>();
                }

                var resultList = new List<TU>();
                T structure;

                for (var i = 0; i < count; i++)
                {
                    var ptr = Marshal.ReadIntPtr(arrayPtr, i * IntPtr.Size);
                    structure = retrieve(ptr);
                    var managedStruct = create(structure);
                    resultList.Add(managedStruct);
                }

                return resultList.ToArray();
            }
            finally
            {
                if(arrayPtr != IntPtr.Zero)
                {
                    releaseRef(arrayPtr, countLong);
                    arrayPtr = IntPtr.Zero;
                }
            }
        }

        /// <summary>
        /// Generic marshalling function to retrieve structs from libvlc by reading from unmanaged memory with offsets
        /// This supports ulong libvlc signatures and an additional enum configuration parameter.
        /// </summary>
        /// <typeparam name="T">Internal struct type</typeparam>
        /// <typeparam name="TU">publicly facing struct type</typeparam>
        /// <typeparam name="TE">Additional enum confugation type</typeparam>
        /// <param name="nativeRef">native reference of the parent</param>
        /// <param name="extraParam">Additional enum confugation type</param>
        /// <param name="getRef">Native libvlc call: retrieve collection start pointer from parent reference</param>
        /// <param name="retrieve">Retrieve the internal struct by marshalling the native pointer</param>
        /// <param name="create">Create a publicly facing struct from the internal struct values</param>
        /// <param name="releaseRef">Native libvlc call: release the array allocated with the getRef call with the given element count</param>
        /// <returns>An array of publicly facing struct types</returns>
        internal static TU[] Retrieve<T, TU, TE>(IntPtr nativeRef, TE extraParam, CategoryArrayOut<TE> getRef, Func<IntPtr, T> retrieve,
            Func<T, TU> create, Action<IntPtr, ulong> releaseRef) 
            where T : struct
            where TU : struct
            where TE : Enum
        {
            var arrayPtr = IntPtr.Zero;
            ulong countLong = 0;

            try
            { 
                countLong = getRef(nativeRef, extraParam, out arrayPtr);
                var count = (int)countLong;
                if (count == 0)
                {
                    return Array.Empty<TU>();
                }

                var resultList = new List<TU>();
                T structure;

                for (var i = 0; i < count; i++)
                {
                    var ptr = Marshal.ReadIntPtr(arrayPtr, i * IntPtr.Size);
                    structure = retrieve(ptr);
                    var managedStruct = create(structure);
                    resultList.Add(managedStruct);
                }

                return resultList.ToArray();
            }
            finally
            {
                if (arrayPtr != IntPtr.Zero)
                {
                    releaseRef(arrayPtr, countLong);
                    arrayPtr = IntPtr.Zero;
                }
            }
        }

        // These delegates allow the definition of generic functions with [OUT] parameters
        internal delegate ulong CategoryArrayOut<T>(IntPtr nativeRef, T enumType, out IntPtr array) where T : Enum;
        internal delegate uint ArrayOut(IntPtr nativeRef, out IntPtr array);
        internal delegate ulong ArrayLongOut(IntPtr nativeRef, out IntPtr array);

        /// <summary>
        /// Turns an array of UTF16 C# strings to an array of pointer to UTF8 strings
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Array of pointer you need to release when you're done with Marshal.FreeHGlobal</returns>
        internal static IntPtr[] ToUtf8(this string[] args)
        {
            var utf8Args = new IntPtr[args?.Length ?? 0];
            
            for (var i = 0; i < utf8Args.Length; i++)
            {
                utf8Args[i] = args[i].ToUtf8();
            }

            return utf8Args;
        }

        /// <summary>
        /// Marshal a pointer to a struct
        /// Helper with netstandard1.1 and net40 support
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ptr"></param>
        /// <returns></returns>
        internal static T PtrToStructure<T>(IntPtr ptr)
        {
            return Marshal.PtrToStructure<T>(ptr);
        }


        /// <summary>
        /// Frees an heap allocation returned by a LibVLC function.
        /// If you know you're using the same underlying C run-time as the LibVLC
        /// implementation, then you can call ANSI C free() directly instead.
        /// </summary>
        /// <param name="ptr">the pointer</param>
        internal static void LibVLCFree(ref IntPtr ptr)
        {
            if (ptr == IntPtr.Zero) return;

            Native.LibVLCFree(ptr);
            ptr = IntPtr.Zero;
        }

        /// <summary>
        /// Performs the native call, frees the ptrs and returns the result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="interopCall"></param>
        /// <param name="ptrs"></param>
        /// <returns></returns>
        internal static T PerformInteropAndFree<T>(Func<T> interopCall, params IntPtr[] ptrs)
        {
            try
            {
                return interopCall();
            }
            finally
            {
                Free(ptrs);
            }
        }

        /// <summary>
        /// Performs the native call and frees the ptrs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="interopCall"></param>
        /// <param name="ptrs"></param>
        internal static void PerformInteropAndFree(Action interopCall, params IntPtr[] ptrs)
        {
            try
            {
                interopCall();
            }
            finally
            {
                Free(ptrs);
            }
        }

        internal static int SizeOf<T>(T structure)
        {
            return Marshal.SizeOf<T>(structure);
        }

        private static void Free(params IntPtr[] ptrs)
        {
            foreach (var ptr in ptrs)
                Marshal.FreeHGlobal(ptr);
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class MonoPInvokeCallbackAttribute : Attribute
    {
        public MonoPInvokeCallbackAttribute(Type type)
        {
            Type = type;
        }
        public Type Type { get; private set; }
    }
}