using System.Collections;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace WinUISimpleGrid;


[TemplatePart(Name = nameof(Content), Type = typeof(DataGridRowsView))]
[TemplatePart(Name = nameof(Header), Type = typeof(DataGridHeader))]
public sealed class DataGrid : Control
{
    public static readonly DependencyProperty SelfProperty =
        DependencyProperty.Register(nameof(Self), typeof(DataGrid), typeof(DataGrid), new PropertyMetadata(null));

    public DataGrid Self
    {
        get { return (DataGrid)GetValue(SelfProperty); }
        set { SetValue(SelfProperty, value); }
    }

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

    public int CurrentPage { get; set; }

    public int PagesTotal { get; set; }

    public DataGridHeader Header { get; private set; }
    public DataGridRowsView Content { get; private set; }

    public DataGrid()
    {
        CurrentPage = 1;
        PagesTotal = 10;
        Self = this;

        this.DefaultStyleKey = typeof(DataGrid);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        Header = GetTemplateChild(nameof(Header)) as DataGridHeader;
        Content = GetTemplateChild(nameof(Content)) as DataGridRowsView;


    }
}
