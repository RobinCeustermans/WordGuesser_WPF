using MahApps.Metro.Controls;
using WordGuessGame_WPF.ViewModels;

namespace WordGuessGame_WPF
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowModel();
        }
    }
}