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
                _board.AddQuestion(new Question { Text = "Pop Question " + i, Category = Category.Pop });
                _board.AddQuestion(new Question { Text = "Science Question " + i, Category = Category.Science });
                _board.AddQuestion(new Question { Text = "Sports Question " + i, Category = Category.Sports });
                _board.AddQuestion(new Question { Text = "Rock Question " + i, Category = Category.Rock });
            }
        }

        public bool IsPlayable()
        {
            return _players.Count > 3;
        }

        public void Add(string playerName)
        {
            _players.Add(new Player { Name = playerName });
            _currentPlayer = _players[0];
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
        }

        public void Roll(int roll)
        {
            var currentPlayer = _players[_currentPlayerIndex];
            Console.WriteLine(currentPlayer + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (currentPlayer.IsInPenaltyBox)
            {
                _currentPlayer.IsGettingOutOfPenaltyBox = roll % 2 == 1;

                Console.WriteLine(_currentPlayer.IsGettingOutOfPenaltyBox?
                    currentPlayer + " is getting out of the penalty box" :
                    currentPlayer + " is not getting out of the penalty box");
            }

            if (currentPlayer.IsInPenaltyBox && _currentPlayer.IsGettingOutOfPenaltyBox || !currentPlayer.IsInPenaltyBox)
                MovePlayer(roll, currentPlayer);
        }

        private void MovePlayer(int roll, Player currentPlayer)
        {
            currentPlayer.Place += roll;
            currentPlayer.Place %= MaxPlaces;

            Console.WriteLine(currentPlayer
                              + "'s new location is "
                              + currentPlayer.Place);
            Console.WriteLine("The category is " + CurrentCategory());
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

        public bool WasCorrectlyAnswered()
        {

            if (_currentPlayer.IsInPenaltyBox)
            {
                if (_currentPlayer.IsGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _players[_currentPlayerIndex].Purse++;
                    Console.WriteLine(_players[_currentPlayerIndex]
                            + " now has "
                            + _players[_currentPlayerIndex].Purse
                            + " Gold Coins.");

                    var winner = DidPlayerWin();
                    _currentPlayerIndex++;
                    if (_currentPlayerIndex == _players.Count) _currentPlayerIndex = 0;
                    _currentPlayer = _players[_currentPlayerIndex];
                    return winner;
                }
                else
                {
                    _currentPlayerIndex++;
                    if (_currentPlayerIndex == _players.Count) _currentPlayerIndex = 0;
                    _currentPlayer = _players[_currentPlayerIndex];
                    return true;
                }
            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                _players[_currentPlayerIndex].Purse++;
                Console.WriteLine(_players[_currentPlayerIndex]
                        + " now has "
                        + _players[_currentPlayerIndex].Purse
                        + " Gold Coins.");

                var winner = DidPlayerWin();
                _currentPlayerIndex++;
                if (_currentPlayerIndex == _players.Count) _currentPlayerIndex = 0;
                _currentPlayer = _players[_currentPlayerIndex];
                return winner;
            }
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players[_currentPlayerIndex] + " was sent to the penalty box");
            _currentPlayer.IsInPenaltyBox = true;

            _currentPlayerIndex++;
            if (_currentPlayerIndex == _players.Count) _currentPlayerIndex = 0;
            _currentPlayer = _players[_currentPlayerIndex];
            return true;
        }

        private bool DidPlayerWin()
        {
            return _players[_currentPlayerIndex].Purse != 6;
        }
    }
}
