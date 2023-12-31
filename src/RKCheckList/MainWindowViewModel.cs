using System;
using CommunityToolkit.Mvvm.Input;
using RKCheckList.Controls;
using RKCheckList.Model;
using RKCheckList.Services;
using RKCheckList.Util;
using RKCheckList.Views;
using RolandK.AvaloniaExtensions.ViewServices;

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
        var srvMessageBox = this.GetViewService<IMessageBoxViewService>();
        
        if (string.IsNullOrEmpty(_rkCheckListArgumentParser.InitialFile))
        {
            srvNavigation.NavigateTo<HomeViewModel>();
            _initialNavigationDone = true;
        }
        else
        {
            CheckListModel checkListFile;
            try
            {
                checkListFile = await CheckListModel.FromYamlFileAsync(_rkCheckListArgumentParser.InitialFile);
            }
            catch (Exception)
            {
                await srvMessageBox.ShowAsync(
                    "Error",
                    "Unable to read file!",
                    MessageBoxButtons.Ok);
            
                srvNavigation.NavigateTo<HomeViewModel>();
                _initialNavigationDone = true;
                return;
            }
            
            srvNavigation.NavigateTo<CheckListViewModel, CheckListModel>(checkListFile);
            _initialNavigationDone = true;
        }
    }

    /// <inheritdoc />
    protected override void OnAssociatedViewChanged(object? associatedView)
    {
        if (associatedView == null) { return; }

        this.TriggerInitialNavigation();
        
        base.OnAssociatedViewChanged(associatedView);
    }
}
