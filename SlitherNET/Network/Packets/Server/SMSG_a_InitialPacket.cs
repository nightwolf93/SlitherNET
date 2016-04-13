using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherNET.Network.Packets.Server
{
    public class SMSG_a_InitialPacket : IPacket
    {
        public char ProtocolId
        {
            get
            {
                return 'a';
            }
        }

        public int Radius { get; set; }

        public SMSG_a_InitialPacket(int radius)
        {
            this.Radius = radius;
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
            writer.WriteInt24(this.Radius);
            writer.WriteShort(411);
            writer.WriteShort(480);
            writer.WriteShort(130);
            writer.WriteByte((byte)4.8 * 10);
            writer.WriteShort((short) 4.25 * 100);
            writer.WriteShort((short) 0.5f * 100);
            writer.WriteShort((short) 12 * 100);
            writer.WriteShort((short)(0.033 * 1E3));
            writer.WriteShort((short)(0.028 * 1E3));
            writer.WriteShort((short)(0.43 * 1E3));
            writer.WriteByte(Metadata.PROTOCOL_VERSION);

            return bytes;
        }
    }
}
