using System;
using Avalonia;
using RolandK.AvaloniaExtensions.DependencyInjection;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RKCheckList.ViewServices;

public class DependencyInjectionViewService(StyledElement parent) : ViewServiceBase, IDependencyInjectionViewService
{
    public IServiceProvider ServiceProvider => parent.GetServiceProvider();
}