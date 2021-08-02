using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Windows.ApplicationModel.DataTransfer;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ListViewDragDropIssue.Controls
{
    public sealed partial class ListPage : UserControl
    {
        public ListPage()
        {
            for (var r = 0; r < 3; r++)
            {
                var helper = new LayoutHelper
                {
                    Control = new TestControl($"Panel {r}")
                };
                Controls.Add(helper);
            }
            InitializeComponent();
        }

        public ObservableCollection<LayoutHelper> Controls = new();

        private void UIElement_OnDragStarting(UIElement sender, DragStartingEventArgs args)
        {
            if (sender is ContentControl control
                && control.DataContext is LayoutHelper layout)
            {
                args.AllowedOperations = DataPackageOperation.Move;
                args.Data.Properties.Add("LayoutHelper", layout);
                args.DragUI.SetContentFromBitmapImage(new Microsoft.UI.Xaml.Media.Imaging.BitmapImage());
            }
        }

        private void StackPanel_DragOver(object sender, DragEventArgs e)
        {
            if (sender is StackPanel container
                && e.DataView.Properties.TryGetValue("LayoutHelper", out object layoutObj)
                && layoutObj is LayoutHelper layout)
            {
                RemoveBorders();
                var ownIndex = Controls.IndexOf(layout);
                var position = e.GetPosition(container);
                container.GetInsertionIndexes(position, out var firstIndex, out var secondIndex);
                if (firstIndex == ownIndex || secondIndex == ownIndex)
                {
                    e.AcceptedOperation = DataPackageOperation.None;
                    e.Handled = true;
                    return;
                }

                if (firstIndex > 0
                   && firstIndex > ownIndex)
                {
                    Controls[firstIndex].ShowRightInsertBorder = true;
                }
                else if (firstIndex < 0 && secondIndex == 0)
                {
                    Controls[secondIndex].ShowLeftInsertBorder = true;
                }
                else
                {
                    Controls[firstIndex].ShowLeftInsertBorder = true;
                }

                e.AcceptedOperation = DataPackageOperation.Move;
                e.DragUIOverride.Caption = "Move Control";
                e.Handled = true;
            }
        }

        private void RemoveBorders()
        {
            var highlightedBorders = Controls.Where(x => x.ShowLeftInsertBorder || x.ShowRightInsertBorder);
            foreach (var entry in highlightedBorders)
            {
                entry.ShowLeftInsertBorder = false;
                entry.ShowRightInsertBorder = false;
            }
        }

        private void StackPanel_Drop(object sender, DragEventArgs e)
        {
            if (sender is StackPanel container
               && e.DataView.Properties.TryGetValue("LayoutHelper", out var layoutObj)
               && layoutObj is LayoutHelper layout)
            {
                var ownIndex = Controls.IndexOf(layout);
                var position = e.GetPosition(container);
                container.GetInsertionIndexes(position, out var firstIndex, out var secondIndex);
                if (firstIndex >= 0
                    && firstIndex < Controls.Count)
                {
                    Controls.Move(ownIndex, firstIndex);
                }

                if (firstIndex < 0
                    && secondIndex == 0)
                {
                    Controls.Move(ownIndex, secondIndex);
                }

                e.Handled = true;
                RemoveBorders();
            }
        }
    }
}