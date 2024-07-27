namespace WordGuessGame_WPF.Models
{
    public class GameModel
    {
        public int WordLength { get; set; }
        public int TurnsAmount { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public List<string> PotentialWords { get; set; } = new List<string>();
    }
}
