namespace RKCheckList.Controls;

public interface INavigationViewService
{
    void NavigateTo(string targetName);

    void NavigateTo<TDto>(string targetName, TDto dto);

    bool TryNavigateTo(string targetName);

    bool TryNavigateTo<TDto>(string targetName, TDto dto);
}
