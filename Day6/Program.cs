using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var groupYesResponses = GetUniqueYesAnswersByGroup("./data/questionnaire.txt");
            int totalYes = 0;

            foreach (var groupResponse in groupYesResponses)
            {
                totalYes += groupResponse.Count;
            }

            Console.WriteLine($"Unique yes responses by question was {totalYes}");

            totalYes = 0;
            var groupTotalResponses = GetYesAnswersByGroup("./data/questionnaire.txt");
            totalYes = groupTotalResponses.Sum();

            Console.WriteLine($"Unique questions that all in party answered yes to {totalYes}");
        }

        private static List<HashSet<char>> GetUniqueYesAnswersByGroup(string data)
        {
            List<HashSet<char>> groupResponses = new List<HashSet<char>>();

            try
            {
                using (var sr = new StreamReader(data))
                {
                    string line;
                    HashSet<char> group = new HashSet<char>();
                    while((line = sr.ReadLine()) != null)
                    {
                        if (String.IsNullOrEmpty(line))
                        {
                            Console.WriteLine("Found a break");
                            // add current group's response to list
                            groupResponses.Add(group);
                            // reinitialize dict
                            group = new HashSet<char>();
                            continue;
                        }

                        foreach (char answer in line)
                        {
                            group.Add(answer);
                        }
                    }

                    // we may have one more
                    if (group.Count > 0)
                        groupResponses.Add(group);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message); // if this was an actual emergency, you would have been instructed where to log your error...
            }
            
            return groupResponses;
        }

        private static List<int> GetYesAnswersByGroup(string data)
        {
            List<int> matchCount = new List<int>();
            try
            {
                using (var sr = new StreamReader(data))
                {
                    string line;                    
                    List<string> groupResponses = new List<string>();

                    while((line = sr.ReadLine()) != null)
                    {
                        
                        if (String.IsNullOrEmpty(line))
                        {
                            matchCount.Add(GetMatchCount(groupResponses));
                            // reset
                            groupResponses = new List<string>();
                            continue;
                        }

                        groupResponses.Add(line);
                    }

                    // add the last group
                    matchCount.Add(GetMatchCount(groupResponses));
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message); // if this was an actual emergency, you would have been instructed where to log your error...
            }
            
            return matchCount;
        }

        private static int GetMatchCount(List<string> groupResponses)
        {
            // a little brute force, but cut corners where possible
            int count = 0;
            string shortest = groupResponses.OrderBy(x => x.Length).FirstOrDefault();
            foreach (var answer in shortest)
            {
                bool all = true;
                foreach (var questionnaire in groupResponses)
                {                                    
                    if (!questionnaire.Contains(answer))
                    {
                        all = false;
                        continue;
                    }
                }

                if (all)
                    count++;
            }

            return count;
        }
    }
}
