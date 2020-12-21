using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode_20
{

    class Matrix
    {
        private char[][] matrix;
        public long id;
        public bool oriented;

        public Matrix(long tileid, string[] data)
        {
            id = tileid;
            matrix = new char[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                matrix[i] = new char[data[0].Length];
                for (int j = 0; j < data[0].Length; j++)
                {
                    matrix[i][j] = data[i][j];
                }
            }
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Matrix other = (Matrix) obj;
                return id == other.id;
            }
        }

        public override int GetHashCode()
        {
            return (int) id;
        }

        public char[] Left()
        {
            char[] left = new char[matrix.Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                left[i] = matrix[i][0];
            }

            return left;
        }

        public char[] Right()
        {
            char[] right = new char[matrix.Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                right[i] = matrix[i][matrix.Length - 1];
            }

            return right;
        }

        public char[] Top()
        {
            return (char[]) matrix[0].Clone();
        }

        public char[] Bottom()
        {
            return (char[]) matrix[matrix.Length - 1].Clone();
        }

        // Due to symmetry (the pieces are square), this whole function could be simplified to just compare all sides.
        public bool[] MatchSides(Matrix other)
        {
            // top, bottom, left, right
            bool[] ret = { false, false, false, false };

            // match top and bottoms
            char[] top = matrix[0];
            char[] bottom = matrix[matrix.Length - 1];
            char[] top_rotated = (char[]) bottom.Clone();
            char[] bottom_rotated = (char[]) top.Clone();
            Array.Reverse(top_rotated);
            Array.Reverse(bottom_rotated);

            char[] other_top = other.Top();
            char[] other_bottom = other.Bottom();
            char[] other_top_rotated = (char[]) other_bottom.Clone();
            char[] other_bottom_rotated = (char[]) other_top.Clone();
            Array.Reverse(other_top_rotated);
            Array.Reverse(other_bottom_rotated);


            // match left and right
            char[] left = Left();
            char[] right = Right();
            char[] left_rotated = (char[]) left.Clone();
            char[] right_rotated = (char[]) right.Clone();
            Array.Reverse(left_rotated);
            Array.Reverse(right_rotated);

            char[] other_left = other.Left();
            char[] other_right = other.Right();
            char[] other_left_rotated = (char[]) other_right.Clone();
            char[] other_right_rotated = (char[]) other_left.Clone();
            Array.Reverse(other_left_rotated);
            Array.Reverse(other_right_rotated);

            ret[0] = top.SequenceEqual(other_top) || top.SequenceEqual(other_top_rotated) ||
                     top.SequenceEqual(other_bottom) || top.SequenceEqual(other_bottom_rotated) ||
                     top.SequenceEqual(other_left) || top.SequenceEqual(other_left_rotated) ||
                     top.SequenceEqual(other_right) || top.SequenceEqual(other_right_rotated);

            ret[1] = bottom.SequenceEqual(other_top) || bottom.SequenceEqual(other_top_rotated) ||
                     bottom.SequenceEqual(other_bottom) || bottom.SequenceEqual(other_bottom_rotated) ||
                     bottom.SequenceEqual(other_left) || bottom.SequenceEqual(other_left_rotated) ||
                     bottom.SequenceEqual(other_right) || bottom.SequenceEqual(other_right_rotated);

            ret[2] = left.SequenceEqual(other_left) || left.SequenceEqual(other_left_rotated) ||
                     left.SequenceEqual(other_right) || left.SequenceEqual(other_right_rotated) ||
                     left.SequenceEqual(other_top) || left.SequenceEqual(other_top_rotated) ||
                     left.SequenceEqual(other_bottom) || left.SequenceEqual(other_bottom_rotated);

            ret[3] = right.SequenceEqual(other_right) || right.SequenceEqual(other_right_rotated) ||
                     right.SequenceEqual(other_left) || right.SequenceEqual(other_left_rotated) ||
                     right.SequenceEqual(other_top) || right.SequenceEqual(other_top_rotated) ||
                     right.SequenceEqual(other_bottom) || right.SequenceEqual(other_bottom_rotated);

            return ret;
        }

        public bool ExactMatched(Matrix other, int direction)
        {
            // top, bottom, left, right
            bool[] ret = { false, false, false, false };

            // match top and bottoms
            char[] top = matrix[0];
            char[] bottom = matrix[matrix.Length - 1];

            char[] other_top = other.Top();
            char[] other_bottom = other.Bottom();

            // match left and right
            char[] left = Left();
            char[] right = Right();

            char[] other_left = other.Left();
            char[] other_right = other.Right();

            ret[0] = top.SequenceEqual(other_bottom);
            ret[1] = bottom.SequenceEqual(other_top);
            ret[2] = left.SequenceEqual(other_right);
            ret[3] = right.SequenceEqual(other_left);

            return ret[direction];
        }

        public override string ToString()
        {
            string ret = "";

            ret += $"Tile {id}:\n";

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    ret += matrix[i][j];
                }
                ret += "\n";
            }

            return ret;
        }

        public List<string> SimpleStrings()
        {
            List<string> strings = new List<string>();
            for (int i = 0; i < matrix.Length; i++)
            {
                strings.Add("");
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    strings[i] += matrix[i][j];
                }
            }
            return strings;
        }

        public string SimpleString()
        {
            string retstr = "";
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    retstr += matrix[i][j];
                }
                retstr += "\n";
            }
            return retstr;
        }

        public Matrix ConvertToPart2()
        {
            string[] newdata = new string[matrix.Length - 2];

            for (int i = 1; i < matrix.Length - 1; i++)
            {
                newdata[i - 1] = "";
                for (int j = 1; j < matrix[i].Length - 1; j++)
                {
                    newdata[i - 1] += matrix[i][j];
                }
            }

            return new Matrix(id, newdata);
        }

        public static bool check_bool_arrays(bool[] a, bool[] b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static void Orient(Matrix a, Matrix b, bool[] goal_a, bool[] goal_b)
        {
            bool[] a_matchb = a.MatchSides(b);
            bool[] b_matcha = b.MatchSides(a);

            while (!check_bool_arrays(a_matchb, goal_a))
            {
                a.ClockwiseRotate();
                a_matchb = a.MatchSides(b);
            }

            while (!check_bool_arrays(b_matcha, goal_b))
            {
                b.ClockwiseRotate();
                b_matcha = b.MatchSides(a);
            }

            int i;
            for (i = 0; i < a_matchb.Length; i++)
            {
                if (a_matchb[i])
                {
                    break;
                }
            }

            int j;
            for (j = 0; j < b_matcha.Length; j++)
            {
                if (b_matcha[j])
                {
                    break;
                }
            }

            char[] a_side = null;
            switch(i)
            {
                case 0:
                    a_side = a.Top();
                    break;
                case 1:
                    a_side = a.Bottom();
                    break;
                case 2:
                    a_side = a.Left();
                    break;
                case 3:
                    a_side = a.Right();
                    break;
                default:
                    break;
            }

            char[] b_side = null;
            switch(j)
            {
                case 0:
                    b_side = b.Top();
                    if (!a_side.SequenceEqual(b_side))
                    {
                        b.HorizontalFlip();
                    }
                    b_side = b.Top();
                    if (!a_side.SequenceEqual(b_side))
                    {
                        throw new Exception();
                    }
                    break;
                case 1:
                    b_side = b.Bottom();
                    if (!a_side.SequenceEqual(b_side))
                    {
                        b.HorizontalFlip();
                    }
                    b_side = b.Bottom();
                    if (!a_side.SequenceEqual(b_side))
                    {
                        throw new Exception();
                    }
                    break;
                case 2:
                    b_side = b.Left();
                    if (!a_side.SequenceEqual(b_side))
                    {
                        b.VerticalFlip();
                    }
                    b_side = b.Left();
                    if (!a_side.SequenceEqual(b_side))
                    {
                        throw new Exception();
                    }
                    break;
                case 3:
                    b_side = b.Right();
                    if (!a_side.SequenceEqual(b_side))
                    {
                        b.VerticalFlip();
                    }
                    b_side = b.Right();
                    if (!a_side.SequenceEqual(b_side))
                    {
                        throw new Exception();
                    }
                    break;
                default:
                    break;
            }
        }

        // Equivalent to rotations
        public void VerticalFlip()
        {
            char[][] newMatrix = new char[matrix.Length][];

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (newMatrix[matrix.Length - 1 - i] == null)
                    {
                        newMatrix[matrix.Length - 1 - i] = new char[matrix[i].Length];
                    }
                    newMatrix[matrix.Length - 1 - i][j] = matrix[i][j];
                }
            }
            matrix = newMatrix;
        }

        // Real flip not equivalent to rotations
        public void HorizontalFlip()
        {
            foreach (char[] array in matrix)
            {
                Array.Reverse(array);
            }
        }

        public void ClockwiseRotate()
        {
            char[][] newMatrix = new char[matrix.Length][];

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    if (newMatrix[j] == null)
                    {
                        newMatrix[j] = new char[matrix[i].Length];
                    }
                    newMatrix[j][matrix[i].Length - 1 - i] = matrix[i][j];
                }
            }

            matrix = newMatrix;
        }

        public static void JointOrient(Matrix corner, Matrix bottom, Matrix right)
        {
            bool[] corner_match_bottom = corner.MatchSides(bottom);
            bool[] corner_match_right = corner.MatchSides(right);
            bool[] bottom_match_corner = bottom.MatchSides(corner);
            bool[] right_match_corner = right.MatchSides(corner);

            bool found = false;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        for (int x = 0; x < 4; x++)
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                for (int z = 0; z < 4; z++)
                                {
                                    if (corner.ExactMatched(bottom, 1) && corner.ExactMatched(right, 3))
                                    {
                                        found = true;
                                        break;
                                    }
                                    bottom.ClockwiseRotate();
                                }
                                if (found)
                                {
                                    break;
                                }
                                else
                                {
                                    right.ClockwiseRotate();
                                }
                            }
                            if (found)
                            {
                                break;
                            }
                            else
                            {
                                corner.ClockwiseRotate();
                            }
                        }
                        if (found)
                        {
                            break;
                        }
                        else
                        {
                            right.HorizontalFlip();
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                    else
                    {
                        bottom.HorizontalFlip();
                    }
                }
                if (found)
                {
                    break;
                }
                else
                {
                    corner.HorizontalFlip();
                }
            }

            if (!found)
            {
                throw new Exception();
            }
        }

        // TODO
        public static void JointOrientLast(Matrix corner, Matrix top, Matrix left)
        {
            bool[] corner_match_top = corner.MatchSides(top);
            bool[] corner_match_left = corner.MatchSides(left);
            bool[] top_match_corner = top.MatchSides(corner);
            bool[] left_match_corner = left.MatchSides(corner);

            bool found = false;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        for (int x = 0; x < 4; x++)
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                for (int z = 0; z < 4; z++)
                                {
                                    if (corner.ExactMatched(top, 0) && corner.ExactMatched(left, 2))
                                    {
                                        found = true;
                                        break;
                                    }
                                    top.ClockwiseRotate();
                                }
                                if (found)
                                {
                                    break;
                                }
                                else
                                {
                                    left.ClockwiseRotate();
                                }
                            }
                            if (found)
                            {
                                break;
                            }
                            else
                            {
                                corner.ClockwiseRotate();
                            }
                        }
                        if (found)
                        {
                            break;
                        }
                        else
                        {
                            left.HorizontalFlip();
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                    else
                    {
                        top.HorizontalFlip();
                    }
                }
                if (found)
                {
                    break;
                }
                else
                {
                    corner.HorizontalFlip();
                }
            }

            if (!found)
            {
                    throw new Exception();
            }
        }
    }

class Program
    {
        static void part1()
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-20\input-20-test.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            string line;

            Matrix piece;
            List<Matrix> pieces = new List<Matrix>();
            List<string> data = new List<string>();
            long id = 0;

            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains("Tile"))
                {
                    string[] parts = line.Split();
                    id = long.Parse(parts[1].Substring(0, parts[1].Length - 1));
                }
                else if (string.IsNullOrEmpty(line))
                {
                    piece = new Matrix(id, data.ToArray());
                    pieces.Add(piece);
                    data = new List<string>();
                    id = 0;
                }
                else
                {
                    data.Add(line);
                }
            }

            List<Matrix> corners = new List<Matrix>();
            List<Matrix> center = new List<Matrix>();
            foreach (Matrix printpiece in pieces)
            {
                int matches = 0;
                foreach (Matrix compare in pieces)
                {
                    if (compare.id != printpiece.id)
                    {
                        bool[] compared = printpiece.MatchSides(compare);
                        Console.WriteLine($"{compare.id}: {compared[0]}, {compared[1]}, {compared[2]}, {compared[3]}");

                        if (compared.Count(c => c) > 0)
                        {
                            matches++;
                        }
                    }
                }
                Console.WriteLine($"{printpiece.id} Matches: {matches}");

                if (matches == 2) corners.Add(printpiece);
                if (matches == 4) center.Add(printpiece);
            }

            long accumulator = 1;
            foreach (Matrix m in corners)
            {
                accumulator *= m.id;
            }

            Console.WriteLine($"Corner IDs multiplied = {accumulator}");
        }

        static List<Matrix> solve(List<Matrix> edges, List<Matrix> corners, Dictionary<Matrix, List<Matrix>> matches, Matrix starter = null)
        {
            List<Matrix> clockwise_ordering = new List<Matrix>(edges.Count + corners.Count);
            int corner = 0;

            if (starter == null)
            {
                clockwise_ordering.Add(corners[0]);
            } else
            {
                clockwise_ordering.Add(starter);
            }

            int i;
            for (i = 1; i < edges.Count + corners.Count; i++)
            {
                foreach (Matrix m in matches[clockwise_ordering[i - 1]])
                {
                    if ((edges.Contains(m) || corners.Contains(m)) && !clockwise_ordering.Contains(m))
                    {
                        clockwise_ordering.Add(m);
                        break;
                    }
                }
            }

            // Finally configure with original corner
            // orient bottom to top
            if (matches.Count > 1)
            {
                Matrix.Orient(clockwise_ordering[0], clockwise_ordering[i - 1], new bool[] { false, true, false, false }, new bool[] { true, false, false, false });
                Console.WriteLine(" --- DEBUG (B to T) --- ");
                Console.WriteLine(clockwise_ordering[0]);
                Console.WriteLine(clockwise_ordering[i - 1]);
            }

            if (matches.Count > 1 && !matches[clockwise_ordering[0]].Contains(clockwise_ordering[clockwise_ordering.Count - 1]))
            {
                throw new Exception();
            }

            return clockwise_ordering;
        }

        static void print_puzzle(Matrix[][] puzzle)
        {
            List<string> strings = new List<string>(puzzle.Length * puzzle[0][0].SimpleStrings().Count);
            for (int i = 0; i < puzzle.Length * puzzle[0][0].SimpleStrings().Count; i++)
            {
                strings.Add("");
            }

            for (int i = 0; i < puzzle.Length; i++)
            {
                int k = 0;
                for (int j = 0; j < puzzle[i].Length; j++)
                {
                    List<string> additionalStrings = puzzle[i][j].SimpleStrings();
                    for (k = 0; k < additionalStrings.Count; k++)
                    {
                        strings[i * additionalStrings.Count + k] += additionalStrings[k] + " ";
                    }
                }
                strings[i * puzzle[0][0].SimpleStrings().Count + k - 1] += "\n";
            }

            foreach (string s in strings)
            {
                Console.WriteLine(s);
            }
        }

        static string[] string_array_puzzle(Matrix[][] puzzle)
        {
            List<string> strings = new List<string>(puzzle.Length * puzzle[0][0].ConvertToPart2().SimpleStrings().Count);
            for (int i = 0; i < puzzle.Length * puzzle[0][0].ConvertToPart2().SimpleStrings().Count; i++)
            {
                strings.Add("");
            }

            for (int i = 0; i < puzzle.Length; i++)
            {
                int k = 0;
                for (int j = 0; j < puzzle[i].Length; j++)
                {
                    List<string> additionalStrings = puzzle[i][j].ConvertToPart2().SimpleStrings();

                    for (k = 0; k < additionalStrings.Count; k++)
                    {
                        strings[i * additionalStrings.Count + k] += additionalStrings[k];
                    }
                }
            }

            return strings.ToArray();
        }

        static string single_string_puzzle(Matrix[][] puzzle)
        {
            string returns = "";
            List<string> strings = new List<string>(puzzle.Length * puzzle[0][0].ConvertToPart2().SimpleStrings().Count);
            for (int i = 0; i < puzzle.Length * puzzle[0][0].ConvertToPart2().SimpleStrings().Count; i++)
            {
                strings.Add("");
            }

            for (int i = 0; i < puzzle.Length; i++)
            {
                int k = 0;
                for (int j = 0; j < puzzle[i].Length; j++)
                {
                    List<string> additionalStrings = puzzle[i][j].ConvertToPart2().SimpleStrings();
                    for (k = 0; k < additionalStrings.Count; k++)
                    {
                        strings[i * additionalStrings.Count + k] += additionalStrings[k] + "";
                    }
                }
            }

            foreach (string s in strings)
            {
                returns += s + "\n";
            }

            return returns;
        }

        static void part2()
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-20\input-20.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            string line;

            Matrix piece;
            List<Matrix> pieces = new List<Matrix>();
            List<string> data = new List<string>();
            long id = 0;

            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains("Tile"))
                {
                    string[] parts = line.Split();
                    id = long.Parse(parts[1].Substring(0, parts[1].Length - 1));
                }
                else if (string.IsNullOrEmpty(line))
                {
                    piece = new Matrix(id, data.ToArray());
                    pieces.Add(piece);
                    data = new List<string>();
                    id = 0;
                }
                else
                {
                    data.Add(line);
                }
            }

            List<Matrix> outer_layer;
            int side = (int)Math.Sqrt(pieces.Count);
            Matrix[][] puzzle = new Matrix[side][];

            for (int i = 0; i < puzzle.Length; i++)
            {
                puzzle[i] = new Matrix[side];
            }

            int layer = 0;
            Matrix starter = null;
            List<Matrix> pieces_copy = new List<Matrix>(pieces);
            Dictionary<Matrix, List<Matrix>> matches = new Dictionary<Matrix, List<Matrix>>();
            Dictionary<Matrix, List<Matrix>> newmatches;
            while (pieces.Count > 0)
            {
                side = (int)Math.Sqrt(pieces.Count);
                (starter, outer_layer, newmatches) = solve_puzzle(pieces, starter);
                matches = matches.Concat(newmatches).GroupBy(d => d.Key).ToDictionary(d => d.Key, d => d.First().Value);

                Console.WriteLine("----- Layer -----");
                for (int i  = 0; i < outer_layer.Count; i++)
                {
                    int x, y;
                    if (i < side)
                    {
                        x = layer + i; // Counts up (going towards the right along the top)
                        y = layer; // FIXED
                        Console.WriteLine($"TOP i = {i}, {x},{y}");
                        puzzle[y][x] = outer_layer[i];
                    } else if (i < side + side - 1)
                    {
                        x = layer + side - 1; // FIXED
                        y = layer + i - (side - 1); // Counts up (going down the right side)
                        Console.WriteLine($"RIGHT i = {i}, {x},{y}");
                        puzzle[y][x] = outer_layer[i];
                    } else if (i < side + side + side - 1 - 1)
                    {
                        // FIX
                        x = (side - 2 + layer) - (i - (2 * side - 1)); // Should count down (going towards the left along the bottom)
                        y = layer + side - 1; // FIXED
                        Console.WriteLine($"BOTTOM i = {i}, {x},{y}");
                        puzzle[y][x] = outer_layer[i];
                    } else if (i < side + side + side + side - 1 - 1 - 2)
                    {
                        // FIX
                        x = layer; // FIXED
                        y = (side + side + side + side - 1 - 1 - 2 + layer) - (i); // Should count down going up the left side
                        Console.WriteLine($"LEFT i = {i}, {x},{y}");
                        puzzle[y][x] = outer_layer[i];
                    } else
                    {
                        throw new Exception();
                    }

                    Console.WriteLine($"{outer_layer[i].id}");
                }
                Console.WriteLine("----------------");
                pieces.RemoveAll(x => outer_layer.Contains(x));
                layer++;
            }

            for (int i = 0; i < puzzle.Length; i++)
            {
                for (int j = 0; j < puzzle[i].Length; j++)
                {
                    Console.Write($"{puzzle[i][j].id} ");
                }
                Console.WriteLine();
            }

            for (int i = 0; i < puzzle.Length; i++)
            {
                for (int j = 0; j < puzzle[i].Length; j++)
                {
                    Console.Write($"{puzzle[i][j]} ");
                }
                Console.WriteLine();
            }

            orient_puzzle(puzzle, matches);

            print_puzzle(puzzle);

            Matrix part2combined = new Matrix(0, string_array_puzzle(puzzle));
            Console.WriteLine(part2combined.SimpleString());

            (int monster_count, int total_hashes) = find_sea_monsters(part2combined);
            Console.WriteLine($"Sea monsters found: {monster_count}\nTotal Hashes Left: {total_hashes}");
        }

        private static (int monster_count, int total_hashes) find_sea_monsters(Matrix part2combined)
        {
            string[] seamonster = @"                  # 
#    ##    ##    ###
 #  #  #  #  #  #   ".Split("\r\n");

            int monster_count = 0;
            int total_hashes = Regex.Matches(part2combined.SimpleString(), "#").Count();
            bool match = true;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    List<string> current = part2combined.SimpleStrings();
                    for (int k = 0; k < current[0].Length - seamonster[0].Length; k += 1)
                    {
                        for (int l = 0; l < current.Count - seamonster.Length; l += 1)
                        {
                            match = true;
                            for (int y = 0; y < seamonster.Length; y++)
                            {
                                for (int x = 0; x < seamonster[0].Length; x++)
                                {
                                    if (seamonster[y][x] == '#' && current[l + y][k + x] != '#')
                                    {
                                        match = false;
                                        break;
                                    }
                                }
                                if (!match)
                                {
                                    break;
                                }
                            }
                            if (match)
                            {
                                monster_count += 1;
                            }
                        }
                    }
                    if (monster_count > 0)
                    {
                        break;
                    } else
                    {
                        part2combined.ClockwiseRotate();
                    }
                }
                if (monster_count > 0)
                {
                    break;
                } else
                {
                    part2combined.HorizontalFlip();
                }
            }

            return (monster_count, total_hashes - (monster_count * 15));
        }

        private static void orient_puzzle(Matrix[][] puzzle, Dictionary<Matrix, List<Matrix>> matches)
        {
            int i = 0, j = 0, candidate = 0;
            Matrix swapped = null;
            bool worked;
            int original_x = 0, original_y = 0;
            int swapped_x = 0, swapped_y = 0;
            int list1 = 0, list2 = 0, list3 = 0;

            try
            {
                for (i = 0; i < puzzle.Length - 1; i++)
                {
                    for (j = 0; j < puzzle.Length - 1; j++)
                    {
                        // Orient(top, bottom, right)
                            Matrix.JointOrient(puzzle[i][j], puzzle[i + 1][j], puzzle[i][j + 1]);
                            if (candidate > 0)
                            {
                                // reset exception
                                candidate = 0;
                                swapped = null;
                                swapped_x = 0;
                                swapped_y = 0;
                                list1 = 0;
                                list2 = 0;
                                list3 = 0;
                            }
                    }
                }
            }
            catch (Exception e)
            {
                // TODO SWAP THEM AND SEE?
                Console.WriteLine("-- Exception Raised --");
                // puzzle[i][j] could be wrong
                // puzzle[i + 1][j] could be wrong
                // puzzle[i][j + 1] could be wrong
                list1 = matches[puzzle[i][j]].Count;
                list2 = matches[puzzle[i + 1][j]].Count;
                list3 = matches[puzzle[i][j + 1]].Count;

                if (candidate < list1)
                {
                    if (candidate > 0)
                    {
                        // put it back
                        puzzle[swapped_y][swapped_x] = puzzle[original_y][original_x];
                        puzzle[original_y][original_x] = swapped;
                    }
                    swapped = puzzle[i][j];
                    (worked, swapped_y, swapped_x) = swapout(puzzle[i][j], i, j, candidate, puzzle, matches);
                    original_y = i;
                    original_x = j;
                }
                else if (candidate - list1 < list2)
                {
                    if (candidate > 0)
                    {
                        // put it back
                        puzzle[swapped_y][swapped_x] = puzzle[original_y][original_x];
                        puzzle[original_y][original_x] = swapped;
                    }
                    swapped = puzzle[i + 1][j];
                    (worked, swapped_y, swapped_x) = swapout(puzzle[i + 1][j], i + 1, j, candidate - list1, puzzle, matches);
                    original_y = i + 1;
                    original_x = j;
                }
                else if (candidate - list1 - list2 < list3)
                {
                    if (candidate > 0)
                    {
                        // put it back
                        puzzle[swapped_y][swapped_x] = puzzle[original_y][original_x];
                        puzzle[original_y][original_x] = swapped;
                    }
                    swapped = puzzle[i][j + 1];
                    (worked, swapped_y, swapped_x) = swapout(puzzle[i][j + 1], i, j + 1, candidate - list1 - list2, puzzle, matches);
                    original_y = i;
                    original_x = j + 1;
                }
                else
                {
                    //throw;
                }

                j -= 1; // repeat?
                candidate++;
            }
            Matrix.JointOrientLast(puzzle[i][j], puzzle[i - 1][j], puzzle[i][j - 1]);
        }

        private static (bool, int, int) swapout(Matrix matrix, int pos_y, int pos_x, int candidate, Matrix[][] puzzle, Dictionary<Matrix, List<Matrix>> matches)
        {
            if (candidate >= matches[matrix].Count)
            {
                return (false, -1, -1);
            }

            Matrix swapper = matches[matrix][candidate];

            int i = 0, j = 0;
            for (i = 0; i < puzzle.Length; i++)
            {
                for (j = 0; j < puzzle[i].Length; j++)
                {
                    if (puzzle[i][j] == swapper)
                    {
                        swapper = puzzle[i][j];
                        puzzle[pos_y][pos_x] = swapper;
                        puzzle[i][j] = matrix;
                        break;
                    }
                }
                if (puzzle[pos_y][pos_x] == swapper)
                {
                    break;
                }
            }

            return (false, i, j);
        }

        private static Tuple<Matrix, List<Matrix>, Dictionary<Matrix, List<Matrix>>> solve_puzzle(List<Matrix> pieces, Matrix starter = null)
        {
            List<Matrix> corners = new List<Matrix>();
            List<Matrix> center = new List<Matrix>();
            List<Matrix> walls = new List<Matrix>();
            Dictionary<Matrix, List<Matrix>> matches = new Dictionary<Matrix, List<Matrix>>();
            foreach (Matrix printpiece in pieces)
            {
                int total_matches = 0;
                foreach (Matrix compare in pieces)
                {
                    if (compare.id != printpiece.id)
                    {
                        bool[] compared = printpiece.MatchSides(compare);

                        if (compared.Count(c => c) > 0)
                        {
                            total_matches++;
                            if (!matches.ContainsKey(printpiece))
                            {
                                matches[printpiece] = new List<Matrix>();
                            }
                            matches[printpiece].Add(compare);
                        }
                    }
                }
                Console.WriteLine($"{printpiece.id} Matches: {total_matches}");

                switch (total_matches)
                {
                    case 0:
                    case 2:
                        corners.Add(printpiece);
                        break;
                    case 3:
                        walls.Add(printpiece);
                        break;
                    case 4:
                        center.Add(printpiece);
                        break;
                    default:
                        throw new Exception();
                        break;
                }

            }

            List<Matrix> outer_layer = solve(walls, corners, matches, starter);

            foreach (Matrix m in matches.Keys)
            {
                if (outer_layer.Contains(m))
                {
                    continue;
                }

                if (matches[outer_layer[1]].Contains(m) && matches[outer_layer[outer_layer.Count - 1]].Contains(m))
                {
                    starter = m;
                }
            }

            if (starter == null)
            {
                throw new Exception();
            }

            return new Tuple<Matrix, List<Matrix>, Dictionary<Matrix, List<Matrix>>>(starter, outer_layer, matches);
        }

        static void Main(string[] args)
        {
            //part1();
            // TODO: re-orient pieces based on matches ---> need to actually transform based on what they are connecting to
            part2();
        }
    }
}
