using System;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using RolandK.AvaloniaExtensions.ExceptionHandling;

namespace RKCheckList.ExceptionViewer;

public partial class App : ExceptionViewerApplication
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
}