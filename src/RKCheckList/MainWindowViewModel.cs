using System;
using CommunityToolkit.Mvvm.Input;
using RKCheckList.Util;

namespace RKCheckList;

public partial class MainWindowViewModel : OwnViewModelBase
{
    private readonly string[] _args;

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
}
