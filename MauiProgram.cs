using Camera.MAUI;
using Ejercicio_1._4.Helpers;
using Microsoft.Extensions.Logging;

namespace Ejercicio_1._4
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCameraView()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif
            string dbPath = FileAccessHelper.GetLocalFilePath("empleados2.db3");
            builder.Services.AddSingleton<DbContext>(s=>ActivatorUtilities.CreateInstance<DbContext>(s, dbPath));

            builder.Services.AddSingleton<IMediaPicker>(MediaPicker.Default);
            builder.Services.AddTransient<MainPage>();

            return builder.Build();
        }
    }
}