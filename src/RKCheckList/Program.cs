using Avalonia;
using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using RKCheckList.Services;
using RKCheckList.Views;
using RolandK.AvaloniaExtensions.DependencyInjection;
using RolandK.AvaloniaExtensions.ExceptionHandling;
using RolandK.InProcessMessaging;

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
            GlobalErrorReporting.TryShowBlockingGlobalExceptionDialogInAnotherProcess(
                ex, 
                ".RKCheckList",
                "RKCheckList.ExceptionViewer");
            throw;
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
                var messenger = new InProcessMessenger();
                
                // Services
                services.AddSingleton<IRKCheckListArgumentsContainer>(_ => new RKCheckListArgumentsContainer(messenger, args));
                services.AddSingleton<IInProcessMessagePublisher>(messenger);
                services.AddSingleton<IInProcessMessageSubscriber>(messenger);

                // ViewModels
                services.AddTransient<MainWindowViewModel>();
                services.AddTransient<CheckListViewModel>();
                services.AddTransient<HomeViewModel>();
            });
}
