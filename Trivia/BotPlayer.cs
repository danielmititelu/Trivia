using System;

namespace Trivia
{
    public class BotPlayer : Player
    {
        public override string AskQuestion(Question question, Random random)
        {
            return random.Next(9) == 7 ? "Incorrect Answer" : question.Answer;
        }
    }
}
