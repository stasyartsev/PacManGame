using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PacMan
{
    class Obstacle: GameElement
    {
                public Obstacle()
        {
            Type = "Obstacle";
            Size = 15;
            Color = Color.FromArgb(255, 0, 0, 0);
        }

                public Obstacle(string type, double posX, double posY, Color color, int size, Rectangle rectangle)
            : base(type, posX, posY, color, size, rectangle)
        {
            Rect = rectangle;
        }
        
        public override void Draw(Graphics graphicContext)
        {
            SolidBrush brush = new SolidBrush(Color);
            graphicContext.FillRectangle(brush, (int)PositionX, (int)PositionY, Size, Size);
            graphicContext.FillEllipse(brush, (int)PositionX, (int)PositionY, Size, Size);

        }

    }
}
