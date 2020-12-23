using System;
using System.Collections.Generic;

namespace AdventOfCode_24
{

    class Program
    {
        static void part1()
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-24\input-24-test.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            
            Console.WriteLine(fileName);
            Console.WriteLine(file);

            string line;

            while ((line = file.ReadLine()) != null)
            {
            }
        }

        static void part2()
        {
        }

        static void Main(string[] args)
        {
            part1();
            part2();
        }
    }
}
