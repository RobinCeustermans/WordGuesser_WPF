using System.Windows;
using WordGuessGame_WPF.ViewModels;

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
            DataContext = new WordGuesserWindowModel();
        }
    }
}
