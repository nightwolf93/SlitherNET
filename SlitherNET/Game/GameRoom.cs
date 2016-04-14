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

        public GameRoom()
        {
            this.Foods = new List<Food>();
            this.initializeFoods();
        }

        private void initializeFoods()
        {
            for (int i = 0; i < 10000; i++)
            {
                this.Foods.Add(new Food());
            }
        }

        public void AddPlayer(GameSession session)
        {

        }

        public void ShowFoods(GameSession session)
        {
            session.SendPacket(new SMSG_F_MapFoods(this.Foods));
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
