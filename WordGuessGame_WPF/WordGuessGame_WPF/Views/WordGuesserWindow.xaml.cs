using System.Windows;
using WordGuessGame_WPF.ViewModels;
using WordGuessGame_WPF.ViewModels.Helpers;

namespace WordGuessGame_WPF.Views
{
    /// <summary>
    /// Interaction logic for WordGuesserWindow.xaml
    /// </summary>
    public partial class WordGuesserWindow : Window
    {
        public WordGuesserWindow()
        {
            InitializeComponent();
            var game = new WordGuessCheck();
            var viewModel = new WordGuesserWindowModel(game);
            DataContext = viewModel;
        }
    }
}
