namespace Trivia
{
    public enum Category { Pop, Science, Sports, Rock }
    public class Question
    {
        public string Text { get; set; }
        public Category Category { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
