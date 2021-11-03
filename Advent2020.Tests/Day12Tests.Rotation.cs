using System;
using Advent2020.Constants;
using Xunit;

namespace Advent2020.Tests
{
    public class Rotation
    {
        [Fact]
        public void Right90DegreesIsEast()
        {
            int shipDirection = Direction.NORTH;
            shipDirection += 90;

            Assert.Equal(Direction.EAST, shipDirection);
        }
    }
}
