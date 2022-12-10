using System;
using System.IO;
using System.Linq;

namespace day10
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("../../input.txt");
            int sum = 0;
            int register = 1;
            int cycleCount = 0;
            
            foreach (var line in lines)
            {
                var inst = line.Split(' ');
                int cycle = inst[0] switch { "noop" => 1, "addx" => 2 };

                for (int i = 0; i < cycle; i++)
                {
                    
                    char pixel = new[] { register - 1, register, register + 1 }.Contains(cycleCount % 40) ? '#' : '.';
                    Console.Write(pixel);

                    cycleCount++;

                    if ((cycleCount - 20) % 40 == 0)
                        sum += cycleCount * register;
                    
                    if(cycleCount % 40 == 0)
                        Console.Write("\n");
                }
                
                if (cycle == 2)
                {
                    register += int.Parse(inst[1]);
                }
            }
            
            Console.WriteLine("part 1: " + sum);
        }
    }
}