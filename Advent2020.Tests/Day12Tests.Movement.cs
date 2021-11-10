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


        }

        [Fact]
        public void WaypointRotateRightFrom90()
        {
            ManhattanLocation waypoint = new(10, 4);

            waypoint = FerryMover.RotateWaypoint(waypoint, Directive.ROTATE_RIGHT, 90);

            Assert.Equal(waypoint.HorizontalPosition, 4);
            Assert.Equal(waypoint.VerticalPosition, -10);
        }

        [Fact]
        public void WaypointRotateLeft90From90()
        {
            ManhattanLocation waypoint = new(10, 4);

            waypoint = FerryMover.RotateWaypoint(waypoint, Directive.ROTATE_LEFT, 90);

            Assert.Equal(waypoint.HorizontalPosition, -4);
            Assert.Equal(waypoint.VerticalPosition, 10);
        }
    }
}