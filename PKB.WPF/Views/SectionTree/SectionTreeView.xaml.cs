using System;
using System.Linq;
using PKB.Utility;
using PKB.WPF.Common;
using PKB.WPF.Interactivity;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TreeView;
using Telerik.Windows.DragDrop;
using DragEventArgs = Telerik.Windows.DragDrop.DragEventArgs;

namespace PKB.WPF.Views.SectionTree
{
    public partial class SectionTreeView : AppUserControl<SectionTreePresenter>
    {
        public SectionTreeView()
        {
            InitializeComponent();
            DragDropManager.AddDropHandler(_radTreeView, OnDrop, true);
            DragDropManager.AddDragDropCompletedHandler(_radTreeView, OnDragDropCompleted, true);
        }


        private static void OnDrop(object sender, DragEventArgs e)
        {
            var options = DragDropOptions(e.Data);
            if (options == null)
                return;

            options.DropAction = DropAction.None;
        }

        private void OnDragDropCompleted(object sender, DragDropCompletedEventArgs e)
        {
            var options = DragDropOptions(e.Data);

            if (options == null)
                return;

            if (!IsDropPossible(options))
                return;

            DoDragDrop(options);
        }

        private static bool IsDropPossible(TreeViewDragDropOptions options)
        {
            return ((TreeViewDragVisual)options.DragVisual).IsDropPossible;
        }

        private void DoDragDrop(TreeViewDragDropOptions options)
        {
            switch (options.DropPosition)
            {
                case DropPosition.Before:
                    Presenter.DragDropBefore(DraggedItem(options), TargetItem(options));
                    break;
                case DropPosition.Inside:
                    Presenter.DragDropInside(DraggedItem(options), TargetItem(options));
                    break;
                case DropPosition.After:
                    Presenter.DragDropAfter(DraggedItem(options), TargetItem(options));
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private static SectionViewModel DraggedItem(TreeViewDragDropOptions options)
        {
            return (SectionViewModel)options.DraggedItems.Single();
        }

        private Maybe<SectionViewModel> TargetItem(TreeViewDragDropOptions options)
        {
            if (options.DropTargetItem != null)
                return (SectionViewModel)options.DropTargetItem.Item;
            else
                return Maybe<SectionViewModel>.Nothing;
        }

        private static TreeViewDragDropOptions DragDropOptions(object data)
        {
            return (TreeViewDragDropOptions)DragDropPayloadManager.GetDataFromObject(data, TreeViewDragDropOptions.Key);
        }

        private void OnItemDoubleClick(object sender, RadRoutedEventArgs e)
        {
            e.OriginalSource
             .As<RadTreeViewItem>()
             .DoIfHasValue(item => item.IsInEditMode = true);
        }

        private void _radTreeView_OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.OriginalSource
             .As<UIElement>()
             .With(s => s.FindParentByType<RadTreeViewItem>())
             .DoIfHasValue(clickedItem =>
             {
                 clickedItem.IsSelected = true;
                 clickedItem.Focus();
             })
             .DoIfNothing(() =>
             {
                 _radTreeView.SelectedItem = null;
                 _radTreeView.Focus();
             });
        }

        private void OnDeleteConfirmation(object sender, InteractionRequestedEventArgs e)
        {
            var interaction = (EditSectionConfirmation)e.Interaction;

            if (IsDeleteConfirmed(interaction))
                interaction.Confirm();
            else
                interaction.Cancel();
        }

        private bool IsDeleteConfirmed(EditSectionConfirmation interaction)
        {
            var window = Window.GetWindow(this);
            string messageText = string.Format("Are you sure you want to delete {0} ?", interaction.Section.Name);
            const string caption = "Delete confirmation";

            return MessageBoxResult.OK == (window != null
                ? MessageBox.Show(window, messageText, caption, MessageBoxButton.OKCancel)
                : MessageBox.Show(messageText, caption, MessageBoxButton.OKCancel));
        }

    }
}