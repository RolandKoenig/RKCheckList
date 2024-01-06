using System;
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
    public string Title => "Home";

    /// <inheritdoc />
    public static Control CreateViewInstance()
    {
        return new HomeView();
    }
    
    [RelayCommand]
    private async Task LoadCheckListFileAsync()
    {
        var srvOpenFile = this.GetViewService<IOpenFileViewService>();
        var srvNavigation = this.GetViewService<INavigationViewService>();
        var srvMessageBox = this.GetViewService<IMessageBoxViewService>();
        
        // User chooses the file to load
        var fileToOpen = await srvOpenFile.ShowOpenFileDialogAsync(
            new FileDialogFilter[]
            {
                new FileDialogFilter("CheckList", ".rkCheckList")
            },
            "Open CheckList");
        if (string.IsNullOrEmpty(fileToOpen)) { return; }

        // Load the file
        CheckListModel checkList;
        try
        {
            checkList = await CheckListModel.FromYamlFileAsync(fileToOpen);
        }
        catch (Exception)
        {
            await srvMessageBox.ShowAsync(
                "Error",
                "Unable to read file!",
                MessageBoxButtons.Ok);
            return;
        }
        
        // Navigate to the checklist
        srvNavigation.NavigateTo<CheckListViewModel, CheckListModel>(checkList);
    }
}
