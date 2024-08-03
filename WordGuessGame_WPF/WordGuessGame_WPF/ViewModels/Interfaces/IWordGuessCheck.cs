namespace WordGuessGame_WPF.ViewModels.Interfaces
{
    public interface IWordGuessCheck
    {
        byte CurrentAttempt { get; set; }
        string CorrectWord { get; }
        string CheckGuess(string guess);
        bool IsCorrectGuess(string guess);
        bool IsGameOver();
    }
}
