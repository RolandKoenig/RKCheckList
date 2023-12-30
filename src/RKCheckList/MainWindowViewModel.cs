using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using RKCheckList.Controls;
using RKCheckList.Util;
using RKCheckList.Views;

namespace RKCheckList;

public partial class MainWindowViewModel : OwnViewModelBase
{
    private readonly string[] _args;

    private bool _initialNavigationDone;

    public static MainWindowViewModel DesignViewModel => new MainWindowViewModel(Array.Empty<string>());

    public MainWindowViewModel(string[] args)
    {
        _args = args;
    }

    [RelayCommand]
    public void CloseApplication()
    {
        base.CloseHostWindow();
    }

    private void TriggerInitialNavigation()
    {
        if (_initialNavigationDone) { return; }
        
        var srvNavigation = this.TryGetViewService<INavigationViewService>();
        if (srvNavigation != null)
        {
            srvNavigation.NavigateTo<HomeViewModel>();
            _initialNavigationDone = true;
        }
    }

    /// <inheritdoc />
    protected override async void OnAssociatedViewChanged(object? associatedView)
    {
        if (associatedView == null) { return; }

        this.TriggerInitialNavigation();
        
        base.OnAssociatedViewChanged(associatedView);
    }
}
