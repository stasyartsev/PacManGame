using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PacMan
{
    public partial class Form1 : Form
    {

        Engine mEngine;

        public Form1()
        {
            mEngine = Engine.GetInstance();
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {


            foreach (DrawableShape elem in mEngine.Drawables)
            {
                elem.Draw(e.Graphics);
            }
            mEngine.CalcNextFrameState();
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            Direction charDirection = mEngine.MainPlayer.Direction;

            if (e.KeyCode == Keys.Right)
            {
                ChangeDirection(charDirection, Direction.RIGHT);
            }
            else if (e.KeyCode == Keys.Left)
            {
                ChangeDirection(charDirection, Direction.LEFT);
            }
            else if (e.KeyCode == Keys.Up)
            {
                ChangeDirection(charDirection, Direction.UP);
            }
            else if (e.KeyCode == Keys.Down)
            {
                ChangeDirection(charDirection, Direction.DOWN);

            }

            //if (mEngine._pellet != null)
            //{
            //    //bool isCollapsed = mEngine.CollisionDetection(mEngine.MainPlayer, mEngine._pellet);
            //    if (isCollapsed == true)
            //    {

            //        mEngine.RemovePellet(mEngine._pellet);
            //        this.Invalidate();
            //    }
            //    // this.Invalidate();
            //}
        }

        private void ChangeDirection(Direction prevDirection, Direction nextDirection)
        {
            mEngine.MainPlayer.Direction = nextDirection;
            if (mEngine.WillCollideWall(mEngine.MainPlayer))
            {
                mEngine.MainPlayer.RequestedDir = nextDirection;
                mEngine.MainPlayer.Direction = prevDirection;
            }
            else
            {
                mEngine.MainPlayer.RequestedDir = Direction.NO_DIRECTION;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string file = @"C:\Users\Stassilla\Documents\Visual Studio 2013\Projects\PacMan\map.txt";
            mEngine.ReadFromFile(file);
        }
    }
}
