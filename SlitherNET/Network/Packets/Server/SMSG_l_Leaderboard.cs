using SlitherNET.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherNET.Network.Packets.Server
{
    public class SMSG_l_Leaderboard : IPacket
    {
        public char ProtocolId
        {
            get
            {
                return 'l';
            }
        }

        public short Rank { get; set; }
        public List<Snake> Snakes { get; set; }

        public SMSG_l_Leaderboard(short rank, List<Snake> snakes)
        {
            this.Rank = rank;
            this.Snakes = snakes;
        }

        public void Deserialize(byte[] data)
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize()
        {
            var lengthOfUsername = 0;
            this.Snakes.ForEach(x => lengthOfUsername += x.Player.Username.Length);
            var bytes = new byte[(8 + lengthOfUsername) + (this.Snakes.Count * 7)];
            var writer = new BigEndianWriter(new MemoryStream(bytes));

            writer.WriteByte(0);
            writer.WriteByte(0);
            writer.WriteByte(Convert.ToByte(this.ProtocolId));
            writer.WriteByte(0);
            writer.WriteShort(this.Rank);
            writer.WriteShort((short)this.Snakes.Count);
            foreach(var snake in this.Snakes)
            {
                writer.WriteShort(306);
                writer.WriteInt24(0.7810754645511785 * 16777215);
                writer.WriteByte((byte)snake.Skin);
                writer.WriteString(snake.Player.Username);
            }

            return bytes;
        }
    }
}
