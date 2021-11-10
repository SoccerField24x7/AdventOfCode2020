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

        public static ManhattanLocation MoveDirectionally(ManhattanLocation position, char direction, int amount, int multiplier = 1)
        {
            ManhattanLocation endingPosition = new(position.HorizontalPosition, position.VerticalPosition);

            switch (direction)
            {
                case Directive.MOVE_NORTH:
                    endingPosition.VerticalPosition += amount;
                    break;
                case Directive.MOVE_SOUTH:
                    endingPosition.VerticalPosition -= amount;
                    break;
                case Directive.MOVE_EAST:
                    endingPosition.HorizontalPosition += amount;
                    break;
                case Directive.MOVE_WEST:
                    endingPosition.HorizontalPosition -= amount;
                    break;
            }

            return endingPosition;
        }

        public static ManhattanLocation MoveForward(ManhattanLocation position, int directionFacing, int amount, int multiplier = 1)
        {
            ManhattanLocation endingPosition = new(position.HorizontalPosition, position.VerticalPosition);

            switch (directionFacing)
            {
                case Direction.EAST:
                    endingPosition.HorizontalPosition += amount * multiplier;
                    break;

                case Direction.WEST:
                    endingPosition.HorizontalPosition -= amount * multiplier;
                    break;

                case Direction.NORTH:
                    endingPosition.VerticalPosition += amount * multiplier;
                    break;

                case Direction.SOUTH:
                    endingPosition.VerticalPosition -= amount * multiplier;
                    break;

                default:
                    throw new Exception("Invalid move direction.");
            }

            return endingPosition;
        }

        public static ManhattanLocation MoveToWaypoint(ManhattanLocation waypointLocation, ManhattanLocation shipLocation, int multiplier)
        {
            ManhattanLocation endingWaypointPosition = new(waypointLocation.HorizontalPosition * multiplier, waypointLocation.VerticalPosition * multiplier);
            
            ManhattanLocation endingShipPosition = new(endingWaypointPosition.HorizontalPosition + shipLocation.HorizontalPosition, endingWaypointPosition.VerticalPosition + shipLocation.VerticalPosition);

            return endingShipPosition;
        }

        public static ManhattanLocation RotateWaypoint( ManhattanLocation waypointLocation, char rotateDirection, int amount)
        {
            if (amount != 360 && amount != 270 && amount != 180 && amount != 90 && amount != 0)
            {
                throw new Exception("Unknown rotation amount.");
            }

            ManhattanLocation endingPosition = new(waypointLocation.HorizontalPosition, waypointLocation.VerticalPosition);

            switch (amount)
            {
                case 90:
                    endingPosition.VerticalPosition = waypointLocation.HorizontalPosition * -1;
                    endingPosition.HorizontalPosition = waypointLocation.VerticalPosition;
                    if (rotateDirection == Directive.ROTATE_LEFT)
                    {
                        endingPosition.VerticalPosition = endingPosition.VerticalPosition * -1; // undo
                        endingPosition.HorizontalPosition = endingPosition.HorizontalPosition * -1;
                    }
                    break;

                case 180:
                    endingPosition.VerticalPosition = waypointLocation.HorizontalPosition * -1;
                    endingPosition.HorizontalPosition = waypointLocation.VerticalPosition * -1;
                    break;

                case 270:
                    endingPosition.VerticalPosition = waypointLocation.HorizontalPosition;
                    endingPosition.HorizontalPosition = waypointLocation.VerticalPosition * -1;
                    if (rotateDirection == Directive.ROTATE_LEFT)
                    {
                        endingPosition.VerticalPosition = endingPosition.VerticalPosition * -1; 
                        endingPosition.HorizontalPosition = endingPosition.HorizontalPosition * -1; // undo
                    }
                    break;

                case 360:
                case 0:
                    break;
            }

            return endingPosition;
        }
    }
}