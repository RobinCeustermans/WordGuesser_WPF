using WordGuessGame_DAL.DomainModels;
using WordGuessGame_DAL.EntitySets;
using WordGuessGame_DAL.FileOperations;

namespace WordGuessGame_DAL.DataBaseOperation
{
    public class GameDataManager
    {
        public static List<Game> GetGames()
        {
            using (GameEntities gameEntities =  new GameEntities())
            {
                return gameEntities.Games.ToList();
            }
        }

        public static int SaveGame(Game game)
        {
            try
            {
                using (GameEntities gameEntities = new GameEntities())
                {
                    gameEntities.Games.Add(game);
                    return gameEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                return 0;
            }
        }
    }
}
