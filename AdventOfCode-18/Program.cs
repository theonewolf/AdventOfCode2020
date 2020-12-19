using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_18
{
    class Program
    {

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

        static Stack<string> eval_expression2(string[] tokens)
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
                        next = eval_expression2(tokens_arr);
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
                        }
                        else
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

        static int precedence(string token)
        {
            switch (token)
            {
                case "+":
                    return 2;
                case "*":
                    return 1;
                default:
                    return -1;
            }
        }

        static List<string> to_postfix(string[] tokens)
        {
            List<string> return_tokens = new List<string>();

            Stack<string> stack = new Stack<string>();

            foreach (string token in tokens)
            {
                switch (token)
                {
                    case "*":
                    case "+":
                        while (stack.Count > 0 && precedence(token) <= precedence(stack.Peek()))
                        {
                            return_tokens.Add(stack.Pop());
                        }
                        stack.Push(token);
                        break;
                   case "(":
                        stack.Push(token);
                        break;
                    case ")":
                        while (stack.Peek() != "(")
                        {
                            return_tokens.Add(stack.Pop());
                        }
                        stack.Pop();
                        break;
                    default:
                        return_tokens.Add(token);
                        break;
                }
            }

            while (stack.Count > 0)
            {
                return_tokens.Add(stack.Pop());
            }

            return return_tokens;
        }

        static long postfix_eval(List<string> tokens)
        {
            Stack<string> stack = new Stack<string>();

            foreach (string token in tokens)
            {
                switch(token)
                {
                    case "*":
                        long lhs = long.Parse(stack.Pop());
                        long rhs = long.Parse(stack.Pop());
                        stack.Push((lhs * rhs).ToString());
                        break;
                    case "+":
                        long lhs2 = long.Parse(stack.Pop());
                        long rhs2 = long.Parse(stack.Pop());
                        stack.Push((lhs2 + rhs2).ToString());
                        break;
                    default:
                        stack.Push(token);
                        break;
                }
            }

            return long.Parse(stack.Pop());
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
            }

            long sum = 0;
            foreach (long eval in evaluations)
            {
                sum += eval;
            }

            Console.WriteLine($"Total sum of evaluations = {sum}");

            file = new System.IO.StreamReader(fileName);
            evaluations = new List<long>();
            while ((line = file.ReadLine()) != null)
            {
                line = line.Replace("(", "( ");
                line = line.Replace(")", " )");
                string[] tokens = line.Split();
                evaluations.Add(postfix_eval(to_postfix(tokens)));
            }

            sum = 0;
            foreach (long eval in evaluations)
            {
                sum += eval;
            }

            Console.WriteLine($"Total sum of evaluations = {sum}");
        }
    }
}
