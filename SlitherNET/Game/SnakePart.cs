using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherNET.Game
{
    public class SnakePart
    {
        public Snake Body { get; set; }
        public Vector2f Position { get; set; }

        public SnakePart(Snake body, Vector2f position)
        {
            this.Body = body;
            this.Position = position;
        }
    }
}
