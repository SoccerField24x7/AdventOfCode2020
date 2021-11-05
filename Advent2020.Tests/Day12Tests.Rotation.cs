namespace Advent2020.Tests
{
    using Advent2020.Constants;
    using Advent2020.Day12;
    using Xunit;

    public class Rotation
    {
        [Fact]
        public void NorthTurningRight90DegreesIsEast()
        {
            int shipDirection = Direction.NORTH;
            shipDirection = FerryMover.RotateShip(shipDirection, Rotate.RIGHT, 90);

            Assert.Equal(Direction.EAST, shipDirection);
        }

        [Fact]
        public void NorthTurningRight180DegreesIsSouth()
        {
            int shipDirection = Direction.NORTH;
            shipDirection = FerryMover.RotateShip(shipDirection, Rotate.RIGHT, 180);

            Assert.Equal(Direction.SOUTH, shipDirection);
        }

        [Fact]
        public void NorthTurningRight270DegreesIsWest()
        {
            int shipDirection = Direction.NORTH;
            shipDirection = FerryMover.RotateShip(shipDirection, Rotate.RIGHT, 270);

            Assert.Equal(Direction.WEST, shipDirection);
        }

        [Fact]
        public void NorthTurningRight360DegreesIsNorth()
        {
            int shipDirection = Direction.NORTH;
            shipDirection = FerryMover.RotateShip(shipDirection, Rotate.RIGHT, 360);

            Assert.Equal(Direction.NORTH, shipDirection);
        }

        [Fact]
        public void EastTurningRight360DegreesIsEast()
        {
            int shipDirection = Direction.EAST;
            shipDirection = FerryMover.RotateShip(shipDirection, Rotate.RIGHT, 360);

            Assert.Equal(Direction.EAST, shipDirection);
        }

        [Fact]
        public void EastTurningLeft90DegreesIsNorth()
        {
            int shipDirection = Direction.EAST;
            shipDirection = FerryMover.RotateShip(shipDirection, Rotate.LEFT, 90);

            Assert.Equal(Direction.NORTH, shipDirection);
        }

        [Fact]
        public void EastTurningLeft180DegreesIsWest()
        {
            int shipDirection = Direction.EAST;
            shipDirection = FerryMover.RotateShip(shipDirection, Rotate.LEFT, 180);

            Assert.Equal(Direction.WEST, shipDirection);
        }
    }
}
