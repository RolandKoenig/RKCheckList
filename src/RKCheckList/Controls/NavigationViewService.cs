using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RKCheckList.Controls;

internal class NavigationViewService(NavigationControl navigationControl) : ViewServiceBase, INavigationViewService
{
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
}