using RKCheckList.Controls;
using RKCheckList.ViewServices;
using RolandK.AvaloniaExtensions.Mvvm.Controls;

namespace RKCheckList;

public partial class MainWindow : MvvmWindow
{
    public MainWindow()
    {
        this.InitializeComponent();
        
        this.ViewServices.Add(new DependencyInjectionViewService(this));
        this.ViewServices.Add(new NavigationViewService(this.CtrlNavigation));
    }
}