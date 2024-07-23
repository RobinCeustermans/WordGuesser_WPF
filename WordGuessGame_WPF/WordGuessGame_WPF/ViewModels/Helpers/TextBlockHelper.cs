
namespace WordGuessGame_WPF.ViewModels.Helpers
{
    public static class TextBlockHelper
    {
        public static void UpdateStatus(ref string statusTextBlock, string message)
        {
            statusTextBlock = message;
        }
    }
}
