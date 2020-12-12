using System;
using System.Collections.Generic;

namespace AdventOfCode_11
{
    class Program
    {
        static void print_grid(List<List<char>> grid)
        {
            foreach (List<char> row in grid)
            {
                foreach (char cell in row)
                {
                    Console.Write(cell);
                }
                Console.Write('\n');
            }
        }

        static List<List<char>> copy_grid(List<List<char>> grid)
        {
            List<List<char>> copy = new List<List<char>>();
            foreach (List<char> row in grid)
            {
                copy.Add(new List<char>());
                foreach (char cell in row)
                {
                    copy[copy.Count - 1].Add(cell);
                }
            }

            return copy;
        }

        static int count_occupied(List<List<char>> grid)
        {
            int count = 0;

            foreach(List<char> row in grid)
            {
                foreach(char cell in row)
                {
                    if (cell == '#')
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        static void first_rules()
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-11\input-11.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            List<List<char>> grid = new List<List<char>>();


            string line;

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            while ((line = file.ReadLine()) != null)
            {
                List<char> charlist = new List<char>();
                grid.Add(charlist);
                foreach (char c in line)
                {
                    charlist.Add(c);
                }
            }

            // Apply rules
            bool changed = true;
            int minX = 0, minY = 0, maxY = grid.Count, maxX = grid[0].Count;

            print_grid(grid);

            while (changed)
            {
                List<List<char>> newgrid = copy_grid(grid);
                changed = false;
                for (int x = 0; x < maxX; x++)
                {
                    for (int y = 0; y < maxY; y++)
                    {
                        if (grid[y][x] == 'L') // empty
                        {
                            // top left
                            if (y == 0 && x == 0)
                            {
                                if (grid[y + 1][x] != '#' && grid[y + 1][x + 1] != '#' && grid[y][x + 1] != '#')
                                {
                                    newgrid[y][x] = '#';
                                    changed = true;
                                }
                            }
                            //top right
                            else if (y == 0 && x == maxX - 1)
                            {
                                if (grid[y + 1][x - 1] != '#' && grid[y + 1][x] != '#' &&
                                    grid[y][x - 1] != '#')
                                {
                                    newgrid[y][x] = '#';
                                    changed = true;
                                }
                            }
                            // left-most column
                            else if (x == 0 && y != maxY - 1)
                            {
                                if (grid[y - 1][x] != '#' && grid[y - 1][x + 1] != '#' &&
                                    grid[y + 1][x] != '#' && grid[y + 1][x + 1] != '#' &&
                                    grid[y][x + 1] != '#')
                                {
                                    newgrid[y][x] = '#';
                                    changed = true;
                                }
                            }
                            // first row
                            else if (y == 0 && x != maxX - 1)
                            {
                                if (grid[y + 1][x - 1] != '#' && grid[y + 1][x] != '#' && grid[y + 1][x + 1] != '#' &&
                                    grid[y][x - 1] != '#' && grid[y][x + 1] != '#')
                                {
                                    newgrid[y][x] = '#';
                                    changed = true;
                                }
                            }
                            // bottom right
                            else if (x == maxX - 1 && y == maxY - 1)
                            {
                                if (grid[y - 1][x - 1] != '#' && grid[y - 1][x] != '#' && grid[y][x - 1] != '#')
                                {
                                    newgrid[y][x] = '#';
                                    changed = true;
                                }
                            }
                            // right most column
                            else if (x == maxX - 1 && y < maxY - 1)
                            {
                                if (grid[y - 1][x - 1] != '#' && grid[y - 1][x] != '#' &&
                                    grid[y + 1][x - 1] != '#' && grid[y + 1][x] != '#' &&
                                    grid[y][x - 1] != '#')
                                {
                                    newgrid[y][x] = '#';
                                    changed = true;
                                }
                            }
                            // bottom left
                            else if (y == maxY - 1 && x == 0)
                            {
                                if (grid[y - 1][x] != '#' && grid[y - 1][x + 1] != '#' &&
                                    grid[y][x + 1] != '#')
                                {
                                    newgrid[y][x] = '#';
                                    changed = true;
                                }
                            }
                            // bottom row
                            else if (y == maxY - 1 && x > 0 && x < maxX - 1)
                            {
                                if (grid[y - 1][x - 1] != '#' && grid[y - 1][x] != '#' && grid[y - 1][x + 1] != '#' &&
                                    grid[y][x - 1] != '#' && grid[y][x + 1] != '#')
                                {
                                    newgrid[y][x] = '#';
                                    changed = true;
                                }
                            }
                            // middle
                            else
                            {
                                if (grid[y - 1][x - 1] != '#' && grid[y - 1][x] != '#' && grid[y - 1][x + 1] != '#' &&
                                    grid[y + 1][x - 1] != '#' && grid[y + 1][x] != '#' && grid[y + 1][x + 1] != '#' &&
                                    grid[y][x - 1] != '#' && grid[y][x + 1] != '#')
                                {
                                    newgrid[y][x] = '#';
                                    changed = true;
                                }
                            }
                        }
                        else if ((grid[y][x]) == '#')
                        { // occupied, become L if 4 or more around are occupied
                            int occupied = 0;
                            // top left
                            if (y == 0 && x == 0)
                            {
                                // Can never become empty again?  Only 3 adjacent seats.
                            }
                            //top right
                            else if (y == 0 && x == maxX - 1)
                            {
                                // Can never become empty again?  Only 3 adjacent seats.
                            }
                            // left-most column
                            else if (x == 0 && y != maxY - 1)
                            {
                                if (grid[y - 1][x] == '#') occupied++;
                                if (grid[y - 1][x + 1] == '#') occupied++;
                                if (grid[y + 1][x] == '#') occupied++;
                                if (grid[y + 1][x + 1] == '#') occupied++;
                                if (grid[y][x + 1] == '#') occupied++;

                                if (occupied >= 4)
                                {
                                    newgrid[y][x] = 'L';
                                    changed = true;
                                }
                            }
                            // first row
                            else if (y == 0 && x != maxX - 1)
                            {
                                if (grid[y + 1][x - 1] == '#') occupied++;
                                if (grid[y + 1][x] == '#') occupied++;
                                if (grid[y + 1][x + 1] == '#') occupied++;
                                if (grid[y][x - 1] == '#') occupied++;
                                if (grid[y][x + 1] == '#') occupied++;

                                if (occupied >= 4)
                                {
                                    newgrid[y][x] = 'L';
                                    changed = true;
                                }
                            }
                            // bottom right
                            else if (x == maxX - 1 && y == maxY - 1)
                            {
                                // Can never become empty again?  Only 3 adjacent seats.
                            }
                            // right most column
                            else if (x == maxX - 1 && y < maxY - 1)
                            {
                                if (grid[y - 1][x - 1] == '#') occupied++;
                                if (grid[y - 1][x] == '#') occupied++;
                                if (grid[y + 1][x - 1] == '#') occupied++;
                                if (grid[y + 1][x] == '#') occupied++;
                                if (grid[y][x - 1] == '#') occupied++;

                                if (occupied >= 4)
                                {
                                    newgrid[y][x] = 'L';
                                    changed = true;
                                }
                            }
                            // bottom left
                            else if (y == maxY - 1 && x == 0)
                            {
                                // Can never become empty again?  Only 3 adjacent seats.
                            }
                            // bottom row
                            else if (y == maxY - 1 && x > 0 && x < maxX - 1)
                            {
                                if (grid[y - 1][x - 1] == '#') occupied++;
                                if (grid[y - 1][x] == '#') occupied++;
                                if (grid[y - 1][x + 1] == '#') occupied++;
                                if (grid[y][x - 1] == '#') occupied++;
                                if (grid[y][x + 1] == '#') occupied++;

                                if (occupied >= 4)
                                {
                                    newgrid[y][x] = 'L';
                                    changed = true;
                                }
                            }
                            // middle
                            else
                            {
                                if (grid[y - 1][x - 1] == '#') occupied++;
                                if (grid[y - 1][x] == '#') occupied++;
                                if (grid[y - 1][x + 1] == '#') occupied++;
                                if (grid[y + 1][x - 1] == '#') occupied++;
                                if (grid[y + 1][x] == '#') occupied++;
                                if (grid[y + 1][x + 1] == '#') occupied++;
                                if (grid[y][x - 1] == '#') occupied++;
                                if (grid[y][x + 1] == '#') occupied++;

                                if (occupied >= 4)
                                {
                                    newgrid[y][x] = 'L';
                                    changed = true;
                                }
                            }
                        }
                    }

                }
                Console.WriteLine("-------------");
                grid = newgrid;
                print_grid(grid);
            }
            Console.WriteLine($"Occupied after final round: {count_occupied(grid)}");
        }

        static bool check_north(int x, int y, List<List<char>> grid)
        {
            for (int i = y - 1; i >= 0; i--)
            {
                if (grid[i][x] == '#') return true;
                if (grid[i][x] == 'L') return false;
            }
            return false;
        }

        static bool check_south(int x, int y, List<List<char>> grid)
        {
            for (int i = y + 1; i < grid.Count; i++)
            {
                if (grid[i][x] == '#') return true;
                if (grid[i][x] == 'L') return false;
            }
            return false;
        }

        static bool check_west(int x, int y, List<List<char>> grid)
        {
            for (int i = x - 1; i >= 0; i--)
            {
                if (grid[y][i] == '#') return true;
                if (grid[y][i] == 'L') return false;
            }
            return false;
        }

        static bool check_east(int x, int y, List<List<char>> grid)
        {
            for (int i = x + 1; i < grid[0].Count; i++)
            {
                if (grid[y][i] == '#') return true;
                if (grid[y][i] == 'L') return false;
            }
            return false;
        }

        static bool check_northwest(int x, int y, List<List<char>> grid)
        {
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0;)
            {
                if (grid[j][i] == '#') return true;
                if (grid[j][i] == 'L') return false;
                i--;
                j--;
            }
            return false;
        }

        //bad
        static bool check_northeast(int x, int y, List<List<char>> grid)
        {
            for (int i = x + 1, j = y - 1; i < grid[0].Count && j >= 0;)
            {
                if (grid[j][i] == '#') return true;
                if (grid[j][i] == 'L') return false;
                i++;
                j--;
            }
            return false;
        }

        static bool check_southeast(int x, int y, List<List<char>> grid)
        {
            for (int i = x + 1, j = y + 1; i < grid[0].Count && j < grid.Count;)
            {
                if (grid[j][i] == '#') return true;
                if (grid[j][i] == 'L') return false;
                i++;
                j++;
            }
            return false;
        }

        //bad
        static bool check_southwest(int x, int y, List<List<char>> grid)
        {
            for (int i = x - 1, j = y + 1; i >= 0 && j < grid.Count;)
            {
                if (grid[j][i] == '#') return true;
                if (grid[j][i] == 'L') return false;
                i--;
                j++;
            }
            return false;
        }

        static void second_rules()
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-11\input-11.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            List<List<char>> grid = new List<List<char>>();


            string line;

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            while ((line = file.ReadLine()) != null)
            {
                List<char> charlist = new List<char>();
                grid.Add(charlist);
                foreach (char c in line)
                {
                    charlist.Add(c);
                }
            }

            // Apply rules
            bool changed = true;
            int minX = 0, minY = 0, maxY = grid.Count, maxX = grid[0].Count;

            print_grid(grid);

            while (changed)
            {
                List<List<char>> newgrid = copy_grid(grid);
                changed = false;
                for (int x = 0; x < maxX; x++)
                {
                    for (int y = 0; y < maxY; y++)
                    {
                        int occupied = 0;
                        if (check_north(x, y, grid)) occupied++;
                        if (check_northeast(x, y, grid)) occupied++;
                        if (check_east(x, y, grid)) occupied++;
                        if (check_southeast(x, y, grid)) occupied++;
                        if (check_south(x, y, grid)) occupied++;
                        if (check_southwest(x, y, grid)) occupied++;
                        if (check_west(x, y, grid)) occupied++;
                        if (check_northwest(x, y, grid)) occupied++;

                        if (grid[y][x] == 'L') // empty
                        {
                            if (occupied == 0)
                            {
                                newgrid[y][x] = '#';
                                changed = true;
                            }
                        }
                        else if ((grid[y][x]) == '#')
                        {
                            if (occupied >= 5)
                            {
                                newgrid[y][x] = 'L';
                                changed = true;
                            }
                        }
                    }
                }
                Console.WriteLine("-------------");
                grid = newgrid;
                print_grid(grid);
            }
            Console.WriteLine($"Occupied after final round: {count_occupied(grid)}");
        }

        static void Main(string[] args)
        {
            //first_rules();
            second_rules();
        }
    }
}
