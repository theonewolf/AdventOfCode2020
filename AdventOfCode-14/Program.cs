using System;
using System.Collections.Generic;

namespace AdventOfCode_14
{
    class Program
    {
        static void step1()
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-14\input-14.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            ulong[] bitmasks = new ulong[3];
            bitmasks[2] = 0x0000000fffffffff;
            Dictionary<ulong, ulong> instructions = new Dictionary<ulong, ulong>();

            string line;

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            while ((line = file.ReadLine()) != null)
            {
                // Values and memory are 36-bit values
                // mem[8] = 11, writes 11 into memory position 8
                // bitmask 0 or 1 overwrites the position
                // bitmask X leaves it alone
                // OR with the mask
                // AND with all 1's except the 0
                Console.WriteLine(line);

                if (line.StartsWith("mask"))
                {
                    string[] masksplit = line.Split();
                    string maskdata = masksplit[2];
                    bitmasks[0] = 0x0000000000000000;
                    bitmasks[1] = 0xffffffffffffffff;

                    for (int i = 0; i < maskdata.Length; i++)
                    {
                        switch (maskdata[i])
                        {
                            case 'X':
                                break;
                            case '1':
                                bitmasks[0] |= ((ulong)1) << (35 - i);
                                break;
                            case '0':
                                bitmasks[1] ^= ((ulong)1) << (35 - i);
                                break;
                            default:
                                Console.WriteLine($"Bad character in mask: {maskdata[i]}");
                                return;
                        }
                    }

                }
                else if (line.StartsWith("mem"))
                {
                    string[] instructionsplit = line.Split();
                    ulong position = ulong.Parse(instructionsplit[0].Split("[")[1].Split("]")[0]);
                    ulong value = ulong.Parse(instructionsplit[2]);
                    value |= bitmasks[0];
                    value &= bitmasks[1];
                    value &= bitmasks[2];
                    instructions[position] = value;
                }
                else
                {
                    Console.WriteLine($"Unexpected input: {line}");
                    return;
                }
            }

            ulong sum = 0;

            foreach (ulong position in instructions.Keys)
            {
                sum += instructions[position];
            }

            Console.WriteLine($"Sum of values in 36-bit memory: {sum}");
        }

        static void step2()
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-14\input-14.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            List<ulong[]> bitmasks = null;
            Dictionary<ulong, ulong> instructions = new Dictionary<ulong, ulong>();

            string line;

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            while ((line = file.ReadLine()) != null)
            {
                // Values and memory are 36-bit values
                // mem[8] = 11, writes 11 into memory position 8
                // bitmask 0 or 1 overwrites the position
                // bitmask X leaves it alone
                // OR with the mask
                // AND with all 1's except the 0
                Console.WriteLine(line);

                if (line.StartsWith("mask"))
                {
                    string[] masksplit = line.Split();
                    string maskdata = masksplit[2];

                    bitmasks = new List<ulong[]>();
                    bitmasks.Add(new ulong[3]);
                    bitmasks[0][0] = 0x0000000000000000;
                    bitmasks[0][1] = 0xffffffffffffffff;
                    bitmasks[0][2] = 0x0000000fffffffff;

                    for (int i = 0; i < maskdata.Length; i++)
                    {

                        switch (maskdata[i])
                        {
                            case 'X':
                                break;
                            case '1':
                                bitmasks[0][0] |= ((ulong)1) << (35 - i);
                                break;
                            case '0':
                                // 0 does nothing to memory addresses in Version 2
                                //bitmasks[0][1] ^= ((ulong)1) << (35 - i);
                                break;
                            default:
                                Console.WriteLine($"Bad character in mask: {maskdata[i]}");
                                return;
                        }
                    }

                    for (int i = 0; i < maskdata.Length; i++)
                    {
                        if (maskdata[i] == 'X')
                        {
                            List<ulong[]> newmasks = new List<ulong[]>();
                            foreach (ulong[] masks in bitmasks)
                            {
                                newmasks.Add(new ulong[3]);
                                newmasks[newmasks.Count - 1][0] = masks[0];
                                newmasks[newmasks.Count - 1][1] = masks[1] ^ ((ulong)1) << (35 - i);
                                newmasks[newmasks.Count - 1][2] = masks[2];
                                masks[0] |= ((ulong)1) << (35 - i);
                            }

                            foreach(ulong[] masks in newmasks)
                            {
                                bitmasks.Add(masks);
                            }
                        }
                    }

                }
                else if (line.StartsWith("mem"))
                {
                    string[] instructionsplit = line.Split();
                    ulong position = ulong.Parse(instructionsplit[0].Split("[")[1].Split("]")[0]);
                    ulong value = ulong.Parse(instructionsplit[2]);

                    foreach (ulong[] current_bitmasks in bitmasks)
                    {
                        position |= current_bitmasks[0];
                        position &= current_bitmasks[1];
                        position &= current_bitmasks[2];
                        instructions[position] = value;
                    }
                }
                else
                {
                    Console.WriteLine($"Unexpected input: {line}");
                    return;
                }
            }

            ulong sum = 0;

            foreach (ulong position in instructions.Keys)
            {
                sum += instructions[position];
            }

            Console.WriteLine($"Sum of values in 36-bit memory: {sum}");
        }

        static void Main(string[] args)
        {
            //step1();
            step2();
        }
    }
}
