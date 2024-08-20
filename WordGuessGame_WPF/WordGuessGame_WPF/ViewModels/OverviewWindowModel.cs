using System.Collections.ObjectModel;
using WordGuessGame_DAL.DataBaseOperation;
using WordGuessGame_DAL.DomainModels;
using WordGuessGame_WPF.ViewModels;

public class OverviewWindowModel : BaseViewModel
{
    private readonly GameDataManager _gameDataManager;

    public string? PlayerName { get; set; }
    public DateTime? StartDate { get; set; }
    public byte? AmountOfLetters { get; set; }
    public byte? NumberOfTurns { get; set; }
    public byte? AmountOfGuesses { get; set; }
    public bool? GuessedCorrectlyOption { get; set; } // This will hold the selected value from the ComboBox

    public ObservableCollection<Game> Games { get; private set; }

    public OverviewWindowModel()
    {
        _gameDataManager = GameManager;
        Games = new ObservableCollection<Game>(_gameDataManager.GetGames());
    }

    private void SearchHighScores()
    {
        var filteredGames = _gameDataManager.GetGames().AsQueryable();

        if (!string.IsNullOrWhiteSpace(PlayerName))
            filteredGames = filteredGames.Where(game => game.PlayerName.Contains(PlayerName, StringComparison.OrdinalIgnoreCase));

        if (StartDate.HasValue)
            filteredGames = filteredGames.Where(game => game.Start.Date == StartDate.Value.Date);

        if (AmountOfLetters.HasValue)
            filteredGames = filteredGames.Where(game => game.AmountOfLetters == AmountOfLetters);

        if (NumberOfTurns.HasValue)
            filteredGames = filteredGames.Where(game => game.NumberOfTurns == NumberOfTurns);

        if (AmountOfGuesses.HasValue)
            filteredGames = filteredGames.Where(game => game.AmountOfGuesses == AmountOfGuesses);

        if (GuessedCorrectlyOption.HasValue)
            filteredGames = filteredGames.Where(game => game.GuessedCorrectly == GuessedCorrectlyOption.Value);
        else
            filteredGames = filteredGames.Where(game => game.GuessedCorrectly == true || game.GuessedCorrectly == false); // Include all games

        // Update the Games collection
        Games.Clear();
        foreach (var game in filteredGames)
        {
            Games.Add(game);
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return parameter?.ToString() == "SearchHighScores";
    }

    public override void Execute(object? parameter)
    {
        if (parameter?.ToString() == "SearchHighScores")
        {
            SearchHighScores();
        }
    }

    public override string this[string columnName] => string.Empty;
}
