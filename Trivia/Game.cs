using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly List<Player> _players = new List<Player>();

        private readonly bool[] _inPenaltyBox = new bool[6];

        public readonly LinkedList<string> PopQuestions = new LinkedList<string>();
        public readonly LinkedList<string> ScienceQuestions = new LinkedList<string>();
        public readonly LinkedList<string> SportsQuestions = new LinkedList<string>();
        public readonly LinkedList<string> RockQuestions = new LinkedList<string>();

        private int _currentPlayer = 0;
        private bool _isGettingOutOfPenaltyBox;

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                PopQuestions.AddLast("Pop Question " + i);
                ScienceQuestions.AddLast(("Science Question " + i));
                SportsQuestions.AddLast(("Sports Question " + i));
                RockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool IsPlayable()
        {
            return (HowManyPlayers() > 3);
        }

        public bool Add(string playerName)
        {
            _players.Add(new Player { Name = playerName });
            _inPenaltyBox[HowManyPlayers()] = false;

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
            var currentPlayer = _players[_currentPlayer];
            Console.WriteLine(currentPlayer + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

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
                    _isGettingOutOfPenaltyBox = false;
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
            if (_players[_currentPlayer].Place == 0) return "Pop";
            if (_players[_currentPlayer].Place == 4) return "Pop";
            if (_players[_currentPlayer].Place == 8) return "Pop";
            if (_players[_currentPlayer].Place == 1) return "Science";
            if (_players[_currentPlayer].Place == 5) return "Science";
            if (_players[_currentPlayer].Place == 9) return "Science";
            if (_players[_currentPlayer].Place == 2) return "Sports";
            if (_players[_currentPlayer].Place == 6) return "Sports";
            if (_players[_currentPlayer].Place == 10) return "Sports";
            return "Rock";
        }

        public bool WasCorrectlyAnswered()
        {
            if (_inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _players[_currentPlayer].Purse++;
                    Console.WriteLine(_players[_currentPlayer]
                            + " now has "
                            + _players[_currentPlayer].Purse
                            + " Gold Coins.");

                    var winner = DidPlayerWin();
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;

                    return winner;
                }
                else
                {
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;
                    return true;
                }
            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                _players[_currentPlayer].Purse++;
                Console.WriteLine(_players[_currentPlayer]
                        + " now has "
                        + _players[_currentPlayer].Purse
                        + " Gold Coins.");

                var winner = DidPlayerWin();
                _currentPlayer++;
                if (_currentPlayer == _players.Count) _currentPlayer = 0;

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return true;
        }

        private bool DidPlayerWin()
        {
            return _players[_currentPlayer].Purse != 6;
        }
    }
}
