﻿namespace Trivia
{
    public class Player
    {
        public string Name { get; set; }
        public int Purse { get; set; }
        public int Place { get; set; }
        public bool IsInPenaltyBox { get; set; }
        public bool IsGettingOutOfPenaltyBox { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
