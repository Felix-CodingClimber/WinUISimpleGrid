using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace WinUISimpleGrid;


[TemplatePart(Name = nameof(Content), Type = typeof(DataGridRowsView))]
[TemplatePart(Name = nameof(Header), Type = typeof(DataGridHeader))]
[TemplatePart(Name = nameof(Pagination), Type = typeof(PaginationControl))]
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
        DependencyProperty.Register(nameof(ItemsSource), typeof(IList), typeof(DataGrid), new PropertyMetadata(null, OnItemsSourcePropertyChanged));

    private static void OnItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DataGrid self = (DataGrid)d;
        self.UpdatePagination();
    }

    public IList ItemsSource
    {
        get { return (IList)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public static readonly DependencyProperty ItemsPerPageProperty =
        DependencyProperty.Register(nameof(ItemsPerPage), typeof(int), typeof(DataGrid), new PropertyMetadata(50, OnItemsPerPagePropertyChanged));

    private static void OnItemsPerPagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DataGrid self = (DataGrid)d;
        self.UpdatePagination();
    }

    public int ItemsPerPage
    {
        get { return (int)GetValue(ItemsPerPageProperty); }
        set { SetValue(ItemsPerPageProperty, value); }
    }

    public static readonly DependencyProperty ShownItemsProperty =
        DependencyProperty.Register(nameof(ShownItems), typeof(IList), typeof(DataGrid), new PropertyMetadata(null));

    public IList ShownItems
    {
        get { return (IList)GetValue(ShownItemsProperty); }
        set { SetValue(ShownItemsProperty, value); }
    }

    public int CurrentPage { get; set; }

    public DataGridHeader Header { get; private set; }
    public DataGridRowsView Content { get; private set; }
    public PaginationControl Pagination { get; private set; }

    public DataGrid()
    {
        Self = this;

        this.DefaultStyleKey = typeof(DataGrid);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        Header = GetTemplateChild(nameof(Header)) as DataGridHeader;
        Content = GetTemplateChild(nameof(Content)) as DataGridRowsView;
        Pagination = GetTemplateChild(nameof(Pagination)) as PaginationControl;

        UpdatePagination();
        Pagination.SelectedPageChanged += Pagination_SelectedPageChanged;
    }

    private void UpdatePagination()
    {
        if (Pagination is null)
            return;

        Pagination.NumberOfPages = (int)Math.Ceiling((double)ItemsSource.Count / ItemsPerPage);
    }

    private void Pagination_SelectedPageChanged(object sender, SelectedPageChangedEventArgs e)
    {
        List<object> tempList = new List<object>(ItemsPerPage);

        int start = (e.SelectedPage - 1) * ItemsPerPage;
        int end = start + ItemsPerPage > ItemsSource.Count ? ItemsSource.Count : start + ItemsPerPage;
        for (int i = start; i < end; i++)
        {
            tempList.Add(ItemsSource[i]);
        }

        ShownItems = tempList;
    }
}
