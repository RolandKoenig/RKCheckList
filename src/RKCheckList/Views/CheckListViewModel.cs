using System.Collections.ObjectModel;
using Avalonia.Controls;
using RKCheckList.Controls;
using RKCheckList.Model;
using RKCheckList.Util;

namespace RKCheckList.Views;

public class CheckListViewModel : OwnViewModelBase, INavigationTarget, INavigationDataReceiver<CheckListModel>
{
    public static CheckListViewModel DesignViewModel => new ();

    public ObservableCollection<CheckListItemViewModel> Items { get; } = new ();

    /// <inheritdoc />
    public static Control CreateViewInstance()
    {
        return new CheckListView();
    }
    
    public void OnReceiveParameterFromNavigation(CheckListModel dto)
    {
        this.Items.Clear();

        foreach (var actItemModel in dto.Items)
        {
            this.Items.Add(new CheckListItemViewModel(actItemModel));
        }
    }
}
