using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using RKCheckList.Messages;
using RolandK.AvaloniaExtensions.DependencyInjection;
using RolandK.AvaloniaExtensions.Mvvm.Controls;
using RolandK.InProcessMessaging;

namespace RKCheckList.Controls;

public partial class NavigationControl : ViewServiceHostUserControl
{
    public string CurrentViewTitle
    {
        get
        {
            if (this.CtrlTransition.Content is not StyledElement control) { return string.Empty; }
            if (control.DataContext is not INavigationTarget navTarget) { return string.Empty; }
            return navTarget.Title;
        }
    }
    
    public NavigationControl()
    {
        this.InitializeComponent();
    }

    public void NavigateTo<TViewModel, TNavigationArgument>(TNavigationArgument argument)
        where TViewModel : INavigationTarget, INavigationDataReceiver<TNavigationArgument>
    {
        var serviceProvider = this.GetServiceProvider();
        var srvMessagePublisher = serviceProvider.GetRequiredService<IInProcessMessagePublisher>();
        
        var viewModel = serviceProvider.GetRequiredService<TViewModel>();
        viewModel.OnReceiveParameterFromNavigation(argument);
        
        var viewObject = TViewModel.CreateViewInstance();
        viewObject.DataContext = viewModel;
        
        this.CtrlTransition.Content = viewObject;
        
        srvMessagePublisher.Publish(new NavigationCompleteMessage());
    }
    
    public void NavigateTo<TViewModel>()
        where TViewModel : INavigationTarget
    {
        var serviceProvider = this.GetServiceProvider();
        var srvMessagePublisher = serviceProvider.GetRequiredService<IInProcessMessagePublisher>();
        
        var viewModel = serviceProvider.GetRequiredService<TViewModel>();
        
        var viewObject = TViewModel.CreateViewInstance();
        viewObject.DataContext = viewModel;
        
        this.CtrlTransition.Content = viewObject;
        
        srvMessagePublisher.Publish(new NavigationCompleteMessage());
    }

    public bool IsCurrentlyOn<TViewModel>()
    {
        if (this.CtrlTransition.Content == null) { return false; }

        return
            (this.CtrlTransition.Content is StyledElement currentView) &&
            (currentView.DataContext is TViewModel);
    }

    public bool IsCurrentlyOnAnyView()
    {
        return this.CtrlTransition.Content != null;
    }
}