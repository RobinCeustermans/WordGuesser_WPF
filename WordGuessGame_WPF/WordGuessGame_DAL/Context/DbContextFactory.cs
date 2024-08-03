using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using WordGuessGame_DAL.DatabaseSettings;

namespace WordGuessGame_DAL.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WordGuessGameEntities>
    {
        public WordGuessGameEntities CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WordGuessGameEntities>();
            optionsBuilder.UseSqlServer(DbSettings.DefaultConnectionString);

            return new WordGuessGameEntities(optionsBuilder.Options);
        }
    }
}
