using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly List<Player> _players = new List<Player>();

        public readonly LinkedList<Question> PopQuestions = new LinkedList<Question>();
        public readonly LinkedList<Question> ScienceQuestions = new LinkedList<Question>();
        public readonly LinkedList<Question> SportsQuestions = new LinkedList<Question>();
        public readonly LinkedList<Question> RockQuestions = new LinkedList<Question>();

        private int _currentPlayerIndex;
        private Player _currentPlayer;

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                PopQuestions.AddLast(new Question { Text = "Pop Question " + i });
                ScienceQuestions.AddLast(new Question { Text = "Science Question " + i });
                SportsQuestions.AddLast(new Question { Text = "Sports Question " + i });
                RockQuestions.AddLast(new Question { Text = "Rock Question " + i });
            }
        }

        public bool IsPlayable()
        {
            return (HowManyPlayers() > 3);
        }

        public bool Add(string playerName)
        {
            _players.Add(new Player { Name = playerName });
            _currentPlayer = _players[0];
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
            return true;
        }

        public int HowManyPlayers()
        {
            return _players.Count;
        }

        public void Roll(int roll)
        {
            var currentPlayer = _players[_currentPlayerIndex];
            Console.WriteLine(currentPlayer + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (currentPlayer.IsInPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    _currentPlayer.IsGettingOutOfPenaltyBox = true;

                    Console.WriteLine(currentPlayer + " is getting out of the penalty box");
                    currentPlayer.Place += roll;
                    if (currentPlayer.Place > 11) currentPlayer.Place -= 12;

                    Console.WriteLine(currentPlayer
                            + "'s new location is "
                            + currentPlayer.Place);
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(currentPlayer + " is not getting out of the penalty box");
                    _currentPlayer.IsGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                currentPlayer.Place += roll;
                if (currentPlayer.Place > 11) currentPlayer.Place -= 12;

                Console.WriteLine(currentPlayer
                        + "'s new location is "
                        + currentPlayer.Place);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Console.WriteLine(PopQuestions.First());
                PopQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(ScienceQuestions.First());
                ScienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(SportsQuestions.First());
                SportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock")
            {
                Console.WriteLine(RockQuestions.First());
                RockQuestions.RemoveFirst();
            }
        }

        private string CurrentCategory()
        {
            if (_currentPlayer.Place == 0) return "Pop";
            if (_currentPlayer.Place == 4) return "Pop";
            if (_currentPlayer.Place == 8) return "Pop";
            if (_currentPlayer.Place == 1) return "Science";
            if (_currentPlayer.Place == 5) return "Science";
            if (_currentPlayer.Place == 9) return "Science";
            if (_currentPlayer.Place == 2) return "Sports";
            if (_currentPlayer.Place == 6) return "Sports";
            if (_currentPlayer.Place == 10) return "Sports";
            return "Rock";
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
