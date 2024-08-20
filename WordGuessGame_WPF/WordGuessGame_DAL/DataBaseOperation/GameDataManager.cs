using WordGuessGame_DAL.Context;
using WordGuessGame_DAL.DomainModels;
using WordGuessGame_DAL.FileOperations;

namespace WordGuessGame_DAL.DataBaseOperation
{
    public class GameDataManager
    {
        private readonly WordGuessGameEntities _context;

        public GameDataManager(WordGuessGameEntities context)
        {
            _context = context;
        }

        public List<Game> GetGames(
            string? playerName = null,
            DateTime? start = null,
            byte? amountOfLetters = null,
            byte? numberOfTurns = null,
            byte? amountOfGuesses = null,
            bool? guessedCorrectly = null)
        {
            var query = _context.Games.AsQueryable();

            if (!string.IsNullOrEmpty(playerName))
            {
                query = query.Where(g => g.PlayerName.Contains(playerName));
            }

            if (start.HasValue)
            {
                query = query.Where(g => g.Start.Date == start.Value.Date);
            }

            if (amountOfLetters.HasValue)
            {
                query = query.Where(g => g.AmountOfLetters == amountOfLetters.Value);
            }

            if (numberOfTurns.HasValue)
            {
                query = query.Where(g => g.NumberOfTurns == numberOfTurns.Value);
            }

            if (amountOfGuesses.HasValue)
            {
                query = query.Where(g => g.AmountOfGuesses == amountOfGuesses.Value);
            }

            if (guessedCorrectly.HasValue)
            {
                query = query.Where(g => g.GuessedCorrectly == guessedCorrectly.Value);
            }

            return query.ToList();
        }

        public int SaveGame(Game game)
        {
            try
            {
                _context.Games.Add(game);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                return 0;
            }
        }
    }
}
