﻿using System;
using Advent2020.Helpers;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var instructions = FileHelper.GetFileContents<string>("./data/data.txt");

            const int NORTH = 0;
            const int EAST = 90;
            const int SOUTH = 180;
            const int WEST = 270;

            const char MOVE_NORTH = 'N';
            const char MOVE_EAST = 'E';
            const char MOVE_SOUTH = 'S';
            const char MOVE_WEST = 'W';
            const char MOVE_FORWARD = 'F';
            const char ROTATE_RIGHT = 'R';
            const char ROTATE_LEFT = 'L';

            int currentDirection = EAST;
            int distanceVertically = 0;
            int distanceHorizontally = 0;

            foreach (string instruction in instructions)
            {
                char directive = instruction[0];
                int amount = int.Parse(instruction.Substring(1));
                Console.WriteLine($"Directive: {directive}, Amount: {amount}");

                continue;

                // brute force it with a switch for now
                switch(directive)
                {
                    case MOVE_NORTH:
                        distanceVertically += amount;
                        break;
                    case MOVE_SOUTH:
                        distanceVertically -= amount;
                        break;
                    case MOVE_EAST:
                        distanceHorizontally += amount;
                        break;
                    case MOVE_WEST:
                        distanceHorizontally -= amount;
                        break;
                    case MOVE_FORWARD:
                        if (currentDirection == EAST)
                            distanceHorizontally += amount;
                        if (currentDirection == WEST)
                            distanceHorizontally -= amount;
                        if (currentDirection == NORTH)
                            distanceVertically += amount;
                        if (currentDirection == SOUTH)
                            distanceVertically -= amount;
                        break;
                    case ROTATE_RIGHT:
                        if (amount != 360 && amount != 270 && amount != 180 && amount != 90 && amount != 0)
                        {
                            throw new Exception("Unknown rotation amount.");
                        }

                        if (currentDirection + amount > 360)
                            currentDirection = (currentDirection + amount) % 360;
                        
                        if (currentDirection + amount == 360)
                            currentDirection = NORTH;

                        if (currentDirection + amount < 360)
                            currentDirection = currentDirection + amount;

                        break;
                    case ROTATE_LEFT:
                        if (amount != 360 && amount != 270 && amount != 180 && amount != 90 && amount != 0)
                        {
                            throw new Exception("Unknown rotation amount.");
                        }

                        if (currentDirection - amount < 0)
                            currentDirection = 360 - Math.Abs(currentDirection - amount);
                        
                        if (currentDirection - amount >= 0)
                            currentDirection = currentDirection - amount;

                        break;
                    default:
                        throw new Exception("Undefined directive");
                        break;
                }
            }

            Console.WriteLine(Math.Abs(distanceHorizontally) + Math.Abs(distanceVertically));
        }
    }
}
