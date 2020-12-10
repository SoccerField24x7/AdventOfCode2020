using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day7
{
    class Program
    {
        public static readonly string MY_BAG_TYPE = "shiny gold";
        public static Dictionary<string,string> bagRules;
        static void Main(string[] args)
        {
            bagRules = ParseRules("./data/rules.txt");
            List<string> allBags = new List<string>();

            // first get the bags that directly contain our bag
            var result = GetOuterBags(MY_BAG_TYPE);
            
            // now recursively find all of the bags that can contain bags that contain our bag
            foreach (var bag in result)
            {
                allBags.AddRange(GetContainingBags("0 " + bag.Key.Trim()));
            }

            var unique = allBags.Where(x => x != MY_BAG_TYPE).Distinct().ToList();

            Console.WriteLine($"There are {unique.Count} bags that can contain our bag.");

            // get the bags directly in our bag
            result = GetInnerBagList(MY_BAG_TYPE);
            
            int count = 0;
            foreach (var bag in result)
            {
                // now get the bags in our bag
                count += GetInnerBags(bag.Value.Trim(), 1);
            }

            Console.WriteLine($"There are {count} individual bags in your bag!");
        }

        private static List<KeyValuePair<string,string>> GetOuterBags(string bagName)
        {
            return bagRules.Where(x => x.Value.Contains(bagName)).ToList();
        }

        private static List<KeyValuePair<string, string>> GetInnerBagList(string bagName)
        {
            return bagRules.Where(x => x.Key.Contains(bagName)).ToList();
        }

        private static int GetInnerBags(string bagName, int multiplier)
        {
            int count = 0;

            Regex regex = new Regex(@"(\d) ((.*?) (.*?) | bag)");
            var v = regex.Matches(bagName);
            foreach(var result in v)
            {
                var parts = result.ToString().Trim().Split(" ", 2);

                string bag = result.ToString().Trim(); 
                
                var recursiveBags = GetInnerBagList(parts[1]);
                count += int.Parse(parts[0]) * multiplier;
                foreach (var recursiveBag in recursiveBags)
                {
                    if (recursiveBag.Value.Contains("no other"))
                        continue;

                    count += GetInnerBags(recursiveBag.Value.Trim(), int.Parse(parts[0])*multiplier);
                }
            }

            return count;
        }

        private static List<string> GetContainingBags(string bagList)
        {
            var results = new List<string>();

            Regex regex = new Regex(@"(?! \d) ((.*?) (.*?) | bag)");
            var v = regex.Matches(bagList);
            foreach(var result in v)
            {
                string bag = result.ToString().Trim(); 
                results.Add(bag);
                var recursiveBags = GetOuterBags(bag);
                foreach (var recursiveBag in recursiveBags)
                {
                    results.AddRange(GetContainingBags("1 " + recursiveBag.Key));
                }
            }

            return results;
        }

        private static Dictionary<string, string> ParseRules(string filePath)
        {
            if (filePath == null)
            {
                return null;
            }

            var bags = new Dictionary<string, string>();

            try
            {
                using (var sr = new StreamReader(filePath))
                {
                    string line;
                
                    while((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split("contain");
                        bags.Add(parts[0].Trim(), parts[1].Trim());
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message); // if this was an actual emergency, you would have been instructed where to log your error...
            }
            
            return bags;
        }
    }
}
