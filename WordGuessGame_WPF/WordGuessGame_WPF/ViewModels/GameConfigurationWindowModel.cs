using System.Windows;
using WordGuessGame_DAL.FileOperations;
using WordGuessGame_WPF.Models;
using WordGuessGame_WPF.ViewModels.Helpers;
using WordGuessGame_WPF.Views;
using WordGuessGame_DAL.Context;

namespace WordGuessGame_WPF.ViewModels
{
    public class GameConfigurationWindowModel : BaseViewModel
    {
        private readonly Window _window;
        private byte? _turnsAmount;
        private string _playerName;
        private byte _selectedWordLength;
        private List<byte> _wordLengths;
        private List<string> _words;

        public GameConfigurationWindowModel(Window window)
        {
            _window = window;
            ReadFile();
        }

        public byte? TurnsAmount
        {
            get => _turnsAmount;
            set
            {
                _turnsAmount = value;
                NotifyPropertyChanged(nameof(TurnsAmount));
            }
        }

        public string PlayerName
        {
            get => _playerName;
            set
            {
                _playerName = value;
                NotifyPropertyChanged(nameof(PlayerName));
            }
        }

        public List<string> Words
        {
            get => _words;
            set
            {
                _words = value;
                NotifyPropertyChanged(nameof(Words));
            }
        }

        // For dropdown
        public byte SelectedWordLength
        {
            get => _selectedWordLength;
            set
            {
                _selectedWordLength = value;
                NotifyPropertyChanged(nameof(SelectedWordLength));
            }
        }

        public List<byte> WordLengths
        {
            get => _wordLengths;
            set
            {
                _wordLengths = value;
                NotifyPropertyChanged(nameof(WordLengths));
            }
        }

        private void ReadFile()
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "words.txt");
            this.Words = FileReader.ReadFile(filePath);
            this.WordLengths = FileReader.GetWordLengths(this.Words);
        }

        public override string this[string columnName] => throw new NotImplementedException();

        public override bool CanExecute(object? parameter)
        {
            if (parameter == null)
                return false;
            switch (parameter.ToString())
            {
                case "OpenWordGuesserGame":
                    return true;
                default:
                    return false;
            }
        }

        public override void Execute(object? parameter)
        {
            if (parameter == null)
                return;

            switch (parameter.ToString())
            {
                case "OpenWordGuesserGame":
                    OpenWordGuesserGame();
                    break;
            }
        }

        private void OpenWordGuesserGame()
        {
            if (!TurnsAmount.HasValue || string.IsNullOrEmpty(PlayerName) || SelectedWordLength <= 0)
            {
                MessageBox.Show("Please make sure all fields are filled correctly.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var gameModel = new GuessGameModel
            {
                PlayerName = PlayerName,
                WordLength = SelectedWordLength,
                TurnsAmount = TurnsAmount.Value,
                PotentialWords = this.Words.Where(x => x.Length == SelectedWordLength).ToList()
            };

            var gameChecker = new WordGuessCheck(gameModel);

            // Create the context and GameDataManager
            var context = new WordGuessGameEntities();

            WordGuesserWindowModel viewModel = new WordGuesserWindowModel(gameChecker, GameManager, gameModel);

            WordGuesserWindow wordGuesserWindow = new WordGuesserWindow
            {
                DataContext = viewModel
            };

            this._window.Close();
            wordGuesserWindow.ShowDialog();
        }
    }
}
