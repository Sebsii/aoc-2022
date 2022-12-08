using System;
using System.Collections.Generic;
using System.IO;

namespace day7
{
    internal class Program
    {
        private const int DiskSpace = 70000000;
        private const int UnusedSpace = 30000000;

        public static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("../../input.txt");
            var root = BuildTree(lines);
            // Part1(root);
            Part2(root);
        }

        private static void Part2(Dir root)
        {
            int smallest = 11111111;
            var size = root.GetSize();
            var freeSpace = DiskSpace - size;
            var requiredSize = UnusedSpace - freeSpace;
            root.GetSmallestButBigEnoughToHaveEnoughUnusedSpaceAfterDeleting(ref smallest, requiredSize);
            Console.WriteLine(smallest);
        }

        private static void Part1(Dir root)
        {
            int total = 0;
            root.GetSizeOfMax100k(ref total);
            Console.WriteLine(total);
        }

        private static Dir BuildTree(string[] lines)
        {
            Dir root = new Dir();
            var currentDir = root;

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var parts = line.Split(' ');

                if (parts[0] == "dir")
                {
                    var newDir = new Dir();
                    newDir.parent = currentDir;
                    currentDir.children.Add(line.Split(' ')[1], newDir);
                }
                else if (int.TryParse(parts[0], out int value))
                {
                    currentDir.files.Add(value);
                }
                else if (parts[1] == "cd")
                {
                    if (parts[2] == "..")
                    {
                        currentDir = currentDir.parent;
                        continue;
                    }

                    currentDir = currentDir.children[parts[2]];
                }
            }

            return root;
        }

        class Dir
        {
            public Dir parent = null;
            public Dictionary<string, Dir> children = new Dictionary<string, Dir>();
            public List<int> files = new List<int>(); // just sizes

            public int GetSize()
            {
                int sum = 0;

                foreach (var file in files)
                    sum += file;

                foreach (var child in children)
                    sum += child.Value.GetSize();

                return sum;
            }

            public void GetSizeOfMax100k(ref int total)
            {
                foreach (var child in children)
                {
                    child.Value.GetSizeOfMax100k(ref total);
                }

                int size = GetSize();
                if (size <= 100000)
                    total += size;
            }

            public void GetSmallestButBigEnoughToHaveEnoughUnusedSpaceAfterDeleting(ref int smallest, int requiredSize)
            {
                int size = GetSize();
                if (size >= requiredSize &&
                    smallest > size)
                    smallest = size;
                
                foreach (var child in children)
                {
                    child.Value.GetSmallestButBigEnoughToHaveEnoughUnusedSpaceAfterDeleting(ref smallest, requiredSize);
                }
            }
        }
    }
}