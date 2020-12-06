using System;
using System.Collections.Generic;

namespace AdventOfCode_6
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-6\input-6.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            string line;
            int family = 0;
            int member = 0;
            int yesquestions = 0;
            int yesintersectquestions = 0;
            List<HashSet<char>> families = new List<HashSet<char>>();
            List<List<HashSet<char>>> familymembers = new List<List<HashSet<char>>>();
            
            Console.WriteLine(fileName);
            Console.WriteLine(file);

            families.Add(new HashSet<char>());
            familymembers.Add(new List<HashSet<char>>());
            while ((line = file.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    // new family
                    yesquestions += families[family].Count;
                    family++;
                    member = 0;
                    families.Add(new HashSet<char>());
                    familymembers.Add(new List<HashSet<char>>());
                    continue;
                }

                familymembers[family].Add(new HashSet<char>());
                foreach (char c in line)
                {
                    families[family].Add(c);
                    familymembers[family][member].Add(c);
                }
                member++;
            }

            yesquestions += families[family].Count;

            Console.WriteLine($"Total Unique Yes Questions per Family: {yesquestions}");

            // Intersect family members...
            foreach (List<HashSet<char>> familylist in familymembers)
            {
                HashSet<char> initialmember = familylist[0];

                foreach(HashSet<char> familymemberset in familylist)
                {
                    initialmember.IntersectWith(familymemberset);
                }

                yesintersectquestions += initialmember.Count;
            }

            Console.WriteLine($"Total Unanimous Unique Yes Questions per family: {yesintersectquestions}");
        }
    }
}
