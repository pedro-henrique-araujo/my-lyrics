using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace MyLyrics.Logic.UnitTests
{
    [TestClass]
    public class TextHelperTests
    {
        [TestMethod]
        [DataRow("Input1", "Expected1")]
        [DataRow("Input2", "Expected2")]
        [DataRow("Input3", "Expected3")]
        public void RemoveRepeatedSections_FromFiles_ReturnExpectedResult(string inputFileName, string expectedFileName)
        {
            var inputText = File.ReadAllText(inputFileName + ".txt").Replace("\r", "");
            var expected = File.ReadAllText(expectedFileName + ".txt").Replace("\r", "");
            var actual = TextHelper.RemoveRepeatedSections(inputText);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("a", "a")]
        [DataRow(null, null)]
        public void RemoveRepeatedSections_ReturnExpected(string inputText, string expected)
        {
            var actual = TextHelper.RemoveRepeatedSections(inputText);
            Assert.AreEqual(expected, actual);
        }
    }
}
