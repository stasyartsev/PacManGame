using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    enum Direction { UP, DOWN, LEFT, RIGHT, NO_DIRECTION };
    
    interface MovableShape
    {
        double PositionX { get; set; }
        double PositionY { get; set; }
        double PrevPosX { get; set; }
        double PrevPosY { get; set; }
        double Velocity { get; set; }
        void Move(float time);
        Direction Direction { get; set; }
        bool IsCollided { get; set; }
        Direction RequestedDir { get; set; }
        
    }
}
