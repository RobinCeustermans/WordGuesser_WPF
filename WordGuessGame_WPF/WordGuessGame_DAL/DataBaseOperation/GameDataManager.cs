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

        public List<Game> GetGames()
        {
            return _context.Games.ToList();
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
