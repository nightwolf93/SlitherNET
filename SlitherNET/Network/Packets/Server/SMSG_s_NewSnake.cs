using SlitherNET.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherNET.Network.Packets.Server
{
    public class SMSG_s_NewSnake : IPacket
    {
        public char ProtocolId
        {
            get
            {
                return 's';
            }
        }

        public Snake @Snake { get; set; }

        public SMSG_s_NewSnake(Snake snake)
        {
            this.Snake = snake;
        }

        public void Deserialize(byte[] data)
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize()
        {
            var bytes = new byte[(37 + this.Snake.Name.Length) + (2 * this.Snake.Parts.Count)];
            var writer = new BigEndianWriter(new MemoryStream(bytes));

            writer.WriteByte(0);
            writer.WriteByte(0);
            writer.WriteByte(Convert.ToByte(this.ProtocolId));
            writer.WriteShort((short)this.Snake.ID);
            writer.WriteInt24(3.1415926535 / Math.PI * 16777215);
            writer.WriteByte(0);
            writer.WriteInt24(3.1415926535 / Math.PI * 16777215);
            writer.WriteShort((short)this.Snake.Speed);
            writer.WriteInt24(0.028860630325116536 * 16777215);
            writer.WriteByte((byte)this.Snake.Skin);
            writer.WriteInt24(this.Snake.Position.X);
            writer.WriteInt24(this.Snake.Position.Y);
            writer.WriteString(this.Snake.Name);
            writer.WriteInt24(this.Snake.HeadPosition.X);
            writer.WriteInt24(this.Snake.HeadPosition.Y);
            foreach(var part in this.Snake.Parts)
            {
                writer.WriteByte((byte)part.Position.X);
                writer.WriteByte((byte)part.Position.Y);
            }

            return bytes;
        }
    }
}
