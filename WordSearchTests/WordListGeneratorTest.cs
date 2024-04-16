using WordSearch;
using System.Collections.Generic;

namespace WordSortTests
{
    [TestClass]
    public class WordListGeneratorTest
    {
        [TestMethod]
        [DataRow(2)]
        [DataRow(4)]
        [DataRow(5)]

        public void Generate_DimensionIsN_26SquaredNElements(int n)
        {
            string[] elements = WordListGenerator.Generate(n);

            Assert.AreEqual(Math.Pow(26, n), elements.Length);
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(4)]
        [DataRow(5)]
        public void Shuffle_DimensionIsN_AfterShuffleFirstNSquaredNElementsAreNotTheSameAsBefore(int n)    
        {
            string[] elements = WordListGenerator.Generate(n);
            List<string> before_shuffling = elements.ToList().GetRange(0, (int) Math.Pow(n, n));
            WordListGenerator.Shuffle(elements);
            List<string> after_shuffling = elements.ToList().GetRange(0, (int) Math.Pow(n, n));

            CollectionAssert.AreNotEqual(before_shuffling, after_shuffling);
        }


    }
}