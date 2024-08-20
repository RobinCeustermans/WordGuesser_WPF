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
                case "OpenGameConfiguration":
                    return true;
                case "OpenOverviewPage":
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
                case "OpenGameConfiguration":
                    OpenGameConfiguration();
                    break; 
                case "OpenOverviewPage":
                    OpenOverviewPage();
                    break;
            }
        }

        private void OpenGameConfiguration()
        {
            GameConfigurationWindow gameConfigurationWindow = new GameConfigurationWindow();
            gameConfigurationWindow.ShowDialog(); 
        }

        private void OpenOverviewPage()
        {
            OverviewWindow overviewWindow = new OverviewWindow();
            overviewWindow.ShowDialog();
        }
    }
}
