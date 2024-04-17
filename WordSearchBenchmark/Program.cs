using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using WordSearch;

namespace WordSearchBenchmark
{
    public class WordSearchBenchmark
    {
        SearchAlgorithm algorithm = new LinearSearch();

        public string SearchWord_N4 = "ABC";
        public string[] Result_N4 = new string[26];
        public string[] WordList = WordSearch.WordListGenerator.Generate(4).ToArray();

        // TODO add 3 again
        [Params(4)]
        public int dimensions { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            WordList = WordSearch.WordListGenerator.Generate(dimensions).ToArray();
            WordSearch.WordListGenerator.Shuffle(WordList);
        }

        //[Benchmark]
        public int LinearSearch() => WordSearch.LinearSearch.StartsWith(SearchWord_N4, WordList, Result_N4);

        [Benchmark]
        public void ParallelLinearSearch() => WordSearch.LinearSearch.ParallelStartsWith(SearchWord_N4, WordList, Result_N4);

    }


    public class WordGenerationBenchmark
    {
        public string[] WordList;

        [Params(4)]
        public int Length { get; set; }

        [Benchmark]
        public int GenerateRecursive()
        {
            WordList = WordSearch.WordListGenerator.GenerateFixedSizeStringBuilder(Length);
            return WordList.Length;
        }

        [Benchmark]
        public int GenerateFixedSizeStringBuilder()
        {
            WordList = WordSearch.WordListGenerator.GenerateFixedSizeStringBuilder(Length);
            return WordList.Length;
        }

        [Benchmark]
        public int GenerateFixedSizeStringSpan()
        {
            WordList = WordSearch.WordListGenerator.GenerateFixedSizeStringSpan(Length);
            return WordList.Length;
        }

        [Benchmark]
        public int GenerateForDynamicStringSpan()
        {
            WordList = WordSearch.WordListGenerator.GenerateForDynamic(Length);
            return WordList.Length;
        }

    }

        public class Program
    {
        
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<WordGenerationBenchmark>();
        }
    }
}