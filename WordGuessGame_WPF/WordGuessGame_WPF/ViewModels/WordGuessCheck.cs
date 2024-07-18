namespace WordGuessGame_WPF.ViewModels
{
    public class WordGuessCheck
    {
        private string[] wordList = { "apple", "truck", "trail", "grape", "melon" };

        public int CurrentAttempt { get; set; } = 0;

        public string CorrectWord { get; private set; }

        public WordGuessCheck()
        {
            //for now just a random word, in future perhaps read it form a txt file
            Random random = new Random();
            CorrectWord = wordList[random.Next(wordList.Length)];
        }

        public string CheckGuess(string guess)
        {
            char[] result = new char[5];
            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == CorrectWord[i])
                {
                    result[i] = '1'; // Correct letter in the correct place
                }
                else if (CorrectWord.Contains(guess[i]))
                {
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
    }
}
