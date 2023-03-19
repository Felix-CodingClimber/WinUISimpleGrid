using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace WinUISimpleGrid;

[TemplatePart(Name = nameof(CellGrid), Type = typeof(Grid))]
public sealed class DataGridRow : Control
{
    public Grid CellGrid { get; private set; }

    public static readonly DependencyProperty ItemProperty =
        DependencyProperty.Register(nameof(Item), typeof(object), typeof(DataGridRow), new PropertyMetadata(null));

    public object Item
    {
        get { return (object)GetValue(ItemProperty); }
        set { SetValue(ItemProperty, value); }
    }

    // todo check if there is a better way of setting the parent dataGrid
    public static readonly DependencyProperty ParentDataGridProperty =
        DependencyProperty.Register(nameof(ParentDataGrid), typeof(DataGrid), typeof(DataGridRow), new PropertyMetadata(null));

    public DataGrid ParentDataGrid
    {
        get { return (DataGrid)GetValue(ParentDataGridProperty); }
        set { SetValue(ParentDataGridProperty, value); }
    }

    public DataGridRow()
    {
        this.DefaultStyleKey = typeof(DataGridRow);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        CellGrid = GetTemplateChild(nameof(CellGrid)) as Grid;

        int columnIndex = 0;
        foreach (DataGridColumnDefinition column in ParentDataGrid.ColumnDefinitions)
        {
            DataGridTextCell cell = new DataGridTextCell();
            cell.SetBinding(DataGridTextCell.TextProperty, new Binding() { Source = Item, Path = new PropertyPath(column.PropertyName), Mode = BindingMode.OneWay });
            CellGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(200) });
            Grid.SetColumn(cell, columnIndex++);
            CellGrid.Children.Add(cell);
        }
    }
}
