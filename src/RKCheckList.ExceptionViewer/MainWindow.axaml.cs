using System.IO;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;

namespace RKCheckList.ExceptionViewer;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
        
        this.Loaded += this.OnLoaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        var appLifetime = App.Current.ApplicationLifetime as ClassicDesktopStyleApplicationLifetime;
        var filePath = appLifetime!.Args![0];
        this.CtrlErrorDetails.Text = File.ReadAllText(filePath);
        
        this.Activate();
    }

    private void OnCmdOK_Click(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}