using Microsoft.UI.Xaml;

namespace WinUISimpleGrid;

public sealed partial class DataGridColumnDefinition : DependencyObject
{
    public static readonly DependencyProperty PropertyNameProperty =
        DependencyProperty.Register(nameof(PropertyName), typeof(string), typeof(DataGridColumnDefinition), new PropertyMetadata(""));

    public string PropertyName
    {
        get { return (string)GetValue(PropertyNameProperty); }
        set { SetValue(PropertyNameProperty, value); }
    }
}
