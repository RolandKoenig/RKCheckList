using Avalonia.Controls;

namespace RKCheckList.Controls;

public interface INavigationTarget
{
    string Title { get; }
    
    /// <summary>
    /// Creates a view instance that is responsible to display this viewmodel.
    /// </summary>
    static abstract Control CreateViewInstance();
}