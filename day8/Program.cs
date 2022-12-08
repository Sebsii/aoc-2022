using System;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace day8
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("../../input.txt");

            int rows = lines.Length;
            int cols = lines[0].Length;

            Tree[,] trees = new Tree[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var height = int.Parse(lines[i][j].ToString());
                    trees[i, j] = new Tree(height);
                }
            }

            // Part1(trees);
            Part2(trees);
        }

        private static void Part2(Tree[,] trees)
        {
            int rows = trees.GetLength(0);
            int cols = trees.GetLength(1);

            int best = 0;
            
            for (int i = 1; i < rows - 1; i++)
            {
                for (int j = 1; j < cols - 1; j++)
                {
                    var tree = trees[i, j];
                    var dist = new[] {0, 0, 0, 0};
                    
                    // Top
                    for (int k = 0; k < i; k++)
                    {
                        var otherTree = trees[i - k - 1, j];
                        dist[0]++;
                        if (otherTree.Height >= tree.Height)
                            break;
                    }

                    // Bot
                    for (int k = 0; k < rows - 1 - i; k++)
                    {
                        var otherTree = trees[i + k + 1, j];
                        dist[1]++;
                        if (otherTree.Height >= tree.Height)
                            break;
                    }

                    // Left
                    for (int k = 0; k < j; k++)
                    {
                        var otherTree = trees[i, j - k - 1];
                        dist[2]++;
                        if (otherTree.Height >= tree.Height)
                            break;
                    }

                    // Right
                    for (int k = 0; k < cols - 1 - j; k++)
                    {
                        var otherTree = trees[i, j + k + 1];
                        dist[3]++;
                        if (otherTree.Height >= tree.Height)
                            break;
                    }

                    int score = dist[0] * dist[1] * dist[2] * dist[3];
                    if (score > best)
                        best = score;
                }
            }
            
            Console.WriteLine(best);
        }

        private static void Part1(Tree[,] trees)
        {
            int rows = trees.GetLength(0);
            int cols = trees.GetLength(1);
            
            for (int i = 1; i < rows - 1; i++)
            {
                for (int j = 1; j < cols - 1; j++)
                {
                    var tree = trees[i, j];

                    // Top
                    for (int k = 0; k < i; k++)
                    {
                        var otherTree = trees[i - k - 1, j];
                        if (otherTree.Height < tree.Height) continue;
                        tree.Visible[0] = false;
                        break;
                    }

                    // Bot
                    for (int k = 0; k < rows - 1 - i; k++)
                    {
                        var otherTree = trees[i + k + 1, j];
                        if (otherTree.Height < tree.Height) continue;
                        tree.Visible[1] = false;
                        break;
                    }

                    // Left
                    for (int k = 0; k < j; k++)
                    {
                        var otherTree = trees[i, j - k - 1];
                        if (otherTree.Height < tree.Height) continue;
                        tree.Visible[2] = false;
                        break;
                    }

                    // Right
                    for (int k = 0; k < cols - 1 - j; k++)
                    {
                        var otherTree = trees[i, j + k + 1];
                        if (otherTree.Height < tree.Height) continue;
                        tree.Visible[3] = false;
                        break;
                    }
                }
            }

            int sum = 0;
            foreach (var tree in trees)
            {
                if (tree.IsVisible())
                    sum++;
            }
            
            Console.WriteLine(sum);
        }

        public class Tree
        {
            public int Height;
            public bool[] Visible = { true, true, true, true };

            public Tree(int height)
            {
                Height = height;
            }

            public bool IsVisible()
            {
                foreach (var vis in Visible)
                {
                    if (vis) return true;
                }

                return false;
            }
        }
    }
}