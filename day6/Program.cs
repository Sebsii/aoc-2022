using System;
using System.Linq;

namespace day6
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("../../input.txt");
            string line = lines[0];

            // Part1(line);
            Part2(line);
        }

        private static void Part2(string line)
        {
            for (int i = 13; i < line.Length; i++)
            {
                var quad = line.Substring(i - 13, 14);
                if (quad.Distinct().Count() == quad.Length)
                {
                    Console.WriteLine(i + 1);
                    break;
                }
            }
        }

        private static void Part1(string line)
        {
            for (int i = 3; i < line.Length; i++)
            {
                var quad = line.Substring(i - 3, 4);
                if (quad.Distinct().Count() == quad.Length)
                {
                    Console.WriteLine(i + 1);
                    break;
                }
            }
        }
    }
}