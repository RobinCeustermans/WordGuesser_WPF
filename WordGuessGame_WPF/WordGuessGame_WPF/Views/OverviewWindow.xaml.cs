using System.Windows;
using WordGuessGame_WPF.ViewModels;

namespace WordGuessGame_WPF.Views
{
    /// <summary>
    /// Interaction logic for OverviewWindow.xaml
    /// </summary>
    public partial class OverviewWindow : Window
    {
        public OverviewWindow()
        {
            InitializeComponent();
            DataContext = new OverviewWindowModel();
        }
    }
}