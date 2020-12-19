using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode_19
{
    class Program
    {
        static void print_rule(List<string> tokens)
        {
            Console.WriteLine("----------");
            Console.WriteLine(string_rule(tokens));
            Console.WriteLine("\n----------");
        }

        static string string_rule(List<string> tokens)
        {
            string rulestring = "";
            foreach (string token in tokens)
            {
                switch (token)
                {
                    case "\"a\"":
                        rulestring += "a";
                        break;
                    case "\"b\"":
                        rulestring += "b";
                        break;
                    case "|":
                    default:
                        rulestring += token;
                        break;
                }
            }
            return rulestring;
        }

        static void expand_rules(Dictionary<string, List<string>> rules)
        {
            bool applied = true;

            rules["0"].Insert(0, "^");
            rules["0"].Add("$");

            while (applied)
            {
                applied = false;
                print_rule(rules["0"]);
                List<string> target = rules["0"];
                List<string> updated = new List<string>();
                foreach (string token in target)
                {
                    switch (token)
                    {
                        case "(":
                        case ")":
                        case "^":
                        case "$":
                        case "|":
                        case "+":
                        case "!":
                        case "?":
                        case "'":
                        case "O":
                        case "C":
                        case "-":
                        case "\"a\"":
                        case "\"b\"":
                            updated.Add(token);
                            break;
                        default:
                            applied = true;
                            updated.Add("(");
                            foreach (string innertoken in rules[token])
                            {
                                updated.Add(innertoken);
                            }
                            updated.Add(")");
                            break;
                    }
                }
                rules["0"] = updated;
                print_rule(rules["0"]);
            }
        }

        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-19\input-19-2.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            Dictionary<string, List<string>> raw_rules = new Dictionary<string, List<string>>();
            List<string> valid_strings = new List<string>();
            
            Console.WriteLine(fileName);
            Console.WriteLine(file);

            string line;

            while ((line = file.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                string[] rule = line.Split(":");
                raw_rules[rule[0]] = new List<string>();
                foreach (string token in rule[1].Split())
                {
                    if (!string.IsNullOrEmpty(token))
                    {
                        raw_rules[rule[0]].Add(token);
                    }
                }
            }

            expand_rules(raw_rules);
            string pattern = string_rule(raw_rules["0"]);

            while ((line = file.ReadLine()) != null)
            {
                if (Regex.IsMatch(line, pattern))
                {
                    valid_strings.Add(line);
                }
            }

            Console.WriteLine($"Valid strings: {valid_strings.Count}");
        }
    }
}
