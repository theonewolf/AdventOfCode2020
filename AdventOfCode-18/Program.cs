using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_18
{
    class Program
    {


        static string reverse(string line)
        {
            string return_string = "";
            string number = "";
            int i = line.Length - 1;
            Console.WriteLine("Parse call");

            for (; i >= 0; i--)
            {
                switch (line[i])
                {
                    case '*':
                        return_string += '*';
                        break;
                    case '+':
                        return_string += '+';
                        break;
                    case '(':
                        if (number != "")
                        {
                            char[] arr = number.ToCharArray();
                            Array.Reverse(arr);
                            number = new string(arr);
                            return_string += number;
                            number = "";
                        }
                        return_string += ')';
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
                        if (number != "")
                        {
                            char[] arr = number.ToCharArray();
                            Array.Reverse(arr);
                            number = new string(arr);
                            return_string += number;
                            number = "";
                        }
                        return_string += '(';
                        break;
                    case ' ':
                        if (number != "")
                        {
                            char[] arr = number.ToCharArray();
                            Array.Reverse(arr);
                            number = new string(arr);
                            return_string += number;
                            number = "";
                        }
                        return_string += ' ';
                        break;
                    default:
                        throw new Exception();
                }
            }

            if (number != "")
            {
                char[] arr = number.ToCharArray();
                Array.Reverse(arr);
                number = new string(arr);
                return_string += number;
                number = "";
            } 

            return return_string;
        }

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
                        Console.WriteLine($"{previous_evaluation} * {evaluation}");
                        previous_evaluation *= evaluation;
                        i += parsed;
                        break;
                    case '+':
                        (parsed, evaluation) = parse(line.Substring(i + 1), false);
                        Console.WriteLine($"{previous_evaluation} + {evaluation}");
                        previous_evaluation += evaluation;
                        i += parsed;
                        break;
                    case '(':
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
                        (parsed, evaluation) = parse(line.Substring(i + 1), false);
                        previous_evaluation = evaluation;
                        i += parsed;
                        return new Tuple<int, int>(i, previous_evaluation);
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

        static Stack<string> eval_expression(string[] tokens)
        {
            Stack<string> stack = new Stack<string>();
            Stack<string> next = null;
            string operation = null;
            int parens = 0;

            foreach (string token in tokens)
            {
                switch (token)
                {
                    case "+":
                        operation = token;
                        stack.Push(operation);
                        break;
                    case "*":
                        operation = token;
                        stack.Push(operation);
                        break;
                    case "(":
                        parens++;
                        stack.Push(token);
                        operation = null;
                        break;
                    case ")":
                        List<string> parens_tokens = new List<string>();
                        while (stack.Peek() != "(")
                        {
                            parens_tokens.Add(stack.Pop());
                        }
                        string[] tokens_arr = parens_tokens.ToArray();
                        Array.Reverse(tokens_arr);
                        next = eval_expression(tokens_arr);
                        stack.Pop();
                        foreach (string val in next)
                        {
                            stack.Push(val);
                        }
                        parens--;
                        operation = null;
                        break;
                    default:
                        if (operation == null || parens != 0 || stack.Count > 2)
                        {
                            stack.Push(token);
                        } else
                        {
                            switch (operation)
                            {
                                case "+":
                                    stack.Pop();
                                    stack.Push((long.Parse(stack.Pop()) + long.Parse(token)).ToString());
                                    break;
                                case "*":
                                    stack.Pop();
                                    stack.Push((long.Parse(stack.Pop()) * long.Parse(token)).ToString());
                                    break;
                                default:
                                    throw new Exception();
                            }
                            operation = null;
                        }
                        break;
                }
            }
            return stack;
        }

        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-18\input-18.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            List<long> evaluations = new List<long>();

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            string line;

            while ((line = file.ReadLine()) != null)
            {
                //line = "1 + 2 * (3 + 4) * (5 + 6)";
                //line = "((2) * (2) + 1) * (1 + 1)";
                //line = "(9 + 9 + (3 * 6 + 6 * 2) * 9 + 4) * (7 * 4 + 3) * 4 + 3 + 9";
                line = line.Replace("(", "( ");
                line = line.Replace(")", " )");
                string[] tokens = line.Split();
                Stack<string> returnstack;
                while ((returnstack = eval_expression(tokens)).Count > 1)
                {
                    tokens = returnstack.ToArray();
                    Array.Reverse(tokens);
                }
                evaluations.Add(long.Parse(returnstack.Pop()));
                //break;
            }

            long sum = 0;
            foreach (long eval in evaluations)
            {
                sum += eval;
            }

            Console.WriteLine($"Total sum of evaluations = {sum}");
        }
    }
}
