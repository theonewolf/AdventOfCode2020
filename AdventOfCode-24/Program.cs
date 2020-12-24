using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace AdventOfCode_24
{
    class Hexagon
    {
        public int x;
        public int y;
        public int z;

        public Hexagon(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override bool Equals(object obj)
        {
            return obj is Hexagon hexagon &&
                   this.x == hexagon.x &&
                   this.y == hexagon.y &&
                   this.z == hexagon.z;
        }

        public static Hexagon operator +(Hexagon a, Hexagon b)
        => new Hexagon(a.x + b.x, a.y + b.y, a.z + b.z);

        public override int GetHashCode()
        {
            return x << 24 | y << 16 | z << 8 | (x >> 24 | y >> 24 | z >> 24);
        }

        public override string ToString()
        {
            return $"Hexagon[{x},{y},{z}]";
        }
    }

    class Program
    {

        static void part1()
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-24\input-24.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            List<List<int>> directions = new List<List<int>>();
            Dictionary<Hexagon, int> flips = new Dictionary<Hexagon, int>();

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            string line;

            while ((line = file.ReadLine()) != null)
            {
                List<int> current_directions = new List<int>();

                while (!string.IsNullOrEmpty(line))
                {
                    if (line.StartsWith("e"))
                    {
                        current_directions.Add(0);
                        line = line.Substring(1);
                    }
                    else if (line.StartsWith("ne"))
                    {
                        current_directions.Add(1);
                        line = line.Substring(2);
                    }
                    else if (line.StartsWith("nw"))
                    {
                        current_directions.Add(2);
                        line = line.Substring(2);
                    }
                    else if (line.StartsWith("w"))
                    {
                        current_directions.Add(3);
                        line = line.Substring(1);
                    }
                    else if (line.StartsWith("sw"))
                    {
                        current_directions.Add(4);
                        line = line.Substring(2);
                    }
                    else if (line.StartsWith("se"))
                    {
                        current_directions.Add(5);
                        line = line.Substring(2);
                    }
                }

                directions.Add(current_directions);
            }

            // 0 = E, 1 = NE, 2 = NW, 3 = W, 4 = SW, 5 = SE
            // Inspiration: https://www.redblobgames.com/grids/hexagons/#neighbors
            Hexagon[] cube_directions = new Hexagon[]  { new Hexagon(+1, -1, 0), // E
                                                         new Hexagon(+1, 0, -1), // NE
                                                         new Hexagon(0, +1, -1), // NW
                                                         new Hexagon(-1, +1, 0), // W
                                                         new Hexagon(-1, 0, +1), // SW
                                                         new Hexagon(0, -1, +1 ) // SE
            };

            foreach (List<int> current_directions in directions)
            {
                Hexagon current_hex = new Hexagon(0,0,0);
                foreach (int direction in current_directions)
                {
                    current_hex += cube_directions[direction];
                }

                if (flips.ContainsKey(current_hex))
                {
                    flips[current_hex] = flips[current_hex] == 0 ? 1 : 0;
                }
                else
                {
                    flips.Add(current_hex, 0);
                }
            }

            foreach (Hexagon hex in flips.Keys)
            {
                Console.WriteLine($"{hex}: {flips[hex]}");
            }

            Console.WriteLine($"Black tiles: {flips.Where(x => x.Value == 0).Count()}");
        }

        static void part2()
        {
        }

        static void Main(string[] args)
        {
            part1();
            part2();
        }
    }
}
