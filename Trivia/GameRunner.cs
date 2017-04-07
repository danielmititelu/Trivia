using System;
using System.Collections.Generic;

namespace Trivia
{
    public class GameRunner
    {

        private static bool _isGameOver;

        public static void Main(String[] args)
        {
            var random = new Random();
            if (args.Length == 1) random = new Random(int.Parse(args[0]));

            var aGame = new Game(new QuestionRepository().GetQuestions(), random);
            aGame.Add(new Player { Name = "Chet" });
            aGame.Add(new Player { Name = "Pat" });
            aGame.Add(new Player { Name = "Sue" });

            do
            {
                aGame.NextTurn();
                _isGameOver = random.Next(9) == 7 ? aGame.GiveWrongAnswer() : aGame.GiveCorrectAnswer();
            } while (!_isGameOver);
        }
    }
}

