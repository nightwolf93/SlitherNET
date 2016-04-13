using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherNET.Network.Packets.Client
{
    public class CMSG_Update : IPacket
    {
        public char ProtocolId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public byte ActionType { get; set; }

        public void Deserialize(byte[] data)
        {
            var reader = new BigEndianReader(new MemoryStream(data));
            this.ActionType = reader.ReadByte();
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
