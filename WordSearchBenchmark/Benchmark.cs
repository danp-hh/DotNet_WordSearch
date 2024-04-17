using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using WordSearch;

namespace WordSearchBenchmark
{
    public class SearchBenchmark
    {
        IWordListGenerator generator = new WordListGenerator();
        public string[] SearchWords = new string[] { "A", "AB", "ABC", "ABCD", "ABCDE" };
        public string SearchWord = "";
        public string[] Result;
        public string[] WordListRandom;
        public string[] WordListSorted;
        public string[] SortList;

        [Params(4, 5)]
        public int Dimension { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            WordListRandom = generator.Generate(Dimension);
            WordSearch.WordListGenerator.Shuffle(WordListRandom);
            WordListSorted = generator.Generate(Dimension);

            SortList = new string[WordListRandom.Length];
            WordListRandom.CopyTo(SortList, 0);

            Result = new string[WordListRandom.Length];
            SearchWord = SearchWords[Dimension - 1];
        }

        [Benchmark]
        public int ParallelLinearSearch()
        {
            return WordSearch.LinearSearch.ParallelStartsWith(SearchWord, WordListRandom, Result);
        }

        [Benchmark]
        public int BinarySearch()
        {
            return WordSearch.BinarySearch.SearchPrefix(SearchWord, WordListSorted, Result);
        }

        [Benchmark]
        public int Sort()
        {   
            string[] sortedArray = SortList.OrderBy(str => str).ToArray();
            return sortedArray.Length;
        }

    }


    public class LinearSearchBenchmark
    {

        public string[] SearchWords = new string[] { "A", "AB", "ABC", "ABCD", "ABCDE" };
        public string SearchWord = "";
        public string[] Result;
        public string[] WordList;
        IWordListGenerator generator = new WordListGenerator();

        public string[] WordListSorted;

        [Params(4, 5)]
        public int dimensions { get; set; }


        [GlobalSetup]
        public void Setup()
        {  
            WordList = generator.Generate(dimensions);
            WordSearch.WordListGenerator.Shuffle(WordList);
            Result = new string[WordList.Length];
            SearchWord = SearchWords[dimensions - 1];

            WordListSorted = generator.Generate(dimensions);
        }

        [Benchmark]
        public int LinearSearch()
        {
            return WordSearch.LinearSearch.StartsWith(SearchWord, WordList, Result);
        }

        [Benchmark]
        public int ParallelLinearSearch()
        {
            return WordSearch.LinearSearch.ParallelStartsWith(SearchWord, WordList, Result);
        }

        [Benchmark]
        public int ParallelLinearSearchConcurrentQueue()
        {
            return WordSearch.LinearSearch.ParallelStartsWithConrurrentQueue(SearchWord, WordList, Result);
        }

    }


    public class WordGenerationBenchmark
    {
        public string[]? WordList;

        [Params(4, 5)]
        public int Length { get; set; }

        [Benchmark]
        public int GenerateRecursiveStringSpan()
        {
            WordList = WordSearch.WordListGenerator.GenerateRecursiveStringSpan(Length);
            return WordList.Length;
        }

        [Benchmark]
        public int GenerateFixedSizeString()
        {
            if (Length == 4)
                WordList = WordSearch.WordListGenerator.GenerateFixedSizeString_N4();
            else if (Length == 5)
                WordList = WordSearch.WordListGenerator.GenerateFixedSizeString_N5();
            else
                WordList = new string[Length];
            
            return WordList.Length;
        }

        [Benchmark]
        public int GenerateFixedSizeStringSpan()
        {
            if (Length == 4)
                WordList = WordSearch.WordListGenerator.GenerateFixedSizeStringSpan_N4();
            else if (Length == 5)
                WordList = WordSearch.WordListGenerator.GenerateFixedSizeStringSpan_N5();
            else
                WordList = new string[0];

            return WordList.Length;
        }

        [Benchmark]
        public int GenerateDynamicStringSpan()
        {
            WordList = WordSearch.WordListGenerator.GenerateDynamicStringSpan(Length);
            return WordList.Length;
        }

    }

    public class Benchmark
    {
        
        public static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<WordGenerationBenchmark>();
            var summary = BenchmarkRunner.Run<SearchBenchmark>();
        }
    }
}