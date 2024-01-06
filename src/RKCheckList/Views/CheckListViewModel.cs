using System.Collections.ObjectModel;
using Avalonia.Controls;
using RKCheckList.Controls;
using RKCheckList.Model;
using RKCheckList.Util;

namespace RKCheckList.Views;

public class CheckListViewModel : OwnViewModelBase, INavigationTarget, INavigationDataReceiver<CheckListModel>
{
    private CheckListModel? _currentModel;
    
    public static CheckListViewModel DesignViewModel => new ();

    public ObservableCollection<CheckListItemViewModel> Items { get; } = new ();

    /// <inheritdoc />
    public string Title => _currentModel?.Title ?? string.Empty;

    /// <inheritdoc />
    public static Control CreateViewInstance()
    {
        return new CheckListView();
    }
    
    public void OnReceiveParameterFromNavigation(CheckListModel dto)
    {
        this.Items.Clear();

        _currentModel = dto;
        foreach (var actItemModel in dto.Items)
        {
            this.Items.Add(new CheckListItemViewModel(actItemModel));
        }
    }
}
