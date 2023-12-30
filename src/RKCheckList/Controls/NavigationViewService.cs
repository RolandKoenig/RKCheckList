using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RKCheckList.Controls;

internal class NavigationViewService : ViewServiceBase, INavigationViewService
{
    private readonly NavigationControl _navigationControl;

    public NavigationViewService(NavigationControl navigationControl)
    {
        _navigationControl = navigationControl;
    }

    /// <inheritdoc />
    public void NavigateTo(string targetName)
    {
        _navigationControl.NavigateTo(targetName);
    }

    public void NavigateTo<TDto>(string targetName, TDto dto)
    {
        _navigationControl.NavigateTo(targetName, dto);
    }

    /// <inheritdoc />
    public bool TryNavigateTo(string targetName)
    {
        return _navigationControl.TryNavigateTo(targetName);
    }

    public bool TryNavigateTo<TDto>(string targetName, TDto dto)
    {
        return _navigationControl.TryNavigateTo(targetName, dto);
    }
}
