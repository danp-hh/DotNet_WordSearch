using System.Collections.Generic;

namespace WordSearch
{
    public class WordListGenerator
    {
        public static string[] Generate(int dimension)
        {
            string[] list = new string[(int)Math.Pow(26, dimension)];
            int j = 0;

            void AddCombinations(string currentCombination, int remainingLength)
            {
                if(remainingLength == 0)
                {
                    list[j] = currentCombination;
                    j++;
                    return;
                }

                for(char c = 'A'; c <= 'Z';c++) 
                {
                    AddCombinations(currentCombination + c, remainingLength - 1);
                }
            }

            string startWord = string.Empty;
            AddCombinations(startWord, dimension);

            return list;

        }

        public static void Shuffle(string[] list)
        {
            Random random = new Random();

            int n = list.Length;
            while(n > 1) 
            {
                n--;
                int k = random.Next(n + 1);
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Shuffle(List<string> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }
}