using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherNET.Network.Packets.Client
{
    public class CMSG_SetUsername : IPacket
    {
        public char ProtocolId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Username { get; set; }
        public byte SkinId { get; set; }

        public void Deserialize(byte[] data)
        {
            var reader = new BigEndianReader(new MemoryStream(data));
            reader.ReadByte();
            reader.ReadByte();
            this.SkinId = reader.ReadByte();
            this.Username = reader.ReadEndString();
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
