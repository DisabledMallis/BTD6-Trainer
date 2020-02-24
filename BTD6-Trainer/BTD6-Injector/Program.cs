using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BTD6_Injector
{
    class Program
    {
        static Process btdProc;
        static IntPtr pHandle;
        public static void Main(string[] args)
        {
            Console.WriteLine("BTD6 Trainer injector");

            string dataDir = Environment.ExpandEnvironmentVariables(@"%appdata%\TD6-Trainer");

            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }

            string clrDll = "";
            if (!File.Exists(dataDir + "/Trainer-CLR.dll"))
            {
                Console.WriteLine("Please place the CLR dll in \"" + dataDir + "\\Trainer-CLR.dll" + "\"");
                Console.ReadLine();
                return;
            }
            else
            {
                clrDll = dataDir + "/Trainer-CLR.dll";
            }
            clrDll = clrDll.Replace("\"", "");
            string clrInjectable = "";
            if (!File.Exists(dataDir + "/Trainer-Injectable.dll"))
            {
                Console.WriteLine("Please place the Injectable dll in \"" + dataDir + "\\Trainer-Injectable.dll" + "\"");
                Console.ReadLine();
                return;
            }
            else
            {
                clrInjectable = dataDir + "/Trainer-Injectable.dll";
            }

            Process[] btdProcs = Process.GetProcessesByName("BloonsTD6");
            if (btdProcs.Length == 0)
            {
                Console.WriteLine("Could not find Bloons! Launch it now? (y/n)");
                string inp = Console.ReadLine();
                if (inp == "y")
                {
                    Process.Start("steam://rungameid/960090");
                }
                else
                {
                    return;
                }
            }
            Thread.Sleep(1000);
            btdProcs = Process.GetProcessesByName("BloonsTD6");
            btdProc = btdProcs[0];
            pHandle = Win32.OpenProcess(0x1F0FFF, true, btdProc.Id);

            Thread.Sleep(1000);
            InjectDll(clrDll);

            Console.WriteLine("Injected CLR!");
            Console.WriteLine("Trainer is injected!");
            Thread.Sleep(1000);
        }

        public static String GetStringResource(IntPtr hModuleInstance, uint uiStringID)
        {
            StringBuilder sb = new StringBuilder(255);
            Win32.LoadString(hModuleInstance, uiStringID, sb, sb.Capacity + 1);
            return sb.ToString();
        }

        //Code from https://github.com/erfg12/memory.dll/blob/master/Memory/memory.cs
        public static void InjectDll(String strDllName)
        {
            IntPtr bytesout;

            foreach (ProcessModule pm in btdProc.Modules)
            {
                if (pm.ModuleName.StartsWith("inject", StringComparison.InvariantCultureIgnoreCase))
                    return;
            }

            if (!btdProc.Responding)
                return;

            int lenWrite = strDllName.Length + 1;
            UIntPtr allocMem = Win32.VirtualAllocEx(pHandle, (UIntPtr)null, (uint)lenWrite, 0x00001000 | 0x00002000, 0x04);

            Win32.WriteProcessMemory(pHandle, allocMem, strDllName, (UIntPtr)lenWrite, out bytesout);
            UIntPtr injector = Win32.GetProcAddress(Win32.GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            if (injector == null)
                return;

            IntPtr hThread = Win32.CreateRemoteThread(pHandle, (IntPtr)null, 0, injector, allocMem, 0, out bytesout);
            if (hThread == null)
                return;

            int Result = Win32.WaitForSingleObject(hThread, 10 * 1000);
            if (Result == 0x00000080L || Result == 0x00000102L)
            {
                if (hThread != null)
                    Win32.CloseHandle(hThread);
                return;
            }
            Win32.VirtualFreeEx(pHandle, allocMem, (UIntPtr)0, 0x8000);

            if (hThread != null)
                Win32.CloseHandle(hThread);

            return;
        }

        public static void unprotectMemory(IntPtr address, int bytesToUnprotect)
        {
            Int64 receiver = 0;
            Win32.VirtualProtectEx(pHandle, address, bytesToUnprotect, 0x40, ref receiver);
        }
    }
}
