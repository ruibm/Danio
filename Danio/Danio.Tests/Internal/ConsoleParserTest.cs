namespace Ruibm.Danio.Tests.Internal
{
    using NUnit.Framework;
    using Ruibm.Danio.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class ConsoleParserTest
    {
        private ConsoleParser _parser;

        [SetUp]
        public void SetUp()
        {
            _parser = new ConsoleParser();
        }

        [Test]
        [TestCase(new string[] { }, 0)]
        [TestCase(new string[] { "--hello=21" }, 1)]
        [TestCase(new string[] { "--hello", "21" }, 1)]
        [TestCase(new string[] { "--super", "42", "--cool=21" }, 2)]
        [TestCase(new string[] { "--super", "42", "--cool", "21" }, 2)]
        [TestCase(new string[] { "--super=42", "--cool=21" }, 2)]
        [TestCase(new string[] { "--super=42", "--cool", "21" }, 2)]
        [TestCase(new string[] { "--super", "--cool" }, 1)]
        public void TestValidArguments(string[] args, int expectedArgumentCount)
        {
            ParseResult result = _parser.ParseArgs(args);
            Assert.IsTrue(result.Success, string.Join(", ", args));
            Assert.AreEqual(expectedArgumentCount, result.Arguments.Count, string.Join(", ", args));
            Assert.AreEqual(0, result.ErrorLog.Count, string.Join(", ", args));
        }

        [Test]
        [TestCase(new string[] { "--hello 21" }, 0, 1)]
        [TestCase(new string[] { "--hell%$#@^o", "21" }, 0, 2)]
        [TestCase(new string[] { "super", "42"}, 0, 2)]
        [TestCase(new string[] { "--cool=" }, 0, 1)]
        [TestCase(new string[] { "--super" }, 0, 1)]
        public void TestInvalidArguments(string[] args, int expectedArgumentCount, int expectedErrorCount)
        {
            ParseResult result = _parser.ParseArgs(args);
            Assert.IsFalse(result.Success, string.Join(", ", args));
            Assert.AreEqual(expectedArgumentCount, result.Arguments.Count, string.Join(", ", args));
            Assert.AreEqual(expectedErrorCount, result.ErrorLog.Count, string.Join(", ", args));
        }
    }
}
