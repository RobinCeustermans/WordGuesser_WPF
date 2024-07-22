using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WordGuessGame_WPF.ViewModels
{
    public class WordGuesserWindowModel : BaseViewModel
    {
        private WordGuessCheck game;
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

        public override string this[string columnName] => throw new NotImplementedException();

        public WordGuesserWindowModel()
        {
            game = new WordGuessCheck();
            InitializeGuessGrid();
            IsSubmitButtonEnabled = true; // Initially enable the submit button
        }

        private void InitializeGuessGrid()
        {
            _guessTextBoxes = new ObservableCollection<TextBox>();

            for (int i = 0; i < 30; i++)
            {
                var textBox = new TextBox
                {
                    FontSize = 24,
                    Width = 50,
                    Height = 50,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    MaxLength = 1,
                    IsEnabled = false  // Initially disable all TextBoxes
                };

                // Register TextChanged event handler
                textBox.TextChanged += TextBoxTextChangedHandler;

                // Register PreviewKeyDown event handler to detect Enter key press and Backspace
                textBox.PreviewKeyDown += TextBoxPreviewKeyDownHandler;

                _guessTextBoxes.Add(textBox);
            }

            // Enable the TextBoxes for the first row
            EnableTextBoxesForCurrentRow();
        }

        private void SubmitGuess()
        {
            if (game.CurrentAttempt >= 6)
            {
                StatusTextBlock = "Game Over! You've used all attempts.";
                return;
            }

            // Collect current guess
            int startIndex = game.CurrentAttempt * 5;
            List<string> guessLetters = new List<string>();

            for (int i = startIndex; i < startIndex + 5; i++)
            {
                var textBox = _guessTextBoxes[i];
                guessLetters.Add(textBox.Text.Trim().ToLower());
            }

            string guess = string.Join("", guessLetters);

            if (guess.Length != 5)
            {
                MessageBox.Show("Guess must be 5 letters long.", "Invalid Guess", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string result = game.CheckGuess(guess);

            // Track which positions in the guess have been highlighted
            HashSet<char> highlightedLetters = new HashSet<char>();

            // Count occurrences of each letter in guess and correct word
            Dictionary<char, int> guessLetterCounts = new Dictionary<char, int>();
            Dictionary<char, int> correctLetterCounts = new Dictionary<char, int>();

            foreach (char c in guess)
            {
                if (guessLetterCounts.ContainsKey(c))
                    guessLetterCounts[c]++;
                else
                    guessLetterCounts[c] = 1;
            }

            foreach (char c in game.CorrectWord)
            {
                if (correctLetterCounts.ContainsKey(c))
                    correctLetterCounts[c]++;
                else
                    correctLetterCounts[c] = 1;
            }

            // Determine the maximum number of mismatches that can be colored yellow for each letter
            Dictionary<char, int> maxYellowCounts = new Dictionary<char, int>();

            foreach (char c in guessLetterCounts.Keys)
            {
                if (correctLetterCounts.ContainsKey(c))
                {
                    int maxCount = Math.Min(guessLetterCounts[c], correctLetterCounts[c]);
                    maxYellowCounts[c] = maxCount;
                }
                else
                {
                    maxYellowCounts[c] = 0;
                }
            }

            for (int i = 0; i < result.Length; i++)
            {
                _guessTextBoxes[startIndex + i].Text = guess[i].ToString().ToUpper();

                // Set background color based on result
                switch (result[i])
                {
                    case '1':
                        _guessTextBoxes[startIndex + i].Background = Brushes.Green; // Correct letter in the correct place
                        break;
                    case '2':
                        char guessedChar = guess[i];

                        // Check if we can still color this letter yellow
                        if (maxYellowCounts[guessedChar] > 0)
                        {
                            _guessTextBoxes[startIndex + i].Background = Brushes.Yellow; // Correct letter in the wrong place
                            maxYellowCounts[guessedChar]--;
                        }
                        else
                        {
                            _guessTextBoxes[startIndex + i].Background = Brushes.White; // Incorrect letter
                        }
                        break;
                    default:
                        _guessTextBoxes[startIndex + i].Background = Brushes.White; // Incorrect letter
                        break;
                }
            }

            // Disable current row's TextBoxes
            for (int i = startIndex; i < startIndex + 5; i++)
            {
                _guessTextBoxes[i].IsEnabled = false;
            }

            // Move to the next row if the game is not over
            if (!game.IsGameOver() && !game.IsCorrectGuess(guess))
            {
                game.CurrentAttempt++;
                EnableTextBoxesForCurrentRow();

                // Set focus to the first TextBox of the next row
                int nextRowStartIndex = game.CurrentAttempt * 5;
                if (nextRowStartIndex < _guessTextBoxes.Count)
                {
                    _guessTextBoxes[nextRowStartIndex].Focus();
                }
            }

            if (game.IsCorrectGuess(guess))
            {
                StatusTextBlock = "Congratulations! You guessed the word!";

                // Disable all TextBoxes after correct guess
                foreach (var textBox in _guessTextBoxes)
                {
                    textBox.IsEnabled = false;
                }

                // Disable and grey out the SubmitButton
                IsSubmitButtonEnabled = false;
            }
            else if (game.IsGameOver())
            {
                StatusTextBlock = "Game Over! You've used all attempts.";
                IsSubmitButtonEnabled = false; // Disable the button
            }
            else
            {
                StatusTextBlock = $"Attempts left: {6 - game.CurrentAttempt}";
            }
        }

        private void TextBoxPreviewKeyDownHandler(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                if (e.Key == Key.Enter)
                {
                    // Call SubmitGuess_Click when Enter key is pressed
                    SubmitGuess();
                }
                else if (e.Key == Key.Back)
                {
                    int currentIndex = _guessTextBoxes.IndexOf(textBox);
                    int currentRowStartIndex = game.CurrentAttempt * 5;

                    // Clear the text of the last filled TextBox in the current row
                    for (int i = currentIndex; i >= currentRowStartIndex; i--)
                    {
                        if (i >= 0 && !string.IsNullOrEmpty(_guessTextBoxes[i].Text))
                        {
                            _guessTextBoxes[i].Text = "";
                            _guessTextBoxes[i].Focus();
                            break;
                        }
                    }

                    // Prevent further handling of the Back key
                    e.Handled = true;
                }
            }
        }

        private void TextBoxTextChangedHandler(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox != null && textBox.Text.Length == 1)
            {
                int currentIndex = _guessTextBoxes.IndexOf(textBox);
                int currentColumn = currentIndex % 5;  // Determine current column index

                // Set the typed letter to uppercase (assuming input is lowercase)
                textBox.Text = textBox.Text.ToUpper();

                // Move focus to the next TextBox in the current row and column
                if (currentColumn < 4)
                {
                    // Calculate index of next column in the same row
                    int nextColumnIndex = currentIndex + 1;

                    // Focus the next TextBox in the same row
                    _guessTextBoxes[nextColumnIndex].Focus();
                }
                else
                {
                    // If at the last column of the current row, move focus back to the same TextBox
                    textBox.Focus();
                }
            }
        }

        private void EnableTextBoxesForCurrentRow()
        {
            int startIndex = game.CurrentAttempt * 5;
            for (int i = startIndex; i < startIndex + 5; i++)
            {
                if (i < _guessTextBoxes.Count)
                {
                    _guessTextBoxes[i].IsEnabled = true;
                    _guessTextBoxes[i].Text = "";  // Clear previous guess
                }
            }
        }

        public override bool CanExecute(object? parameter)
        {
            if (parameter == null)
                return false;
            switch (parameter.ToString())
            {
                case "SubmitGuess":
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
                case "SubmitGuess":
                    SubmitGuess();
                    break;
            }
        }
    }
}