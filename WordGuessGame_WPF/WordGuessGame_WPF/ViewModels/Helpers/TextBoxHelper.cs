using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace WordGuessGame_WPF.ViewModels.Helpers
{
    public static class TextBoxHelper
    {
        public static TextBox CreateTextBox(TextChangedEventHandler textChangedHandler, KeyEventHandler keyDownHandler)
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
                Background = Brushes.LightGray,
                Foreground = Brushes.Black,
                BorderBrush = Brushes.DarkGray,
                MaxLength = 1,
                IsEnabled = false
            };

            textBox.TextChanged += textChangedHandler;
            textBox.PreviewKeyDown += keyDownHandler;

            return textBox;
        }

        public static void HandleTextBoxTextChanged(object sender, ObservableCollection<TextBox> textBoxes, int wordLength)
        {
            if (sender is TextBox textBox && textBox.Text.Length == 1)
            {
                int currentIndex = textBoxes.IndexOf(textBox);
                int currentColumn = currentIndex % wordLength;

                textBox.Text = textBox.Text.ToUpper();

                if (currentColumn < wordLength - 1)
                {
                    int nextColumnIndex = currentIndex + 1;
                    textBoxes[nextColumnIndex].Focus();
                }
                else
                {
                    textBox.Focus();
                }
            }
        }

        public static void HandleTextBoxPreviewKeyDown(object sender, KeyEventArgs e, ObservableCollection<TextBox> textBoxes, int currentAttempt, int wordLength)
        {
            if (sender is TextBox textBox)
            {
                if (e.Key == Key.Enter)
                {
                    // This can be handled by a method call in the ViewModel
                }
                else if (e.Key == Key.Back)
                {
                    int currentIndex = textBoxes.IndexOf(textBox);
                    int currentRowStartIndex = currentAttempt * wordLength;

                    for (int i = currentIndex; i >= currentRowStartIndex; i--)
                    {
                        if (i >= 0 && !string.IsNullOrEmpty(textBoxes[i].Text))
                        {
                            textBoxes[i].Text = "";
                            textBoxes[i].Focus();
                            break;
                        }
                    }

                    e.Handled = true;
                }
            }
        }

        public static void EnableTextBoxesForCurrentRow(ObservableCollection<TextBox> textBoxes, int currentAttempt, int wordLength)
        {
            int startIndex = currentAttempt * wordLength;
            for (int i = startIndex; i < startIndex + wordLength; i++)
            {
                if (i < textBoxes.Count)
                {
                    textBoxes[i].IsEnabled = true;
                    textBoxes[i].Text = ""; 
                    textBoxes[i].Background = Brushes.White;
                }
            }
        }

        public static void UpdateTextBoxBackground(TextBox textBox, char result, char guessedChar, Dictionary<char, int> letterCounts)
        {
            switch (result)
            {
                case '1':
                    textBox.Background = Brushes.Green;
                    break;
                case '2':
                    if (letterCounts[guessedChar] > 0)
                    {
                        textBox.Background = Brushes.Yellow;
                        letterCounts[guessedChar]--;
                    }
                    else
                    {
                        textBox.Background = Brushes.White;
                    }
                    break;
                default:
                    textBox.Background = Brushes.White;
                    break;
            }
        }

        public static void DisableAllTextBoxes(ObservableCollection<TextBox> textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                textBox.IsEnabled = false;
                textBox.Background = Brushes.Tan;
            }
        }

        public static void FocusNextRowFirstTextBox(ObservableCollection<TextBox> textBoxes, int currentAttempt, int wordLength)
        {
            int nextRowStartIndex = currentAttempt * wordLength;
            if (nextRowStartIndex < textBoxes.Count)
            {
                textBoxes[nextRowStartIndex].Focus();
            }
        }
    }
}
