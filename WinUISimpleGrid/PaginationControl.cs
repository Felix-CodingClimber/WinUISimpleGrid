using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace WinUISimpleGrid;

[TemplatePart(Name = nameof(BackButton), Type = typeof(ToolbarButton))]
[TemplatePart(Name = nameof(FirstPageButton), Type = typeof(ToolbarButton))]
[TemplatePart(Name = nameof(LastPageButton), Type = typeof(ToolbarButton))]
[TemplatePart(Name = nameof(ForwardButton), Type = typeof(ToolbarButton))]
[TemplatePart(Name = nameof(Root), Type = typeof(Grid))]
[TemplatePart(Name = nameof(GoToPageDownFlyout), Type = typeof(Flyout))]
[TemplatePart(Name = nameof(GoToPageUpFlyout), Type = typeof(Flyout))]
public sealed class PaginationControl : Control
{
    public event EventHandler<SelectedPageChangedEventArgs> SelectedPageChanged;

    public static readonly DependencyProperty SelectedPageProperty =
        DependencyProperty.Register(nameof(SelectedPage), typeof(int), typeof(PaginationControl), new PropertyMetadata(0, OnSelectedPagePropertyChanged));

    private static void OnSelectedPagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PaginationControl control = d as PaginationControl;

        control.SelectedPageChanged?.Invoke(control, new SelectedPageChangedEventArgs((int)e.NewValue));

        control.UpdateSelectablePages();
    }

    public int SelectedPage
    {
        get { return (int)GetValue(SelectedPageProperty); }
        set { SetValue(SelectedPageProperty, value); }
    }

    public static readonly DependencyProperty NumberOfSelectablePagesProperty =
        DependencyProperty.Register(nameof(NumberOfSelectablePages), typeof(int), typeof(PaginationControl), new PropertyMetadata(6, OnNumberOfSelectablePagesPropertyChanged));

    private static void OnNumberOfSelectablePagesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as PaginationControl).InitSelectablePages();
    }

    public int NumberOfSelectablePages
    {
        get { return (int)GetValue(NumberOfSelectablePagesProperty); }
        set { SetValue(NumberOfSelectablePagesProperty, value); }
    }

    public static readonly DependencyProperty NumberOfPagesProperty =
        DependencyProperty.Register(nameof(NumberOfPages), typeof(int), typeof(PaginationControl), new PropertyMetadata(0, OnNumberOfPagesChanged));

    private static void OnNumberOfPagesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as PaginationControl).InitSelectablePages();
    }

    public int NumberOfPages
    {
        get { return (int)GetValue(NumberOfPagesProperty); }
        set { SetValue(NumberOfPagesProperty, value); }
    }

    public static readonly DependencyProperty SelectedForegroundProperty =
        DependencyProperty.Register(nameof(SelectedForeground), typeof(SolidColorBrush), typeof(PaginationControl), new PropertyMetadata((SolidColorBrush)Application.Current.Resources["AccentTextFillColorTertiaryBrush"]));

    public SolidColorBrush SelectedForeground
    {
        get { return (SolidColorBrush)GetValue(SelectedForegroundProperty); }
        set { SetValue(SelectedForegroundProperty, value); }
    }

    public static readonly DependencyProperty SelectablePagesProperty =
        DependencyProperty.Register(nameof(SelectablePages), typeof(List<SelectablePage>), typeof(PaginationControl), new PropertyMetadata(null));

    public List<SelectablePage> SelectablePages
    {
        get { return (List<SelectablePage>)GetValue(SelectablePagesProperty); }
        set { SetValue(SelectablePagesProperty, value); }
    }

    public IRelayCommand<double> GoToPageButtonClickCommand { get; private set; }

    public ToolbarButton BackButton { get; private set; }
    public ToolbarButton FirstPageButton { get; private set; }
    public ToolbarButton LastPageButton { get; private set; }
    public ToolbarButton ForwardButton { get; private set; }
    public Grid Root { get; private set; }
    public Flyout GoToPageUpFlyout { get; private set; }
    public Flyout GoToPageDownFlyout { get; private set; }

    public VisualStateManager manager;

    private static SolidColorBrush PageForegroundDefaultColor;

    private SelectablePage selectedPage_Internal;

    public PaginationControl()
    {
        this.DefaultStyleKey = typeof(PaginationControl);
        this.DataContext = this;

        PageForegroundDefaultColor = (SolidColorBrush)Application.Current.Resources["TextFillColorPrimaryBrush"];
        GoToPageButtonClickCommand = new RelayCommand<double>((value) =>
        {
            GoToPage((int)value);
            GoToPageDownFlyout.Hide();
            GoToPageUpFlyout.Hide();
        });
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        BackButton = GetTemplateChild(nameof(BackButton)) as ToolbarButton;
        BackButton.Click += BackButton_Click;
        FirstPageButton = GetTemplateChild(nameof(FirstPageButton)) as ToolbarButton;
        FirstPageButton.Click += FirstPageButton_Click;
        LastPageButton = GetTemplateChild(nameof(LastPageButton)) as ToolbarButton;
        LastPageButton.Click += LastPageButton_Click;
        ForwardButton = GetTemplateChild(nameof(ForwardButton)) as ToolbarButton;
        ForwardButton.Click += ForwardButton_Click;

        Root = GetTemplateChild(nameof(Root)) as Grid;
        GoToPageDownFlyout = GetTemplateChild(nameof(GoToPageDownFlyout)) as Flyout;
        GoToPageUpFlyout = GetTemplateChild(nameof(GoToPageUpFlyout)) as Flyout;

        InitSelectablePages();
        SelectedPage = 1;
    }

    private void ForwardButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedPage++;
    }

    private void LastPageButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedPage = NumberOfPages;
    }

    private void FirstPageButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedPage = 1;
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedPage--;
    }

    private void GoToPage(int pageIndex)
    {
        SelectedPage = pageIndex;
    }

    private void InitSelectablePages()
    {
        int numSelectablePages = NumberOfPages < NumberOfSelectablePages ? NumberOfPages : NumberOfSelectablePages;

        SelectablePages = new List<SelectablePage>();
        for (int i = 1; i <= numSelectablePages; i++)
            SelectablePages.Add(new SelectablePage(i) { Foreground = PageForegroundDefaultColor, PageButtonClickCommand = new RelayCommand<int>(GoToPage) });
    }

    private void UpdateSelectablePages()
    {
        if (NumberOfPages <= NumberOfSelectablePages)
            return;

        int minPage = Math.Min(NumberOfPages - NumberOfSelectablePages + 1, Math.Max(1, SelectedPage - NumberOfSelectablePages / 2));
        int maxPage = Math.Min(NumberOfPages, SelectedPage + NumberOfSelectablePages / 2);

        ResetSetSelectedPage();

        if (SelectablePages[0].PageIndex != minPage || SelectablePages[^1].PageIndex != maxPage)
        {
            for (int i = 0, j = minPage; i < SelectablePages.Count; i++, j++)
                SelectablePages[i].PageIndex = j;
        }

        SetSelectedPage(SelectablePages.First(page => page.PageIndex == SelectedPage));

        if (minPage == 1)
        {
            if (SelectedPage == 1)
                VisualStateManager.GoToState(this, "CanNotGoDown", false);
            else
                VisualStateManager.GoToState(this, "CanGoDown", false);
        }
        else
        {
            bool test = VisualStateManager.GoToState(this, "HasMorePagesDown", false);
        }

        if (maxPage == NumberOfPages)
        {
            if (SelectedPage == NumberOfPages)
                VisualStateManager.GoToState(this, "CanNotGoUp", false);
            else
                VisualStateManager.GoToState(this, "CanGoUp", false);
        }
        else
        {
            bool test = VisualStateManager.GoToState(this, "HasMorePagesUp", false);
        }
    }

    private void ResetSetSelectedPage()
    {
        if (selectedPage_Internal is not null)
            selectedPage_Internal.Foreground = PageForegroundDefaultColor;
    }

    private void SetSelectedPage(SelectablePage selectedPage)
    {
        selectedPage_Internal = selectedPage;
        if (selectedPage_Internal is not null)
            selectedPage_Internal.Foreground = SelectedForeground;
    }
}

public class SelectablePage : ObservableObject
{
    public IRelayCommand<int> PageButtonClickCommand { get; set; }

    private int pageIndex;
    public int PageIndex
    {
        get => pageIndex;
        set => SetProperty(ref pageIndex, value);
    }

    private SolidColorBrush foreground;
    public SolidColorBrush Foreground
    {
        get => foreground;
        set => SetProperty(ref foreground, value);
    }

    public SelectablePage(int pageIndex)
    {
        PageIndex = pageIndex;
    }
}

public class SelectedPageChangedEventArgs : EventArgs
{
    public int SelectedPage { get; private init; }

    public SelectedPageChangedEventArgs(int selectedPage)
    {
        SelectedPage = selectedPage;
    }
}
