using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace WinUISimpleGrid;

public sealed class ToolbarButton : Button
{
    public static readonly DependencyProperty IconSizeProperty =
        DependencyProperty.Register(nameof(IconSize), typeof(double), typeof(ToolbarButton), new PropertyMetadata(0));

    public double IconSize
    {
        get { return (double)GetValue(IconSizeProperty); }
        set { SetValue(IconSizeProperty, value); }
    }

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(Symbol), typeof(ToolbarButton), new PropertyMetadata(null, OnIconPropertyChanged));

    private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as ToolbarButton).UpdateContentState();
    }

    public Symbol Icon
    {
        get { return (Symbol)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(ToolbarButton), new PropertyMetadata(default, OnTextPropertyChanged));

    private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as ToolbarButton).UpdateContentState();
    }

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public ToolbarButton()
    {
        this.DefaultStyleKey = typeof(ToolbarButton);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        UpdateContentState();
    }

    private void UpdateContentState()
    {
        if (string.IsNullOrEmpty(Text))
            VisualStateManager.GoToState(this, "IconOnlyContent", false);
        else if (Icon == default)
            VisualStateManager.GoToState(this, "TextOnlyContent", false);
        else
            VisualStateManager.GoToState(this, "NormalContent", false);
    }
}
