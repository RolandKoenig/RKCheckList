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
    public static void Main(string[] args) => BuildAvaloniaApp(args)
        .StartWithClassicDesktopLifetime(args);

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
