using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace AdventOfCode_25
{

    class Program
    {

        static void part1()
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-25\input-25-test.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            string line;

            while ((line = file.ReadLine()) != null)
            {
                while (!string.IsNullOrEmpty(line))
                {
                }
            }

            static void part2()
            {
                string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-25\input-25-test.txt";
                System.IO.StreamReader file = new System.IO.StreamReader(fileName);

                Console.WriteLine(fileName);
                Console.WriteLine(file);

                string line;

                while ((line = file.ReadLine()) != null)
                {
                }
            }

            static void Main(string[] args)
            {
                part1();
                //part2();
            }
        }
    }
}