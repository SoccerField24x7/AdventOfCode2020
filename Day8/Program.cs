using System;
using System.Collections.Generic;
using Advent2020.Helpers;

namespace Day8
{
    class Program
    {
        private static double acc;
        private static readonly string[] actions = new string[] {"acc", "jmp", "nop"};
        static void Main(string[] args)
        {
            acc = 0;
            int index = 0;
            List<int> executionStack = new List<int>();

            var code = FileHelper.GetFileContents("./data/code.txt");
            string[] codeLines = code.Split("\n");

            while(!executionStack.Contains(index))
            {
                executionStack.Add(index);
                index = ExecuteAction(codeLines[index], index);
            }

            Console.WriteLine($"The accumulator value is {acc}");

            // reset for part two
            acc = 0;
            index = 0;
            executionStack.Clear();
            bool didComplete = false;
            List<int> replacements = new List<int>();
            int i;

            for (i=0; i < codeLines.Length; i++)
            {
                var originalInstruction = codeLines[i];
                if (codeLines[i].Contains("jmp"))
                {
                    codeLines[i] = codeLines[i].Replace("jmp", "nop");
                }
                else if (codeLines[i].Contains("nop"))
                {
                    codeLines[i] = codeLines[i].Replace("nop", "jmp");
                }

                // now run the code to see if it finishes
                while(!executionStack.Contains(index) && !didComplete)
                {
                    executionStack.Add(index);
                    index = ExecuteAction(codeLines[index], index);
                    if (index >= codeLines.Length)
                    {
                        didComplete = true;
                        break;
                    }
                }

                if (!didComplete)
                {
                    // reset everything
                    codeLines[i] = originalInstruction;
                    acc = 0;
                    index = 0;
                    executionStack.Clear();
                }
                else
                {
                    Console.WriteLine($"On full execution, the accumulator value is {acc}");
                    break;
                }
            }
        }

        public static int ExecuteAction(string action, int currentAction)
        {
            var instructions = action.Split(" ");
            int next = currentAction;

            switch(instructions[0])
            {
                case "acc":
                    acc += int.Parse(instructions[1]);
                    next++;
                    break;
                case "jmp":
                    next += int.Parse(instructions[1]);
                    break;
                case "nop":
                    next++;
                    break;
            }

            return next;
        } 
    }
}
