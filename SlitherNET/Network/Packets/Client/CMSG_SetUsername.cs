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

        public void Deserialize(byte[] data)
        {
            var reader = new BigEndianReader(new MemoryStream(data));
            var loc3 = reader.ReadByte();
            this.Username = reader.ReadUTF();
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
