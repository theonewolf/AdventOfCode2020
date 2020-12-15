using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode_15
{
    class Program
    {
        static long find_turn(bool fast, int max_index, long previous, long[] spoken, Dictionary<long, long> cache)
        {
            if (!fast)
            {
                int max_found = 0;

                for (int i = 0; i < max_index; i++)
                {
                    if (spoken[i] == previous && i + 1 > max_found)
                    {
                        max_found = i + 1;
                    }
                }

                if (cache.ContainsKey(previous) && max_found != cache[previous])
                {
                    throw new Exception();
                }
                else if (!cache.ContainsKey(previous) && max_found != 0)
                {
                    throw new Exception();
                }

                return max_found;
            }
            else
            {
                if (!cache.ContainsKey(previous))
                {
                    return 0;
                } else
                {
                    return cache[previous];
                }
            }
        }
        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-15\input-15.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            long[] spoken = new long[30000000];
            Dictionary<long, long> last_seen_cache = new Dictionary<long, long>();
            int i = 0;

            string line;

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            while ((line = file.ReadLine()) != null)
            {
                string[] splitline = line.Split(",");

                for (; i < splitline.Length; i++)
                {
                    spoken[i] = long.Parse(splitline[i]);
                    if (i <= splitline.Length - 1 && i > 0)
                    {
                        long previous_turn = i;
                        long previous = spoken[previous_turn - 1];
                        last_seen_cache[previous] = previous_turn;
                    }
                    Console.WriteLine($"Turn {i + 1}, spoke {spoken[i]}");
                }
            }

            for (; i < 30000000; i++)
            {
                // Turn correct
                // Previous number spoken
                long previous_turn = i;
                long previous = spoken[previous_turn - 1];
                long next_spoken = 0;
                //Console.WriteLine($"Previous: {previous}");
                long previous_position = find_turn(true, i - 1, previous, spoken, last_seen_cache);
                if (previous_position != 0)
                {
                    next_spoken = previous_turn - previous_position;
                }

                if (i % 100000 == 0) Console.WriteLine($"Progress: {i}");
                //Console.WriteLine($"Last saw {previous_turn}:{previous} at {previous_position}");
                spoken[i] = next_spoken;
                last_seen_cache[previous] = previous_turn;
            }
            Console.WriteLine($"Turn {i}, spoke {spoken[i - 1]}");
        }
    }
}
