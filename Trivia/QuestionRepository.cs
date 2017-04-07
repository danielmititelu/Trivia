using System.Collections.Generic;

namespace Trivia
{
    public class QuestionRepository
    {
        public List<Question> GetQuestions()
        {
            var questions = new List<Question>();
            for (var i = 0; i < 50; i++)
            {
                questions.Add(new Question { Text = $"Pop Question {i}", Answer = $"Pop Answer {i}", Category = Category.Pop });
                questions.Add(new Question { Text = $"Science Question {i}", Answer = $"Science Answer {i}", Category = Category.Science });
                questions.Add(new Question { Text = $"Sports Question {i}", Answer = $"Sports Answer {i}", Category = Category.Sports });
                questions.Add(new Question { Text = $"Rock Question {i}", Answer = $"Rock Answer {i}", Category = Category.Rock });
            }
            return questions;
        }
    }
}
