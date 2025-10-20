using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PacMan
{
    class Enemy: GamePlayer
    {
        public enum Position
        {
            Left, Right, Up, Down
        }
        public Enemy()
        {
            Type = "Enemy";
            Size = 15;
            Color = Color.FromArgb(255, 255, 0, 0);
        }

       public Enemy(string type, double prevPosX, double prevPosY, double posX, double posY, Color color, int size, double velocity, Rectangle rectangle, bool isCollided)
            : base(type, prevPosX, prevPosY, posX, posY, color, size, velocity, rectangle, isCollided)
        {
            Rect = rectangle;
            rectangle.X = (int)this.PositionX;
            rectangle.Y = (int)this.PositionY;
        }
        public override void Draw(Graphics graphicContext)
        {
            SolidBrush brush = new SolidBrush(Color);
            graphicContext.FillEllipse(brush, (int)PositionX, (int)PositionY, Size, Size);
        }
        public void RevertPosition()
        {
            PositionX = PrevPosX;
            PositionY = PrevPosY;
        }
    }
}
