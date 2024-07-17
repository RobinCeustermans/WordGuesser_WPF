namespace WordGuessGame_DAL.FileOperations
{
    public static class ExceptionLogger
    {
        public static void LogException(Exception ex)
        {
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "errorLog.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now}] {ex.GetType()}: {ex.Message}");
                    writer.WriteLine($"error message: {ex.Message}");
                    writer.WriteLine($"StackTrace: {ex.StackTrace}");
                    writer.WriteLine();
                }
            }
            catch (Exception writeEx)
            {
                Console.WriteLine($"Failed to write to log file: {writeEx.Message}");
            }
        }
    }
}