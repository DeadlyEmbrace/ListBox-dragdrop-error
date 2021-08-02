using Microsoft.UI.Xaml;

namespace ListViewDragDropIssue.Controls
{
    public class BindConverters
    {
        public static Visibility ConvertBoolToVisibility(bool boolToConvert) => boolToConvert
    ? Visibility.Visible
    : Visibility.Collapsed;
    }
}
