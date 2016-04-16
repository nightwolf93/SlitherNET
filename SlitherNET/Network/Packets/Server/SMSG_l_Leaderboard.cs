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
            var bytes = new byte[300];
            var writer = new BigEndianWriter(new MemoryStream(bytes));

            writer.WriteByte(0);
            writer.WriteByte(0);
            writer.WriteByte(Convert.ToByte(this.ProtocolId));
            writer.WriteShort(this.Rank);
            writer.WriteShort((short)this.Snakes.Count);
            foreach(var snake in this.Snakes)
            {
                writer.WriteString(snake.Player.Username);
                writer.WriteShort(306);
                writer.WriteInt24(0.7810754645511785 * 16777215);
                writer.WriteByte((byte)snake.Skin);
            }

            //b += msgUtil.writeInt8(b, arr, 0);
            //b += msgUtil.writeInt8(b, arr, 0);
            //b += msgUtil.writeInt8(b, arr, this.packetType);
            //b += msgUtil.writeInt16(b, arr, this.rank);
            //b += msgUtil.writeInt16(b, arr, this.playersCount);

            //for (var i = 0; i < this.topTen.length; i++)
            //{
            //    b += msgUtil.writeInt8(b, arr, this.topTen[i].snake.username.length);
            //    b += msgUtil.writeString(b, arr, this.topTen[i].snake.username);
            //    b += msgUtil.writeInt16(b + 1, arr, this.topTen[i].snake.J);
            //    b += msgUtil.writeInt24(b + 1, arr, this.topTen[i].snake.I);
            //    b += msgUtil.writeInt8(b + 1, arr, this.topTen[i].snake.color);
            //}

            return bytes;
        }
    }
}
