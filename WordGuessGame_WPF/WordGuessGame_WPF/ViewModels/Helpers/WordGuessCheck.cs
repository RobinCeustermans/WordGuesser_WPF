using WordGuessGame_WPF.ViewModels.Interfaces;

namespace WordGuessGame_WPF.ViewModels.Helpers
{
    public class WordGuessCheck : IWordGuessCheck
    {
        private string[] wordList = { "apple", "truck", "trail", "grape", "melon" };//temporary, future calls for reading a txt file

        public int CurrentAttempt { get; set; } = 0;
        public string CorrectWord { get; private set; }

        public Dictionary<char, int> CorrectWordLetters { get; set; }
        public Dictionary<char, int> GuessedWordLetters { get; set; }

        public WordGuessCheck()
        {
            // For now just a random word, in future perhaps read it from a txt file
            Random random = new Random();
            CorrectWord = wordList[random.Next(wordList.Length)];
        }

        public string CheckGuess(string guess)
        {
            char[] result = new char[5];
            var answerDict = ConvertWordToDictionary(CorrectWord);
            var guessDict = ConvertWordToDictionary(guess);

            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == CorrectWord[i] && answerDict.ContainsKey(guess[i]) && answerDict[guess[i]] > 0)
                {
                    DecreaseCounterDictionary(answerDict, CorrectWord, i);
                    result[i] = '1'; // Correct letter in the correct place
                }
                else if (answerDict.ContainsKey(guess[i]) && answerDict[guess[i]] > 0)
                {
                    DecreaseCounterDictionary(answerDict, guess, i);
                    result[i] = '2'; // Correct letter in the wrong place
                }
                else
                {
                    result[i] = '0'; // Incorrect letter
                }
            }
            return new string(result);
        }

        public bool IsCorrectGuess(string guess)
        {
            return guess == CorrectWord;
        }

        public bool IsGameOver()
        {
            return CurrentAttempt >= 6;
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
