namespace WordSearch
{
    public interface ISearchAlgorithm
    {
        public abstract int Search(string pattern, string[] list, string[] result);
    }
}
