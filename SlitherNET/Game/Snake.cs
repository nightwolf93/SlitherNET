using SlitherNET.Network;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherNET.Game
{
    public class Snake
    {
        public int ID { get; set; }
        public float Speed { get; set; }
        public int Skin { get; set; }
        public Vector2f Position { get; set; }
        public string Name { get; set; }
        public Vector2f HeadPosition { get; set; }
        public List<SnakePart> Parts { get; set; }
        public int Size { get; set; }
        public short CurrentAngle { get; set; }

        public GameClient Player { get; set; }

        public Snake()
        {
            this.Parts = new List<SnakePart>();
            for (int i = 0; i < 50; i += 2)
            {
                this.Parts.Add(new SnakePart(this, new Vector2f(i + 1, i + 2)));
            }
        }
    }
}
