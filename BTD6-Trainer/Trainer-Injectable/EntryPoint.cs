using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Trainer_Injectable
{
    public class EntryPoint
    {
        public static int Main(string param)
        {
            AllocConsole();
            while (true)
            {
                Console.WriteLine("(1) Patcher mode (2) Trainer mode");
                Console.Write("->");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    Console.WriteLine("Not implemented");
                }
                else if (input == "2")
                {
                    Application.Run(new Trainer());
                }
                else
                {
                    continue;
                }
            }
        }

        private const UInt32 StdOutputHandle = 0xFFFFFFF5;
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetStdHandle(UInt32 nStdHandle);
        [DllImport("kernel32.dll")]
        private static extern void SetStdHandle(UInt32 nStdHandle, IntPtr handle);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
