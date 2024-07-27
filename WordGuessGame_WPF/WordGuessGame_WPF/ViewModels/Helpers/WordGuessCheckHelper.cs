namespace WordGuessGame_WPF.ViewModels.Helpers
{
    public static class WordGuessCheckHelper
    {
        public static string GetCorrectWord(List<string> potentialWords)
        {
            Random random = new Random();
            return potentialWords[random.Next(potentialWords.Count)];
        }
    }
}
