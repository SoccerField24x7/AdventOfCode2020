using System;
using System.Collections.Generic;
using System.Linq;
using Advent2020.Helpers;

namespace ExpenseReport
{
    class Program
    {
        static void Main(string[] args)
        {
            var entries = FileHelper.GetFileContents<int>("./data/expenses.txt");
            
            var total = entries.Count;
            int i, x, z, answerOne = 0, answerTwo = 0;
            
            // Method One: nothing fancy, just loop

            for (i = 0; i < total-1; i++)
            {
                for (x = i + 1; x < total; x++)
                {
                    if (entries[i] + entries[x] == 2020)
                    {
                        // we have a winner
                        answerOne = entries[i] * entries[x];
                    }

                    for (z = x + 1; z < total; z++)
                    {
                        if (entries[i] + entries[x] + entries[z] == 2020)
                        {
                            answerTwo = entries[i] * entries[x] * entries[z];
                        }
                    }
                }
            }

            Console.WriteLine(answerOne);
            Console.WriteLine(answerTwo);
            
            // Method Two: Linq
            
            var result = entries.Where(value1 => entries.Any(value2 => value1 + value2 == 2020)).ToList();
            answerOne = result.Aggregate((a, b) => a * b);
            
            result = entries.Where(value1 => entries.Any(value2 => entries.Any(value3 => value1 + value2 + value3 == 2020))).ToList();
            answerTwo = result.Aggregate((a, b) => a * b);
            
            Console.WriteLine(answerOne);
            Console.WriteLine(answerTwo);
        }
    }
}