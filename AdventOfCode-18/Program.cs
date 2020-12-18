using System;
using System.Collections.Generic;

namespace AdventOfCode_18
{
    class Program
    {
        // parses and evaluates a single expression at a time
        static Tuple<int, int> parse(string line, bool leftmost)
        {
            int evaluation = 0;
            int previous_evaluation = 0;
            string number = "";
            int i = 0;
            int parsed = 0;
            Console.WriteLine("Parse call");

            for (; i < line.Length; i++)
            {
                Console.WriteLine($"leftmost == {leftmost}, i == {i}, line == '{line}', line.Length == {line.Length}, line[i] == '{line[i]}', previous_evaluation == {previous_evaluation}, evaluation == {evaluation}, number == '{number}'");
                switch(line[i])
                {
                    case '*':
                        (parsed, evaluation) = parse(line.Substring(i + 1), false);
                        previous_evaluation *= evaluation;
                        i += parsed + 1;
                        break;
                    case '+':
                        (parsed, evaluation) = parse(line.Substring(i + 1), false);
                        previous_evaluation += evaluation;
                        i += parsed + 1;
                        break;
                    case '(':
                        (parsed, evaluation) = parse(line.Substring(i + 1), false);
                        previous_evaluation = evaluation;
                        i += parsed + 1;
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        number += line[i];
                        break;
                    case ')':
                    case ' ':
                    case '\n':
                    case '\r':
                        if (number != "")
                        {
                            previous_evaluation = int.Parse(number);
                            number = "";
                            if (!leftmost)
                            {
                                Console.WriteLine($"Returning: ({i}, {previous_evaluation})");
                                return new Tuple<int, int>(i, previous_evaluation);
                            }
                        }
                        continue;
                    default:
                        throw new Exception();
                }
            }

            if (previous_evaluation == 0 && number != "")
            {
                previous_evaluation = int.Parse(number);
            }

            Console.WriteLine($"Returning: ({i}, {previous_evaluation})");

            return new Tuple<int, int>(i, previous_evaluation);
        }
        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-18\input-18-test-8.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            List<int> evaluations = new List<int>();

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            string line;

            while ((line = file.ReadLine()) != null)
            {
                var (_, evaluation) = parse(line, true);
                evaluations.Add(evaluation);
            }

            int sum = 0;
            foreach (int eval in evaluations)
            {
                sum += eval;
            }

            Console.WriteLine($"Total sum of evaluations = {sum}");
        }
    }
}
