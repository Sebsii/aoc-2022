using System;
using System.Collections.Generic;
using System.IO;

namespace day9
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("../../input.txt");

            // Part1(lines);
            Part2(lines);
        }

        private static void Part1(string[] lines)
        {
            var visited = new HashSet<string>();
            int headX, headY, tailX, tailY;
            headX = headY = tailX = tailY = 0;

            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                Tuple<int, int> nextMove = new Tuple<int, int>(0, 1);
                string direction = parts[0];
                if (direction == "D") nextMove = new Tuple<int, int>(0, -1);
                else if (direction == "L") nextMove = new Tuple<int, int>(-1, 0);
                else if (direction == "R") nextMove = new Tuple<int, int>(1, 0);

                int reps = int.Parse(parts[1]);
                for (int i = 0; i < reps; i++)
                {
                    headX += nextMove.Item1;
                    headY += nextMove.Item2;

                    if (Math.Abs(headX - tailX) <= 1 && Math.Abs(headY - tailY) <= 1) continue;
                    if (headX == tailX) tailY += nextMove.Item2;
                    else if (headY == tailY) tailX += nextMove.Item1;
                    else
                    {
                        tailX += headX - tailX > 0 ? 1 : - 1;
                        tailY += headY - tailY > 0 ? 1 : - 1;
                    }

                    visited.Add(tailX.ToString() + ',' + tailY); 
                }
            }
            
            Console.WriteLine(visited.Count);
        }

        private static void Part2(string[] lines)
        {
            var visited = new HashSet<string>();
            var rope = new Knot[10];

            for (var i = 0; i < rope.Length; i++)
                rope[i] = new Knot();
            
            var head = rope[0];
            var tail = rope[9];

            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                Tuple<int, int> nextMove = new Tuple<int, int>(0, 1);
                string direction = parts[0];
                if (direction == "D") nextMove = new Tuple<int, int>(0, -1);
                else if (direction == "L") nextMove = new Tuple<int, int>(-1, 0);
                else if (direction == "R") nextMove = new Tuple<int, int>(1, 0);

                int reps = int.Parse(parts[1]);
                for (int i = 0; i < reps; i++)
                {
                    head.X += nextMove.Item1;
                    head.Y += nextMove.Item2;

                    for (int j = 1; j < rope.Length; j++)
                    {
                        var curr = rope[j];
                        var prev = rope[j - 1];
                        
                        if (Math.Abs(prev.X - curr.X) <= 1 && Math.Abs(prev.Y - curr.Y) <= 1) continue;
                        if (prev.X == curr.X) curr.Y += prev.Y - curr.Y > 0 ? 1 : - 1;
                        else if (prev.Y == curr.Y) curr.X += prev.X - curr.X > 0 ? 1 : - 1;
                        else
                        {
                            curr.X += prev.X - curr.X > 0 ? 1 : - 1;
                            curr.Y += prev.Y - curr.Y > 0 ? 1 : - 1;
                        }
                    }
                    
                    visited.Add(tail.X.ToString() + ',' + tail.Y); 
                }
            }
            
            Console.WriteLine(visited.Count);
        }

        class Knot
        {
            public int X;
            public int Y;
        }
    }
}