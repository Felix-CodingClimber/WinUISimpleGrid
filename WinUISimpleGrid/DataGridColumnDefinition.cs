using Microsoft.UI.Xaml;

namespace WinUISimpleGrid;

public sealed partial class DataGridColumnDefinition : DependencyObject
{
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(DataGridColumnDefinition), new PropertyMetadata(""));

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public static readonly DependencyProperty PropertyNameProperty =
        DependencyProperty.Register(nameof(PropertyName), typeof(string), typeof(DataGridColumnDefinition), new PropertyMetadata(""));

    public string PropertyName
    {
        get { return (string)GetValue(PropertyNameProperty); }
        set { SetValue(PropertyNameProperty, value); }
    }

    public static readonly DependencyProperty CellTemplateProperty =
        DependencyProperty.Register(nameof(CellTemplate), typeof(DataTemplate), typeof(DataGridColumnDefinition), new PropertyMetadata(0));

    public DataTemplate CellTemplate
    {
        get { return (DataTemplate)GetValue(CellTemplateProperty); }
        set { SetValue(CellTemplateProperty, value); }
    }
}
