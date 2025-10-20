using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace PacMan
{
   abstract class GamePlayer:DrawableShape, MovableShape, Deserializable
    {
       private Rectangle mRect;
       public Rectangle Rect { get { mRect= new Rectangle((int)PositionX, (int)PositionY, Size, Size); return mRect; } set { } }
       
        public string Type { get; set; }
        public double PrevPosX { get; set; }
        public double PrevPosY { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public Color Color { get; set; }
        public int Size { get; set; }
        public double Velocity { get; set; }
        public Direction Direction { get; set; }
        public bool IsCollided { get; set; }

        public Direction RequestedDir { get; set; }

        public GamePlayer()
        {
            Velocity = 0.05f;
        }
        public GamePlayer(string type, double prevPosX, double prevPosY, double posX, double posY, Color color, int size, double velocity, Rectangle rectangle, bool isCollided)
        {
            Rect = new Rectangle(0, 0, rectangle.Width, rectangle.Height);
            Type = type;
            PrevPosX = prevPosX;
            PrevPosY = prevPosY;
            PositionX = posX;
            PositionY = posY;
            mRect.Location = new Point((int)PositionX, (int)PositionY);
            Color = color;
            Size = size;
            Velocity = velocity;
            IsCollided = isCollided;
            RequestedDir = Direction.NO_DIRECTION;

        }
        abstract public void Draw(System.Drawing.Graphics ctx);

         public virtual void Move(float timeDiff)
        {
            PrevPosX = PositionX;
            PrevPosY = PositionY;
            double distDiff = timeDiff * Velocity;
            if (Direction.RIGHT == Direction)
            {
                PositionX = PositionX + distDiff;
            }
             else if (Direction.LEFT == Direction)
            {
                PositionX = PositionX - distDiff;
            }
            else if (Direction.UP == Direction)
            {
                PositionY = PositionY - distDiff;
            }
            else if (Direction.DOWN == Direction)
            {
                PositionY = PositionY + distDiff;
            }
        }
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
