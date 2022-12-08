using System;

namespace day2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("../../input.txt");

            int scoreSum = 0;
            
            foreach (var line in lines)
            {
                var opp = MapInput(line[0]);
                var mine = MapInput(line[2]);

                int outcomeScore = 0;
                if (mine == opp) outcomeScore = 3;
                if (mine == Shape.Rock && opp == Shape.Scissors) outcomeScore = 6;
                if (mine == Shape.Paper && opp == Shape.Rock) outcomeScore = 6;
                if (mine == Shape.Scissors && opp == Shape.Paper) outcomeScore = 6;

                scoreSum += outcomeScore;
                scoreSum += (int)mine;
            }
            
            Console.WriteLine(scoreSum); // first half of puzzle

            scoreSum = 0;
            
            foreach (var line in lines)
            {
                var outcome = line[2];
                var opp = MapInput(line[0]);
                var mine = MapInputFromRoundEnd(opp, outcome);

                int outcomeScore = 0;
                if (outcome == 'Y') outcomeScore = 3;
                if (outcome == 'Z') outcomeScore = 6;

                scoreSum += outcomeScore;
                scoreSum += (int)mine;
            }
            
            Console.WriteLine(scoreSum); // second half of puzzle
        }

        private static Shape MapInput(char input)
        {
            if (input == 'A' || input == 'X')
                return Shape.Rock;
            if (input == 'B' || input == 'Y')
                return Shape.Paper;
            return Shape.Scissors;
        }

        private static Shape MapInputFromRoundEnd(Shape opp, char input)
        {
            var mine = opp;
            if (input == 'X')
            {
                mine = opp - 1;
            }

            if (input == 'Z') 
            {
                mine = opp + 1;
            }
            
            if ((int)mine == 0) mine = (Shape)3;
            if ((int)mine == 4) mine = (Shape)1;
            
            return mine;
        }


        public enum Shape
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }
    }
}