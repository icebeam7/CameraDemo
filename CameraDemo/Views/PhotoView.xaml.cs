using CameraDemo.ViewModels;

namespace CameraDemo.Views;

public partial class PhotoView : ContentPage
{
	public PhotoView(CameraViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}