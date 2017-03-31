using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
    public class GameRunner
    {

        private static bool isNotGameOver;

        public static void Main(String[] args)
        {
            Game aGame = new Game();

            aGame.Add("Chet");
            aGame.Add("Pat");
            aGame.Add("Sue");

            var rand = new Random();
            if (args.Length == 1) rand = new Random(int.Parse(args[0]));

            do
            {
                aGame.Roll(rand.Next(5) + 1); 

                if (rand.Next(9) == 7)
                {
                    isNotGameOver = aGame.GiveWrongAnswer();
                }
                else
                {
                    isNotGameOver = aGame.GiveCorrectAnswer();
                }
            } while (isNotGameOver);
        }
    }
}

