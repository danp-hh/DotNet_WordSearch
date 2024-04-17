using System.Collections.Generic;
using System.Text;

namespace WordSearch
{
    public class WordListGenerator
    {
        public static string[] Generate(int dimension)
        {
            string[] list = new string[(int)Math.Pow(26, dimension)];
            int j = 0;
            Span<char> word = new Span<char>(new char[dimension]);

            void AddCombinations(Span<char> currentCombination, int charIndex, ref int writeIndex)
            {
                if(charIndex >= dimension)
                {
                    list[j] = currentCombination.ToString();
                    writeIndex++;
                    return;
                }

                for(char c = 'A'; c <= 'Z';c++) 
                {
                    currentCombination[charIndex] = c;
                    AddCombinations(currentCombination, charIndex + 1, ref writeIndex);
                }
            }

            
            AddCombinations(word, 0, ref j);

            return list;

        }
        public static string[] GenerateFixedSizeStringBuilder(int dimension)
        {
            string[] list = new string[(int)Math.Pow(26, dimension)];
            int j = 0;

            Span<char> word = new Span<char>(new char[dimension]);
            var sb = new StringBuilder();

            for (char c1 = 'A'; c1 <= 'Z'; c1++)
            {
                sb.Append(c1);
                for (char c2 = 'A'; c2 <= 'Z'; c2++)
                {
                    sb.Append(c2);
                    for (char c3 = 'A'; c3 <= 'Z'; c3++)
                    {
                        sb.Append(c3);
                        for (char c4 = 'A'; c4 <= 'Z'; c4++)
                        {
                            sb.Append(c4);
                            list[j] = sb.ToString();
                            sb.Clear();
                        }
                    }
                }

            }

            return list;

        }

        public static string[] GenerateFixedSizeStringSpan(int dimension)
        {
            string[] list = new string[(int)Math.Pow(26, dimension)];
            int j = 0;

            Span<char> word = new Span<char>(new char[dimension]);

            for (char c1 = 'A'; c1 <= 'Z'; c1++)
            {
                word[0] = c1;
                for (char c2 = 'A'; c2 <= 'Z'; c2++)
                {
                    word[1] = c2;
                    for (char c3 = 'A'; c3 <= 'Z'; c3++)
                    {
                        word[2] = c3;
                        for (char c4 = 'A'; c4 <= 'Z'; c4++)
                        {
                            word[3] = c4;
                            list[j] = word.ToString();
                        }
                    }
                }
            }

            return list;
        }

        public static string[] GenerateForDynamic(int dimension)
        {
            int arrayLength = (int)Math.Pow(2, dimension);
            string[] list = new string[arrayLength];

            Span<char> word = new Span<char>(new char[dimension]);
            for (int i = 0; i < arrayLength; i++)
            {
                int charIndex = i;
                for(int dim = dimension - 1; dim >= 0; dim--)
                {
                    word[dim] = (char)('A' + charIndex % 26);
                    charIndex /= 26;
                }

                list[i] = word.ToString();
            }

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