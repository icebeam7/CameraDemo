using CameraDemo.ViewModels;
using CameraDemo.Views;
using CommunityToolkit.Maui;
using Kotlin.Contracts;
using Microsoft.Extensions.Logging;

namespace CameraDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IMediaPicker>(MediaPicker.Default);
            builder.Services.AddSingleton<IFileSystem>(FileSystem.Current);

            builder.Services.AddTransient<CameraViewModel>();
            builder.Services.AddTransient<PhotoView>();
            builder.Services.AddTransient<VideoView>();


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
