using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace AdventOfCode_25
{

    class Program
    {

        static void part1()
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-25\input-25.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            long[] public_keys = new long[2];
            long subject_number = 7;
            long door_secret_loop_size = 0;
            long card_secret_loop_size = 0;
            long door_secret_key = 0;
            long card_secret_key = 0;

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            string line;

            int counter = 0;
            while ((line = file.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    public_keys[counter++] = long.Parse(line);
                }
            }

            // The handshake used by the card and the door involves an operation that transforms a subject number.
            // To transform a subject number, start with the value 1.
            // Then, a number of times called the loop size, perform the following steps:
            //    Set the value to itself multiplied by the subject number.
            //    Set the value to the remainder after dividing the value by 20201227.
            // The card always uses a specific, secret loop size when it transforms a subject number.
            // The door always uses a different, secret loop size.
            long value = 1;
            while (value != public_keys[0])
            {
                value *= subject_number;
                value %= 20201227;
                card_secret_loop_size++;
            }

            value = 1;
            while (value != public_keys[1])
            {
                value *= subject_number;
                value %= 20201227;
                door_secret_loop_size++;
            }

            // The card transforms the subject number of 7 according to the card's secret loop size. The result is called the card's public key.
            // The door transforms the subject number of 7 according to the door's secret loop size. The result is called the door's public key.
            // The card and door use the wireless RFID signal to transmit the two public keys(your puzzle input) to the other device.
            // Now, the card has the door's public key, and the door has the card's public key.
            // Because you can eavesdrop on the signal, you have both public keys, but neither device's loop size.
            // The card transforms the subject number of the door's public key according to the card's loop size. The result is the encryption key.
            // The door transforms the subject number of the card's public key according to the door's loop size. The result is the same encryption key as the card calculated.
            card_secret_key = 1;
            for (int i = 0; i < card_secret_loop_size; i++)
            {
                card_secret_key *= public_keys[1];
                card_secret_key %= 20201227;
            }

            Console.WriteLine($"Door public key: {public_keys[1]}");
            Console.WriteLine($"Card secret loop size: {card_secret_loop_size}");
            Console.WriteLine($"Card secret key: {card_secret_key}");

            door_secret_key = 1;
            for (int i = 0; i < door_secret_loop_size; i++)
            {
                door_secret_key *= public_keys[0];
                door_secret_key %= 20201227;
            }

            Console.WriteLine($"Card public key: {public_keys[0]}");
            Console.WriteLine($"Door secret loop size: {door_secret_loop_size}");
            Console.WriteLine($"Door secret key: {door_secret_key}");

            // If you can use the two public keys to determine each device's loop size, you will have enough information to calculate the secret encryption key
            if (card_secret_key != door_secret_key)
            {
                throw new Exception();
            }

            Console.WriteLine($"Secret key: {card_secret_key}");
        }

        static void part2()
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-25\input-25-test.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            string line;

            while ((line = file.ReadLine()) != null)
            {
            }
        }

            static void Main(string[] args)
            {
                part1();
                // No second challenge!
                //part2();
            }
        }
    }