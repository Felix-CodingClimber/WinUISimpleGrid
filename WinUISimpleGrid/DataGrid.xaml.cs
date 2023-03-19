using System.Collections;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace WinUISimpleGrid;

public sealed partial class DataGrid : UserControl
{
    public static readonly DependencyProperty ColumnsProperty =
        DependencyProperty.Register(nameof(ColumnDefinitions), typeof(List<DataGridColumnDefinition>), typeof(DataGrid), new PropertyMetadata(new List<DataGridColumnDefinition>()));

    public List<DataGridColumnDefinition> ColumnDefinitions
    {
        get { return (List<DataGridColumnDefinition>)GetValue(ColumnsProperty); }
        set { SetValue(ColumnsProperty, value); }
    }

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(IList), typeof(DataGrid), new PropertyMetadata(null));

    public IList ItemsSource
    {
        get { return (IList)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public DataGrid()
    {
        this.InitializeComponent();
    }
}
