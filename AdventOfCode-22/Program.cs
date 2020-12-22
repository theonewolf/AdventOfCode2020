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

        private static string round_string(LinkedList<long> player1_deck, LinkedList<long> player2_deck)
        {
            string retstring = "";

            retstring += "Player 1 Deck: [";
            foreach (long card in player1_deck)
            {
                retstring += $"{card},";
            }

            retstring += "] Player 2 Deck: [";
            foreach (long card in player2_deck)
            {
                retstring += $"{card},";
            }

            retstring += "]";

            return retstring;
        }

        private static long play_game(LinkedList<long> player1_deck, LinkedList<long> player2_deck)
        {
            // Play Rounds
            long player1_card = long.MinValue;
            long player2_card = long.MinValue;
            long round = 1;
            long winner = 0;
            List<string> played_rounds = new List<string>();

            // Play game
            while (winner == 0)
            {
                //Console.WriteLine($"-- Round {round} --");
                //print_deck("Player 1's deck: ", player1_deck);
                //print_deck("Player 2's deck: ", player2_deck);

                string current_round = round_string(player1_deck, player2_deck);

                // Before either player deals a card,
                // if there was a previous round in this game that had exactly the same cards in the same order in the same players' decks,
                // the game instantly ends in a win for player 1. Previous rounds from other games are not considered.
                // (This prevents infinite games of Recursive Combat, which everyone agrees is a bad idea.)
                if (played_rounds.Contains(current_round))
                {
                    winner = 1;
                    continue;
                }

                played_rounds.Add(current_round);

                player1_card = player1_deck.First.Value;
                player2_card = player2_deck.First.Value;

                //Console.WriteLine($"Player 1 plays: {player1_card}");
                //Console.WriteLine($"Player 2 plays: {player2_card}");

                player1_deck.RemoveFirst();
                player2_deck.RemoveFirst();

                // If both players have at least as many cards remaining in their deck as the value of the card they just drew,
                // the winner of the round is determined by playing a new game of Recursive Combat (see below).
                if (player1_deck.Count >= player1_card &&
                    player2_deck.Count >= player2_card)
                {
                    // ***(the quantity of cards copied is equal to the number on the card they drew to trigger the sub-game)***
                    LinkedList<long> copy1 = new LinkedList<long>();
                    LinkedList<long> copy2 = new LinkedList<long>();

                    int counter = 0;

                    foreach (long value in player1_deck)
                    {
                        copy1.AddLast(value);
                        counter++;
                        if (counter == player1_card)
                        {
                            break;
                        }
                    }

                    counter = 0;
                    foreach (long value in player2_deck)
                    {
                        copy2.AddLast(value);
                        counter++;
                        if (counter == player2_card)
                        {
                            break;
                        }
                    }

                    winner = play_game(copy1, copy2);
                }

                // Otherwise, at least one player must not have enough cards left in their deck to recurse; the winner of the round is the player with the higher-value card.
                // As in regular Combat, the winner of the round (even if they won the round by winning a sub-game) takes
                // the two cards dealt at the beginning of the round and places them on the bottom of their own deck
                // (again so that the winner's card is above the other card).
                if (winner == 1 || ((winner != 2) && (player1_card > player2_card)))
                {
                    //Console.WriteLine("Player 1 wins the round!");
                    player1_deck.AddLast(player1_card);
                    player1_deck.AddLast(player2_card);
                }
                else if (winner == 2 || ((winner != 1) && (player2_card > player1_card)))
                {
                    //Console.WriteLine("Player 2 wins the round!");
                    player2_deck.AddLast(player2_card);
                    player2_deck.AddLast(player1_card);
                }

                winner = 0;

                // If collecting cards by winning the round causes a player to have all of the cards, they win, and the game ends.
                if (player1_deck.Count == 0)
                {
                    winner = 2;
                    continue;
                }
                else if (player2_deck.Count == 0)
                {
                    winner = 1;
                    continue;
                }

                round++;
            }

            //Console.WriteLine("== Post-game Results ==");
            //print_deck("Player 1's deck: ", player1_deck);
            //print_deck("Player 2's deck: ", player2_deck);

            return winner;
        }

        private static void part2()
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

            long winner = play_game(player1_deck, player2_deck);

            // Calculate winning score
            LinkedListNode<long> current = (winner == 1) ? player1_deck.Last : player2_deck.Last;
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

        static void Main(string[] args)
        {
            //part1();
            part2();
        }
    }
}
