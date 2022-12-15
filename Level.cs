using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnake
{
    /// <summary>
    /// The class which represents a level.
    /// </summary>
    public class Level
    {
        public double maxTick { get; }
        public double startLength { get; }
        public double endLength { get; }
        
        public double maxLengthFromMoney { get; }
        public double minLengthFromMoney { get; }
    }
}
