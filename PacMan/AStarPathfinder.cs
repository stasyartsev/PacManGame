using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    public class AStarPathfinder
    {
        private readonly bool[,] _walkable;
        private readonly int _width;
        private readonly int _height;

        public AStarPathfinder(Map map)
        {
            _width = map.Obstacles.Max(o => (int)o.PositionX + o.Size);
            _height = map.Obstacles.Max(o => (int)o.PositionY + o.Size);
            _walkable = new bool[_width, _height];
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _walkable[x, y] = true;
                }
            }

            foreach (var obstacle in map.Obstacles)
            {
                for (int y = (int)obstacle.PositionY; y < (int)obstacle.PositionY + obstacle.Size; y++)
                {
                    for (int x = (int)obstacle.PositionX; x < (int)obstacle.PositionX + obstacle.Size; x++)
                    {
                        if (x >= 0 && x < _width && y >= 0 && y < _height)
                        {
                            _walkable[x, y] = false;
                        }
                    }
                }
            }
        }

        public List<AStarNode> FindPath(AStarNode start, AStarNode end)
        {
            var openList = new List<AStarNode>();
            var closedList = new List<AStarNode>();
            openList.Add(start);

            while (openList.Count > 0)
            {
                var currentNode = openList.OrderBy(n => n.F).First();

                if (currentNode.X == end.X && currentNode.Y == end.Y)
                {
                    return ReconstructPath(currentNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (var neighbor in GetNeighbors(currentNode))
                {
                    if (closedList.Any(n => n.X == neighbor.X && n.Y == neighbor.Y))
                    {
                        continue;
                    }

                    if (!_walkable[neighbor.X, neighbor.Y])
                    {
                        continue;
                    }

                    var newG = currentNode.G + 1;
                    var existingNode = openList.FirstOrDefault(n => n.X == neighbor.X && n.Y == neighbor.Y);

                    if (existingNode == null)
                    {
                        neighbor.G = newG;
                        neighbor.H = Math.Abs(neighbor.X - end.X) + Math.Abs(neighbor.Y - end.Y);
                        neighbor.Parent = currentNode;
                        openList.Add(neighbor);
                    }
                    else if (newG < existingNode.G)
                    {
                        existingNode.G = newG;
                        existingNode.Parent = currentNode;
                    }
                }
            }

            return null; // No path found
        }

        private List<AStarNode> GetNeighbors(AStarNode node)
        {
            var neighbors = new List<AStarNode>();
            if (node.X > 0) neighbors.Add(new AStarNode(node.X - 1, node.Y));
            if (node.X < _width - 1) neighbors.Add(new AStarNode(node.X + 1, node.Y));
            if (node.Y > 0) neighbors.Add(new AStarNode(node.X, node.Y - 1));
            if (node.Y < _height - 1) neighbors.Add(new AStarNode(node.X, node.Y + 1));
            return neighbors;
        }

        private List<AStarNode> ReconstructPath(AStarNode node)
        {
            var path = new List<AStarNode>();
            while (node != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            path.Reverse();
            return path;
        }
    }
}
