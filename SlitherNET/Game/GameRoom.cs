using SlitherNET.Network;
using SlitherNET.Network.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherNET.Game
{
    public class GameRoom
    {
        public List<Food> Foods { get; set; }
        public List<GameClient> Players { get; set; }

        public GameRoom()
        {
            this.Foods = new List<Food>();
            this.Players = new List<GameClient>();
            this.initializeFoods();
        }

        private void initializeFoods()
        {
            for (int i = 0; i < 10000; i++)
            {
                this.Foods.Add(new Food());
            }
        }

        public void AddPlayer(GameClient session)
        {
            lock (this.Players)
            {
                this.Players.Add(session);
            }
        }

        public void RemovePlayer(GameClient session)
        {
            lock (this.Players)
            {
                this.Players.Remove(session);
            }
        }

        public void ShowFoods(GameClient session)
        {
            session.SendPacket(new SMSG_F_MapFoods(this.Foods));
        }
        
        public void UpdateLeaderboard(GameClient client)
        {
            var snakes = new List<Snake>();
            foreach(var c in this.Players)
            {
                snakes.Add(c.MySnake);
            }
            client.SendPacket(new SMSG_l_Leaderboard(1, snakes));
        }

        private static GameRoom mRoom { get; set; }
        public static GameRoom Instance
        {
            get
            {
                if (mRoom == null) mRoom = new GameRoom();
                return mRoom;
            }
        }
    }
}
