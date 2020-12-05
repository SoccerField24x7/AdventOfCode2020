using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day4
{
    class Program
    {
        public static readonly bool DEEP_VALIDATION = false;  // false for part 1 solution, true for part 2 solution
        public static readonly List<string> REQUIRED_FIELDS = new List<string> {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid"}; 
        static void Main(string[] args)
        {
            var passports = GetKeyValuePairs("./data/passports.txt");
            int validPassports = 0;

            foreach (var passport in passports)
            {
                if (IsValid(passport, new string[] {"cid"}))  // ignore country id
                    validPassports++;
            }

            Console.WriteLine($"Based on your rules, there were {validPassports} valid passports.");
        }

        private static bool IsValid(Dictionary<string,string> passport, string[] overlook)
        {
            var validators = REQUIRED_FIELDS;
            foreach(var key in overlook)
            {
                validators.Remove(key);
            }

            // now that we have the updated validation keys, let's make sure we have all of them!
            foreach (var key in validators)
            {
                if (!passport.ContainsKey(key))
                    return false;
            }

            if (DEEP_VALIDATION) // and now let's see if they contain valid data (part 2)
            {
                foreach (var kvp in passport)
                {
                    if (!HasValidData(kvp.Key, kvp.Value))
                        return false;
                }
            }

            return true;
        }

        private static bool HasValidData(string key, string value)
        {
            List<string> eyeColors = new List<string>() {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

            switch(key.ToLower())
            {
                case "byr":
                    // four digits; at least 1920 and at most 2002

                    if (value.Length != 4 || int.Parse(value) < 1920 || int.Parse(value) > 2002)
                        return false;

                    break;
                case "iyr":
                    // four digits; at least 2010 and at most 2020

                    if (value.Length != 4 || int.Parse(value) < 2010 || int.Parse(value) > 2020)
                        return false;

                    break;
                case "eyr":
                    // four digits; at least 2020 and at most 2030

                    if (value.Length != 4 || int.Parse(value) < 2020 || int.Parse(value) > 2030)
                        return false;

                    break;
                case "hgt":
                    // a number followed by either cm or in:
                    // If cm, the number must be at least 150 and at most 193.
                    // If in, the number must be at least 59 and at most 76.

                    value = value.Trim(); // ensure we don't have any spaces
                    var uom = value.Substring(value.Length - 2);
                    
                    if (uom != "cm" && uom != "in")
                        return false;
                    
                    var height = int.Parse(value.Substring(0, value.Length - 2));

                    if (uom == "cm" && (height < 150 || height > 193 ))
                        return false;

                    if (uom == "in" && (height < 59 || height > 76 ))
                        return false;

                    break;
                case "hcl":
                    // a # followed by exactly six characters 0-9 or a-f

                    Regex rg = new Regex("^#([0-9a-fA-F]{6})$");
                    if (!rg.IsMatch(value))
                        return false;

                    break;
                case "ecl":
                    // exactly one of: amb blu brn gry grn hzl oth

                    if (!eyeColors.Contains(value))
                        return false;

                    break;
                case "pid":
                    // a nine-digit number, including leading zeroes

                    if (value.Length != 9)
                        return false;

                    break;
                case "cid":
                    break;
                default:
                    return false;
            }

            return true;
        }

        private static List<Dictionary<string, string>> GetKeyValuePairs(string data)
        {
            List<Dictionary<string, string>> passports = new List<Dictionary<string, string>>();

            try
            {
                using (var sr = new StreamReader(data))
                {
                    string line;
                    Dictionary<string, string> passport = new Dictionary<string, string>();
                    while((line = sr.ReadLine()) != null)
                    {
                        if (String.IsNullOrEmpty(line))
                        {
                            Console.WriteLine("Found a break");
                            // add current passport to list
                            passports.Add(passport);
                            // reinitialize dict
                            passport = new Dictionary<string, string>();
                            continue;
                        }

                        // split line by space
                        string[] values = line.Split(" ");
                        foreach (var kvp in values)
                        {
                            var pairData = kvp.Split(":");
                            passport.Add(pairData[0].Trim(), pairData[1].Trim());
                        }

                        Console.WriteLine(line);
                    }

                    // we may have one more
                    if (passport.Count > 0)
                        passports.Add(passport);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message); // if this was an actual emergency, you would have been instructed where to log your error...
            }
            
            return passports;
        }
    }
}
