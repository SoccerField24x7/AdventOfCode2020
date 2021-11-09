using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent2020.Day12
{
    public class ManhattanLocation
    {
        public int VerticalPosition { get; set; }
        public int HorizontalPosition { get; set; }

        public ManhattanLocation()
        {}

        public ManhattanLocation(int hPos, int vPos)
        {
            HorizontalPosition = hPos;
            VerticalPosition = vPos;
        }

        public void SetPosition(int hPos, int vPos)
        {
            HorizontalPosition = hPos;
            VerticalPosition = vPos;
        }
    }
}