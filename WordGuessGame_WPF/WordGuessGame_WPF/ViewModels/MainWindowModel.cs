using WordGuessGame_WPF.Views;

namespace WordGuessGame_WPF.ViewModels
{
    public class MainWindowModel : BaseViewModel
    {
        public override string this[string columnName] => throw new NotImplementedException();

        public MainWindowModel()
        {
        }

        public override bool CanExecute(object? parameter)
        {
            if (parameter == null)
                return false;
            switch (parameter.ToString())
            {
                case "OpenWordGuesserGame":
                    return true;
                default:
                    return false;
            }
        }

        public override void Execute(object? parameter)
        {
            if (parameter == null)
                return;

            switch (parameter.ToString())
            {
                case "OpenWordGuesserGame":
                    OpenWordGuesserGame();
                    break;
            }
        }

        private void OpenWordGuesserGame()
        {
            WordGuesserWindow wordGuesserWindow = new WordGuesserWindow();
            wordGuesserWindow.ShowDialog();
        }
    }
}
