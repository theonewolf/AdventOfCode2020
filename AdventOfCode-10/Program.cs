using System;
using System.Collections.Generic;

namespace AdventOfCode_10
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-10\input-10.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            List<int> integers = new List<int>();


            string line;

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            while ((line = file.ReadLine()) != null)
            {
                integers.Add(int.Parse(line));
            }

            integers.Sort();

            int joltcurrent = 0;
            int diff1count = 0;
            int diff3count = 0;
            int myadapter = integers[integers.Count - 1] + 3;
            integers.Add(myadapter);
            foreach (int jolt in integers)
            {
                Console.WriteLine($"Choosing jolt: {jolt}");
                if (jolt - joltcurrent == 1)
                {
                    Console.WriteLine("Diff was: 1");
                    diff1count += 1;
                    joltcurrent = jolt;
                }
                else if (jolt - joltcurrent == 3)
                {
                    Console.WriteLine("Diff was: 3");
                    diff3count += 1;
                    joltcurrent = jolt;
                }
                else
                {
                    Console.WriteLine("ERROR: difference is not 1 or 3!");
                    return;
                }
            }

            Console.WriteLine($"1 Jolt Diff: {diff1count}, 3 Jolt Diff: {diff3count}");
            Console.WriteLine($"Jolt Diff's multiplied: {diff1count * diff3count}");

            int currentjolt = 0;
            int previousjoltdistance = 0;
            int runcount = 0;
            long total = 1;
            foreach (int nextjolt in integers)
            {
                Console.WriteLine($"Choosing jolt: {nextjolt}");
                
                if (nextjolt - currentjolt == 1 && previousjoltdistance == 1)
                {
                    runcount++;

                    // Run of three only has 7 valid combos it contributes [001, 010, 011, 100, 101, 110, 111]
                    // as 000 is not possible: distance between jolts would be greater than 3
                    if (runcount == 3)
                    {
                        total *= 7;
                        runcount = 0;
                    }
                } else
                {
                    if (runcount == 1) // two binary combinations
                    {
                        total *= 2;
                    } else if (runcount == 2) // four binary combinations
                    {
                        total *= 4;
                    }
                    runcount = 0;
                }

                previousjoltdistance = nextjolt - currentjolt;
                currentjolt = nextjolt;
            }

            Console.WriteLine($"total: {total}");

            // ***FINAL SOLUTION***
            // run of one is 2 combos
            // run of two is 4 combos
            // run of three is 7 combos
            // ***FINAL SOLUTION***
            // reset

            // Computing permutations sketch:
            //    1. Walk the list
            //    2. Count elements in subsets (probably 1 diff, can be present or not present, within 3 of the ends)
            //        + Note: elements can be deleted!
            //    3. Subsets contribute permutations, then multiply subset contributions together
        }
    }
}
