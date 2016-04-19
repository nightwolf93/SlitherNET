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
                    Skin = usernamePacket.SkinId,
                    Position = new Vector2f((float)(28907.6 * 5), (float)(21137.4 * 5)),
                    Name = this.Username == "" ? "Anonymous" : this.Username,
                    HeadPosition = new Vector2f(28907.3f * 5, 21136.8f * 5),
                };
                this.SendPacket(new SMSG_s_NewSnake(this.MySnake));
                this.SendPacket(new SMSG_m_MessageOfTheDay("SlitherNET, a .net server engine for slither.io", "https://github.com/nightwolf93/SlitherNET"));
                
                GameRoom.Instance.AddPlayer(this);
                GameRoom.Instance.ShowFoods(this);
                GameRoom.Instance.UpdateLeaderboard();
                this.SendPacket(new SMSG_p_Pong());

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
                    var degrees = (short)Math.Floor(updatePacket.ActionType * 1.44);
                    this.MySnake.CurrentAngle = degrees;
                    Console.WriteLine("Mouse angle : " + degrees);
                    this.SendPacket(new SMSG_e_UpdateSnakeDirection(this.MySnake, degrees));
                }
            }
            
        }

        public void UpdateSnake()
        {
            this.MySnake.Position.X += ((float)Math.Cos(this.MySnake.CurrentAngle) / 5) * 1000;
            this.MySnake.Position.Y += ((float)Math.Sin(this.MySnake.CurrentAngle) / 5) * 1000;

            //Console.WriteLine("X : " + (float)Math.Cos(this.MySnake.CurrentAngle) / 5);
            //Console.WriteLine("Y : " + (float)Math.Sin(this.MySnake.CurrentAngle) / 5);

            this.SendPacket(new SMSG_e_UpdateSnakeDirection(this.MySnake, this.MySnake.CurrentAngle));
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
