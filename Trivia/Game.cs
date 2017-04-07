using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly Random _random;
        private readonly List<Player> _players = new List<Player>();
        private readonly Board _board = new Board();

        const int MaxPlaces = 12;
        private int _currentPlayerIndex;
        private Player _currentPlayer;

        public Game(List<Question> questions, Random random)
        {
            _random = random;
            foreach (var question in questions)
            {
                _board.AddQuestion(question);
            }
        }

        public void Add(Player player)
        {
            _players.Add(player);
            _currentPlayer = _players[0];
            Console.WriteLine($"{player.Name} was added");
            Console.WriteLine($"They are player number {_players.Count}");
        }

        public void NextTurn()
        {
            var roll = _currentPlayer.RollDice(_random);
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

        public bool IsGameOver()
        {
            return _players.Any(player => player.Purse == 6);
        }
    }
}
