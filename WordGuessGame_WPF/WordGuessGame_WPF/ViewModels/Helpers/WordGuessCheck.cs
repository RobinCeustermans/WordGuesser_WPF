using WordGuessGame_WPF.Models;
using WordGuessGame_WPF.ViewModels.Interfaces;

namespace WordGuessGame_WPF.ViewModels.Helpers
{
    public class WordGuessCheck : IWordGuessCheck
    {
        private GameModel _gameModel;
        public int CorrectWordLength { get; private set; }
        public int CurrentAttempt { get; set; } = 0;
        public string CorrectWord { get; private set; }
        
        public WordGuessCheck(GameModel gameModel)
        {
            _gameModel = gameModel;
            this.CorrectWord = WordGuessCheckHelper.GetCorrectWord(gameModel.PotentialWords);
            this.CorrectWordLength = this.CorrectWord.Length;
        }

        public string CheckGuess(string guess)
        {
            char[] result = new char[guess.Length];
            var answerDict = ConvertWordToDictionary(this.CorrectWord);

            // First pass: Check for correct letters in the correct place
            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == this.CorrectWord[i] && answerDict[guess[i]] > 0)
                {
                    result[i] = '1'; // Correct letter in the correct place
                    answerDict[guess[i]]--; // Decrease count of matched letter
                }
            }

            // Second pass: Check for correct letters in the wrong place
            for (int i = 0; i < guess.Length; i++)
            {
                if (result[i] != '1') // Skip already matched letters
                {
                    if (answerDict.ContainsKey(guess[i]) && answerDict[guess[i]] > 0)
                    {
                        result[i] = '2'; // Correct letter in the wrong place
                        answerDict[guess[i]]--; // Decrease count of matched letter
                    }
                    else
                    {
                        result[i] = '0'; // Incorrect letter
                    }
                }
            }

            return new string(result);
        }

        public bool IsCorrectGuess(string guess)
        {
            return guess == this.CorrectWord;
        }

        public bool IsGameOver()
        {
            return CurrentAttempt >= _gameModel.TurnsAmount;
        }

        //local helpers

        private Dictionary<char, int> ConvertWordToDictionary(string word)
        {
            var returnDict = new Dictionary<char, int>();
            foreach (char c in word)
            {
                if (!returnDict.ContainsKey(c))
                    returnDict.Add(c, 1);
                else
                    returnDict[c]++;
            }
            return returnDict;
        }

        private void DecreaseCounterDictionary(Dictionary<char, int> answerDict, string correctWord, int i)
        {
            answerDict[correctWord[i]]--;
        }
    }
}
