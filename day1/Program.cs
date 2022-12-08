using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace day1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("../../input.txt");
            List<int> elves = new();
            elves.Add(0);
            int elvesIdx = 0;
            
            foreach (var input in lines)
            {
                if (int.TryParse(input, out int result))
                {
                    elves[elvesIdx] += result;
                }
                else
                {
                    elves.Add(0);
                    elvesIdx++;
                }
            }

            var top3 = new int[3];
            foreach (var elf in elves)
            {
                if (elf <= top3[2]) continue;
                if (elf >= top3[0])
                {
                    top3[2] = top3[1];
                    top3[1] = top3[0];
                    top3[0] = elf;
                    continue;
                }

                if (elf >= top3[1])
                {
                    top3[2] = top3[1];
                    top3[1] = elf;
                    continue;
                }
                
                if (elf > top3[2])
                {
                    top3[2] = elf;
                }
            }

            var sum = top3[0] + top3[1] + top3[2];
            
            Console.WriteLine(sum);
        }
    }
}