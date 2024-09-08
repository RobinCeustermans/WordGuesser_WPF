using MahApps.Metro.Controls;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using WordGuessGame_WPF.ViewModels;

namespace WordGuessGame_WPF.Views
{
    /// <summary>
    /// Interaction logic for GameConfigurationWindow.xaml
    /// </summary>
    public partial class GameConfigurationWindow : MetroWindow
    {
        public GameConfigurationWindow()
        {
            InitializeComponent();
            DataContext = new GameConfigurationWindowModel(this);
        }

        private void NumberOnlyTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                Regex regex = new Regex("[^1-7]+");

                if (regex.IsMatch(e.Text))
                {
                    e.Handled = true;
                    return;
                }
                string newText = textBox.Text.Insert(textBox.SelectionStart, e.Text);

                if (!int.TryParse(newText, out int result) || result < 1 || result > 7)
                {
                    e.Handled = true; 
                }
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
