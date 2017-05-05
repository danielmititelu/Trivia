using System;
using NUnit.Framework;

namespace Trivia.UnitTests
{
    public class BoardTests
    {
        [Test]
        public void GetQuestionFromBoard()
        {
            var board = new Board();
            var question = new Question { Category = Category.Pop, Answer = "42", Text = "What is life?" };
            board.AddQuestion(question);

            var questionFromBoard = board.GetQuestion(4);

            Assert.AreEqual(question, questionFromBoard);
        }

        [Test]
        public void ThrowsExceptionIfPlaceIsNegative()
        {
            var board = new Board();
            var question = new Question { Category = Category.Pop, Answer = "42", Text = "What is life?" };
            board.AddQuestion(question);

            Assert.Throws(
                Is.TypeOf<ArgumentException>()
                .And.Message.EqualTo("Player place cannot be negative"),
                delegate { board.GetQuestion(-1); });
        }

        [Test]
        public void GetQuestionFromBoardWithNoQuestion()
        {
            Assert.Throws(
                Is.TypeOf<InvalidOperationException>()
                .And.Message.EqualTo("Board has no questions"),
                delegate { new Board().GetQuestion(4); });
        }
    }
}
