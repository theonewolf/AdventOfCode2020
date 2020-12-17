using System;
using System.Collections.Generic;

namespace AdventOfCode_17
{
    class Program
    {

        static void print_cube3d(List<List<List<char>>> cube)
        {
            int negative_layers = (cube.Count - 1) / 2;
            Console.WriteLine("---- Cube ----");
            for (int z = 0; z < cube.Count; z++)
            {
                Console.WriteLine($"z={z - negative_layers}");
                for (int y = 0; y < cube[z].Count; y++)
                {
                    for (int x = 0; x < cube[z][y].Count; x++)
                    {
                        Console.Write($"{cube[z][y][x]}");
                    }
                    Console.Write("\n");
                }
            }
        }

        static void print_cube4d(List<List<List<List<char>>>> cube)
        {
            int negative_layers = (cube.Count - 1) / 2;
            Console.WriteLine("---- Cube ----");
            for (int w = 0; w < cube.Count; w++)
            {
                for (int z = 0; z < cube[w].Count; z++)
                {
                    Console.WriteLine($"z={z - negative_layers}, w={w - negative_layers}");
                    for (int y = 0; y < cube[w][z].Count; y++)
                    {
                        for (int x = 0; x < cube[w][z][y].Count; x++)
                        {
                            Console.Write($"{cube[w][z][y][x]}");
                        }
                        Console.Write("\n");
                    }
                }
            }
        }

        static List<List<List<char>>> create_cube3d(List<List<List<char>>> old_cube)
        {
            int z_max = old_cube.Count + 2;
            int y_max = old_cube[0].Count + 2;
            int x_max = old_cube[0][0].Count + 2;
            List<List<List<char>>> new_cube = new List<List<List<char>>>(z_max);
            
            for (int z = 0; z < z_max; z++)
            {
                new_cube.Add(new List<List<char>>(y_max));
                for (int y = 0; y < y_max; y++)
                {
                    new_cube[z].Add(new List<char>(x_max));
                    for (int x = 0; x < x_max; x++)
                    {
                        new_cube[z][y].Add('.');
                    }
                }
            }

            for (int z = 0; z < old_cube.Count; z++)
            {
                for (int y = 0; y < old_cube[z].Count; y++)
                {
                    for (int x = 0; x < old_cube[z][y].Count; x++)
                    {
                        new_cube[z + 1][y + 1][x + 1] = old_cube[z][y][x];
                    }
                }
            }

            return new_cube;
        }

        static List<List<List<List<char>>>> create_cube4d(List<List<List<List<char>>>> old_cube)
        {
            int w_max = old_cube.Count + 2;
            int z_max = old_cube[0].Count + 2;
            int y_max = old_cube[0][0].Count + 2;
            int x_max = old_cube[0][0][0].Count + 2;
            List<List<List<List<char>>>> new_cube = new List<List<List<List<char>>>>(w_max);

            for (int w = 0; w < w_max; w++)
            {
                new_cube.Add(new List<List<List<char>>>());
                for (int z = 0; z < z_max; z++)
                {
                    new_cube[w].Add(new List<List<char>>(y_max));
                    for (int y = 0; y < y_max; y++)
                    {
                        new_cube[w][z].Add(new List<char>(x_max));
                        for (int x = 0; x < x_max; x++)
                        {
                            new_cube[w][z][y].Add('.');
                        }
                    }
                }
            }

            for (int w = 0; w < old_cube.Count; w++)
            {
                for (int z = 0; z < old_cube[w].Count; z++)
                {
                    for (int y = 0; y < old_cube[w][z].Count; y++)
                    {
                        for (int x = 0; x < old_cube[w][z][y].Count; x++)
                        {
                            new_cube[w + 1][z + 1][y + 1][x + 1] = old_cube[w][z][y][x];
                        }
                    }
                }
            }

            return new_cube;
        }

        static List<List<List<char>>> clone_cube3d(List<List<List<char>>> old_cube)
        {
            List<List<List<char>>> new_cube = new List<List<List<char>>>();

            for (int z = 0; z < old_cube.Count; z++)
            {
                new_cube.Add(new List<List<char>>());
                for (int y = 0; y < old_cube[z].Count; y++)
                {
                    new_cube[z].Add(new List<char>());
                    for (int x = 0; x < old_cube[z][y].Count; x++)
                    {
                        new_cube[z][y].Add(old_cube[z][y][x]);
                    }
                }
            }

            return new_cube;
        }

        static int count_active_neighbors3d(int z_coord, int y_coord, int x_coord, List<List<List<char>>> cube)
        {
            int count = 0;
            int[] choices = { -1, 0, 1 };

            foreach (int z in choices)
            {
                foreach (int y in choices)
                {
                    foreach (int x in choices)
                    {
                        int target_z = z_coord + z;
                        int target_y = y_coord + y;
                        int target_x = x_coord + x;

                        if (target_z < 0 || target_y < 0 || target_x < 0)
                        {
                            // Off grid are inactive anyways
                            continue;
                        }

                        if (target_z >= cube.Count || target_y >= cube[target_z].Count || target_x >= cube[target_z][target_y].Count)
                        {
                            // Off grid are inactive anyways
                            continue;
                        }

                        if (target_z == z_coord && target_y == y_coord && target_x == x_coord)
                        {
                            continue;
                        }

                        if (cube[target_z][target_y][target_x] == '#')
                        {
                            count++;
                        }

                    }
                }
            }

            return count;
        }

        static List<List<List<List<char>>>> clone_cube4d(List<List<List<List<char>>>> old_cube)
        {
            List<List<List<List<char>>>> new_cube = new List<List<List<List<char>>>>();

            for (int w = 0; w < old_cube.Count; w++)
            {
                new_cube.Add(new List<List<List<char>>>());
                for (int z = 0; z < old_cube[w].Count; z++)
                {
                    new_cube[w].Add(new List<List<char>>());
                    for (int y = 0; y < old_cube[w][z].Count; y++)
                    {
                        new_cube[w][z].Add(new List<char>());
                        for (int x = 0; x < old_cube[w][z][y].Count; x++)
                        {
                            new_cube[w][z][y].Add(old_cube[w][z][y][x]);
                        }
                    }
                }
            }

            return new_cube;
        }

        static int count_active_neighbors4d(int w_coord, int z_coord, int y_coord, int x_coord, List<List<List<List<char>>>> cube)
        {
            int count = 0;
            int[] choices = { -1, 0, 1 };

            foreach (int w in choices)
            {
                foreach (int z in choices)
                {
                    foreach (int y in choices)
                    {
                        foreach (int x in choices)
                        {
                            int target_w = w_coord + w;
                            int target_z = z_coord + z;
                            int target_y = y_coord + y;
                            int target_x = x_coord + x;

                            if (target_w < 0 || target_z < 0 || target_y < 0 || target_x < 0)
                            {
                                // Off grid are inactive anyways
                                continue;
                            }

                            if (target_w >= cube.Count || target_z >= cube[target_w].Count || target_y >= cube[target_w][target_z].Count || target_x >= cube[target_w][target_z][target_y].Count)
                            {
                                // Off grid are inactive anyways
                                continue;
                            }

                            if (target_w == w_coord && target_z == z_coord && target_y == y_coord && target_x == x_coord)
                            {
                                continue;
                            }

                            if (cube[target_w][target_z][target_y][target_x] == '#')
                            {
                                count++;
                            }

                        }
                    }
                }
            }

            return count;
        }

        static List<List<List<char>>> update_cube3d(List<List<List<char>>> old_cube)
        {
            List<List<List<char>>> new_cube = create_cube3d(old_cube);
            List<List<List<char>>> new_cube_ro_clone = clone_cube3d(new_cube);

            for (int z = 0; z < new_cube.Count; z++)
            {
                for (int y = 0; y < new_cube[z].Count; y++)
                {
                    for (int x = 0; x < new_cube[z][y].Count; x++)
                    {
                        int active_count = count_active_neighbors3d(z, y, x, new_cube_ro_clone);
                        switch (new_cube_ro_clone[z][y][x])
                        {
                            case '#':
                                if (active_count != 2 && active_count != 3)
                                {
                                    new_cube[z][y][x] = '.';
                                }
                                break;
                            case '.':
                                if (active_count == 3)
                                {
                                    new_cube[z][y][x] = '#';
                                }
                                break;
                            default:
                                throw new Exception();
                        }
                    }
                }
            }

            return new_cube;
        }

        static List<List<List<List<char>>>> update_cube4d(List<List<List<List<char>>>> old_cube)
        {
            List<List<List<List<char>>>> new_cube = create_cube4d(old_cube);
            List<List<List<List<char>>>> new_cube_ro_clone = clone_cube4d(new_cube);

            for (int w = 0; w < new_cube.Count; w++)
            {
                for (int z = 0; z < new_cube[w].Count; z++)
                {
                    for (int y = 0; y < new_cube[w][z].Count; y++)
                    {
                        for (int x = 0; x < new_cube[w][z][y].Count; x++)
                        {
                            int active_count = count_active_neighbors4d(w, z, y, x, new_cube_ro_clone);
                            switch (new_cube_ro_clone[w][z][y][x])
                            {
                                case '#':
                                    if (active_count != 2 && active_count != 3)
                                    {
                                        new_cube[w][z][y][x] = '.';
                                    }
                                    break;
                                case '.':
                                    if (active_count == 3)
                                    {
                                        new_cube[w][z][y][x] = '#';
                                    }
                                    break;
                                default:
                                    throw new Exception();
                            }
                        }
                    }
                }
            }

            return new_cube;
        }

        static int count_all_active3d(List<List<List<char>>> cube)
        {
            int count = 0;
            for (int z = 0; z < cube.Count; z++)
            {
                for (int y = 0; y < cube[z].Count; y++)
                {
                    for (int x = 0; x < cube[z][y].Count; x++)
                    {
                        if (cube[z][y][x] == '#')
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        static int count_all_active4d(List<List<List<List<char>>>> cube)
        {
            int count = 0;
            for (int w = 0; w < cube.Count; w++) {
                for (int z = 0; z < cube[w].Count; z++)
                {
                    for (int y = 0; y < cube[w][z].Count; y++)
                    {
                        for (int x = 0; x < cube[w][z][y].Count; x++)
                        {
                            if (cube[w][z][y][x] == '#')
                            {
                                count++;
                            }
                        }
                    }
                }
            }

            return count;
        }

        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-17\input-17.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            List<List<List<char>>> conway_cube = new List<List<List<char>>>();
            List<List<List<List<char>>>> conway_cube4d = new List<List<List<List<char>>>>();

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            string data = file.ReadToEnd();
            string[] lines = data.Split();

            conway_cube.Add(new List<List<char>>());
            conway_cube4d.Add(new List<List<List<char>>>());
            conway_cube4d[0].Add(new List<List<char>>());

            int skipped = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    skipped += 1;
                    continue;
                }

                conway_cube[0].Add(new List<char>());
                conway_cube4d[0][0].Add(new List<char>());

                foreach (char c in lines[i])
                {
                    if (!char.IsWhiteSpace(c))
                    {
                        conway_cube[0][i - skipped].Add(c);
                        conway_cube4d[0][0][i - skipped].Add(c);
                    }
                }
            }

            print_cube3d(conway_cube);
            Console.WriteLine("--- Cycle 1 ---");
            conway_cube = update_cube3d(conway_cube);
            print_cube3d(conway_cube);
            Console.WriteLine("--- Cycle 2 ---");
            conway_cube = update_cube3d(conway_cube);
            print_cube3d(conway_cube);
            Console.WriteLine("--- Cycle 3 ---");
            conway_cube = update_cube3d(conway_cube);
            print_cube3d(conway_cube);
            Console.WriteLine("--- Cycle 4 ---");
            conway_cube = update_cube3d(conway_cube);
            print_cube3d(conway_cube);
            Console.WriteLine("--- Cycle 5 ---");
            conway_cube = update_cube3d(conway_cube);
            print_cube3d(conway_cube);
            Console.WriteLine("--- Cycle 6 ---");
            conway_cube = update_cube3d(conway_cube);
            print_cube3d(conway_cube);

            Console.WriteLine($"Total Active Count: {count_all_active3d(conway_cube)}");


            print_cube4d(conway_cube4d);
            Console.WriteLine("--- Cycle 1 ---");
            conway_cube4d = update_cube4d(conway_cube4d);
            print_cube4d(conway_cube4d);
            Console.WriteLine("--- Cycle 2 ---");
            conway_cube4d = update_cube4d(conway_cube4d);
            print_cube4d(conway_cube4d);
            Console.WriteLine("--- Cycle 3 ---");
            conway_cube4d = update_cube4d(conway_cube4d);
            print_cube4d(conway_cube4d);
            Console.WriteLine("--- Cycle 4 ---");
            conway_cube4d = update_cube4d(conway_cube4d);
            print_cube4d(conway_cube4d);
            Console.WriteLine("--- Cycle 5 ---");
            conway_cube4d = update_cube4d(conway_cube4d);
            print_cube4d(conway_cube4d);
            Console.WriteLine("--- Cycle 6 ---");
            conway_cube4d = update_cube4d(conway_cube4d);
            print_cube4d(conway_cube4d);

            Console.WriteLine($"Total Active Count: {count_all_active4d(conway_cube4d)}");
        }
    }
}
