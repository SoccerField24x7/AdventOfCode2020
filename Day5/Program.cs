using System;
using System.Collections.Generic;
using System.Linq;
using Advent2020.Helpers;

namespace Day5
{
    class Program
    {
        public static readonly int ROW_HIGH = 127;
        public static readonly int COLUMN_HIGH = 7;

        static void Main(string[] args)
        {
            var seatAssignments = FileHelper.GetFileContents<string>("./data/seats.txt");

            int highSeat = 0;
            List<int> seatList = new List<int>();

            foreach (var ticket in seatAssignments)
            {
                string rows = ticket.Substring(0, ticket.Length - 3);
                string columns = ticket.Substring(ticket.Length - 3);
                
                int row = GetValueFromSequence(0, ROW_HIGH, rows);
                int column = GetValueFromSequence(0, COLUMN_HIGH, columns);
                int seatId = row * 8 + column;
                
                seatList.Add(seatId);

                highSeat = seatId > highSeat ? seatId : highSeat;
            }

            Console.WriteLine($"The highest seat number is {highSeat}");

            // now let's see what's missing
            List<int> sortedList = seatList.OrderBy(c => c).ToList();
            List<int> gaps = Enumerable.Range(sortedList.First(), sortedList.Last() - sortedList.First() + 1).Except(sortedList).ToList(); // 4270321 FTW!

            if (gaps.Count > 1)
            {
                Console.WriteLine("Too many seat gaps!");
                return;
            }

            Console.Write($"Your seat number is {gaps[0]}");
        }

        static int GetValueFromSequence(int low, int high, string sequence)
        {
            while (low != high)
            {
                foreach (char i in sequence)
                {
                    (low, high) = DoSplit(low, high, i);
                }
            }

            return low;
        }

        static (int low, int high) DoSplit(int low, int high, char splitType)
        {
            //F means to take the lower half, keeping rows 0 through 63
            if (splitType == 'F' || splitType == 'L')
            {
                var totalRows = high - low;
                var mid = totalRows / 2;

                return (low, low + mid);
            }

            if (splitType == 'B' || splitType == 'R')
            {
                var totalRows = high - low;
                
                var mid = (int)Math.Round(totalRows / 2d);
                
                return (totalRows > 1 ? low + mid : high, high);
            }

            return (-1, -1);  // We had an invalid character, return error
        }
    }
}
