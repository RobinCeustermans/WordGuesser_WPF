using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WordGuessGame_WPF.ViewModels.Helpers;
using WordGuessGame_WPF.ViewModels.Interfaces;

namespace WordGuessGame_WPF.ViewModels
{
    public class WordGuesserWindowModel : BaseViewModel
    {
        private readonly IWordGuessCheck _game;
        private ObservableCollection<TextBox> _guessTextBoxes;
        private string _statusTextBlock;
        private bool _isSubmitButtonEnabled;

        public string StatusTextBlock
        {
            get => _statusTextBlock;
            set
            {
                _statusTextBlock = value;
                NotifyPropertyChanged(nameof(StatusTextBlock));
            }
        }

        public ObservableCollection<TextBox> GuessTextBoxes
        {
            get => _guessTextBoxes;
            set
            {
                _guessTextBoxes = value;
                NotifyPropertyChanged(nameof(GuessTextBoxes));
            }
        }

        public bool IsSubmitButtonEnabled
        {
            get => _isSubmitButtonEnabled;
            set
            {
                _isSubmitButtonEnabled = value;
                NotifyPropertyChanged(nameof(IsSubmitButtonEnabled));
            }
        }

        public override string this[string columnName] => string.Empty;

        public WordGuesserWindowModel(IWordGuessCheck game)
        {
            _game = game;
            InitializeGuessGrid();
            IsSubmitButtonEnabled = true;
        }

        private void InitializeGuessGrid()
        {
            _guessTextBoxes = new ObservableCollection<TextBox>();

            for (int i = 0; i < 30; i++)
            {
                var textBox = TextBoxHelper.CreateTextBox(TextBoxTextChangedHandler, TextBoxPreviewKeyDownHandler);
                _guessTextBoxes.Add(textBox);
            }

            TextBoxHelper.EnableTextBoxesForCurrentRow(_guessTextBoxes, _game.CurrentAttempt);
        }

        private void TextBoxPreviewKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (e.Key == Key.Enter)
                {
                    Execute("SubmitGuess");
                }
                else if (e.Key == Key.Back)
                {
                    TextBoxHelper.HandleTextBoxPreviewKeyDown(sender, e, _guessTextBoxes, _game.CurrentAttempt);
                    e.Handled = true;
                }
            }
        }

        private void TextBoxTextChangedHandler(object sender, TextChangedEventArgs e)
        {
            TextBoxHelper.HandleTextBoxTextChanged(sender, _guessTextBoxes);
        }

        private void SubmitGuess()
        {
            if (_game.CurrentAttempt >= 6)
            {
                TextBlockHelper.UpdateStatus(ref _statusTextBlock, "Game Over! You've used all attempts.");
                return;
            }

            var guess = CollectCurrentGuess();
            if (guess.Length != 5)
            {
                ShowInvalidGuessMessage();
                return;
            }

            var result = _game.CheckGuess(guess);
            ProcessGuessResult(guess, result);

            if (_game.IsCorrectGuess(guess))
            {
                TextBlockHelper.UpdateStatus(ref _statusTextBlock, "Congratulations! You guessed the word!");
                TextBoxHelper.DisableAllTextBoxes(_guessTextBoxes);
                IsSubmitButtonEnabled = false;
            }
            else if (_game.IsGameOver())
            {
                TextBlockHelper.UpdateStatus(ref _statusTextBlock, "Game Over! You've used all attempts.");
                IsSubmitButtonEnabled = false;
            }
            else
            {
                TextBlockHelper.UpdateStatus(ref _statusTextBlock, $"Attempts left: {6 - _game.CurrentAttempt}");
                _game.CurrentAttempt++;
                TextBoxHelper.EnableTextBoxesForCurrentRow(_guessTextBoxes, _game.CurrentAttempt);
                TextBoxHelper.FocusNextRowFirstTextBox(_guessTextBoxes, _game.CurrentAttempt);
            }
        }

        private string CollectCurrentGuess()
        {
            int startIndex = _game.CurrentAttempt * 5;
            var guessLetters = new List<string>();

            for (int i = startIndex; i < startIndex + 5; i++)
            {
                var textBox = _guessTextBoxes[i];
                guessLetters.Add(textBox.Text.Trim().ToLower());
            }

            return string.Join("", guessLetters);
        }

        private void ShowInvalidGuessMessage()
        {
            MessageBox.Show("Guess must be 5 letters long.", "Invalid Guess", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ProcessGuessResult(string guess, string result)
        {
            int startIndex = _game.CurrentAttempt * 5;
            var letterCounts = CalculateLetterCounts(guess);

            for (int i = 0; i < result.Length; i++)
            {
                var textBox = _guessTextBoxes[startIndex + i];
                textBox.Text = guess[i].ToString().ToUpper();
                TextBoxHelper.UpdateTextBoxBackground(textBox, result[i], guess[i], letterCounts);
                textBox.IsEnabled = false;
            }
        }

        private Dictionary<char, int> CalculateLetterCounts(string guess)
        {
            var letterCounts = new Dictionary<char, int>();

            foreach (var c in guess)
            {
                if (!letterCounts.ContainsKey(c))
                {
                    letterCounts[c] = 0;
                }
                letterCounts[c]++;
            }

            return letterCounts;
        }

        public override bool CanExecute(object? parameter)
        {
            return parameter?.ToString() == "SubmitGuess";
        }

        public override void Execute(object? parameter)
        {
            if (parameter?.ToString() == "SubmitGuess")
            {
                SubmitGuess();
            }
        }
    }
}
