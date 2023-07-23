using System;
using System.Threading;

namespace WeCraftServer
{
    internal class StartUp
    {
        private static WeCraftServer server;
        public static void Main(string[] args)
        {
            server = WeCraftServer.GetInstance();
            server.Run();
            ReadConsole();
        }

        private static void ReadConsole()
        {
            while (!server.CancelToken.IsCancellationRequested)
            {
                Console.WriteLine(Console.ReadLine());
                Thread.Sleep(50);
            }
        }
    }
}