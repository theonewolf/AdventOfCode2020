using System;
using System.Collections.Generic;

namespace AdventOfCode_7
{
    class Program
    {
        
        static void parse_sentence(string input, Dictionary<string, Dictionary<string, int>> bags)
        {
             
            string[] parts = input.Split("contain");
            string bagcolor = parts[0].Split("bags")[0].Trim();

            if (parts[1].Trim() == "no other bags.")
            {
                bags[bagcolor] = null;
            } else
            {
                string[] innerbags = parts[1].Replace(".", string.Empty).Split(",");
                
                if (!bags.ContainsKey(parts[0]))
                {
                    bags[bagcolor] = new Dictionary<string, int>();
                }
                
                foreach (string bag in innerbags)
                {
                    string[] bagparts = bag.Trim().Split();
                    int numbags = int.Parse(bagparts[0]);
                    string color = "";
                    for (int i = 1; i < bagparts.Length - 1; i++)
                    {
                        if (i + 1 < bagparts.Length - 1)
                        {
                            color += bagparts[i] + " ";
                        } else
                        {
                            color += bagparts[i];
                        }
                    }
                    bags[bagcolor][color] = numbags;
                }
            }
        }

        static int recurse_search(string target, string bag, Dictionary<string, Dictionary<string, int>> bags)
        {
            int count = 0;

            if (bags[bag] == null)
            {
                return count;
            }

            foreach (var innerbag in bags[bag])
            {
                if (innerbag.Key == target)
                {
                    count += 1;
                    continue;
                }

                count += recurse_search(target, innerbag.Key, bags);
            }
            return count > 0 ? 1 : 0;
        }

        static int bag_counter(string bag, Dictionary<string, Dictionary<string, int>> bags)
        {
            int count = 0;

            if (bags[bag] == null) {
                return 0;
            }

            foreach (var innerbag in bags[bag])
            {
                count += innerbag.Value + innerbag.Value * bag_counter(innerbag.Key, bags); 
            }

            return count;
        }

        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-7\input-7.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            string line;
            int shinygold_capacity = 0;
            Dictionary<string, Dictionary<string, int>> bags = new Dictionary<string, Dictionary<string, int>>();

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            while ((line = file.ReadLine()) != null)
            {
                parse_sentence(line, bags);
            }

            foreach (var outerbag in bags)
            {
                shinygold_capacity += recurse_search("shiny gold", outerbag.Key, bags);
            }

            int bagsinshinygold = bag_counter("shiny gold", bags);

            Console.WriteLine($"Bags that can contain shiny gold, directly or indirectly: {shinygold_capacity}");
            Console.WriteLine($"Bags contained inside shiny gold: {bagsinshinygold}");
        }
    }
}
