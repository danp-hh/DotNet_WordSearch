using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;
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
            try
            {
                int dimension = int.Parse(Dimension);

                IsLoading = true;
                await Task.Run(() =>
                {
                    string[] list = WordSearch.WordListGenerator.Generate(dimension);
                    WordSearch.WordListGenerator.Shuffle(list);
                    WordList = new ObservableCollection<string>(list);
                    OriginalList = new List<string>(list);
                });
                IsLoading = false;
            }
            catch(Exception ex) 
            { }

        }

        private async Task ExecuteSearchWord()
        {
            string[] list = WordList.ToArray();
            string[] result = new string[(int)Math.Pow(26, int.Parse(Dimension)-SearchWord.Length)];
            double totalMilliseconds = 0;

            await Task.Run(() =>
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                WordSearch.LinearSearch.ParallelStartsWith(SearchWord, list, result);
                stopWatch.Stop();
                totalMilliseconds = stopWatch.Elapsed.TotalMilliseconds;
            });

            ElapsedTime = totalMilliseconds.ToString();

            WordList = new ObservableCollection<string>(result);

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
                }

                _searchWord = value;
                
                // NotifyPropertyChanged("SearchWord");

                string pattern = "^[A-Z]+";
                bool isValid = Regex.IsMatch(value, pattern);
                if (isValid)
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
