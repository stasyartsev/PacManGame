using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PacMan
{
    class Pellet: GameElement
    {
        public Pellet()
        {
            Type = "Pellet";
            Size = 10;
            Color = Color.FromArgb(255, 255, 0, 0);

        }
        public Pellet(string type, double posX, double posY, Color color, int size, Rectangle rectangle)
            : base(type, posX, posY, color, size, rectangle)
        {

        }
         public override void Draw(Graphics graphicContext)
        {
            Pen Pen = new Pen(Color);
            graphicContext.DrawEllipse(Pen, (int)PositionX, (int)PositionY, Size, Size);

        }
    }
}
