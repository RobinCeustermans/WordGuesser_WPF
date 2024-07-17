using System.ComponentModel.DataAnnotations;

namespace WordGuessGame_DAL.DomainModels
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? PlayerName { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public byte AmountOfLetters { get; set; }

        [Required]
        public string? CorrectWord { get; set; }

        public byte NumberOfTurns { get; set; }

        public bool GuessedCorrectly { get; set; }
    }
}
