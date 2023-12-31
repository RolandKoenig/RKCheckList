using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using RKCheckList.Controls;
using RKCheckList.Model;
using RKCheckList.Util;
using RolandK.AvaloniaExtensions.ViewServices;
using FileDialogFilter = RolandK.AvaloniaExtensions.ViewServices.FileDialogFilter;

namespace RKCheckList.Views;

public partial class HomeViewModel : OwnViewModelBase, INavigationTarget
{
    public static HomeViewModel DesignViewModel => new();

    /// <inheritdoc />
    public static Control CreateViewInstance()
    {
        return new HomeView();
    }
    
    [RelayCommand]
    private async Task LoadCheckListFileAsync()
    {
        var srvOpenFile = this.GetViewService<IOpenFileViewService>();
        var fileToOpen = await srvOpenFile.ShowOpenFileDialogAsync(
            new FileDialogFilter[]
            {
                new FileDialogFilter("CheckList", ".rkCheckList")
            },
            "Open CheckList");
        if (string.IsNullOrEmpty(fileToOpen)) { return; }

        using var streamReader = new StreamReader(fileToOpen);
        var checkList = await CheckListModel.FromYamlAsync(streamReader);
        
        var srvNavigation = this.GetViewService<INavigationViewService>();
        srvNavigation.NavigateTo<CheckListViewModel, CheckListModel>(checkList);
    }
}
