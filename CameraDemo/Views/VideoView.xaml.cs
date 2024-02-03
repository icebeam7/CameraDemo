using CameraDemo.ViewModels;

namespace CameraDemo.Views;

public partial class VideoView : ContentPage
{
	public VideoView(CameraViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}