using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace WinUISimpleGrid;

[TemplatePart(Name = nameof(HeaderCellGrid), Type = typeof(Grid))]
public sealed class DataGridHeader : Control
{
    // todo check if there is a better way of setting the parent dataGrid
    public static readonly DependencyProperty ParentDataGridProperty =
        DependencyProperty.Register(nameof(ParentDataGrid), typeof(DataGrid), typeof(DataGridHeader), new PropertyMetadata(null));

    public DataGrid ParentDataGrid
    {
        get { return (DataGrid)GetValue(ParentDataGridProperty); }
        set { SetValue(ParentDataGridProperty, value); }
    }

    public Grid HeaderCellGrid { get; private set; }

    public DataGridHeader()
    {
        this.DefaultStyleKey = typeof(DataGridHeader);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        HeaderCellGrid = GetTemplateChild(nameof(HeaderCellGrid)) as Grid;

        int columnIndex = 0;
        foreach (DataGridColumnDefinition column in ParentDataGrid.ColumnDefinitions)
        {
            DataGridHeaderCell cell = new DataGridHeaderCell();
            cell.Title = column.Title;
            HeaderCellGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(200) });
            Grid.SetColumn(cell, columnIndex++);
            HeaderCellGrid.Children.Add(cell);
        }
    }
}
