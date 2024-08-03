using System.Text.RegularExpressions;

namespace WordGuessGame_DAL.FileOperations
{
    public class FileReader
    {
        public static List<string> ReadFile(string file)
        {
            List<string> words = new List<string>();
            string? data = "";

            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    data = reader.ReadLine().Split("\n").ToList().FirstOrDefault();
                    try
                    {
                        if (!string.IsNullOrEmpty(data) && Regex.IsMatch(data, @"^[a-zA-Z]+$"))
                        {
                            words.Add(data);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.LogException(ex);
                    }
                }
            }
            return words;
        }

        public static List<byte> GetWordLengths(List<string> words)
        {
            var wordLengths = words.Select(word => (byte)word.Length).Distinct().ToList();
            return wordLengths;
        }
    }
}
