using System;

namespace Trivia
{
    public class HumanPlayer : Player
    {
        public override string AskQuestion(Question question, Random random)
        {
            return Console.ReadLine();
        }
    }
}
