/*****************************************************************************
 * Open source Advent of Code 2020, C# solutions.  Released under the MIT    *
 * License.                                                                  *
 *                                                                           *
 * MIT License                                                               *
 *                                                                           *
 * Copyright (c) 2020 Wolfgang Richter <wolfgang.richter@gmail.com>          *
 *                                                                           *
 * Permission is hereby granted, free of charge, to any person obtaining a   *
 * copy of this software and associated documentation files (the "Software"),*
 * to deal in the Software without restriction, including without limitation *
 * the rights to use, copy, modify, merge, publish, distribute, sublicense,  *
 * and/or sell copies of the Software, and to permit persons to whom the     *
 * Software is furnished to do so, subject to the following conditions:      *
 *                                                                           *
 * The above copyright notice and this permission notice shall be included   *
 * in all copies or substantial portions of the Software.                    *
 *                                                                           *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR*
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,  *
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL   *
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER*
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING   *
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER       *
 * DEALINGS IN THE SOFTWARE.                                                 *
 ****************************************************************************/
using System;
using System.Linq;

namespace AdventOfCode_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-2\input-2.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            string line;
            int valid1 = 0;
            int valid2 = 0;

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            while ((line = file.ReadLine()) != null)
            {
                string[] splitline = line.Split(' ');
                string[] range = splitline[0].Split('-');
                char letter = splitline[1].Split(':')[0][0];
                string password = splitline[2];;
                int count = password.Count(f => f == letter);

                // Challenge 1 rule
                if (count >= int.Parse(range[0]) && count <= int.Parse(range[1]))
                {
                    valid1++;
                }

                // Challenge 2 rule
                if ((password[int.Parse(range[0]) - 1] == letter || password[int.Parse(range[1]) - 1] == letter) &&
                    !((password[int.Parse(range[0]) - 1] == letter && password[int.Parse(range[1]) - 1] == letter)))
                {
                    valid2++;
                }
            }

            Console.WriteLine($"Valid Password Count, Challenge 1: {valid1}");
            Console.WriteLine($"Valid Password Count, Challenge 2: {valid2}");
        }
    }
}
