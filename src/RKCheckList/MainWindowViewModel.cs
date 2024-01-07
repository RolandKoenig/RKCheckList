using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CommunityToolkit.Mvvm.Input;
using RKCheckList.Controls;
using RKCheckList.Messages;
using RKCheckList.Model;
using RKCheckList.Services;
using RKCheckList.Util;
using RKCheckList.Views;
using RolandK.AvaloniaExtensions.ViewServices;
using RolandK.InProcessMessaging;

namespace RKCheckList;

public partial class MainWindowViewModel : OwnViewModelBase
{
    private readonly IRKCheckListArgumentsContainer _rkCheckListArgumentContainer;
    private readonly IInProcessMessageSubscriber _messageSubscriber;

    private IEnumerable<MessageSubscription>? _messageSubscriptions;
    
    public static MainWindowViewModel DesignViewModel => new MainWindowViewModel(
        new RKCheckListArgumentsContainer(new InProcessMessenger(), Array.Empty<string>()),
        new InProcessMessenger());

    public string Title
    {
        get
        {
            var strBuilder = new StringBuilder(64);
            strBuilder.Append("RK CheckList");

            var version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
            if (!string.IsNullOrEmpty(version) &&
                Version.TryParse(version, out var parsedVersion))
            {
                strBuilder.Append(' ');
                strBuilder.Append($"{parsedVersion.Major}.{parsedVersion.Minor}");
            }

            var srvNavigation = this.TryGetViewService<INavigationViewService>();
            var currentViewTitle = srvNavigation?.CurrentViewTitle;
            if (!string.IsNullOrEmpty(currentViewTitle))
            {
                strBuilder.Append(" - ");
                strBuilder.Append(currentViewTitle);
            }
            
            return strBuilder.ToString();
        }
    }

    public MainWindowViewModel(
        IRKCheckListArgumentsContainer rkCheckListArgumentContainer,
        IInProcessMessageSubscriber messageSubscriber)
    {
        _rkCheckListArgumentContainer = rkCheckListArgumentContainer;
        _messageSubscriber = messageSubscriber;
    }

    [RelayCommand]
    public void CloseApplication()
    {
        base.CloseHostWindow();
    }

    private async void TriggerNavigation()
    {
        var srvNavigation = this.GetViewService<INavigationViewService>();
        var srvMessageBox = this.GetViewService<IMessageBoxViewService>();
        
        if (string.IsNullOrEmpty(_rkCheckListArgumentContainer.InitialFile))
        {
            if (!srvNavigation.IsCurrentlyOnAnyView())
            {
                srvNavigation.NavigateTo<HomeViewModel>();
            }
        }
        else
        {
            CheckListModel checkListFile;
            try
            {
                checkListFile = await CheckListModel.FromYamlFileAsync(_rkCheckListArgumentContainer.InitialFile);
            }
            catch (Exception)
            {
                await srvMessageBox.ShowAsync(
                    "Error",
                    "Unable to read file!",
                    MessageBoxButtons.Ok);
            
                srvNavigation.NavigateTo<HomeViewModel>();
                return;
            }
            
            srvNavigation.NavigateTo<CheckListViewModel, CheckListModel>(checkListFile);
        }
    }

    /// <inheritdoc />
    protected override void OnAssociatedViewChanged(object? associatedView)
    {
        _messageSubscriptions?.UnsubscribeAll();
        _messageSubscriptions = null;
        
        if (associatedView == null) { return; }
        
        _messageSubscriptions = _messageSubscriber.SubscribeAllWeak(this);
        this.TriggerNavigation();
        
        base.OnAssociatedViewChanged(associatedView);
    }

    private void OnMessageReceived(InitialFileChangedMessage message)
    {
        this.TriggerNavigation();
    }

    private void OnMessageReceived(NavigationCompleteMessage message)
    {
        this.OnPropertyChanged(nameof(MainWindowViewModel.Title));
    }
}
