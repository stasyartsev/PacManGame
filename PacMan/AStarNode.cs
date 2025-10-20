using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    public class AStarNode
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public int F { get { return G + H; } }
        public AStarNode Parent { get; set; }

        public AStarNode(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
