using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace Trivia.UnitTests
{
    public class GameTests
    {
        [Test]
        public void CompareWithGoldenMaster()
        {
            for (var seed = 0; seed <= 100; seed++)
            {
                var streamWriter = new StringWriter();
                Console.SetOut(streamWriter);
                GameRunner.Main(new [] {seed + ""});
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
                var assembly = Assembly.GetExecutingAssembly();
                var codebase = new Uri(assembly.CodeBase);
                var path = codebase.LocalPath;
                var savePath = Path.Combine(Directory.GetParent(path).FullName, "..", "..", "GoldenMasters", $"ExpectedOutput{seed}.txt");
                using (var streamWriter = new StreamWriter(savePath))
                {
                    Console.SetOut(streamWriter);
                    GameRunner.Main(new[] { seed + "" });
                    streamWriter.Flush();
                }
            }
        }
    }
}
