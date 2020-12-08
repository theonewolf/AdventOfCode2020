using System;
using System.Collections.Generic;

namespace AdventOfCode_8
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-8\input-8-fixed.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            string line;
            List<KeyValuePair<string, int>> instructions = new List<KeyValuePair<string, int>>();
          
            
            int accumulator = 0;
            int instruction_counter = 0;
            HashSet<int> visited_instructions = new HashSet<int>();

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            while ((line = file.ReadLine()) != null)
            {
                string[] data = line.Split();
                string instruction = data[0].Trim();
                int value = int.Parse(data[1]);

                instructions.Add(new KeyValuePair<string, int>(instruction, value));
            }

            while (instruction_counter < instructions.Count &&
                   !visited_instructions.Contains(instruction_counter))
            {
                visited_instructions.Add(instruction_counter);

                switch(instructions[instruction_counter].Key)
                {
                    case "nop":
                        Console.WriteLine($"NOP visited: {instructions[instruction_counter].Value}");
                        Console.WriteLine($"NOP -> JMP LINE: {instruction_counter + 1}");
                        Console.WriteLine($"NOP -> JMP, instruction counter: {instruction_counter + instructions[instruction_counter].Value}");
                        instruction_counter++;
                        break;
                    case "acc":
                        Console.WriteLine($"ACC visited: {instructions[instruction_counter].Value}");
                        accumulator += instructions[instruction_counter].Value;
                        instruction_counter++;
                        break;
                    case "jmp":
                        Console.WriteLine($"JMP visited: {instructions[instruction_counter].Value}");
                        Console.WriteLine($"JMP -> NOP LINE: {instruction_counter + 1}");
                        Console.WriteLine($"JMP -> NOP, instruction counter: {instruction_counter + 1}");
                        instruction_counter += instructions[instruction_counter].Value;
                        break;
                    default:
                        Console.WriteLine("ILLEGAL INSTRUCTION.");
                        break;
                }
            }

            Console.WriteLine($"Final Accumulator value: {accumulator}");
            Console.WriteLine($"Instruction Counter: {instruction_counter}");
        }
    }
}
