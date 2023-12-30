using System;

namespace RKCheckList.Controls;

public class NavigationItem
{
    public string Name { get; set; } = string.Empty;

    public Type? ViewType { get; set; } = null;
}
