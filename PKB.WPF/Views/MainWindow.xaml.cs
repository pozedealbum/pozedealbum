using System;
using Microsoft.Practices.Prism.Mvvm;
using MVPVM;
using System.Windows;
using PKB.DomainModel;
using PKB.WPF.Common;
using PKB.WPF.Common.Interfaces;
using PKB.WPF.Views.SectionTree;

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