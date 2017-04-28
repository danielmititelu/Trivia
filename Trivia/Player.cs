using System;

namespace Trivia
{
    public abstract class Player
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

        public abstract string AskQuestion(Question question, Random random);
        public override string ToString()
        {
            return Name;
        }
    }
}
