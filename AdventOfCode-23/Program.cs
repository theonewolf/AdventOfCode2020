using System;
using System.Collections.Generic;
using System.Collections;

namespace AdventOfCode_23
{

    // I realized we need all operations in the core loop to be O(1).  A doubly linked list seemed to do the job in C#,
    // until I needed it to be a _circular_ doubly linked list which C# does not support.  Luckily, there are these
    // extension methods (need to learn how they work) as pointed out on StackOverflow for easily implementing such
    // a data structure in C#.
    // Inspiration: https://stackoverflow.com/questions/716256/creating-a-circularly-linked-list-in-c
    static class CircularLinkedList
    {
        public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current)
        {
            return current.Next ?? current.List.First;
        }

        public static LinkedListNode<T> PreviousOrLast<T>(this LinkedListNode<T> current)
        {
            return current.Previous ?? current.List.Last;
        }
    }

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
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-23\input-23-1.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            long min_cup = long.MaxValue;
            long max_cup = long.MinValue;
            LinkedList<long> cups = new LinkedList<long>();
            LinkedListNode<long>[] index = new LinkedListNode<long>[1000000];

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            string line;

            // The cups will be arranged in a circle and labeled clockwise (your puzzle input).
            int counter = 0;
            while ((line = file.ReadLine()) != null)
            {
                foreach (char c in line)
                {
                    long cup = (long)char.GetNumericValue(c);
                    cups.AddLast(cup);
                    index[cup - 1] = cups.Last;
                    counter++;

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

            // the crab starts arranging many cups in a circle on your raft - one million (1000000) in total.
            // Your labeling is still correct for the first few cups;
            // after that, the remaining cups are just numbered in an increasing fashion starting from the number after the highest number in your list and proceeding one by one until one million is reached.
            // In this way, every number from one through one million is used exactly once.
            for (long i = max_cup + 1; i < 1000001; i++)
            {
                cups.AddLast(i);
                index[counter] = cups.Last;
                counter++;
                max_cup = i;
            }

            if (cups.Count != 1000000 || !cups.Contains(1) || !cups.Contains(1000000))
            {
                throw new Exception();
            }

            // Before the crab starts, it will designate the first cup in your list as the current cup.
            // The crab is then going to do 100 moves.
            // Each move, the crab does the following actions:
            LinkedListNode<long>[] three_cups = new LinkedListNode<long>[3];
            LinkedListNode<long> current_cup = cups.First; // O(1)

            for (int i = 0; i < 10000000; i++)
            {
                // The crab picks up the three cups that are immediately clockwise of the current cup.
                // They are removed from the circle; cup spacing is adjusted as necessary to maintain the circle.
                three_cups[0] = current_cup.NextOrFirst(); // O(1)
                three_cups[1] = current_cup.NextOrFirst().NextOrFirst(); // O(1)
                three_cups[2] = current_cup.NextOrFirst().NextOrFirst().NextOrFirst(); // O(1)
                cups.Remove(three_cups[0]); // O(1)
                cups.Remove(three_cups[1]); // O(1)
                cups.Remove(three_cups[2]); // O(1)

                // The crab selects a destination cup: the cup with a label equal to the current cup's label minus one.
                // If this would select one of the cups that was just picked up,
                // the crab will keep subtracting one until it finds a cup that wasn't just picked up.
                // If at any point in this process the value goes below the lowest value on any cup's label,
                // it wraps around to the highest value on any cup's label instead.
                long destination_cup = current_cup.Value - 1;
                while (!(destination_cup >= min_cup) || (three_cups[0].Value == destination_cup || three_cups[1].Value == destination_cup || three_cups[2].Value == destination_cup)) // O(1)
                {
                    destination_cup--;
                    if (destination_cup < min_cup)
                    {
                        destination_cup = max_cup;
                    }
                }

                // The crab places the cups it just picked up so that they are immediately clockwise of the destination cup.
                // They keep the same order as when they were picked up.
                LinkedListNode<long> destination = index[destination_cup - 1]; // O(1)
                cups.AddAfter(destination, three_cups[0]); // O(1)
                index[three_cups[0].Value - 1] = destination.NextOrFirst(); // O(1)
                cups.AddAfter(destination.Next, three_cups[1]); // O(1)
                index[three_cups[1].Value - 1] = destination.NextOrFirst().NextOrFirst(); // O(1)
                cups.AddAfter(destination.Next.Next, three_cups[2]); // O(1)
                index[three_cups[2].Value - 1] = destination.NextOrFirst().NextOrFirst().NextOrFirst(); // O(1)

                // The crab selects a new current cup: the cup which is immediately clockwise of the current cup.
                current_cup = current_cup.NextOrFirst(); // O(1)
            }

            LinkedListNode<long> one = cups.Find(1);

            Console.WriteLine($"first cup after 1: {one.Next.Value}");
            Console.WriteLine($"second cup after 1: {one.Next.Next.Value}");
            Console.WriteLine($"multiplied: {one.Next.Value * one.Next.Next.Value}");
        }
        static void Main(string[] args)
        {
            part1();
            part2();
        }
    }
}
