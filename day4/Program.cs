using System;
using System.Text.RegularExpressions;

namespace day4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("../../input.txt");

            int counter = 0;
            
            foreach (var line in lines)
            {
                var first = line.Substring(0, line.IndexOf(','));
                var second = line.Substring(line.IndexOf(',') + 1);

                var first1 = int.Parse(first.Substring(0, first.IndexOf('-')));
                var first2 = int.Parse(first.Substring(first.IndexOf('-') + 1));
                
                var second1 = int.Parse(second.Substring(0, second.IndexOf('-')));
                var second2 = int.Parse(second.Substring(second.IndexOf('-') + 1));

                // if (first1 <= second1 && first2 >= second2 ||
                //     second1 <= first1 && second2 >= first2)
                //     counter++;

                if (IsOverlapping(first1, first2, second1, second2)) counter++;
            }
            
            Console.WriteLine(counter);
        }

        private static bool IsOverlapping(int a, int b, int c, int d)
        {
            return b >= c && a <= d;
        }
    }
}