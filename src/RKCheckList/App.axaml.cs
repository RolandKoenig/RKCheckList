using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using RKCheckList.Services;
using RolandK.AvaloniaExtensions.DependencyInjection;

namespace RKCheckList;

public partial class App : Application
{
    public App()
    {
        this.UrlsOpened += this.OnUrlsOpened;
    }

    private void OnUrlsOpened(object? sender, UrlOpenedEventArgs e)
    {
        if (e.Urls == null) { return; }
        if (e.Urls.Length == 0) { return; }
        
        var serviceProvider = this.GetServiceProvider();
        var srvArgumentsContainer = serviceProvider.GetRequiredService<IRKCheckListArgumentsContainer>();

        var fileUrl = new Uri(e.Urls.First(), UriKind.Absolute);
        srvArgumentsContainer.NotifyFileOpened(HttpUtility.UrlDecode(fileUrl.AbsolutePath));
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}