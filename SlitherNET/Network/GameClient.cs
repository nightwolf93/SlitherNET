using SlitherNET.Game;
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
using System.Timers;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace SlitherNET.Network
{
    public class GameClient : WebSocketBehavior
    {
        public int GameState = 1;
        public string Username = string.Empty;
        public Snake MySnake = null;
        public bool Active = true;
        public Timer LogicTimer { get; set; }

        protected override void OnClose(CloseEventArgs e)
        {
            Console.WriteLine("Connection closed with the player");
            this.Active = false;
            GameRoom.Instance.RemovePlayer(this);
            GameRoom.Instance.UpdateLeaderboard();
            base.OnClose(e);
        }

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
                this.SendPacket(new SMSG_a_InitialPacket(21600));

                this.MySnake = new Snake()
                {
                    Player = this,
                    ID = 1,
                    Speed = (float)(5.76 * 1E3),
                    Skin = Metadata.Rng.Next(1, 20),
                    Position = new Vector2f((float)(28907.6 * 5), (float)(21137.4 * 5)),
                    Name = this.Username == "" ? "Anonymous" : this.Username,
                    HeadPosition = new Vector2f(28907.3f * 5, 21136.8f * 5),
                };
                this.SendPacket(new SMSG_s_NewSnake(this.MySnake));
                
                {
                    var bytes = new byte[1000];
                    var writer = new BigEndianWriter(new MemoryStream(bytes));
                    writer.WriteByte(0);
                    writer.WriteByte(0);
                    writer.WriteByte(Convert.ToByte('m'));
                    writer.WriteByte((byte)(462 >> 16));
                    writer.WriteByte((byte)(462 >> 8));
                    writer.WriteByte((byte)(462 & 0xFF));

                    var loc1 = (int)0.580671702663404 * 16777215;
                    writer.WriteByte((byte)(loc1 >> 16));
                    writer.WriteByte((byte)(loc1 >> 8));
                    writer.WriteByte((byte)(loc1 & 0xFF));
                    
                    writer.WriteString("https://github.com/nightwolf93/SlitherNET");
                    writer.WriteString("SlitherNET, a .net server engine for slither.io");
                    this.Send(bytes);
                }
                
                //this.SendPacket(new SMSG_g_Unknow(28907, 21136));
                GameRoom.Instance.AddPlayer(this);
                GameRoom.Instance.ShowFoods(this);
                GameRoom.Instance.UpdateLeaderboard();
                this.SendPacket(new SMSG_p_Pong());
                //this.SendPacket(new SMSG_G_UpdateSnake(this.MySnake));

                this.LogicTimer = new Timer(1000);
                this.LogicTimer.Elapsed += (object sender, ElapsedEventArgs e2) =>
                {
                    if (this.Active)
                    {
                        this.UpdateSnake();
                    }
                    else
                    {
                        this.LogicTimer.Stop();
                    }
                };
                this.LogicTimer.Start();
            }
            else if(this.GameState == 2) // Update game
            {
                var updatePacket = new CMSG_Update();
                updatePacket.Deserialize(e.RawData);

                Console.WriteLine("ActionType : " + updatePacket.ActionType);

                if(updatePacket.ActionType == 253) // Mouse down
                {

                }
                else if(updatePacket.ActionType == 254) // Mouse up
                {

                }
                else if (updatePacket.ActionType == 251) // Ping
                {
                    this.SendPacket(new SMSG_p_Pong());
                }
                else // Mouse rotation
                {
                    Console.WriteLine("Mouse rotation (angle: " + updatePacket.ActionType + ") ");
                    this.SendPacket(new SMSG_e_UpdateSnakeDirection(this.MySnake, updatePacket.ActionType / 100));
                }
            }
            
        }

        public void UpdateSnake()
        {
            this.MySnake.Position.X += 100;
            this.MySnake.Position.Y -= 100;
            this.SendPacket(new SMSG_G_UpdateSnake(this.MySnake));
        }

        public void SendPacket(IPacket packet)
        {
            var bs = packet.Serialize();
            Console.WriteLine("Send packet ===> " + packet.GetType().Name + " (len: " + bs.Length + ")");
            this.Send(bs);
        }
    }
}
