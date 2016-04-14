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

        public Snake()
        {
            this.Parts = new List<SnakePart>();
            this.Parts.Add(new SnakePart(this, new Vector2f(1, 2)));
            this.Parts.Add(new SnakePart(this, new Vector2f(3, 4)));
        }
    }
}
