using System.ComponentModel;

namespace ListViewDragDropIssue.Controls
{
    public class LayoutHelper : INotifyPropertyChanged
    {
        /// <summary>
        /// Control to display
        /// </summary>
        public TestControl Control { get; set; }

        /// <summary>
        /// Indicate if the right border of the control should be highlighted
        /// </summary>
        public bool ShowRightInsertBorder { get; set; }

        /// <summary>
        /// Indicate if the left border of the control should be highlighted
        /// </summary>
        public bool ShowLeftInsertBorder { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
