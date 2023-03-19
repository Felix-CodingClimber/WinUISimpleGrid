using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace WinUISimpleGrid;
public sealed class DataGridHeaderCell : Control
{
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(DataGridHeaderCell), new PropertyMetadata(""));

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public DataGridHeaderCell()
    {
        this.DefaultStyleKey = typeof(DataGridHeaderCell);
    }
}
