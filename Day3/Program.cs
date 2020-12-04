using System;
using Advent2020.Helpers;

namespace Day3
{
    class Program
    {
        public static int MOVE_DISTANCE = 1;
        public static int VERTICAL_MOVE_DISTANCE = 2;
        static void Main(string[] args)
        {
            var terrain = FileHelper.GetFileContents<string>("./data/terrain.txt");
            int pos = 0;
            int trees = 0;
            int verticalMove = 0;
            bool firstRow = true;

            foreach (var line in terrain)
            {
                if (verticalMove % VERTICAL_MOVE_DISTANCE != 0 || firstRow) {
                    if (firstRow)
                    {
                        pos += MOVE_DISTANCE;
                        firstRow = false;
                    }

                    verticalMove++;
                    continue;  // skip this row
                }

                if (line[pos] == '#')
                    trees++;

                if (pos + MOVE_DISTANCE + 1 <= line.Length)
                {
                    pos += MOVE_DISTANCE;
                }
                else
                {
                    pos = CalculateWrap(pos, line.Length, MOVE_DISTANCE);
                }

                verticalMove++;
            }

            Console.WriteLine($"You will hit {trees} trees using this route.");
        }

        private static int CalculateWrap(int index, int terrainWidth, int moveDistance)
        {
            return index + moveDistance - terrainWidth;
        }
    }
}
