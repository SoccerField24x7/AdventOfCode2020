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

            int currentDirection = Direction.EAST;
            ManhattanLocation position = new();

            foreach (string instruction in instructions)
            {
                char directive = instruction[0];
                int amount = int.Parse(instruction.Substring(1));

                // brute force it with a switch for now
                switch(directive)
                {
                    case Directive.MOVE_NORTH:
                    case Directive.MOVE_SOUTH:
                    case Directive.MOVE_EAST:
                    case Directive.MOVE_WEST:
                        position = FerryMover.MoveDirectionally(position, directive, amount);
                        break;

                    case Directive.MOVE_FORWARD:
                        position = FerryMover.MoveForward(position, currentDirection, amount);
                        break;

                    case Directive.ROTATE_RIGHT:
                    case Directive.ROTATE_LEFT:
                        currentDirection = FerryMover.RotateShip(currentDirection, directive, amount);

                        break;
                    default:
                        throw new Exception("Undefined directive");
                }
            }

            Console.WriteLine(Math.Abs(position.HorizontalPosition) + Math.Abs(position.VerticalPosition));
        }
    }
}
