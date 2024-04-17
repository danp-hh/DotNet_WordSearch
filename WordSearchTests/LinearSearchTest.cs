using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void StartsWith_DimensionIsNAndWordIsNMinus1_SearchResultHas26ElementsContainingWord(int n)
        {
            string word = InitializeSearchWord(n-1);
            string[] list = InitializeWordArray(n);

            string[] result = new string[26];
            LinearSearch.StartsWith(word, list, result);

            foreach(var elem in result)
            {
                StringAssert.Contains(elem, word);
            }

        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(4)]
        [DataRow(5)]
        public void ParallelStartsWith_DimensionIsNAndWordIsNMinus1_SearchResultHas26ElementsContainingWord(int n)
        {
            string word = InitializeSearchWord(n - 1);
            string[] list = InitializeWordArray(n);

            string[] result = new string[26];
            LinearSearch.ParallelStartsWith(word, list, result);

            foreach(var elem in result ) 
            { 
                StringAssert.Contains(elem, word);
            }
        }
    }

}
