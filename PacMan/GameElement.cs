using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace PacMan
{
    abstract class GameElement : DrawableShape, Deserializable
    {
        public Rectangle Rect { get; set; }
        
        public string Type { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public Color Color { get; set; }
        public int Size { get; set; }

        public GameElement()
        {
        }
        public GameElement(string type, double posX, double posY, Color color, int size, Rectangle rectangle)
        {
            Rect = rectangle;
            Type = type;
            PositionX = posX;
            PositionY = posY;
            Color = color;
            Size = size;

        }
        abstract public void Draw(System.Drawing.Graphics ctx);

        public virtual void Deserialize(List<NameValue> list)
        {
            Rectangle rect = new Rectangle();
            foreach (NameValue curNameVal in list)
            {
                if (curNameVal.Name == " X")
                {
                    int x = -1;
                    int.TryParse(curNameVal.Value, out x);
                    this.PositionX = x;
                    rect.X = x;
                    Rect = rect;
                }
                if (curNameVal.Name == " Y")
                {
                    int y = -1;
                    int.TryParse(curNameVal.Value, out y);
                    this.PositionY = y;
                    rect.Y = y;
                    Rect = rect;
                }

                rect.Width = Size;
                rect.Height = Size;
                Rect = rect;
                       
            }
        }
    }
}
