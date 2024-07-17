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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        public DbSet<Game> Game { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasIndex(p => p.PlayerName)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}