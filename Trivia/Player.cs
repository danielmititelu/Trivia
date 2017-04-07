using System;

namespace Trivia
{
    public class Player
    {
        public string Name { get; set; }
        public int Purse { get; set; }
        public int Place { get; set; }
        public bool IsInPenaltyBox { get; set; }
        public bool IsGettingOutOfPenaltyBox { get; set; }

        public int RollDice(Random random)
        {
            return random.Next(5) + 1;
        }

        public string AskQuestion(Question question, Random random)
        {
            return random.Next(9) == 7 ? "Incorrect Answer" : question.Answer;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
