using SlitherNET.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherNET.Network.Packets.Server
{
    public class SMSG_e_UpdateSnakeDirection : IPacket
    {
        public char ProtocolId
        {
            get
            {
                return 'e';
            }
        }

        public Snake @Snake { get; set; }
        public double Direction { get; set; }

        public SMSG_e_UpdateSnakeDirection(Snake snake, double direction)
        {
            this.Snake = snake;
            this.Direction = direction;
        }

        public void Deserialize(byte[] data)
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize()
        {
            var bytes = new byte[10];
            var writer = new BigEndianWriter(new MemoryStream(bytes));

            writer.WriteByte(0);
            writer.WriteByte(0);
            writer.WriteByte(Convert.ToByte(this.ProtocolId));
            writer.WriteShort((short)this.Snake.ID);
            writer.WriteByte((byte) this.Direction);
            writer.WriteByte((byte) 71);
            writer.WriteByte((byte) 104);

            return bytes;
        }
    }
}
