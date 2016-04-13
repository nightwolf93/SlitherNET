using SlitherNET.Network;
using SlitherNET.Network.Packets;
using SlitherNET.Network.Packets.Client;
using SlitherNET.Network.Packets.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace SlitherNET
{
    public class GameSession : WebSocketBehavior
    {
        public int GameState = 1;
        public string Username = string.Empty;

        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("Received message (LEN : " + e.RawData.Length + ")");
            
            if (GameState == 1) // Username
            {
                // Set the username
                var usernamePacket = new CMSG_SetUsername();
                usernamePacket.Deserialize(e.RawData);
                this.Username = usernamePacket.Username;

                // Send the initial packet
                this.GameState = 2;
                this.SendPacket(new SMSG_a_InitialPacket(21000));
            }
            else if(this.GameState == 2) // Update game
            {
                var updatePacket = new CMSG_Update();
                updatePacket.Deserialize(e.RawData);

                if(updatePacket.ActionType == 253) // Mouse down
                {

                }
                else if(updatePacket.ActionType == 254) // Mouse up
                {

                }
                else if (updatePacket.ActionType == 251) // Ping
                {

                }
                else // Mouse rotation
                {

                }
            }
            
        }

        public void SendPacket(IPacket packet)
        {
            var bs = packet.Serialize();
            Console.WriteLine("Send packet ===> " + packet.GetType().Name + " (len: " + bs.Length + ")");
            this.Send(bs);
        }
    }
}
