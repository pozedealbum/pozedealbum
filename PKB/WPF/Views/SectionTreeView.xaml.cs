using System.Collections.ObjectModel;
using System.Windows.Controls;
using PKB.WPF.ViewModels;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace PKB.WPF.Views
{
    /// <summary>
    /// Interaction logic for SectionTreeView.xaml
    /// </summary>
    public partial class SectionTreeView : UserControl
    {
        public SectionTreeView()
        {
            DataContext = this;
            Sections = DataUtil.MakeSections();
            InitializeComponent();
            MyTree.ExpandAll();

        }

   


        public ObservableCollection<SectionViewModel> Sections { get; private set; }


        private void OnItemDoubleClick(object sender, RadRoutedEventArgs e)
        {
            var element = e.OriginalSource as RadTreeViewItem;
                if (element != null)
                    element.IsInEditMode = true;
        }


    }
}
