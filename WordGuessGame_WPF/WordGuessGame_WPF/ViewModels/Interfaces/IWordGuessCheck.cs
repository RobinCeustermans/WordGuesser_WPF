namespace WordGuessGame_WPF.ViewModels.Interfaces
{
    public interface IWordGuessCheck
    {
        int CurrentAttempt { get; set; }
        string CorrectWord { get; }
        string CheckGuess(string guess);
        bool IsCorrectGuess(string guess);
        bool IsGameOver();
    }
}
