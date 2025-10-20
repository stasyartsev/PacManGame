using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PacMan
{
    public class Map
    {

        private static Map _Instance = null;
        public List<Obstacle> Obstacles;
        public List<Pellet> Pellets;
        public List<Enemy> Enemies;
        public Player MainPlayer { get; private set; }
        private Map()
        {
            
            Obstacles = new List<Obstacle>();
            Pellets = new List<Pellet>();
            Enemies = new List<Enemy>();
            MainPlayer = new Player();
            
        }
        public static Map GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new Map();
            }

            return _Instance;
        }
        public void ReadFromFile(string name)
        {
            string[] lines = File.ReadAllLines(name);
            foreach (string line in lines)
            {
                Player player = null;
                Obstacle wall = null;
                Enemy enemy = null;
                var nameValueList = LineToList(line);
                foreach (NameValue curNameVal in nameValueList)
                {
                    if (curNameVal.Name == "type")
                    {
                        switch (curNameVal.Value)
                        {
                            case "Obstacle":
                                wall = new Obstacle();
                                break;
                            case "Player":
                                player = new Player();

                                break;
                            case "Enemy":
                                enemy = new Enemy();

                                break;
                            //case "square":
                            //    shape = new Square();
                            //    break;
                        }

                        if (wall != null)
                        {
                            wall.Deserialize(nameValueList);
                            Obstacles.Add(wall);
                        }
                        if (player!= null)
                        {
                            player.Deserialize(nameValueList);
                            MainPlayer = player;
                        }
                        if (enemy != null)
                        {
                            enemy.Deserialize(nameValueList);
                            Enemies.Add(enemy);
                        }
                    }
                }
            }
        }

        internal List<NameValue> LineToList(string line)
        {
            List<NameValue> lines = new List<NameValue>();
            var pares = line.Split(new char[] { ';' });
            foreach (var pare in pares)
            {
                var value = pare.Split(new char[] { ':' });
                if (value[0] == "type")
                {
                    lines.Add(new NameValue(value[0], value[1]));
                }
                if (value[0] == " X")
                {
                    lines.Add(new NameValue(value[0], value[1]));
                }
                if (value[0] == " Y")
                {
                    lines.Add(new NameValue(value[0], value[1]));
                }
            }
                return lines;
            }

        }

    }

