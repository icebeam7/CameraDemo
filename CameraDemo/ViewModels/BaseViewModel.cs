using CommunityToolkit.Mvvm.ComponentModel;

namespace CameraDemo.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        string title;
    }
}
