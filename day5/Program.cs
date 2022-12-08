using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day5
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("../../input.txt");

            // Part1(lines);
            Part2(lines);
        }

        private static void Part1(string[] lines)
        {
            // inputs on positions 1, 5, 9... 1 + n * 4
            var cargo = new Stack<char>[9];

            for (int i = 7; i >= 0; i--) // rows
            {
                for (int n = 0; n <= 8; n++) // cols
                {
                    int pos = 1 + n * 4;
                    char container = lines[i][pos];
                    cargo[n] ??= new Stack<char>();
                    if (container != ' ')
                        cargo[n].Push(container);
                }
            }

            for (int i = 10; i < lines.Length; i++)
            {
                var matches = Regex.Matches(lines[i], @"(\d+)")
                    .OfType<Match>()
                    .Select(m => int.Parse(m.Groups[0].Value))
                    .ToArray();

                for (int j = 0; j < matches[0]; j++)
                {
                    var thingy = cargo[matches[1] - 1].Pop();
                    cargo[matches[2] - 1].Push(thingy);
                }
            }

            string result = "";
            foreach (var stack in cargo)
            {
                result += stack.Pop();
            }
            
            Console.WriteLine(result);
        }
        
        private static void Part2(string[] lines)
        {
            // inputs on positions 1, 5, 9... 1 + n * 4
            var cargo = new Stack<char>[9];

            for (int i = 7; i >= 0; i--) // rows
            {
                for (int n = 0; n <= 8; n++) // cols
                {
                    int pos = 1 + n * 4;
                    char container = lines[i][pos];
                    cargo[n] ??= new Stack<char>();
                    if (container != ' ')
                        cargo[n].Push(container);
                }
            }

            for (int i = 10; i < lines.Length; i++)
            {
                var matches = Regex.Matches(lines[i], @"(\d+)")
                    .OfType<Match>()
                    .Select(m => int.Parse(m.Groups[0].Value))
                    .ToArray();

                string thingies = "";
                for (int j = 0; j < matches[0]; j++)
                {
                    var thingy = cargo[matches[1] - 1].Pop();
                    thingies += thingy;
                }

                for (int j = thingies.Length - 1; j >= 0; j--)
                {
                    cargo[matches[2] - 1].Push(thingies[j]);
                }
            }

            string result = "";
            foreach (var stack in cargo)
            {
                result += stack.Pop();
            }
            
            Console.WriteLine(result);
        }

        private static void PrintStack(Stack<char> s)
        {
            // If stack is empty then return
            if (s.Count == 0)
                return;

            char x = s.Peek();

            // Pop the top element of the stack
            s.Pop();

            // Recursively call the function PrintStack
            PrintStack(s);

            // Print the stack element starting
            // from the bottom
            Console.Write(x + " ");

            // Push the same element onto the stack
            // to preserve the order
            s.Push(x);
        }
    }
}