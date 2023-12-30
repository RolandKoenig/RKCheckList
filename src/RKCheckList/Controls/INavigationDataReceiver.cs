namespace RKCheckList.Controls;

public interface INavigationDataReceiver<TDto>
{
    void ReceiveFromNavigation(TDto dto);
}
