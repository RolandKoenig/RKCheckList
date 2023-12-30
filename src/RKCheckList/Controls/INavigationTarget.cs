using Avalonia.Controls;

namespace RKCheckList.Controls;

public interface INavigationTarget
{
    static abstract Control CreateViewInstance();
}