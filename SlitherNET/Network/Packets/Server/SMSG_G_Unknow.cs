using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherNET.Network.Packets.Server
{
    public class SMSG_g_Unknow : IPacket
    {
        public char ProtocolId
        {
            get
            {
                return 'g';
            }
        }

        public void Deserialize(byte[] data)
        {
            throw new NotImplementedException();
        }

        public int X { get; set; }
        public int Y { get; set; }

        public SMSG_g_Unknow(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public byte[] Serialize()
        {
            var bytes = new byte[30];
            var writer = new BigEndianWriter(new MemoryStream(bytes));

            writer.WriteByte(0);
            writer.WriteByte(0);
            writer.WriteByte(Convert.ToByte(this.ProtocolId));
            writer.WriteInt24(this.X);
            writer.WriteInt24(this.Y);

            return bytes;
        }
    }
}
