using SlitherNET.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SlitherNET
{
    public class Program
    {
        public static Settings @Settings { get; set; }

        public static void Main(string[] args)
        {
            Console.Title = "SlitherNET";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("SlitherNET by Nightwolf");
            Console.ForegroundColor = ConsoleColor.Gray;
            LoadSettings();

            var wssv = new WebSocketServer("ws://" + Settings.Network.Addr + ":" + Settings.Network.Port);
            wssv.AddWebSocketService<GameClient>("/slither");
            wssv.Start();
            Console.WriteLine("Listen on " + Settings.Network.Addr + ":" + Settings.Network.Port);

            while (true)
                Console.ReadLine();
        }

        public static void LoadSettings()
        {
            var reader = new StreamReader("Settings.yaml");
            var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());
            @Settings = deserializer.Deserialize<Settings>(reader);
            reader.Close();
        }
    }

    public class Settings
    {
        public NetworkSettings Network { get; set; }

        public class NetworkSettings
        {
            public string Addr { get; set; }
            public int Port { get; set; }
        }
    }
}
