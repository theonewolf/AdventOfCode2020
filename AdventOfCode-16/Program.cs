using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace AdventOfCode_16
{
    enum RuleType
    {
        stat,
        your,
        others,
        invalid
    };

    class Program
    {

        static List<int> validate_numbers(List<int> numbers, List<Tuple<Tuple<int, int>, Tuple<int, int>>> stats)
        {
            List<int> invalid = new List<int>();

            if (numbers.Count != stats.Count)
            {
                throw new Exception();
            }

            for (int i = 0; i < numbers.Count;  i++)
            {
                bool passes = false;
                for (int j = 0; j < stats.Count; j++)
                {
                    Tuple<int, int> limit1 = stats[j].Item1;
                    Tuple<int, int> limit2 = stats[j].Item2;

                    if (numbers[i] >= limit1.Item1 && numbers[i] <= limit1.Item2)
                    {
                        passes = true;
                    }

                    if (numbers[i] >= limit2.Item1 && numbers[i] <= limit2.Item2)
                    {
                        passes = true;
                    }
                }

                if (!passes)
                {
                    invalid.Add(numbers[i]);
                }
            }

            return invalid;
        }

        static List<bool[]> valid_positions(List<List<int>> tickets, List<Tuple<Tuple<int, int>, Tuple<int, int>>> stats)
        {
            List<bool[]> positions = new List<bool[]>();

            // Check for every stat
            foreach (var stat in stats) {
                bool[] position_valid = new bool[stats.Count];

                int min1 = stat.Item1.Item1;
                int max1 = stat.Item1.Item2;
                int min2 = stat.Item2.Item1;
                int max2 = stat.Item2.Item2;

                for (int i = 0; i < position_valid.Length; i++)
                {
                    position_valid[i] = true;
                }

                // Through every ticket, which position could be this stat
                foreach (List<int> row in tickets)
                {
                    for (int i = 0; i < row.Count; i++)
                    {
                        if (!((row[i] >= min1) && (row[i] <= max1)) &&
                            !((row[i] >= min2) && (row[i] <= max2)))
                        {
                            position_valid[i] = false;
                        }
                    }
                }
                positions.Add(position_valid);
            }

            return positions;
        }

        static List<bool[]> solve_positions(List<bool[]> positions)
        {
            bool modified = true;

            while (modified)
            {
                modified = false;

                for (int i = 0; i < positions.Count; i++)
                {
                    int count_true = 0;
                    int true_pos = -1;
 
                    for (int j = 0; j < positions[i].Length; j++)
                    {
                        if (positions[i][j])
                        {
                            count_true++;
                            true_pos = j;
                        }
                    }

                    if (count_true > 1)
                    {
                        modified = true;
                    }

                    if (count_true == 1)
                    {
                        for (int k = 0; k < positions.Count; k++)
                        {
                            if (k != i)
                            {
                                positions[k][true_pos] = false;
                            }
                        }
                    }
                }
            }

            return positions;
        }

        static void print_positions(List<Tuple<Tuple<int, int>, Tuple<int, int>>> stats, List<string> column_names, List<bool[]> positions)
        {
            Console.Write($"{"",-20}\t");
            for (int i = 0; i < stats.Count; i++)
            {
                Console.Write($"{i}\t");
            }
            Console.Write("\n");

            for (int i = 0; i < column_names.Count; i++)
            {
                Console.Write($"{column_names[i],-20}\t");
                foreach (bool value in positions[i])
                {
                    Console.Write($"{value}\t");
                }
                Console.Write("\n");
            }
        }

        static List<int> find_positions(string prefix, List<string> column_names, List<bool[]> solved_positions)
        {
            List<int> found_fields = new List<int>();

            for (int i = 0; i < column_names.Count; i++)
            {
                if (column_names[i].StartsWith(prefix))
                {
                    for (int j = 0; j < solved_positions[i].Length; j++)
                    {
                        if (solved_positions[i][j])
                        {
                            found_fields.Add(j);
                            break;
                        }
                    }
                }
            }

            return found_fields;
        }

        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-16\input-16.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            List<string> column_names = new List<string>();
            List<Tuple<Tuple<int, int>, Tuple<int, int>>> stats = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>();
            List<int> invalid = new List<int>();
            List<List<int>> good_values = new List<List<int>>();
            List<int> my_ticket = new List<int>();

            string line;
            RuleType type = RuleType.invalid;

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            while ((line = file.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line.Contains("or"))
                {
                    type = RuleType.stat;
                }
                else if (line.StartsWith("your ticket:"))
                {
                    type = RuleType.your;
                }
                else if (line.StartsWith("nearby tickets:"))
                {
                    type = RuleType.others;
                }

                switch(type)
                {
                    case RuleType.stat:
                        Console.WriteLine(line);
                        string[] data = line.Split(":");
                        string[] limits = data[1].Split("or");
                        string[] limit1 = limits[0].Split("-");
                        string[] limit2 = limits[1].Split("-");
                        Tuple<int, int> limit1int = new Tuple<int, int>(int.Parse(limit1[0]), int.Parse(limit1[1]));
                        Tuple<int, int> limit2int = new Tuple<int, int>(int.Parse(limit2[0]), int.Parse(limit2[1]));
                        stats.Add(new Tuple<Tuple<int, int>, Tuple<int, int>>(limit1int, limit2int));
                        column_names.Add(data[0]);
                        break;
                    case RuleType.your:
                        Console.WriteLine(line);
                        if (line.Contains(",")) {
                            string[] yourdata = line.Split(",");
                            List<int> values = new List<int>();
                            foreach (string value in yourdata)
                            {
                                values.Add(int.Parse(value));
                            }
                            my_ticket = values;
                            invalid.Concat(validate_numbers(values, stats));
                        }
                        break;
                    case RuleType.others:
                        Console.WriteLine(line);
                        if (line.Contains(","))
                        {
                            string[] otherdata = line.Split(",");
                            List<int> values = new List<int>();
                            foreach (string value in otherdata)
                            {
                                values.Add(int.Parse(value));
                            }
                            List<int> invalids = validate_numbers(values, stats);
                            invalid = invalid.Concat(invalids).ToList();
                            if (invalids.Count == 0)
                            {
                                good_values.Add(values);
                            }
                        }
                        break;
                    case RuleType.invalid:
                    default:
                        throw new Exception();
                }
            }

            int error_rate = 0;
            foreach (int error in invalid)
            {
                error_rate += error;
            }
            Console.WriteLine($"Ticket Scanning Error Rate: {error_rate}");

            /* PART 2 */

            // Is the position valid for the field
            var positions = valid_positions(good_values, stats);
            // Solve such that a single position is left true for each field
            var solved_positions = solve_positions(positions);

            print_positions(stats, column_names, positions);
            print_positions(stats, column_names, solved_positions);
            List<int> fields = find_positions("departure", column_names, solved_positions);
            long accumulator = 1;

            foreach (int field in fields) {
                Console.WriteLine($"Departure Field: {field}");
                Console.WriteLine($"my_ticket[field] == {my_ticket[field]}");
                accumulator *= my_ticket[field];
            }

            Console.WriteLine($"Multiplied together: {accumulator}");
        }
    }
}
