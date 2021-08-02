using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ListViewDragDropIssue.Controls
{
    public sealed partial class TestControl : UserControl
    {
        public TestControl(string myText)
        {
            this.InitializeComponent();
            MyText.Text = myText;
        }
    }
}
