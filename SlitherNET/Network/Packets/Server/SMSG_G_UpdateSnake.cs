using SlitherNET.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherNET.Network.Packets.Server
{
    public class SMSG_G_UpdateSnake : IPacket
    {
        public char ProtocolId
        {
            get
            {
                return 'g';
            }
        }

        public Snake @Snake { get; set; }

        public SMSG_G_UpdateSnake(Snake snake)
        {
            this.Snake = snake;
        }

        public void Deserialize(byte[] data)
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize()
        {
            var bytes = new byte[30];
            var writer = new BigEndianWriter(new MemoryStream(bytes));

            writer.WriteByte(0);
            writer.WriteByte(0);
            writer.WriteByte(Convert.ToByte(this.ProtocolId));
            writer.WriteShort((short)this.Snake.ID);
            writer.WriteShort((short)(this.Snake.Position.X));
            writer.WriteShort((short)(this.Snake.Position.Y));

            return bytes;
        }
    }
}
