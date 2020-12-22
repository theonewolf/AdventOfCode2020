using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode_21
{
    class Program
    {
        static void part1()
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-21\input-21.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            Dictionary<string, HashSet<string>> allergens_and_ingredients = new Dictionary<string, HashSet<string>>();
            HashSet<string> all_ingredients = new HashSet<string>();
            List<string> full_list_of_ingredients = new List<string>();

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            string line;

            while ((line = file.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                line = line.Replace("(", "");
                line = line.Replace(")", "");
                string[] splitdata = line.Split("contains");

                full_list_of_ingredients.Add(splitdata[0]);
                HashSet<string> ingredients = splitdata[0].Split().Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToHashSet();
                HashSet<string> allergens = splitdata[1].Split(",").Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToHashSet();

                // Track all unique ingredients
                all_ingredients.UnionWith(ingredients);

                // For every allergen, we track the candidate ingredients that it could come from.
                // Each ingredient can only have 1 or 0 allergens.  And each allergen appears in only 1 ingredient.
                foreach (string allergen in allergens)
                {
                    if (!allergens_and_ingredients.ContainsKey(allergen))
                    {
                        allergens_and_ingredients[allergen] = new HashSet<string>(ingredients);
                    }

                    allergens_and_ingredients[allergen].IntersectWith(ingredients);
                }
            }

            // For any ingredient with 0 candidate allergens, count their occurrences in the original lines.
            int count = 0;
            List<string> zero_allergies = new List<string>();

            foreach (string ingredient in all_ingredients)
            {
                bool no_allergen = true;

                foreach (string allergen in allergens_and_ingredients.Keys)
                {
                    if (allergens_and_ingredients[allergen].Contains(ingredient))
                    {
                        no_allergen = false;
                    }
                }

                if (no_allergen)
                {
                    zero_allergies.Add(ingredient);
                }
            }

            foreach (string input_line in full_list_of_ingredients)
            {
                foreach (string ingredient in zero_allergies)
                {
                    // Note: need to account for ingredient alone.
                    //  1. Not as part of another string (need surrounding spaces),
                    //  2. OR at the beginning of the string.
                    count += Regex.Matches(input_line, "(^| )" + ingredient + " ").Count;
                }
            }

            Console.WriteLine($"Zero Allergen Ingredient occurrences: {count}");
        }
        static void Main(string[] args)
        {
            part1();
        }
    }
}
