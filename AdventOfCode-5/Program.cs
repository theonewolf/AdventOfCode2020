using System;

namespace AdventOfCode_5
{
    class Program
    {
        static int compute_row(string rowstring)
        {
            // F is a 0 (low bit)
            // B is a 1 (high bit)
            rowstring = rowstring.Replace('F', '0');
            rowstring = rowstring.Replace('B', '1');
            return Convert.ToInt32(rowstring, 2);
        }

        static int compute_column(string columnstring)
        {
            // L is a 0 (low bit)
            // R is a 1 (high bit)
            columnstring = columnstring.Replace('L', '0');
            columnstring = columnstring.Replace('R', '1');

            return Convert.ToInt32(columnstring, 2);
        }

        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-5\input-5.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            string line;
            int high_id = 0;
            System.Collections.Generic.List<int> ids = new System.Collections.Generic.List<int>();

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            System.Collections.Generic.Dictionary<string, string> record = new System.Collections.Generic.Dictionary<string, string>();
            while ((line = file.ReadLine()) != null)
            {
                string rowstring = line.Substring(0, 7);
                string columnstring = line.Substring(7, 3);

                int row = compute_row(rowstring);
                int column = compute_column(columnstring);
                int id = row * 8 + column;
                ids.Add(id);
                
                if (id > high_id)
                {
                    high_id = id;
                }
            }

            Console.WriteLine($"High ID: {high_id}");

            ids.Sort();
            for (int i = 0; i < ids.Count; i++)
            {
                if (ids[i+1] - ids[i] > 1)
                {
                    Console.WriteLine($"Missing ID: {ids[i] + 1}");
                    break;
                }
            }
        }
    }
}
