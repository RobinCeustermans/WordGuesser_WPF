using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using WordGuessGame_WPF.ViewModels.Helpers;
using WordGuessGame_WPF.ViewModels.Interfaces;
using WordGuessGame_WPF.Models;
using WordGuessGame_DAL.DomainModels;
using WordGuessGame_DAL.DataBaseOperation;
using WordGuessGame_WPF.ViewModels;

public class WordGuesserWindowModel : BaseViewModel
{
    private readonly IWordGuessCheck _gameCheck;
    private readonly GameDataManager _gameDataManager;
    private ObservableCollection<TextBox> _guessTextBoxes;
    private string _statusTextBlock;
    private bool _isSubmitButtonEnabled;

    public GuessGameModel CurrentGame { get; private set; }
    public Game Game { get; private set; }

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

    public WordGuesserWindowModel(IWordGuessCheck gameCheck, GameDataManager gameDataManager, GuessGameModel gameModel)
    {
        _gameCheck = gameCheck;
        _gameDataManager = gameDataManager;
        CurrentGame = gameModel;

        InitializeGuessGrid();
        InitializeGame();
        IsSubmitButtonEnabled = true;
    }

    private void InitializeGuessGrid()
    {
        _guessTextBoxes = new ObservableCollection<TextBox>();

        for (int i = 0; i < CurrentGame.TurnsAmount * CurrentGame.WordLength; i++)
        {
            var textBox = TextBoxHelper.CreateTextBox(TextBoxTextChangedHandler, TextBoxPreviewKeyDownHandler);
            _guessTextBoxes.Add(textBox);
        }

        TextBoxHelper.EnableTextBoxesForCurrentRow(_guessTextBoxes, _gameCheck.CurrentAttempt, CurrentGame.WordLength);
    }

    private void InitializeGame()
    {
        Game = new Game
        {
            PlayerName = CurrentGame.PlayerName,
            Start = DateTime.Now,
            AmountOfLetters = CurrentGame.WordLength,
            NumberOfTurns = CurrentGame.TurnsAmount
        };
    }

    private void FinishGame(bool correctlyGuessed)
    {
        Game.End = DateTime.Now;
        Game.GuessedCorrectly = correctlyGuessed;
        Game.CorrectWord = _gameCheck.CorrectWord;
        Game.AmountOfGuesses = _gameCheck.CurrentAttempt;
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
                TextBoxHelper.HandleTextBoxPreviewKeyDown(sender, e, _guessTextBoxes, _gameCheck.CurrentAttempt, CurrentGame.WordLength);
                e.Handled = true;
            }
        }
    }

    private void TextBoxTextChangedHandler(object sender, TextChangedEventArgs e)
    {
        TextBoxHelper.HandleTextBoxTextChanged(sender, _guessTextBoxes, CurrentGame.WordLength);
    }

    private void SubmitGuess()
    {
        if (_gameCheck.CurrentAttempt >= CurrentGame.TurnsAmount)
        {
            StatusTextBlock = TextBlockHelper.UpdateStatus(StatusTextBlock, "Game Over! You've used all attempts.");
            return;
        }

        var guess = CollectCurrentGuess();
        if (guess.Length != CurrentGame.WordLength)
        {
            ShowInvalidGuessMessage();
            return;
        }

        var result = _gameCheck.CheckGuess(guess);
        ProcessGuessResult(guess, result);

        _gameCheck.CurrentAttempt++;
        if (_gameCheck.IsCorrectGuess(guess))
        {
            StatusTextBlock = TextBlockHelper.UpdateStatus(StatusTextBlock, $"Congratulations! You guessed the word!\nTurns needed: {_gameCheck.CurrentAttempt}");
            TextBoxHelper.DisableAllTextBoxes(_guessTextBoxes);
            IsSubmitButtonEnabled = false;
            FinishGame(true);
            _gameDataManager.SaveGame(Game);
        }
        else if (_gameCheck.IsGameOver())
        {
            StatusTextBlock = TextBlockHelper.UpdateStatus(StatusTextBlock, "Game Over! You've used all attempts.");
            IsSubmitButtonEnabled = false;
            FinishGame(false);
            _gameDataManager.SaveGame(Game);
        }
        else
        {
            StatusTextBlock = TextBlockHelper.UpdateStatus(StatusTextBlock, $"Attempts left: {CurrentGame.TurnsAmount - _gameCheck.CurrentAttempt}");
            TextBoxHelper.EnableTextBoxesForCurrentRow(_guessTextBoxes, _gameCheck.CurrentAttempt, CurrentGame.WordLength);
            TextBoxHelper.FocusNextRowFirstTextBox(_guessTextBoxes, _gameCheck.CurrentAttempt, CurrentGame.WordLength);
        }
    }

    private string CollectCurrentGuess()
    {
        int startIndex = _gameCheck.CurrentAttempt * CurrentGame.WordLength;
        var guessLetters = new List<string>();

        for (int i = startIndex; i < startIndex + CurrentGame.WordLength; i++)
        {
            var textBox = _guessTextBoxes[i];
            guessLetters.Add(textBox.Text.Trim().ToLower());
        }

        return string.Join("", guessLetters);
    }

    private void ShowInvalidGuessMessage()
    {
        MessageBox.Show($"Guess must be {CurrentGame.WordLength} letters long.", "Invalid Guess", MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    private void ProcessGuessResult(string guess, string result)
    {
        int startIndex = _gameCheck.CurrentAttempt * CurrentGame.WordLength;
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
