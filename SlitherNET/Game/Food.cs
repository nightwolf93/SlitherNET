using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherNET.Game
{
    public class Food
    {
        public Vector2f Position { get; set; }
        public byte Color { get; set; }

        public Food(Vector2f position, byte color)
        {
            this.Position = position;
            this.Color = color;
        }

        public Food(byte color)
        {
            this.Color = color;
            this.setRandomPosition();
        }

        public Food()
        {
            this.Color = (byte)Metadata.Rng.Next(0, 23);
            this.setRandomPosition();
        }

        private void setRandomPosition()
        {
            var randomX = Metadata.Rng.Next(22907, 30000);
            var randomY = Metadata.Rng.Next(19137, 30337);

            this.Position = new Vector2f(randomX, randomY);
        }
    }
}
