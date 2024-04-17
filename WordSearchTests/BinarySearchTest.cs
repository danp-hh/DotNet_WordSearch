using WordSearch;

namespace WordSortTests
{
    [TestClass]
    public class BinarySearchTest
    {
        private string[] InitializeWordArray(int n)
        {
            WordListGenerator generator = new WordListGenerator();
            string[] list = generator.Generate(n).ToArray();

            return list;
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(4)]
        [DataRow(5)]
        public void Search_DimensionIsNAndWordIsNMinus1_ResultHas26ElementsContainingPattern(int dim)
        {
            string pattern = new string('A', dim-1);

            BinarySearch binarySearch = new BinarySearch();
            string[] list = InitializeWordArray(dim);
            string[] result = new string[(int)Math.Pow(26, dim)];

            int foundEntries = binarySearch.Search(pattern, list, result);

            Assert.AreEqual(26, foundEntries);

            foreach (var elem in result.Take(foundEntries))
            {
                StringAssert.Contains(elem, pattern);
            }

        }

    }
}
