using System.Windows;
using WordGuessGame_WPF.ViewModels;

namespace WordGuessGame_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowModel();
        }
    }
}