namespace RKCheckList.Controls;

public interface INavigationDataReceiver<TDto>
{
    void OnReceiveParameterFromNavigation(TDto dto);
}
