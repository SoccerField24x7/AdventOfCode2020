using System;
using Advent2020.Constants;
using Advent2020.Day12;
using Advent2020.Helpers;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var instructions = FileHelper.GetFileContents<string>("./data/data.txt");

            const char MOVE_NORTH = 'N';
            const char MOVE_EAST = 'E';
            const char MOVE_SOUTH = 'S';
            const char MOVE_WEST = 'W';
            const char MOVE_FORWARD = 'F';
            const char ROTATE_RIGHT = 'R';
            const char ROTATE_LEFT = 'L';

            int currentDirection = Direction.EAST;
            int distanceVertically = 0;
            int distanceHorizontally = 0;

            foreach (string instruction in instructions)
            {
                char directive = instruction[0];
                int amount = int.Parse(instruction.Substring(1));

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
                        if (currentDirection == Direction.EAST)
                            distanceHorizontally += amount;
                        if (currentDirection == Direction.WEST)
                            distanceHorizontally -= amount;
                        if (currentDirection == Direction.NORTH)
                            distanceVertically += amount;
                        if (currentDirection == Direction.SOUTH)
                            distanceVertically -= amount;
                        break;
                    case ROTATE_RIGHT:
                    case ROTATE_LEFT:
                        currentDirection = FerryMover.RotateShip(currentDirection, directive, amount);

                        break;
                    default:
                        throw new Exception("Undefined directive");
                }
            }

            Console.WriteLine(Math.Abs(distanceHorizontally) + Math.Abs(distanceVertically));
        }
    }
}
