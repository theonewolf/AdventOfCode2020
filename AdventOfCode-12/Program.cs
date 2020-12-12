using System;
using System.Collections.Generic;

namespace AdventOfCode_12
{
    class Program
    {
        static void part1()
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-12\input-12.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            List<Tuple<char, int>> orders = new List<Tuple<char, int>>();
            // 0 is north, 90 is east, 180 is south, 270 is west, 360 becomes north again (mod 360 degrees)
            int facing = 90;
            int gridX = 0;
            int gridY = 0;

            string line;

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            while ((line = file.ReadLine()) != null)
            {
                char order = line[0];
                int value = int.Parse(line.Substring(1));
                orders.Add(new Tuple<char, int>(order, value));
                Console.WriteLine($"Parsed new order: {order} => {value}");

                switch (order)
                {
                    case 'N':
                        gridY += value;
                        break;
                    case 'E':
                        gridX += value;
                        break;
                    case 'S':
                        gridY -= value;
                        break;
                    case 'W':
                        gridX -= value;
                        break;
                    case 'F':
                        switch (facing)
                        {
                            case 0:
                                gridY += value;
                                break;
                            case 90:
                                gridX += value;
                                break;
                            case -90:
                                gridX -= value;
                                break;
                            case 180:
                                gridY -= value;
                                break;
                            case -180:
                                gridY -= value;
                                break;
                            case 270:
                                gridX -= value;
                                break;
                            case -270:
                                gridX += value;
                                break;
                            default:
                                Console.WriteLine($"Unhandled degrees: {facing}");
                                return;
                        }
                        break;
                    case 'L':
                        facing -= value;
                        facing %= 360;
                        break;
                    case 'R':
                        facing += value;
                        facing %= 360;
                        break;
                    default:
                        Console.WriteLine("Unknown order!");
                        return;
                }
                Console.WriteLine($"Update || bx: {gridX}, y: {gridY}, degrees: {facing}");
            }

            Console.WriteLine($"Final x: {gridX}, Final y: {gridY}, Final degrees: {facing}");
            Console.WriteLine($"Manhattan (0,0) -> ({gridX}, {gridY}) = {Math.Abs(gridX) + Math.Abs(gridY)}");
        }

        static void part2()
        {
            string fileName = @"C:\Users\wolfg\source\repos\AdventofCode2020\AdventofCode-12\input-12.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            List<Tuple<char, int>> orders = new List<Tuple<char, int>>();
            // 0 is north, 90 is east, 180 is south, 270 is west, 360 becomes north again (mod 360 degrees)
            // Origin is 0,0, North is positive, South negative, East positive, West negative
            int facing = 90;
            // waypoint 0,0 is shipX, shipY
            int waypointX = 10;
            int waypointY = 1;
            // ship 0,0 is actual origin
            int shipX = 0;
            int shipY = 0;

            string line;
            double radians = 0;
            int newwaypointX = 0;
            int newwaypointY = 0;

            Console.WriteLine(fileName);
            Console.WriteLine(file);

            while ((line = file.ReadLine()) != null)
            {
                char order = line[0];
                int value = int.Parse(line.Substring(1));
                orders.Add(new Tuple<char, int>(order, value));
                Console.WriteLine($"Parsed new order: {order} => {value}");

                switch (order)
                {
                    case 'N':
                        waypointY += value;
                        break;
                    case 'E':
                        waypointX += value;
                        break;
                    case 'S':
                        waypointY -= value;
                        break;
                    case 'W':
                        waypointX -= value;
                        break;
                    case 'F':
                        shipX += value * waypointX;
                        shipY += value * waypointY;
                        // waypoint offset remains _the same_
                        break;
                    case 'L':
                        // waypoint rotation
                        radians = (Math.PI / 180) * value;
                        newwaypointX = waypointX * (int) Math.Cos(radians) - waypointY * (int) Math.Sin(radians);
                        newwaypointY = waypointY * (int) Math.Cos(radians) + waypointX * (int) Math.Sin(radians);
                        waypointX = newwaypointX;
                        waypointY = newwaypointY;
                        break;
                    case 'R':
                        // waypoint rotation
                        radians = (Math.PI / 180) * -value;
                        newwaypointX = waypointX * (int) Math.Cos(radians) - waypointY * (int) Math.Sin(radians);
                        newwaypointY = waypointY * (int) Math.Cos(radians) + waypointX * (int) Math.Sin(radians);
                        waypointX = newwaypointX;
                        waypointY = newwaypointY;
                        break;
                    default:
                        Console.WriteLine("Unknown order!");
                        return;
                }
                Console.WriteLine($"Update || x: {shipX}, y: {shipY}, wayX: {waypointX}, wayY: {waypointY}");
            }

            Console.WriteLine($"Manhattan (0,0) -> ({shipX}, {shipY}) = {Math.Abs(shipX) + Math.Abs(shipY)}");
        }

        static void Main(string[] args)
        {
            //part1();
            part2();
        }
    }
}
