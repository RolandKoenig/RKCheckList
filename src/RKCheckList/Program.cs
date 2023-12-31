using Avalonia;
using System;
using Microsoft.Extensions.DependencyInjection;
using RKCheckList.Services;
using RKCheckList.Views;
using RolandK.AvaloniaExtensions.DependencyInjection;

namespace RKCheckList;

internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static int Main(string[] args)
    {
        try
        {
            BuildAvaloniaApp(args)
                .StartWithClassicDesktopLifetime(args);
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return -1;
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp(string[] args)
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseDependencyInjection(services =>
            {
                // Services
                services.AddSingleton<IRKCheckListArgumentParser>(_ => new RKCheckListArgumentsParser(args));
                
                // ViewModels
                services.AddTransient<MainWindowViewModel>();
                services.AddTransient<CheckListViewModel>();
                services.AddTransient<HomeViewModel>();
            });
}
