using System;
using System.Linq;
using Advent2020.Helpers;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = FileHelper.GetFileContents<string>("./data/passwords.txt");
            int validPasswordCount = 0;
            int validPasswordByPositionCount = 0;

            foreach (var item in list)
            {
                var parts = item.Split(":");
                var rules = parts[0].Trim();
                var password = parts[1].Trim();
                validPasswordCount += IsValidPassword(rules, password);
                validPasswordByPositionCount += IsValidPasswordByPosition(rules, password);
            }

            Console.WriteLine($"There were {validPasswordCount} valid passwords for test 1.");
            Console.WriteLine($"There were {validPasswordByPositionCount} valid passwords for test 2.");
        }

        private static int IsValidPassword(string rules, string password)
        {
            // TODO: make this section reusable
            var ruleParts = rules.Split(" ");
            var bounds = ruleParts[0].Split("-");

            int count = password.Count(f => f == ruleParts[1][0]);

            if (count >= int.Parse(bounds[0]) && count <= int.Parse(bounds[1]))
            {
                return 1;
            }

            return 0;
        }

        private static int IsValidPasswordByPosition(string rules, string password)
        {
            var ruleParts = rules.Split(" ");
            var positions = ruleParts[0].Split("-");
            
            bool positionOneGood, positionTwoGood;

            positionOneGood = password[int.Parse(positions[0]) - 1] == ruleParts[1][0];
            positionTwoGood = password[int.Parse(positions[1]) - 1] == ruleParts[1][0];

            if ((positionOneGood && !positionTwoGood) || (!positionOneGood && positionTwoGood))
            {
                return 1;
            }

            return 0;
        }
    }
}
