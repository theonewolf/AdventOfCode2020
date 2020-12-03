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

namespace AdventOfCode_3
{
    class Program
    {

        static int calculate_trees_encountered(string[] map, int slopeX, int slopeY)
        {
            int posX = 0;
            int posY = 0;
            int trees = 0;

            // 0,0 is upper-left
            // map.Length, map[0].Length is bottom-right
            while (posY < map.Length - 1)
            {
                posX += slopeX;
                posX %= map[0].Length; // wrap inside the map, when we go over the right side

                posY += slopeY;

                if (map[posY][posX] == '#')
                {
                    trees += 1;
                }
            }

            if (posY != map.Length - 1)
            {
                Console.WriteLine("We DIDN'T REACH THE BOTTOM!");
            }

            return trees;
        }

        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-3\input-3.txt";

            Console.WriteLine(fileName);

            string[] map = System.IO.File.ReadAllLines(fileName);

            int challenge1Trees = calculate_trees_encountered(map, 3, 1);

            Console.WriteLine($"Trees encountered: {challenge1Trees}");

            // Note: this required a **long** integer.  The multiply overflowed with 32-bit integers.
            long slope1Trees = calculate_trees_encountered(map, 1, 1);
            long slope2Trees = calculate_trees_encountered(map, 3, 1);
            long slope3Trees = calculate_trees_encountered(map, 5, 1);
            long slope4Trees = calculate_trees_encountered(map, 7, 1);
            long slope5Trees = calculate_trees_encountered(map, 1, 2);

            Console.WriteLine($"Slope 1: {slope1Trees}");
            Console.WriteLine($"Slope 2: {slope2Trees}");
            Console.WriteLine($"Slope 3: {slope3Trees}");
            Console.WriteLine($"Slope 4: {slope4Trees}");
            Console.WriteLine($"Slope 5: {slope5Trees}");

            Console.WriteLine($"Trees multiplied: {slope1Trees * slope2Trees * slope3Trees * slope4Trees * slope5Trees}");
        }
    }
}
