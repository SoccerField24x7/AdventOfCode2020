using System;
using System.Collections.Generic;
using Advent2020.Helpers;

namespace Day11
{
    class Program
    {
        public static readonly string OCCUPIED = "#";
        public static readonly string OPEN = "L";
        public static readonly string FLOOR = ".";

        static void Main(string[] args)
        {
            var dataList = FileHelper.GetFileContents<string>("./data/sample.txt");

            // If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
            // If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
            // Otherwise, the seat's state does not change.

            // int evaluatingRow = 0;
            List<string> newSeatingChart = new();
            string result;

            for (var i=0; i < dataList.Count; i++) // start at 1 because we need the second row to determine seating for the first
            {
                // conditional selection of rows for evaluation
                if (i == 0)
                {
                   result = EvaluateRow(dataList.GetRange(i, 2), true, false);
                }
                else if (i + 1 == dataList.Count)
                {
                    result = EvaluateRow(dataList.GetRange(i-1, 2), false, true);
                }
                else
                {
                    result = EvaluateRow(dataList.GetRange(i-1, 3), false, false);
                }

                newSeatingChart.Add(result);
            }

            Console.WriteLine("*********************");

            foreach (string row in newSeatingChart)
            {
                Console.WriteLine(row);
            }
        }

        public static string EvaluateRow(List<string> rows, bool firstBlock, bool lastBlock)
        {
            if (rows.Count > 3 || rows.Count < 2)
            {
                throw new Exception("Wrong number of rows!");
            }

            if (firstBlock && lastBlock)
            {
                throw new Exception("Can't be the first and the last evaluation block.");
            }

            // foreach (var row in rows)
            // {
            //     Console.WriteLine(row);
            // }

            // Console.WriteLine("-");

            if (firstBlock)
            {
                rows.Insert(0, new string('.', rows[1].Length));
            }
            else if (lastBlock)
            {
                rows.Add(new string('.', rows[1].Length));
            }
            else
            {
                //result = EvaluateBlock(dataList.GetRange(i-1, 3), false, false);
            }

            return GetNewSeatAssignmentsForRow(rows);
        }

        public static string GetNewSeatAssignmentsForRow(List<string> rows)
        {
            string result;
            for (var i=0; i < rows.Count; i++)
            {
                if (i == 0)
                {
                    if (rows[1][i] == OPEN)
                    {
                        rows[0][i] == OPEN || 
                    }
                    else
                    {
                        result += OPEN; // move this to outside the ifs
                    }
                }
                else if (i + 1 == rows.Count)
                {

                }
                else
                {
                    
                }
            }

            return "";
        }
    }
}
