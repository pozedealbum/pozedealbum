using System;
using System.Windows;

namespace PKB.WPF.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closed += OnClosed;
            _mainView.Presenter.Activate();
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            _mainView.Presenter.Close();
        }
    }
}