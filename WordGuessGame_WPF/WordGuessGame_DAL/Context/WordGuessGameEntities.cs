using Microsoft.EntityFrameworkCore;
using WordGuessGame_DAL.DatabaseSettings;
using WordGuessGame_DAL.DomainModels;

namespace WordGuessGame_DAL.Context
{
    public class WordGuessGameEntities : DbContext
    {
        private static string _connectionString = DbSettings.DefaultConnectionString;
        public WordGuessGameEntities()
        {
        }

        public WordGuessGameEntities(DbContextOptions<WordGuessGameEntities> options) 
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
