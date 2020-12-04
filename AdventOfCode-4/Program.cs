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

namespace AdventOfCode_4
{
    class Program
    {
        static bool validate_year(string byr, int min, int max)
        {
            if (byr.Length != 4)
            {
                return false;
            }

            if (int.Parse(byr) < min ||
                int.Parse(byr) > max)
            {
                return false;
            }

            return true;
        }

        static bool validate_height(string measurement)
        {
            if (measurement.EndsWith("in"))
            {
                string integer = measurement.Substring(0, measurement.Length - 2);
                if (int.Parse(integer) < 59 ||
                    int.Parse(integer) > 76)
                {
                    return false;
                }
            } else if (measurement.EndsWith("cm"))
            {
                string integer = measurement.Substring(0, measurement.Length - 2);
                if (int.Parse(integer) < 150 ||
                    int.Parse(integer) > 193)
                {
                    return false;
                }
            } else
            {
                return false;
            }


            return true;
        }

        static bool validate_color(string color)
        {
            if (!color.StartsWith("#"))
            {
                return false;
            }

            string hex = color.Substring(1, color.Length - 1);

            if (!System.Text.RegularExpressions.Regex.IsMatch(hex, "[a-f0-9]{6}"))
            {
                return false;
            }

            return true;
        }

        static bool validate_eye(string color)
        {
            if (color == "amb" ||
                color == "blu" ||
                color == "brn" ||
                color == "gry" ||
                color == "grn" ||
                color == "hzl" ||
                color == "oth")
            {
                return true;
            }
            return false;
        }

        static bool validate_pid(string pid)
        {
            if (pid.Length != 9)
            {
                return false;
            }

            int pidint = int.Parse(pid);

            return true;
        }

        static bool validate_cid(string cid)
        {
            return true;
        }

        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-4\input-4.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            string line;
            int valid = 0;

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            System.Collections.Generic.Dictionary<string, string> record = new System.Collections.Generic.Dictionary<string, string>();
            while ((line = file.ReadLine()) != null)
            {
                // Process new record
                if (String.IsNullOrWhiteSpace(line)) {
                    if (record.ContainsKey("byr") &&
                        validate_year(record["byr"], 1920, 2002) &&
                        record.ContainsKey("iyr") &&
                        validate_year(record["iyr"], 2010, 2020) &&
                        record.ContainsKey("eyr") &&
                        validate_year(record["eyr"], 2020, 2030) &&
                        record.ContainsKey("hgt") &&
                        validate_height(record["hgt"]) &&
                        record.ContainsKey("hcl") &&
                        validate_color(record["hcl"]) &&
                        record.ContainsKey("ecl") &&
                        validate_eye(record["ecl"]) &&
                        record.ContainsKey("pid") &&
                        validate_pid(record["pid"]))
                    {
                        if (record.ContainsKey("cid") &&
                            validate_cid(record["cid"]))
                        {
                            valid++;
                        } else if (!record.ContainsKey("cid"))
                        {
                            valid++;
                        }
                    }
                    record = new System.Collections.Generic.Dictionary<string, string>();
                    continue;
                }

                foreach (string pair in line.Split())
                {
                    string[] splitPair = pair.Split(':');
                    string key = splitPair[0];
                    string value = splitPair[1];
                    record[key] = value;
                }
            }

            Console.WriteLine($"Valid Passports, Challenge 1: {valid}");
        }
    }
}
