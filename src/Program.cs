using System;

namespace SocketFree
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new ProgramOptions();
            options.Parse(args);
            
            if (options.ShowHelp)
            {
                ShowUsage();
                return;
            }

            if (options.Port != 0)
            {
                var free = new SocketFree();
                free.KillProcessesByPort(options.Port);
                return;
            }

            ShowUsage();
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("This utility will terminate the process holding open the TCP port");
            Console.WriteLine();
            Console.WriteLine("-? -h -help        Show usage information");
            Console.WriteLine("-p -port           TCP port");
            Console.WriteLine();
        }
    }
}
