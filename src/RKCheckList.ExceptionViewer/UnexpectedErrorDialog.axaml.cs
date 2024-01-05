using System;
using System.IO;
using System.Text.Json;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using RKCheckList.ExceptionViewer.Data;

namespace RKCheckList.ExceptionViewer;

public partial class UnexpectedErrorDialog : Window
{
    public UnexpectedErrorDialog()
    {
        this.InitializeComponent();
        
        this.Loaded += this.OnLoaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        try
        {
            var appLifetime = App.Current.ApplicationLifetime as ClassicDesktopStyleApplicationLifetime;
            var filePath = appLifetime!.Args![0];

            using var inStream = File.OpenRead(filePath);
            this.DataContext = JsonSerializer.Deserialize<ExceptionInfo>(inStream);
        }
        catch(Exception)
        {
            this.Close();
        }
        
        this.Activate();
    }

    private void OnCmdOK_Click(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}