using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using RolandK.AvaloniaExtensions.DependencyInjection;
using RolandK.AvaloniaExtensions.Mvvm.Controls;

namespace RKCheckList.Controls;

public partial class NavigationControl : ViewServiceHostUserControl
{
    public NavigationControl()
    {
        this.InitializeComponent();
    }

    public void NavigateTo<TViewModel, TNavigationArgument>(TNavigationArgument argument)
        where TViewModel : INavigationTarget, INavigationDataReceiver<TNavigationArgument>
    {
        var serviceProvider = this.GetServiceProvider();
        var viewModel = serviceProvider.GetRequiredService<TViewModel>();
        viewModel.OnReceiveParameterFromNavigation(argument);
        
        var viewObject = TViewModel.CreateViewInstance();
        viewObject.DataContext = viewModel;
        
        this.CtrlTransition.Content = viewObject;
    }
    
    public void NavigateTo<TViewModel>()
        where TViewModel : INavigationTarget
    {
        var serviceProvider = this.GetServiceProvider();
        var viewModel = serviceProvider.GetRequiredService<TViewModel>();
        
        var viewObject = TViewModel.CreateViewInstance();
        viewObject.DataContext = viewModel;
        
        this.CtrlTransition.Content = viewObject;
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