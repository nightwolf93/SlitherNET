using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherNET.Network.Packets.Server
{
    public class SMSG_m_MessageOfTheDay : IPacket
    {
        public char ProtocolId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Message { get; set; }
        public string Author { get; set; }

        public SMSG_m_MessageOfTheDay(string message, string author)
        {
            this.Message = message;
            this.Author = author;
        }

        public void Deserialize(byte[] data)
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize()
        {
            var bytes = new byte[1000];
            var writer = new BigEndianWriter(new MemoryStream(bytes));
            writer.WriteByte(0);
            writer.WriteByte(0);
            writer.WriteByte(Convert.ToByte('m'));
            writer.WriteByte((byte)(462 >> 16));
            writer.WriteByte((byte)(462 >> 8));
            writer.WriteByte((byte)(462 & 0xFF));

            var loc1 = (int)0.580671702663404 * 16777215;
            writer.WriteByte((byte)(loc1 >> 16));
            writer.WriteByte((byte)(loc1 >> 8));
            writer.WriteByte((byte)(loc1 & 0xFF));

            writer.WriteString(this.Author);
            writer.WriteString(this.Message);

            return bytes;
        }
    }
}
