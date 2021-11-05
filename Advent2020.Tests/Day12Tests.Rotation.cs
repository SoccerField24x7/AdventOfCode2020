using System;
using Advent2020.Constants;
using Xunit;

namespace Advent2020.Tests
{
    public class Rotation
    {
        public const char ROTATE_RIGHT = 'R';
        public const char ROTATE_LEFT = 'L';

        [Fact]
        public void NorthTurningRight90DegreesIsEast()
        {
            int shipDirection = Direction.NORTH;
            shipDirection = RotateShip(shipDirection, 'R', 90);

            Assert.Equal(Direction.EAST, shipDirection);
        }

        [Fact]
        public void NorthTurningRight180DegreesIsSouth()
        {
            int shipDirection = Direction.NORTH;
            shipDirection = RotateShip(shipDirection, 'R', 180);

            Assert.Equal(Direction.SOUTH, shipDirection);
        }

        [Fact]
        public void NorthTurningRight270DegreesIsWest()
        {
            int shipDirection = Direction.NORTH;
            shipDirection = RotateShip(shipDirection, 'R', 270);

            Assert.Equal(Direction.WEST, shipDirection);
        }

        [Fact]
        public void NorthTurningRight360DegreesIsNorth()
        {
            int shipDirection = Direction.NORTH;
            shipDirection = RotateShip(shipDirection, 'R', 360);

            Assert.Equal(Direction.NORTH, shipDirection);
        }

        [Fact]
        public void EastTurningRight360DegreesIsEast()
        {
            int shipDirection = Direction.EAST;
            shipDirection = RotateShip(shipDirection, 'R', 360);

            Assert.Equal(Direction.EAST, shipDirection);
        }

        [Fact]
        public void EastTurningLeft90DegreesIsNorth()
        {
            int shipDirection = Direction.EAST;
            shipDirection = RotateShip(shipDirection, 'L', 90);

            Assert.Equal(Direction.NORTH, shipDirection);
        }

        [Fact]
        public void EastTurningLeft180DegreesIsWest()
        {
            int shipDirection = Direction.EAST;
            shipDirection = RotateShip(shipDirection, 'L', 180);

            Assert.Equal(Direction.WEST, shipDirection);
        }

        // TODO: move this to a common/shared class
        private int RotateShip(int currentlyFacing, char rotateDirection, int amount)
        {
            int currentDirection = currentlyFacing;

            switch (rotateDirection)
            {
                case ROTATE_RIGHT:
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

                case ROTATE_LEFT:
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
