using System;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RKCheckList.ViewServices;

public interface IDependencyInjectionViewService : IViewService
{
    IServiceProvider ServiceProvider { get; }
}