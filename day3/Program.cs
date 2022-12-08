using System;
using System.Linq;

namespace day3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("../../input.txt");

            int sum = 0;
            int groupCounter = 0;

            for (var i = 0; i < lines.Length; i++)
            {
                // var line = lines[i];
                // var part1 = line.Substring(0, line.Length / 2);
                // var part2 = line.Substring(line.Length / 2);

                // var duplicate = part1.Intersect(part2).ElementAt(0);
                // var prio = duplicate - 96;
                // if (prio <= 0) prio = duplicate - 38;

                if (i % 3 == 2) // last line in group
                {
                    var badge = lines[i].Intersect(lines[i - 1]).Intersect(lines[i - 2]).ElementAt(0);
                    var prio = badge - 96;
                    if (prio <= 0) prio = badge - 38;

                    sum += prio;
                }
            }
            
            Console.WriteLine(sum);
        }
    }
}