namespace WordGuessGame_WPF.Models
{
    public class GuessGameModel
    {
        public byte WordLength { get; set; }
        public byte TurnsAmount { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public List<string> PotentialWords { get; set; } = new List<string>();
    }
}
