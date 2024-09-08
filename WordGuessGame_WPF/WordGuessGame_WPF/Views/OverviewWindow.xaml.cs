using MahApps.Metro.Controls;

namespace WordGuessGame_WPF.Views
{
    /// <summary>
    /// Interaction logic for OverviewWindow.xaml
    /// </summary>
    public partial class OverviewWindow : MetroWindow
    {
        public OverviewWindow()
        {
            InitializeComponent();
            DataContext = new OverviewWindowModel();
        }
    }
}