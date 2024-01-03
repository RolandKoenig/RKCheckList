using Avalonia;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
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
            var errorGuid = Guid.NewGuid();
            var errorFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                $"Error-{errorGuid}.err");

            var errorDetailsBuilder = new StringBuilder();
            var actException = ex;
            while (actException != null)
            {
                if (errorDetailsBuilder.Length > 0)
                {
                    errorDetailsBuilder.AppendLine();
                }
                
                errorDetailsBuilder.Append($"------- Exception of type {actException.GetType().FullName}");
                errorDetailsBuilder.Append(actException.ToString());
                errorDetailsBuilder.AppendLine();
                
                actException = actException.InnerException;
            }
            
            File.WriteAllText(errorFilePath, errorDetailsBuilder.ToString());

            var processStartInfo = new ProcessStartInfo(
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
                    "RKCheckListExceptionViewer"),
                $"\"{errorFilePath}\"");
            processStartInfo.ErrorDialog = false;
            processStartInfo.UseShellExecute = false;
            
            var childProcess = Process.Start(processStartInfo);
            if (childProcess != null)
            {
                childProcess.WaitForExit();
            }
            
            return 0;
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
