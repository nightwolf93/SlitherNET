using SlitherNET.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherNET.Network.Packets.Server
{
    public class SMSG_F_MapFoods : IPacket
    {
        public char ProtocolId
        {
            get
            {
                return 'F';
            }
        }

        public List<Food> Foods { get; set; }

        public SMSG_F_MapFoods(List<Food> foods)
        {
            this.Foods = foods;
        }

        public void Deserialize(byte[] data)
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize()
        {
            var bytes = new byte[3 + (6 * this.Foods.Count)];
            var writer = new BigEndianWriter(new MemoryStream(bytes));

            writer.WriteByte(0);
            writer.WriteByte(0);
            writer.WriteByte(Convert.ToByte(this.ProtocolId));
            foreach(var food in this.Foods)
            {
                writer.WriteByte(food.Color);
                writer.WriteShort((short)food.Position.X);
                writer.WriteShort((short)food.Position.Y);
                writer.WriteByte(food.Color);
            }

            return bytes;
        }
    }
}
