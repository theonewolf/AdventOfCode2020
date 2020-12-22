using System;
using System.Collections.Generic;

namespace AdventOfCode_22
{
    class Program
    {

        static void part1()
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-22\input-22.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            int count = 0;
            LinkedList<long> player1_deck = new LinkedList<long>();
            LinkedList<long> player2_deck = new LinkedList<long>();
            LinkedList<long> deck = player1_deck;

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            string line;

            while ((line = file.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line.Contains("Player"))
                {
                    count++;

                    if (count == 2)
                    {
                        deck = player2_deck;
                    }
                    continue;
                }

                deck.AddLast(long.Parse(line.Trim()));
            }

            // Play Rounds
            long player1_card = long.MinValue;
            long player2_card = long.MinValue;
            long round = 1;
            LinkedList<long> winner = null;

            // Play game
            while (winner == null)
            {
                Console.WriteLine($"-- Round {round} --");
                print_deck("Player 1's deck: ", player1_deck);
                print_deck("Player 2's deck: ", player2_deck);

                if (player1_deck.Count == 0)
                {
                    winner = player2_deck;
                    continue;
                } else if (player2_deck.Count == 0)
                {
                    winner = player1_deck;
                    continue;
                }

                player1_card = player1_deck.First.Value;
                player2_card = player2_deck.First.Value;

                Console.WriteLine($"Player 1 plays: {player1_card}");
                Console.WriteLine($"Player 2 plays: {player2_card}");

                player1_deck.RemoveFirst();
                player2_deck.RemoveFirst();

                if (player1_card > player2_card)
                {
                    Console.WriteLine("Player 1 wins the round!");
                    player1_deck.AddLast(player1_card);
                    player1_deck.AddLast(player2_card);
                } else
                {
                    Console.WriteLine("Player 2 wins the round!");
                    player2_deck.AddLast(player2_card);
                    player2_deck.AddLast(player1_card);
                }

                round++;
            }

            Console.WriteLine("== Post-game Results ==");
            print_deck("Player 1's deck: ", player1_deck);
            print_deck("Player 2's deck: ", player2_deck);

            // Calculate winning score
            LinkedListNode<long> current = winner.Last;
            long score = 0;
            long card_position = 1;
            while (current != null)
            {
                score += card_position * current.Value;
                card_position++;
                current = current.Previous;
            }

            Console.WriteLine($"Final Score: {score}");
        }

        private static void print_deck(string prefix, LinkedList<long> deck)
        {
            Console.Write(prefix + " ");

            foreach (long card in deck)
            {
                Console.Write(card + ", ");
            }

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            part1();
        }
    }
}
