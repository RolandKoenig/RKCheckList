using System.Collections.ObjectModel;
using RKCheckList.Controls;
using RKCheckList.Model;
using RKCheckList.Util;

namespace RKCheckList.Views;

public class CheckListViewModel : OwnViewModelBase, INavigationDataReceiver<CheckListModel>
{
    public static CheckListViewModel DesignViewModel => new ();

    public ObservableCollection<CheckListItemViewModel> Items { get; } = new ();

    public void ReceiveFromNavigation(CheckListModel dto)
    {
        this.Items.Clear();

        foreach (var actItemModel in dto.Items)
        {
            this.Items.Add(new CheckListItemViewModel(actItemModel));
        }
    }
}
