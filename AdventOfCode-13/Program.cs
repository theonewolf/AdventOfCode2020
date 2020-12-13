using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode_13
{

    /* Reference: https://rosettacode.org/wiki/Chinese_remainder_theorem#C.23 */
    class ChineseRemainderTheorem
    {
        public static long Solve(long[] n, long[] a)
        {
            long prod = n.Aggregate((long) 1, (i, j) => i * j);
            long p;
            long sm = 0;
            for (int i = 0; i < n.Length; i++)
            {
                p = prod / n[i];
                sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
            }
            return sm % prod;
        }

        private static long ModularMultiplicativeInverse(long a, long mod)
        {
            long b = a % mod;
            for (long x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-13\input-13.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);


            string line;

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            int timestamp = int.Parse(file.ReadLine());
            List<int> busids = new List<int>();
            string[] busidsline = file.ReadLine().Split(",");

            List<(int, int)> offsets = new List<(int, int)>();
            int offset = 0;
            foreach (string busid in busidsline)
            {
                if (busid == "x")
                {
                    offset++;
                    continue;
                }
                busids.Add(int.Parse(busid));
                offsets.Add((int.Parse(busid), offset));
                offset++;
            }

            int mintime = int.MaxValue;
            int minbus = -1;

            foreach(int busid in busids)
            {
                int result = (int) Math.Ceiling((double)timestamp / (double)busid);
                int closetime = result * busid;
                Console.WriteLine($"Timestamp = {timestamp}, Closetime = {closetime}, Closetime - Timestamp = {closetime - timestamp}");

                // Get the closest time _in the future_ 
                if (closetime > timestamp &&  (closetime - timestamp) < mintime)
                {
                    mintime = Math.Abs(timestamp - closetime);
                    minbus = busid;
                }
            }

            Console.WriteLine($"mintime = {mintime}, minbus = {minbus}, multiple = {minbus * mintime}");

            int i = 0;
            List<int> remainders = new List<int>();
            foreach((int busid, int busoffset) in offsets)
            {
                //Console.WriteLine($"busid = {busid}, offset = {busoffset}");
                //Console.WriteLine($"modulus should be: {busid - busoffset}");
                int remainder = ((((busoffset / busid) + 1) * busid) - busoffset) % busid;
                Console.WriteLine($"busid = {busid} remainder: {remainder}");
                remainders.Add(remainder);
                i++;
            }

            /* Need to replace with Chinese Remainder Theorem efficient implementation... */
            /* Note: all input n_i are prime */
            long[] n_i = new long[offsets.Count];
            long[] a_i = new long[offsets.Count];
            ChineseRemainderTheorem crt = new ChineseRemainderTheorem();

            for (int n = 0; n < offsets.Count; n++)
            {
                n_i[n] = (long) offsets[n].Item1;
                a_i[n] = (long) remainders[n];

                Console.WriteLine($"n_i = {n_i[n]}, a_i = {a_i[n]}");
            }

            long x = ChineseRemainderTheorem.Solve(n_i, a_i);

            Console.WriteLine($"Target timestamp = {x}");
        }
    }
}
