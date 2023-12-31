using Avalonia.Controls;

namespace RKCheckList.Controls;

public interface INavigationTarget
{
    /// <summary>
    /// Creates a view instance that is responsible to display this viewmodel.
    /// </summary>
    static abstract Control CreateViewInstance();
}