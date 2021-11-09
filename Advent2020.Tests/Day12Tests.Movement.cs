namespace Advent2020.Tests
{
    using Advent2020.Constants;
    using Advent2020.Day12;
    using Xunit;

    public class Movement
    {
        [Fact]
        public void Control()
        {
            ManhattanLocation waypoint = new(10, 1);
            ManhattanLocation ship = new();
            int facing = Direction.EAST;
            
            var position = FerryMover.MoveForward(waypoint, facing, 10, 10);

            Assert.Equal(100, position.HorizontalPosition);
            Assert.Equal(10, position.VerticalPosition);
        }
    }
}