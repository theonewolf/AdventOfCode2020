using System;
using System.Collections.Generic;

namespace AdventOfCode_23
{
    class Program
    {

        static void part1()
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-23\input-23-1.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            long min_cup = long.MaxValue;
            long max_cup = long.MinValue;
            List<long> cups = new List<long>();

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            string line;

            // The cups will be arranged in a circle and labeled clockwise (your puzzle input).
            while ((line = file.ReadLine()) != null)
            {
                foreach (char c in line)
                {
                    long cup = (long)char.GetNumericValue(c);
                    cups.Add(cup);

                    if (cup < min_cup)
                    {
                        min_cup = cup;
                    }

                    if (cup > max_cup)
                    {
                        max_cup = cup;
                    }
                }
            }

            // Before the crab starts, it will designate the first cup in your list as the current cup.
            // The crab is then going to do 100 moves.
            // Each move, the crab does the following actions:
            List<long> three_cups = new List<long>();
            long current_cup = cups[0];

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"-- move {i + 1} --");

                Console.WriteLine($"cups: {string.Join(" ", cups)}");
                Console.WriteLine($"current: {current_cup}");

                // The crab picks up the three cups that are immediately clockwise of the current cup.
                // They are removed from the circle; cup spacing is adjusted as necessary to maintain the circle.
                three_cups.Add(cups[(cups.IndexOf(current_cup) + 1) % cups.Count]);
                cups.RemoveAt((cups.IndexOf(current_cup) + 1) % cups.Count);
                three_cups.Add(cups[(cups.IndexOf(current_cup) + 1) % cups.Count]);
                cups.RemoveAt((cups.IndexOf(current_cup) + 1) % cups.Count);
                three_cups.Add(cups[(cups.IndexOf(current_cup) + 1) % cups.Count]);
                cups.RemoveAt((cups.IndexOf(current_cup) + 1) % cups.Count);

                Console.WriteLine($"picked: {string.Join(" ", three_cups)}");

                // The crab selects a destination cup: the cup with a label equal to the current cup's label minus one.
                // If this would select one of the cups that was just picked up,
                // the crab will keep subtracting one until it finds a cup that wasn't just picked up.
                // If at any point in this process the value goes below the lowest value on any cup's label,
                // it wraps around to the highest value on any cup's label instead.
                long destination_cup = current_cup - 1;
                while (!cups.Contains(destination_cup))
                {
                    destination_cup--;
                    if (destination_cup < min_cup)
                    {
                        destination_cup = max_cup;
                    }
                }

                Console.WriteLine($"destination: {destination_cup}");

                // The crab places the cups it just picked up so that they are immediately clockwise of the destination cup.
                // They keep the same order as when they were picked up.
                cups.Insert((cups.IndexOf(destination_cup) + 1) % cups.Count, three_cups[0]);
                three_cups.RemoveAt(0);
                cups.Insert((cups.IndexOf(destination_cup) + 2) % cups.Count, three_cups[0]);
                three_cups.RemoveAt(0);
                cups.Insert((cups.IndexOf(destination_cup) + 3) % cups.Count, three_cups[0]);
                three_cups.RemoveAt(0);

                // The crab selects a new current cup: the cup which is immediately clockwise of the current cup.
                current_cup = cups[(cups.IndexOf(current_cup) + 1) % cups.Count];

                Console.WriteLine();
            }

            Console.WriteLine(string.Join("", cups));
            Console.Write("order after 1: ");
            for (int i = 1; i < cups.Count; i++)
            {
                Console.Write(cups[(cups.IndexOf(1) + i) % cups.Count]);
            }
            Console.WriteLine();
        }

        static void part2()
        {

        }
        static void Main(string[] args)
        {
            part1();
        }
    }
}
