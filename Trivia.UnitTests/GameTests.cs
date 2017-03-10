using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using UglyTrivia;

namespace Trivia.UnitTests
{
    public class GameTests
    {
        private static bool _notAWinner;

        [Test]
        public void FirstTest()
        {
            var streamWriter = new StringWriter();
            Console.SetOut(streamWriter);
            var aGame = new Game();

            aGame.add("Chet");
            aGame.add("Pat");
            aGame.add("Sue");

            var rand = new Random(2);

            do
            {
                aGame.roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    _notAWinner = aGame.wrongAnswer();
                }
                else
                {
                    _notAWinner = aGame.wasCorrectlyAnswered();
                }
            } while (_notAWinner);


            streamWriter.Flush();
            var actual = streamWriter.GetStringBuilder().ToString();

            var assembly = Assembly.GetExecutingAssembly();
            var codebase = new Uri(assembly.CodeBase);
            var path = codebase.LocalPath;
            var expected = File.ReadAllText(Path.Combine(Directory.GetParent(path).FullName, "ExpectedOutput.txt"));

            Assert.AreEqual(expected, actual);
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

                    aGame.add("Chet");
                    aGame.add("Pat");
                    aGame.add("Sue");

                    do
                    {
                        aGame.roll(rand.Next(5) + 1);

                        if (rand.Next(9) == 7)
                        {
                            _notAWinner = aGame.wrongAnswer();
                        }
                        else
                        {
                            _notAWinner = aGame.wasCorrectlyAnswered();
                        }
                    } while (_notAWinner);

                    streamWriter.Flush();
                }
            }
        }
    }
}
