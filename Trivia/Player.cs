namespace Trivia
{
    public class Player
    {
        public string Name { get; set; }
        public int Purse { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
