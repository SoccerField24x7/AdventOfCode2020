using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advent2020.Constants;

namespace Advent2020.Day12
{
    public static class FerryMover
    {
        public static int RotateShip(int currentlyFacing, char rotateDirection, int amount)
        {
            int currentDirection = currentlyFacing;

            switch (rotateDirection)
            {
                case Rotate.RIGHT:
                    if (amount != 360 && amount != 270 && amount != 180 && amount != 90 && amount != 0)
                    {
                        throw new Exception("Unknown rotation amount.");
                    }

                    if (currentDirection + amount > 360)
                    {
                        currentDirection = (currentDirection + amount) % 360;
                    }
                    else if (currentDirection + amount == 360)
                    {
                        currentDirection = Direction.NORTH;
                    }
                    else if (currentDirection + amount < 360)
                    {
                        currentDirection = currentDirection + amount;
                    }
                    else
                    {
                        throw new Exception("Invalid 'R' evaluation");
                    }

                    break;

                case Rotate.LEFT:
                    if (amount != 360 && amount != 270 && amount != 180 && amount != 90 && amount != 0)
                    {
                        throw new Exception("Unknown rotation amount.");
                    }

                    if (currentDirection - amount < 0)
                    {
                        currentDirection = 360 - Math.Abs(currentDirection - amount);
                    }
                    else if (currentDirection - amount >= 0)
                    {
                        currentDirection = currentDirection - amount;
                    }
                    else
                    {
                        throw new Exception("Invalid 'L' Evaluation");
                    }

                    break;
            }

            return currentDirection;
        }
    }
}