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
using System.Collections.Generic;
using System.Linq;

namespace AdventofCode_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-1\input-1.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            string line;
            List<int> numbers = new List<int>();
            bool found1 = false;
            bool found2 = false;

            while ((line = file.ReadLine()) != null)
            {
                numbers.Add(int.Parse(line));
            }

            foreach (int i in numbers)
            {
                foreach (int j in numbers)
                {
                    if (!found1 && i + j == 2020)
                    {
                        Console.WriteLine($"Multiplied Solution 1 ({i} * {j}) =  {i * j}");
                        found1 = true;
                    }

                    foreach (int k in numbers)
                    {
                        if (!found2 && i + j + k == 2020)
                        {
                            Console.WriteLine($"Multiplied Solution 2 ({i} * {j} * {k}) = {i * j * k}");
                            found2 = true;
                            break;
                        }
                    }

                    if (found1 && found2) break;
                }
                if (found1 && found2) break;
            }
        }
    }
}
