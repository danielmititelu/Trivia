using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly List<Player> _players = new List<Player>();
        private readonly Board _board = new Board();

        const int MaxPlaces = 12;
        private int _currentPlayerIndex;
        private Player _currentPlayer;

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                _board.AddQuestion(new Question { Text = $"Pop Question {i}", Category = Category.Pop });
                _board.AddQuestion(new Question { Text = $"Science Question {i}", Category = Category.Science });
                _board.AddQuestion(new Question { Text = $"Sports Question {i}", Category = Category.Sports });
                _board.AddQuestion(new Question { Text = $"Rock Question {i}", Category = Category.Rock });
            }
        }

        public void Add(string playerName)
        {
            _players.Add(new Player { Name = playerName });
            _currentPlayer = _players[0];
            Console.WriteLine($"{playerName} was added");
            Console.WriteLine($"They are player number {_players.Count}");
        }

        public void Roll(int roll)
        {
            Console.WriteLine($"{_currentPlayer} is the current player");
            Console.WriteLine($"They have rolled a {roll}");

            if (_currentPlayer.IsInPenaltyBox)
            {
                _currentPlayer.IsGettingOutOfPenaltyBox = roll % 2 == 1;

                Console.WriteLine(_currentPlayer.IsGettingOutOfPenaltyBox ?
                    $"{_currentPlayer} is getting out of the penalty box" :
                    $"{_currentPlayer} is not getting out of the penalty box");
            }

            if (_currentPlayer.IsInPenaltyBox && _currentPlayer.IsGettingOutOfPenaltyBox || !_currentPlayer.IsInPenaltyBox)
                MovePlayer(roll);
        }

        private void MovePlayer(int roll)
        {
            _currentPlayer.Place += roll;
            _currentPlayer.Place %= MaxPlaces;

            Console.WriteLine($"{_currentPlayer}'s new location is {_currentPlayer.Place}");
            Console.WriteLine($"The category is {CurrentCategory()}");
            AskQuestion();
        }

        private void AskQuestion()
        {
            var question = _board.GetQuestion(_currentPlayer.Place);
            Console.WriteLine(question);
        }

        private Category CurrentCategory()
        {
            return _board.GetCategory(_currentPlayer.Place);
        }

        public bool GiveCorrectAnswer()
        {
            if (_currentPlayer.IsInPenaltyBox && _currentPlayer.IsGettingOutOfPenaltyBox || !_currentPlayer.IsInPenaltyBox)
                return DoWhenPlayerAnswersCorrectly();

            var winner = IsGameOver();
            PassTurnToNextPlayer();
            return winner;
        }

        private bool DoWhenPlayerAnswersCorrectly()
        {
            Console.WriteLine("Answer was correct!!!!");
            _players[_currentPlayerIndex].Purse++;
            Console.WriteLine($"{_currentPlayer} now has {_currentPlayer.Purse} Gold Coins.");

            var winner = IsGameOver();
            PassTurnToNextPlayer();
            return winner;
        }

        private void PassTurnToNextPlayer()
        {
            _currentPlayerIndex++;
            if (_currentPlayerIndex == _players.Count) _currentPlayerIndex = 0;
            _currentPlayer = _players[_currentPlayerIndex];
        }

        public bool GiveWrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine($"{_currentPlayer} was sent to the penalty box");
            _currentPlayer.IsInPenaltyBox = true;

            var winner = IsGameOver();
            PassTurnToNextPlayer();
            return winner;
        }

        private bool IsGameOver()
        {
            return _players[_currentPlayerIndex].Purse == 6;
        }
    }
}
