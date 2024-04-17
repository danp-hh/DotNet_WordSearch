using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;

namespace WordSearchUI
{
    class WordSearchViewModel : INotifyPropertyChanged
    {
        public IAsyncRelayCommand GenerateCommand { get; }

        public WordSearchViewModel() 
        {
            GenerateCommand = new AsyncRelayCommand(GenerateWordList);
            _dimension = "4";
            _searchWord = string.Empty;
            _wordList = new ObservableCollection<string>();
            IsLoading = false;
            OriginalList = new List<string>();
            _elapsedTime = "0";
        
        }

        private async Task GenerateWordList()
        {
            if(int.TryParse(Dimension, out int dimension))
            {
                ErrorText = "";
                IsLoading = true;
                await Task.Run(() =>
                {
                    WordSearch.WordListGenerator generator = new WordSearch.WordListGenerator();
                    string[] list = generator.Generate(dimension);
                    WordSearch.WordListGenerator.Shuffle(list);
                    WordList = new ObservableCollection<string>(list);
                    OriginalList = new List<string>(list);
                });
                IsLoading = false;
            }
            else
            {
                ErrorText = "Only Integers are allowed! [0-9]";
            }

        }

        private async Task ExecuteSearchWord()
        {
            _isLoading = true;
            string[] result = new string[(int)Math.Pow(26, int.Parse(Dimension)-SearchWord.Length)];
            double totalMilliseconds = 0;
            string[] wordListArray = WordList.ToArray();
            int filledElementsIndex = 0;

            await Task.Run(() =>
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                WordSearch.ISearchAlgorithm search = new WordSearch.LinearSearch();
                filledElementsIndex = search.Search(SearchWord, wordListArray, result);
                stopWatch.Stop();
                totalMilliseconds = stopWatch.Elapsed.TotalMilliseconds;
            });

            ElapsedTime = totalMilliseconds.ToString();
            WordList = new ObservableCollection<string>(result.Take(filledElementsIndex));
            _isLoading = false;
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set 
            { 
                _isLoading = value;
                NotifyPropertyChanged("IsLoading");
            }
        }

        private List<string> OriginalList;


        private ObservableCollection<string> _wordList;
        public ObservableCollection<string> WordList
        { 
            get { return _wordList; } 
            set 
            { 
                _wordList = value;
                NotifyPropertyChanged("WordList");
            }
        }

        private string _searchWord;
        public string SearchWord
        {
            get { return _searchWord; }
            set
            {
                // a char has been removed from SearchWord, use original list again
                if(_searchWord.Length > value.Length)
                {
                    WordList = new ObservableCollection<string>(OriginalList);
                    ElapsedTime = "0";
                }

                _searchWord = value.ToUpper();
                
                string pattern = "^[A-Z]+";
                bool isValid = Regex.IsMatch(_searchWord, pattern);

                // only Search, when no other Search is currently running
                // we can not set async in this setter. Can we find a better solution?
                if (isValid && !_isLoading)
                {
                    ExecuteSearchWord();
                }
            }
        }

        private string _dimension;
        public string Dimension
        {
            get { return _dimension.ToString(); }
            set
            {
                _dimension = value;
            }
        }

        private string _elapsedTime;
        public string ElapsedTime
        {
            get { return _elapsedTime; }
            set 
            { 
                _elapsedTime = value;
                NotifyPropertyChanged("ElapsedTime");
            }
        }

        private string _errorText;
        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                NotifyPropertyChanged("ErrorText");
            }
        }
        
        // <summary>
        /// This is used to notify the view if any data changes
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
