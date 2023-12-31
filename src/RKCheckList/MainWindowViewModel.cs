using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using RKCheckList.Controls;
using RKCheckList.Model;
using RKCheckList.Services;
using RKCheckList.Util;
using RKCheckList.Views;

namespace RKCheckList;

public partial class MainWindowViewModel : OwnViewModelBase
{
    private readonly IRKCheckListArgumentParser _rkCheckListArgumentParser;

    private bool _initialNavigationDone;

    public static MainWindowViewModel DesignViewModel => new MainWindowViewModel(
        new RKCheckListArgumentsParser(Array.Empty<string>()));

    public MainWindowViewModel(IRKCheckListArgumentParser rkCheckListArgumentParser)
    {
        _rkCheckListArgumentParser = rkCheckListArgumentParser;
    }

    [RelayCommand]
    public void CloseApplication()
    {
        base.CloseHostWindow();
    }

    private async void TriggerInitialNavigation()
    {
        if (_initialNavigationDone) { return; }

        var srvNavigation = this.GetViewService<INavigationViewService>();
        
        if (string.IsNullOrEmpty(_rkCheckListArgumentParser.InitialFile))
        {
            srvNavigation.NavigateTo<HomeViewModel>();
            _initialNavigationDone = true;
        }
        else
        {
            var checklistFile = await CheckListModel.FromYamlFileAsync(_rkCheckListArgumentParser.InitialFile);
            
            srvNavigation.NavigateTo<CheckListViewModel, CheckListModel>(checklistFile);
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
