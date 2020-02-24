using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Trainer_Injectable
{
    public class MommyMem
    {
        [DllImport("kernel32.dll")]
        static extern bool VirtualProtect(IntPtr lpAddress, ulong dwSize, uint flNewProtect, out uint lpflOldProtect);


        public static IntPtr gameAssembly;

        public MommyMem()
        {
            foreach(ProcessModule module in Process.GetCurrentProcess().Modules)
            {
                if (module.FileName.Contains("GameAssembly"))
                {
                    gameAssembly = module.BaseAddress;
                }
            }
        }

        public static void unprotectMemory(ulong address, ulong bytesToUnprotect)
        {
            uint receiver = 0;
            VirtualProtect((IntPtr)address, bytesToUnprotect, 0x40, out receiver);
        }

        public static byte[] ceByte2Bytes(string byteString)
        {
            string[] byteStr = byteString.Split(' ');
            byte[] bytes = new byte[byteStr.Length];
            int c = 0;
            foreach (string b in byteStr)
            {
                bytes[c] = (Convert.ToByte(b, 16));
                c++;
            }
            return bytes;
        }
        public static int[] ceByte2Ints(string byteString)
        {
            string[] intStr = byteString.Split(' ');
            int[] ints = new int[intStr.Length];
            int c = 0;
            foreach (string b in intStr)
            {
                ints[c] = (int.Parse(b, System.Globalization.NumberStyles.HexNumber));
                c++;
            }
            return ints;
        }



        public static unsafe void writeByte(ulong addr, byte value)
        {
            *(byte*)addr = value;
        }
        public static unsafe byte readByte(ulong addr)
        {
            return *(byte*)addr;
        }
        public static void writeAob(ulong addr, byte[] bytes)
        {
            ulong i = 0;
            foreach(byte b in bytes)
            {
                writeByte(addr + i, b);
                i++;
            }
        }
        public static byte[] readAob(ulong addr, ulong length)
        {
            byte[] bytes = new byte[length];
            for(ulong i =0; i<length; i++)
            {
                bytes[i] = readByte(addr + i);
            }
            return bytes;
        }
        public static unsafe void writeUlong(ulong addr, ulong value)
        {
            *(ulong*)addr = value;
        }
    }
}
