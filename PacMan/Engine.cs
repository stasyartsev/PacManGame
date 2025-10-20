using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace PacMan
{
    class Engine
    {
        private long prevFrameTime;
        private static Engine _Instance = null;
        public List<DrawableShape> Drawables;
        public List<MovableShape> Movables;
        public Player MainPlayer { get; set; }

        public List<Enemy> Enemies;
        public Map Map { get; set; }
        public CollisionDetector _collisionDetector;
        private AStarPathfinder _pathfinder;
        private Engine()
        {
            Enemies = new List<Enemy>();
            _collisionDetector = new CollisionDetector();
            Drawables = new List<DrawableShape>();
            Movables = new List<MovableShape>();
            Map = Map.GetInstance();
        }
        public static Engine GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new Engine();
            }

            return _Instance;
        }
        public void CalcNextFrameState()
        {
            long timeDifferenceBetweenFrames = 0;
            long now = DateTime.Now.Ticks / 10000;
            if (prevFrameTime != 0)
            {
                timeDifferenceBetweenFrames = now - prevFrameTime;
                //  timeDifferenceBetweenFrames = Math.Min(timeDifferenceBetweenFrames, 10);
            }
            prevFrameTime = now;
            Direction prevDirection = MainPlayer.Direction;
            if (MainPlayer.RequestedDir != Direction.NO_DIRECTION)
            {
                MainPlayer.Direction = MainPlayer.RequestedDir;
                if (WillCollideWall(MainPlayer))
                {
                    MainPlayer.Direction = prevDirection;
                }
                else
                {
                    MainPlayer.RequestedDir = Direction.NO_DIRECTION;
                }
            }
            foreach(Enemy elem in Enemies)
            {

            }
            if (_pathfinder != null)
            {
                foreach (Enemy elem in Enemies)
                {
                    var startNode = new AStarNode((int)elem.PositionX, (int)elem.PositionY);
                    var endNode = new AStarNode((int)MainPlayer.PositionX, (int)MainPlayer.PositionY);
                    var path = _pathfinder.FindPath(startNode, endNode);
                    if (path != null && path.Count > 1)
                    {
                        var nextNode = path[1];
                        double dx = nextNode.X - elem.PositionX;
                        double dy = nextNode.Y - elem.PositionY;

                        Direction nextEnemyDirection;

                        if (Math.Abs(dx) > Math.Abs(dy))
                        {
                            // Move horizontally
                            if (dx > 0)
                            {
                                nextEnemyDirection = Direction.RIGHT;
                            }
                            else
                            {
                                nextEnemyDirection = Direction.LEFT;
                            }
                        }
                        else
                        {
                            // Move vertically
                            if (dy > 0)
                            {
                                nextEnemyDirection = Direction.DOWN;
                            }
                            else
                            {
                                nextEnemyDirection = Direction.UP;
                            }
                        }
                        ChangeEnemyDirection(elem, nextEnemyDirection);
                    }
                }
            }
            foreach (MovableShape p in Movables)
            {
                p.Move(timeDifferenceBetweenFrames);
            }
            
            CollisionWithWallsDetection();

        }



        public void RemovePellet(Pellet pellet)
        {
            //if (_pellet == pellet)
            //{
            //    _pellet = null;
            //    GameElements.Remove(pellet);
            //}
        }

        internal void CollisionWithWallsDetection()
        {
            _collisionDetector.CheckCollision(Map.Obstacles, MainPlayer);
            foreach (Enemy elem in Enemies)
            {
                _collisionDetector.CheckEnemyCollision(Map.Obstacles, elem);

            }

        }


        internal void ReadFromFile(string file)
        {
            Map.ReadFromFile(file);
            foreach (var elem in Map.Obstacles)
            {
                Drawables.Add(elem);
            }
            foreach (var elem in Map.Enemies)
            {
                Drawables.Add(elem);
                Movables.Add(elem);
                this.Enemies.Add(elem);
            }

            MainPlayer = Map.MainPlayer;
            Drawables.Add(MainPlayer);
            Movables.Add(MainPlayer);

            _pathfinder = new AStarPathfinder(Map);
        }

        internal bool WillCollideWall(Player player)
        {
            player.Move(50.0f);
            bool isCollided = _collisionDetector.CheckCollision(this.Map.Obstacles, player);
            if (!isCollided)
            {
                player.RevertPosition();
            }
            return isCollided;
        }
        private void ChangeEnemyDirection(Enemy elem, Direction nextDirection)
        {
            Direction prevEnemyDirection = elem.Direction;
            elem.RequestedDir = nextDirection;
            if (elem.RequestedDir != Direction.NO_DIRECTION)
            {
                elem.Direction = elem.RequestedDir;
                if (WillEnemyCollideWall(elem))
                {
                    elem.RequestedDir = elem.Direction;
                    elem.Direction = prevEnemyDirection;
                }
                else
                {
                    elem.RequestedDir = Direction.NO_DIRECTION;
                }
            }
              
        }
        internal bool WillEnemyCollideWall(Enemy elem)
        {
            elem.Move(50.0f);
            bool isCollided = _collisionDetector.CheckEnemyCollision(this.Map.Obstacles, elem);
            if (!isCollided)
            {
                elem.RevertPosition();
            }
            return isCollided;
        }


    }
}
