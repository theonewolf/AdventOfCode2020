using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode_15
{
    class Program
    {
        static long find_turn(int max_index, long previous, long[] spoken)
        {
            int max_found = 0;

            for (int i = 0; i < max_index; i++) {
                if (spoken[i] == previous && i + 1 > max_found)
                {
                    max_found = i + 1;
                }
            }

            return max_found;
        }
        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-15\input-15.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            long[] spoken = new long[2020];
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
                    Console.WriteLine($"Turn {i + 1}, spoke {spoken[i]}");
                }
            }

            for (; i < 2020; i++)
            {
                // Turn correct
                // Previous number spoken
                long previous_turn = i;
                long previous = spoken[previous_turn - 1];
                long next_spoken = 0;
                //Console.WriteLine($"Previous: {previous}");
                long previous_position = find_turn(i - 1, previous, spoken);
                if (previous_position != 0)
                {
                    next_spoken = previous_turn - previous_position;
                }
                Console.WriteLine($"Last saw {previous_turn}:{previous} at {previous_position}");
                spoken[i] = next_spoken;
                Console.WriteLine($"Turn {i + 1}, spoke {spoken[i]}");
            }
        }
    }
}
