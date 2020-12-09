using System;
using System.Collections.Generic;

namespace AdventOfCode_9
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-9\input-9.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            int PREAMBLE_SIZE = 25;
            int[] preamble = new int[PREAMBLE_SIZE];
            List<int> integers = new List<int>();
            int filepos = 0;
            int target = 0;

            string line;

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            while ((line = file.ReadLine()) != null)
            {
                int newint = int.Parse(line);
                integers.Add(newint);

                if (filepos >= PREAMBLE_SIZE)
                {
                    bool sumfound = false;
                    
                    foreach (int i in preamble)
                    {
                        foreach (int j in preamble)
                        {
                            if (i + j == newint)
                            {
                                Console.WriteLine($"Added up: {i} + {j} == {newint}");
                                sumfound = true;
                                break;
                            }
                        }
                    
                        if (sumfound)
                        {
                            break;
                        }
                    }

                    if (!sumfound)
                    {
                        Console.WriteLine($"Preamble sum for int {newint} NOT FOUND!");
                        target = newint;
                        break;
                    }
                }

                preamble[filepos % PREAMBLE_SIZE] = newint;
                filepos++;
            }

            int windowsize = 2;
            bool contiguousfound = false;

            // sliding window to find contiguous integers summing to target value
            while (!contiguousfound && windowsize < integers.Count)
            {
                for (int i = 0; i < integers.Count - windowsize; i++)
                {
                    int sum = 0;
                    int weakness = 0;
                    int max = int.MinValue;
                    int min = int.MaxValue;
                    Console.WriteLine($"Contiguous position start in integers: {i}");

                    for (int j = 0; j < windowsize; j++)
                    {
                        Console.WriteLine($"Adding to sum position: {i + j}");
                        sum += integers[i + j];

                        if (integers[i+j] < min)
                        {
                            min = integers[i + j];
                        }

                        if (integers[i+j] > max)
                        {
                            max = integers[i + j];
                        }
                    }

                    if (sum == target)
                    {
                        contiguousfound = true;
                        weakness = min + max;
                        Console.WriteLine($"Window Size: {windowsize}");
                        Console.WriteLine($"Contiguous indexes: [{i}, {i + windowsize - 1}]");
                        Console.WriteLine($"Weakness value: {weakness}");
                        break;
                    }
                }

                windowsize++;
                Console.WriteLine($"Window size: {windowsize}");
            }
        }
    }
}
