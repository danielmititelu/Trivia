using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Board
    {
        private readonly List<Question> _questions = new List<Question>();

        public void AddQuestion(Question question)
        {
            _questions.Add(question);
        }

        public Question GetQuestion(int playerPlace)
        {
            var category = GetCategory(playerPlace);
            var question = (from q in _questions
                            where q.Category == category
                            select q).First();

            _questions.Remove(question);
            return question;
        }

        public Category GetCategory(int playerPlace)
        {
            var numberOfQuestionCategories = Enum.GetNames(typeof(Category)).Length;
            return (Category)(playerPlace % numberOfQuestionCategories);
        }
    }
}
