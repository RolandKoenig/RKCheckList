using Avalonia;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using RKCheckList.ExceptionViewer;
using RKCheckList.ExceptionViewer.Data;
using RKCheckList.Services;
using RKCheckList.Views;
using RolandK.AvaloniaExtensions.DependencyInjection;
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
            // Write exception details to a temporary file
            // var errorGuid = Guid.NewGuid();
            var errorDirectoryPath = GlobalErrorReporting.GetErrorFileDirectoryAndEnsureCreated();
            var errorFilePath = GlobalErrorReporting.GenerateErrorFilePath(errorDirectoryPath);
            GlobalErrorReporting.WriteExceptionInfoToFile(ex, errorFilePath);

            try
            {
                if (!GlobalErrorReporting.TryFindViewerExecutable(out var executablePath))
                {
                    return -1;
                }
                
                GlobalErrorReporting.ShowGlobalException(errorFilePath, executablePath);
            }
            catch
            {
                // Error while showing the error message
                // Nothing more we can do here...
            }
            finally
            {
                // Delete the temporary file
                File.Delete(errorFilePath);
            }

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
