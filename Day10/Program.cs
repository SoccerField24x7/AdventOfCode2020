using System;
using Advent2020.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataList = FileHelper.GetFileContents<int>("./data/data.txt");
            dataList.Sort();

            var deviceLimit = dataList[^1] + 3;

            int last = 0;
            int oneDiff = 0;
            int threeDiff = 0;
            int totalDiff = 0;
            foreach(var adapter in dataList)
            {
                int diff = adapter - last;
                if (diff <= 0 || diff > 3)
                {
                    Console.WriteLine("Error!");
                }

                if (diff == 1)
                {
                    oneDiff++;
                }

                if (diff == 3)
                {
                    threeDiff++;
                }

                totalDiff += diff;
                last = adapter;
            }

            totalDiff += 3;

            double partOneAnswer = oneDiff * (threeDiff + 1); // last adapter to phone.


            // Part II
            int[] deltas = GetDeltas(dataList.ToArray());

            double count = GetContiguousOnesCounts(deltas).Aggregate(1L, (sum, cur) => sum *= PossibleCombinations(cur));

        }

        // Needed an assist on this one! https://dev.to/rpalo/advent-of-code-2020-solution-megathread-day-10-adapter-array-33ea
        protected static int[] GetDeltas(int[] values)
        {
            var deltas = new int[values.Length + 1];
            for (var i = 1; i < values.Length; i++)
            {
                deltas[i] = values[i] - values[i - 1];
            }

            deltas[0] = values[0];        // joltage between outlet and first adapter.
            deltas[values.Length] = 3;    // joltage between last adapter and device.

            return deltas;
        }

        private static IEnumerable<int> GetContiguousOnesCounts(IEnumerable<int> deltas)
        {
            int contiguousOnes = 0;
            foreach(var delta in deltas)
            {
                if (delta == 1)
                {
                    contiguousOnes++;
                    continue;
                }

                if (contiguousOnes == 0)
                {
                    continue;
                }

                yield return contiguousOnes;
                contiguousOnes = 0;
            }
        }

        private static long PossibleCombinations(int seqLength)
            => seqLength switch
            {
                1 => 1,
                2 => 2,
                3 => 4,
                4 => 7,
                5 => 13,
                6 => 22,
                _ => 0
            };
    }
}
