using WordSearch;

namespace WordSortTests
{
    [TestClass]
    public class LinearSearchTest
    {

        private string InitializeSearchWord(int n) 
        {
            string word = string.Empty;

            for (char c = 'A'; c <= 'A' + n - 1; c++)
            {
                word += c;
            }

            return word;

        }

        private string[] InitializeWordArray(int n) 
        {
            WordListGenerator generator = new WordListGenerator();
            string[] list = generator.Generate(n).ToArray();
            WordListGenerator.Shuffle(list);

            return list;
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(4)]
        [DataRow(5)]
        public void StartsWith_DimensionIsNAndWordIsNMinus1_SearchResultHas26ElementsContainingPattern(int n)
        {
            string word = InitializeSearchWord(n-1);
            string[] list = InitializeWordArray(n);

            string[] result = new string[26];
            int foundEntries = LinearSearch.StartsWith(word, list, result);

            Assert.AreEqual(26, foundEntries);

            foreach(var elem in result.Take(foundEntries))
            {
                StringAssert.Contains(elem, word);
            }

        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(4)]
        [DataRow(5)]
        public void ParallelStartsWith_DimensionIsNAndWordIsNMinus1_SearchResultHas26ElementsContainingPattern(int n)
        {
            string word = InitializeSearchWord(n - 1);
            string[] list = InitializeWordArray(n);

            string[] result = new string[26];
            int foundEntries = LinearSearch.ParallelStartsWith(word, list, result);

            Assert.AreEqual(26, foundEntries);

            foreach (var elem in result.Take(foundEntries)) 
            { 
                StringAssert.Contains(elem, word);
            }
        }
    }

}
