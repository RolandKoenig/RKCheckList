namespace RKCheckList.Controls;

public interface INavigationViewService
{
    void NavigateTo<TViewModel>()
        where TViewModel : INavigationTarget;

    void NavigateTo<TViewModel, TNavigationArgument>(TNavigationArgument argument)
        where TViewModel : INavigationTarget, INavigationDataReceiver<TNavigationArgument>;
}
