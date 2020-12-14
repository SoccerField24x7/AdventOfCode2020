using System;
using Advent2020.Helpers;
using System.Collections.Generic;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataList = FileHelper.GetFileContents<double>("./data/data.txt");

            // outer loop looking for non-conformists
            var result = HasSum(dataList, 0);

            Console.WriteLine("Hello World!");
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
