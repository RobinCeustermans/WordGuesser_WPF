using Microsoft.EntityFrameworkCore;
using WordGuessGame_DAL.DomainModels;

namespace WordGuessGame_DAL.EntitySets
{
    public class GameEntities : DbContext
    {
        public GameEntities() :  base()
        { }

        public DbSet<Game> Games { get; set; }
    }
}
