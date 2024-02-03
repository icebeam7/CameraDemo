using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Graphics.Platform;

namespace CameraDemo.ViewModels
{

    public partial class CameraViewModel : BaseViewModel
    {
        [ObservableProperty]
        string photoPath;

        [ObservableProperty]
        MediaSource videoPath;

        IMediaPicker mediaPicker;
        IFileSystem fileSystem;

        public CameraViewModel(IMediaPicker mediaPicker, IFileSystem fileSystem)
        {
            this.mediaPicker = mediaPicker;
            this.fileSystem = fileSystem;
        }

        [RelayCommand]
        public async Task GetPhotoAsync(bool capture)
        {
            try
            {
                FileResult photo = null;

                if (capture)
                {
                    if (mediaPicker.IsCaptureSupported)
                        photo = await mediaPicker.CapturePhotoAsync();
                }
                else
                    photo = await mediaPicker.PickPhotoAsync();

                if (photo != null)
                {
                    //var stream = await photo.OpenReadAsync();
                    //ImageSource image = ImageSource.FromStream(() => stream);
                    PhotoPath = await LoadFileAsync(photo);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Info", "Photo error", "OK");
            }
        }

        [RelayCommand]
        public async Task GetVideoAsync(bool capture)
        {
            try
            {
                FileResult video = null;

                if (capture)
                {
                    if (mediaPicker.IsCaptureSupported)
                        video = await mediaPicker.CaptureVideoAsync();
                }
                else
                    video = await mediaPicker.PickVideoAsync();

                if (video != null)
                {
                    var newVideo = await LoadFileAsync(video);
                    VideoPath = MediaSource.FromFile(newVideo);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Info", "Video error", "OK");
            }
        }

        private async Task<string> LoadFileAsync(FileResult file)
        {
            if (file == null)
                return string.Empty;

            string fileCopyPath = Path.Combine(fileSystem.CacheDirectory, file.FileName);

            using var stream = await file.OpenReadAsync();
            using var newStream = File.OpenWrite(fileCopyPath);
            await stream.CopyToAsync(newStream);

            //var iimage = PlatformImage.FromStream(stream);
            //iimage.Save(newStream, ImageFormat.Png, 0.85f);

            return fileCopyPath;
        }
    }
}
