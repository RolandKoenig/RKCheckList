using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using RKCheckList.Controls;
using RKCheckList.Model;
using RKCheckList.Util;
using RolandK.AvaloniaExtensions.ViewServices;

namespace RKCheckList.Views;

public partial class HomeViewModel : OwnViewModelBase
{
    public static HomeViewModel DesignViewModel => new HomeViewModel();

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

        // TODO
        fileToOpen = fileToOpen.Substring(8);

        using var streamReader = new StreamReader(fileToOpen);
        var checkList = await CheckListModel.FromYamlAsync(streamReader);

        var srvNavigation = this.GetViewService<INavigationViewService>();
        srvNavigation.NavigateTo("CheckList", checkList);
    }
}
