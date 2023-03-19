using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace WinUISimpleGrid;

public sealed class DataGridTextCell : Control
{
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(DataGridTextCell), new PropertyMetadata(""));

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public DataGridTextCell()
    {
        this.DefaultStyleKey = typeof(DataGridTextCell);
    }
}
