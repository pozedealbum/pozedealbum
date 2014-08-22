using System.Windows;
using PKB.WPF.Views.Main;
using PKB.WPF.Views.SectionTree;

namespace PKB.WPF.Design
{
    public class MainViewModelDesign : MainViewModel
    {
        public MainViewModelDesign()
        {
            SectionTreeView = new SectionTreeView()
            {
                DataContext = new SectionTreeViewModelDesign()
            };
        }
    }
}
