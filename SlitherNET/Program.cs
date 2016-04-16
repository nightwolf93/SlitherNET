using SlitherNET.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace SlitherNET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "SlitherNET";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("SlitherNET by Nightwolf");
            Console.ForegroundColor = ConsoleColor.Gray;

            var wssv = new WebSocketServer("ws://127.0.0.1:444");
            wssv.AddWebSocketService<GameClient>("/slither");
            wssv.Start();

            while (true)
                Console.ReadLine();
        }
    }
}
