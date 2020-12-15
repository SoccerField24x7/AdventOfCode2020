using System;
using Advent2020.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataList = FileHelper.GetFileContents<double>("./data/data.txt");

            // outer loop looking for non-conformists
            int i;
            double target = 0;
            for (i=25; i < dataList.Count; i++)
            {
                List<double> subset = new List<double>();
                subset = dataList.GetRange(i-25, 25);
                target = dataList[i];
                var result = HasSum(subset, target);
                if (!result)
                {
                    // no gots!
                    Console.WriteLine($"The first failing number is {target} at position {i}");
                    break;
                }
            }

            var contig = FindContiguousRun(dataList, target);
            Console.WriteLine($"The encryption weakness is {contig.Min() + contig.Max()}");
        }

        public static List<double> FindContiguousRun(List<double> dataList, double targetValue)
        {
            int i, highWaterMark = 0, inner;
            double thisSum;
            List<double> contig = new List<double>();

            for (i = highWaterMark; i < dataList.Count; i++)
            {
                thisSum = 0;
                for (inner=highWaterMark; inner < dataList.Count ; inner++) // need a better high bound
                {
                    thisSum += dataList[inner];

                    if (thisSum >= targetValue)
                    {
                        break;
                    }
                }

                if (thisSum == targetValue)
                {
                    return dataList.GetRange(highWaterMark, inner - highWaterMark + 1);
                }

                highWaterMark++;
            }

            return contig;
        }

        public static bool HasSum(List<double> dataList, double targetValue)
        {
            int i, highWaterMark = 0, inner;
            
            for (i=highWaterMark; i < dataList.Count -1; i++)
            {
                for (inner = 0; inner < dataList.Count; inner++)
                {
                    if (dataList[i] + dataList[inner] == targetValue)
                        return true;
                }

                highWaterMark++;
            }

            return false;
        }
    }
}
