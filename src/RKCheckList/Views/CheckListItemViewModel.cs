using RKCheckList.Model;

namespace RKCheckList.Views;

public class CheckListItemViewModel
{
    private readonly CheckListItemModel _model;

    public bool IsChecked { get; set; } = false;

    public string Text => _model.Text;

    public CheckListItemViewModel(CheckListItemModel model)
    {
        _model = model;
    }
}
