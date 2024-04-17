namespace WordSearch
{
    public class BinarySearch : ISearchAlgorithm
    {
        public static int SearchPrefix(string pattern, string[] sortedArray, string[] result)
        {
            int left = FindLeftmostIndex(sortedArray, pattern);
            int right = FindRightmostIndex(sortedArray, pattern);

            if (left == -1 || right == -1)
            {
                return 0;
            }

            int matchCount = right - left + 1;

            Array.Copy(sortedArray, left, result, 0, matchCount);

            return matchCount;
        }


        private static int FindLeftmostIndex(string[] sortedArray, string prefix)
        {
            int left = 0;
            int right = sortedArray.Length - 1;
            int result = -1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (sortedArray[mid].StartsWith(prefix))
                {
                    result = mid;
                    right = mid - 1; // Continue searching in the left half
                }
                else if (string.Compare(sortedArray[mid], prefix) > 0)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            return result;
        }

        private static int FindRightmostIndex(string[] sortedArray, string prefix)
        {
            int left = 0;
            int right = sortedArray.Length - 1;
            int result = -1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (sortedArray[mid].StartsWith(prefix))
                {
                    result = mid;
                    left = mid + 1; // Continue searching in the right half
                }
                else if (string.Compare(sortedArray[mid], prefix) > 0)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            return result;
        }

        public int Search(string pattern, string[] list, string[] result)
        {
            return SearchPrefix(pattern, list, result);
        }
    }
}
