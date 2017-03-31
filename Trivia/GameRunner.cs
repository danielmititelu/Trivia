using System;

namespace Trivia
{
    public class GameRunner
    {

        private static bool _isGameOver;

        public static void Main(String[] args)
        {
            var aGame = new Game();

            aGame.Add("Chet");
            aGame.Add("Pat");
            aGame.Add("Sue");

            var rand = new Random();
            if (args.Length == 1) rand = new Random(int.Parse(args[0]));

            do
            {
                var die = rand.Next(5) + 1;
                aGame.Roll(die);
                _isGameOver = rand.Next(9) == 7 ? aGame.GiveWrongAnswer() : aGame.GiveCorrectAnswer();
            } while (!_isGameOver);
        }
    }
}

