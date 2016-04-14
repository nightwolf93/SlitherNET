using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherNET
{
    public class Metadata
    {
        public const byte PROTOCOL_VERSION = 6;
        public static Random Rng = new Random(Environment.TickCount);
    }
}
