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

            // Reset everything for Part II
            currentSeating = FileHelper.GetFileContents<string>("./data/data.txt");
            lastSeating.Clear();
            newSeating.Clear();
            count = 0;

            while (true)
            {
                for (var i=0; i < currentSeating.Count; i++)
                {
                    evaluatedRow = string.Empty;
                    for (var x=0; x < currentSeating[i].Length; x++)
                    {
                        var viewedFromSeat = LookAroundMe(currentSeating, i, x);

                        evaluatedRow += EvaluateSeat(viewedFromSeat, currentSeating[i][x], 5);
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

            foreach (var row in lastSeating)
            {
                count += row.Length - row.Replace(OCCUPIED.ToString(), "").Length;
            }

            Console.WriteLine(count);
        }

        public static char EvaluateSeat(List<char> seatsAroundMe, char thisSeatStatus, int surroundingSeatLimit = 4)
        {
            if (thisSeatStatus == OPEN)
            {
                // we can get cute later, for now just remove the garbage squares
                var cleanBlock = seatsAroundMe.Where(x => x != 'Z' && x != '.').ToList();

                if (cleanBlock.All(x => x == OPEN))
                {
                    return OCCUPIED; // If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
                }
                else
                {
                    return OPEN;  // Otherwise, the seat's state does not change.
                }
            }
            else if (thisSeatStatus == FLOOR)
            {
                return FLOOR;
            }
            else
            {
                // If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
                if (seatsAroundMe.Where(x => x == OCCUPIED).Count() >= surroundingSeatLimit)
                {
                    return OPEN;
                }
                else
                {
                    return thisSeatStatus;
                }
            }
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

        public static List<char> LookAroundMe(List<string> layout, int row, int col)
        {
            // what can I see from this seat?
            List<char> viewedFromSeat = new();
            viewedFromSeat.Add(LookUp(layout, row, col));
            viewedFromSeat.Add(LookDown(layout, row, col));
            viewedFromSeat.Add(LookLeft(layout, row, col));
            viewedFromSeat.Add(LookUpLeft(layout, row, col));
            viewedFromSeat.Add(LookDownLeft(layout, row, col));
            viewedFromSeat.Add(LookRight(layout, row, col));
            viewedFromSeat.Add(LookUpRight(layout, row, col));
            viewedFromSeat.Add(LookDownRight(layout, row, col));

            return viewedFromSeat;
        }

        public static char LookUp(List<string> layout, int row, int col)
        {
            int BOUNDRY = 0;

            if (row == BOUNDRY)
            {
                return 'Z';
            }

            int i = row;

            while (--i >= 0 && layout[i][col] != OPEN && layout[i][col] != OCCUPIED && i >= 0)
            {
                continue;
            }

            if (i == BOUNDRY - 1)
            {
                return 'Z';
            }

            return layout[i][col];
        }

        public static char LookDown(List<string> layout, int row, int col)
        {
            int BOUNDRY = layout.Count - 1;

            if (row == BOUNDRY)
            {
                return 'Z';
            }

            int i = row;

            while (++i <= BOUNDRY && layout[i][col] != OPEN && layout[i][col] != OCCUPIED)
            {
                continue;
            }

            if (i == BOUNDRY + 1)
            {
                return 'Z';
            }

            return layout[i][col];
        }

        public static char LookLeft(List<string> layout, int row, int col)
        {
            int BOUNDRY = 0;

            if (col == BOUNDRY)
            {
                return 'Z';
            }

            int i = col;

            while (--i >= BOUNDRY && layout[row][i] != OPEN && layout[row][i] != OCCUPIED)
            {
                continue;
            }

            if (i == BOUNDRY - 1)
            {
                return 'Z';
            }

            return layout[row][i];
        }

        public static char LookRight(List<string> layout, int row, int col)
        {
            int BOUNDRY = layout[row].Length - 1;

            if (col == BOUNDRY)
            {
                return 'Z';
            }

            int i = col;

            while (++i <= BOUNDRY && layout[row][i] != OPEN && layout[row][i] != OCCUPIED)
            {
                continue;
            }

            if (i == BOUNDRY + 1)
            {
                return 'Z';
            }

            return layout[row][i];
        }

        public static char LookUpLeft(List<string> layout, int row, int col)
        {
            int TOP_BOUNDRY = 0;
            int LEFT_BOUNDRY = 0;

            if (col == LEFT_BOUNDRY || row == TOP_BOUNDRY)
            {
                return 'Z';
            }

            int i = col;
            int x = row;

            while (--i >= LEFT_BOUNDRY && --x >= TOP_BOUNDRY  && layout[x][i] != OPEN && layout[x][i] != OCCUPIED)
            {
                continue;
            }

            if (i == LEFT_BOUNDRY - 1 || x == TOP_BOUNDRY - 1)
            {
                return 'Z';
            }

            return layout[x][i];
        }

        public static char LookUpRight(List<string> layout, int row, int col)
        {
            int TOP_BOUNDRY = 0;
            int RIGHT_BOUNDRY = layout[row].Length - 1;

            if (col == RIGHT_BOUNDRY || row == TOP_BOUNDRY)
            {
                return 'Z';
            }

            int i = col;
            int x = row;

            while (++i <= RIGHT_BOUNDRY && --x >= TOP_BOUNDRY  && layout[x][i] != OPEN && layout[x][i] != OCCUPIED)
            {
                continue;
            }

            if (i == RIGHT_BOUNDRY + 1 || x == TOP_BOUNDRY - 1)
            {
                return 'Z';
            }

            return layout[x][i];
        }

        public static char LookDownLeft(List<string> layout, int row, int col)
        {
            int BOTTOM_BOUNDRY = layout.Count - 1;
            int LEFT_BOUNDRY = 0;

            if (col == LEFT_BOUNDRY || row == BOTTOM_BOUNDRY)
            {
                return 'Z';
            }

            int i = col;
            int x = row;

            while (--i >= LEFT_BOUNDRY && ++x <= BOTTOM_BOUNDRY  && layout[x][i] != OPEN && layout[x][i] != OCCUPIED)
            {
                continue;
            }

            if (i == LEFT_BOUNDRY - 1 || x == BOTTOM_BOUNDRY + 1)
            {
                return 'Z';
            }

            return layout[x][i];
        }

        public static char LookDownRight(List<string> layout, int row, int col)
        {
            int BOTTOM_BOUNDRY = layout.Count - 1;
            int RIGHT_BOUNDRY = layout[row].Length - 1;

            if (col == RIGHT_BOUNDRY || row == BOTTOM_BOUNDRY)
            {
                return 'Z';
            }

            int i = col;
            int x = row;

            while (++i <= RIGHT_BOUNDRY && ++x <= BOTTOM_BOUNDRY  && layout[x][i] != OPEN && layout[x][i] != OCCUPIED)
            {
                continue;
            }

            if (i == RIGHT_BOUNDRY + 1 || x == BOTTOM_BOUNDRY + 1)
            {
                return 'Z';
            }

            return layout[x][i];
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

                result += EvaluateSeat(seatBlock, evalSeat);
            }

            return result;
        }
    }
}
