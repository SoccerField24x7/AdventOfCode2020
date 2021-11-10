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
            
            ship = FerryMover.MoveToWaypoint(waypoint, ship, 10);

            Assert.Equal(100, ship.HorizontalPosition);
            Assert.Equal(10, ship.VerticalPosition);

            waypoint.HorizontalPosition = 10;
            waypoint.VerticalPosition = 1;

            waypoint = FerryMover.MoveDirectionally(waypoint, 'N', 3);

            Assert.Equal(10, waypoint.HorizontalPosition);
            Assert.Equal(4, waypoint.VerticalPosition);

            ship = FerryMover.MoveToWaypoint(waypoint, ship, 7);

            Assert.Equal(170, ship.HorizontalPosition);
            Assert.Equal(38, ship.VerticalPosition);

            waypoint = FerryMover.RotateWaypoint(waypoint, Directive.ROTATE_RIGHT, 90);
            
            Assert.Equal(4, waypoint.HorizontalPosition);
            Assert.Equal(-10, waypoint.VerticalPosition);

            ship = FerryMover.MoveToWaypoint(waypoint, ship, 11);

            Assert.Equal(214, ship.HorizontalPosition);
            Assert.Equal(-72, ship.VerticalPosition);
        }
    }
}