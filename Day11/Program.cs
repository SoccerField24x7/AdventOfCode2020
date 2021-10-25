using System;
using System.Collections.Generic;
using System.Linq;
using Advent2020.Helpers;

namespace Day11
{
    class Program
    {
        public static readonly char OCCUPIED = '#';
        public static readonly char OPEN = 'L';
        public static readonly char FLOOR = '.';

        static void Main(string[] args)
        {
            var currentSeating = FileHelper.GetFileContents<string>("./data/data.txt");

            List<string> newSeating = new();
            List<string> lastSeating = new();
            string evaluatedRow;

            while (true)
            {
                for (var i=0; i < currentSeating.Count; i++)
                {
                    // conditional selection of rows for evaluation: 2 for first and last rows, 3 for all middle evals
                    if (i == 0)
                    {
                    evaluatedRow = PadRowsIfNecessaryAndEvaluate(currentSeating.GetRange(i, 2), true, false);
                    }
                    else if (i + 1 == currentSeating.Count)
                    {
                        evaluatedRow = PadRowsIfNecessaryAndEvaluate(currentSeating.GetRange(i-1, 2), false, true);
                    }
                    else
                    {
                        evaluatedRow = PadRowsIfNecessaryAndEvaluate(currentSeating.GetRange(i-1, 3), false, false);
                    }

                    newSeating.Add(evaluatedRow);
                }

                if (newSeating.SequenceEqual(lastSeating))
                {
                    break;
                }

                // reset: make current seating chart the latest evaluation
                currentSeating.Clear();
                currentSeating.AddRange(newSeating);
                
                // reset: make the last seating chart the latest evaluation
                lastSeating.Clear();
                lastSeating.AddRange(newSeating);

                // finally, reset the new seating chart to prepare for next eval
                newSeating.Clear();
            }

            // count the overall results
            int count = 0;
            foreach (var row in lastSeating)
            {
                count += row.Length - row.Replace(OCCUPIED.ToString(), "").Length;
            }

            Console.WriteLine(count);
        }

        public static string PadRowsIfNecessaryAndEvaluate(List<string> rows, bool firstBlock, bool lastBlock)
        {
            if (rows.Count > 3 || rows.Count < 2)
            {
                throw new Exception("Wrong number of rows!");
            }

            if (firstBlock && lastBlock)
            {
                throw new Exception("Can't be the first and the last evaluation block.");
            }

            if (firstBlock)
            {
                rows.Insert(0, new string('.', rows[1].Length));
            }
            else if (lastBlock)
            {
                rows.Add(new string('.', rows[1].Length));
            }

            return GetNewSeatAssignmentsForRow(rows);
        }

        public static string GetNewSeatAssignmentsForRow(List<string> rows)
        {
            string result = string.Empty;
            
            var thisRow = rows[1];
            var aboveRow = rows[0];
            var belowRow = rows[2];

            for (var i=0; i < rows[1].Length; i++)
            {
                List<char> seatBlock = new();
                var evalSeat = rows[1][i];

                seatBlock.Add(aboveRow[i]);
                seatBlock.Add(belowRow[i]);

                // pad the far right and left positions with Z (out of bounds)
                seatBlock.Add(i == thisRow.Length - 1 ? 'Z' : aboveRow[i+1]);
                seatBlock.Add(i == thisRow.Length - 1 ? 'Z' : thisRow[i+1]);
                seatBlock.Add(i == thisRow.Length - 1 ? 'Z' : belowRow[i+1]);

                seatBlock.Add(i == 0 ? 'Z' : aboveRow[i-1]);
                seatBlock.Add(i == 0 ? 'Z' : thisRow[i-1]);
                seatBlock.Add(i == 0 ? 'Z' : belowRow[i-1]);

                if (evalSeat == OPEN)
                {
                    // we can get cute later, for now just remove the garbage squares
                    var cleanBlock = seatBlock.Where(x => x != 'Z' && x != '.').ToList();

                    if (cleanBlock.All(x => x == OPEN))
                    {
                        result += OCCUPIED; // If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
                    }
                    else
                    {
                        result += OPEN;  // Otherwise, the seat's state does not change.
                    }
                }
                else if (evalSeat == FLOOR)
                {
                    result += FLOOR;
                }
                else
                {
                    // If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
                    if (seatBlock.Where(x => x == OCCUPIED).Count() > 3)
                    {
                        result += OPEN;
                    }
                    else
                    {
                        result += evalSeat;
                    }
                }
            }

            return result;
        }
    }
}
