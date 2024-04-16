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
        public string[] List_N4 = WordSearch.WordListGenerator.Generate(4).ToArray();

        public string SearchWord_N3 = "AB";
        public string[] Result_N3 = new string[26];
        public string[] List_N3 = WordSearch.WordListGenerator.Generate(3).ToArray();





        [IterationSetup]
        public void Setup()
        {
            WordSearch.WordListGenerator.Shuffle(List_N4);
            WordSearch.WordListGenerator.Shuffle(List_N3);
        }

        [Benchmark]
        public void N4_LinearSearch() => WordSearch.LinearSearch.StartsWith(SearchWord_N4, List_N4, Result_N4);

        [Benchmark]
        public void N4_ParallelLinearSearch() => WordSearch.LinearSearch.ParallelStartsWith(SearchWord_N4, List_N4, Result_N4);

        [Benchmark]
        public void N3_LinearSearch() => WordSearch.LinearSearch.StartsWith(SearchWord_N3 , List_N3, Result_N3);

        [Benchmark]
        public void N3_ParallelLinearSearch() => WordSearch.LinearSearch.StartsWith(SearchWord_N3, List_N3, Result_N3);

    }


    public class Program
    {
        
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<WordSearchBenchmark>();
        }
    }
}