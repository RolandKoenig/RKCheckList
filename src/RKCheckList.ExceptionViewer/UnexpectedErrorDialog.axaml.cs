using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace RKCheckList.ExceptionViewer;

public partial class UnexpectedErrorDialog : Window
{
    public UnexpectedErrorDialog()
    {
        this.InitializeComponent();

        this.Loaded += (sender, eArgs) => this.Activate();
    }

    private void OnCmdClose_Click(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}