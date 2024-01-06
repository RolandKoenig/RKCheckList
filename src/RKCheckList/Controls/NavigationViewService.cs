using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RKCheckList.Controls;

internal class NavigationViewService(NavigationControl navigationControl) : ViewServiceBase, INavigationViewService
{
    public string CurrentViewTitle => navigationControl.CurrentViewTitle;
    
    /// <inheritdoc />
    public void NavigateTo<TViewModel, TNavigationArgument>(TNavigationArgument argument) 
        where TViewModel : INavigationTarget, INavigationDataReceiver<TNavigationArgument>
    {
        navigationControl.NavigateTo<TViewModel, TNavigationArgument>(argument);
    }

    /// <inheritdoc />
    public void NavigateTo<TViewModel>() where TViewModel : INavigationTarget
    {
        navigationControl.NavigateTo<TViewModel>();
    }
    
    /// <inheritdoc />
    public bool IsCurrentlyOn<TViewModel>()
    {
        return navigationControl.IsCurrentlyOn<TViewModel>();
    }

    public bool IsCurrentlyOnAnyView()
    {
        return navigationControl.IsCurrentlyOnAnyView();
    }
}