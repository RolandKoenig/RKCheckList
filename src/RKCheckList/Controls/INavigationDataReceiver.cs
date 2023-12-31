namespace RKCheckList.Controls;

public interface INavigationDataReceiver<in TNavigationParameter>
{
    /// <summary>
    /// Passes in some data during navigation.
    /// This method gets called after the viewmodel is created and before it is attached to the view object.
    /// </summary>
    void OnReceiveParameterFromNavigation(TNavigationParameter parameter);
}
