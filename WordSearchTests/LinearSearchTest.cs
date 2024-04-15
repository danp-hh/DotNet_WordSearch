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

        [TestMethod]
        [DataRow(2)]
        [DataRow(4)]
        [DataRow(5)]
        public void SearchEquals_DimensionIsN_WordInSearchResults(int n)
        {
            string word = string.Empty;

            for(char c = 'A';  c <= 'A' + n - 1; c++) 
            {
                word += c;            
            }

            string[] list = WordListGenerator.generate(n).ToArray();
            WordListGenerator.Shuffle(list);
            string[] result = new string[1];
            LinearSearch.SearchEquals(word, list, result);

            CollectionAssert.AreEqual(new List<string>() { word }, result);
        }


        [TestMethod]
        [DataRow(2)]
        [DataRow(4)]
        [DataRow(5)]
        public void ParallelSearchEquals_DimensionIsN_WordInSearchResults(int n)
        {
            string word = string.Empty;

            for (char c = 'A'; c <= 'A' + n - 1; c++)
            {
                word += c;
            }

            string[] list = WordListGenerator.generate(n).ToArray();
            WordListGenerator.Shuffle(list);
            string[] result = new string[1];
            LinearSearch.ParallelSearchEquals(word, list, result);

            CollectionAssert.AreEqual(new List<string>() { word }, result);
        }
    }

}
