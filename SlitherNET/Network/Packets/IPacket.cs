using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherNET.Network.Packets
{
    public interface IPacket
    {
        char ProtocolId { get; }
        byte[] Serialize();
        void Deserialize(byte[] data);
    }
}
