using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using UglyTrivia;

namespace Trivia.UnitTests
{
    public class GameTests
    {
        private static bool _notAWinner;

        [Test]
        public void CompareWithGoldenMaster()
        {
            for (var seed = 0; seed <= 100; seed++)
            {
                var streamWriter = new StringWriter();
                Console.SetOut(streamWriter);
                var aGame = new Game();

                aGame.Add("Chet");
                aGame.Add("Pat");
                aGame.Add("Sue");

                var rand = new Random(seed);

                do
                {
                    aGame.Roll(rand.Next(5) + 1);

                    if (rand.Next(9) == 7)
                    {
                        _notAWinner = aGame.WrongAnswer();
                    }
                    else
                    {
                        _notAWinner = aGame.WasCorrectlyAnswered();
                    }
                } while (_notAWinner);

                streamWriter.Flush();
                var actual = streamWriter.GetStringBuilder().ToString();

                var assembly = Assembly.GetExecutingAssembly();
                var codebase = new Uri(assembly.CodeBase);
                var path = codebase.LocalPath;
                var expected = File.ReadAllText(Path.Combine(Directory.GetParent(path).FullName, "..", "..", "GoldenMasters", $"ExpectedOutput{seed}.txt"));

                Assert.AreEqual(expected, actual);
            }
        }

        [Test]
        [Ignore("Golden master - generated before refactoring")]
        public void GoldenMasterGenerator()
        {
            for (var seed = 0; seed <= 100; seed++)
            {
                var rand = new Random(seed);

                var assembly = Assembly.GetExecutingAssembly();
                var codebase = new Uri(assembly.CodeBase);
                var path = codebase.LocalPath;
                var savePath = Path.Combine(Directory.GetParent(path).FullName, "..", "..", "GoldenMasters", $"ExpectedOutput{seed}.txt");
                using (var streamWriter = new StreamWriter(savePath))
                {
                    Console.SetOut(streamWriter);
                    var aGame = new Game();

                    aGame.Add("Chet");
                    aGame.Add("Pat");
                    aGame.Add("Sue");

                    do
                    {
                        aGame.Roll(rand.Next(5) + 1);

                        if (rand.Next(9) == 7)
                        {
                            _notAWinner = aGame.WrongAnswer();
                        }
                        else
                        {
                            _notAWinner = aGame.WasCorrectlyAnswered();
                        }
                    } while (_notAWinner);

                    streamWriter.Flush();
                }
            }
        }
    }
}
