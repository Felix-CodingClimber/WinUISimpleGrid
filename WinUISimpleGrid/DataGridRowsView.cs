using System.Linq;
using System.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace WinUISimpleGrid;

public class DataGridRowsView : ItemsControl
{
    public static readonly DependencyProperty DataGridProperty =
        DependencyProperty.Register(nameof(DataGrid), typeof(DataGrid), typeof(DataGridRowsView), new PropertyMetadata(null));

    public DataGrid DataGrid
    {
        get { return (DataGrid)GetValue(DataGridProperty); }
        set { SetValue(DataGridProperty, value); }
    }

    public DataGridRowsView()
    {
        
    }
}

