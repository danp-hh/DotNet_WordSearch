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
            List<string> elements = WordListGenerator.generate(n);

            Assert.AreEqual(Math.Pow(26, n), elements.Count);
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(4)]
        [DataRow(5)]
        public void Shuffle_DimensionIsN_AfterShuffleFirstNSquaredNElementsAreNotTheSameAsBefore(int n)    
        {
            List<string> elements = WordListGenerator.generate(n);
            List<string> before_shuffling = elements.GetRange(0, (int) Math.Pow(n, n));
            WordListGenerator.Shuffle(elements);
            List<string> after_shuffling = elements.GetRange(0, (int) Math.Pow(n, n));

            CollectionAssert.AreNotEqual(before_shuffling, after_shuffling);
        }


    }
}