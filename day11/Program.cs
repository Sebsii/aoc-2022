using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day11
{
    internal class Program
    {
        public static readonly Monkey[] Monkeys = new Monkey[8];
        
        public static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("../../input.txt");

            SetupMonkeys(lines);

            int mod = 13 * 17 * 5 * 3 * 19 * 11 * 7 * 2;
            
            for (int _ = 0; _ < 10_000; _++)
            {
                foreach (var monkey in Monkeys)
                {
                    while (monkey.items.Count != 0)
                    {
                        monkey.count++;
                        var item = monkey.items.Dequeue();
                        item = monkey.op(item);
                        // item /= 3; // part 1
                        item %= mod; // part 2
                        if (item % monkey.test == 0)
                            Monkeys[monkey.ifTrue].items.Enqueue(item);
                        else
                            Monkeys[monkey.ifFalse].items.Enqueue(item);
                    }
                }
            }
            
            long[] biggest = Monkeys.Select(monkey => monkey.count).OrderByDescending(x => x).Take(2).ToArray();
            
            Console.WriteLine(biggest[0] * biggest[1]);
        }

        private static void SetupMonkeys(string[] lines)
        {
            for (int i = 0; i < 8; i++)
            {
                Monkeys[i] = new Monkey();

                var items = Regex.Matches(lines[i * 7 + 1], @"(\d+)")
                    .OfType<Match>()
                    .Select(m => int.Parse(m.Groups[0].Value))
                    .ToList();

                foreach (var item in items)
                    Monkeys[i].items.Enqueue(item);

                var opParts = lines[i * 7 + 2].Split(' ');
                opParts = opParts.Skip(opParts.Length - 2).ToArray();

                Monkeys[i].op = old =>
                {
                    long value;
                    var op = opParts[0];
                    var other = opParts[1];
                    if (op == "+")
                        value = old + (other == "old" ? old : long.Parse(other));
                    else
                        value = old * (other == "old" ? old : long.Parse(other));
                    return value;
                };

                var test = lines[i * 7 + 3].Split(' ');
                Monkeys[i].test = int.Parse(lines[i * 7 + 3].Split(' ')[5]);
                Monkeys[i].ifTrue = int.Parse(lines[i * 7 + 4].Split(' ')[9]);
                Monkeys[i].ifFalse = int.Parse(lines[i * 7 + 5].Split(' ')[9]);
            }
        }

        public class Monkey
        {
            public Queue<long> items = new Queue<long>();
            public Func<long, long> op;
            public int test;
            public int ifTrue;
            public int ifFalse;
            public long count;
        }
    }
}