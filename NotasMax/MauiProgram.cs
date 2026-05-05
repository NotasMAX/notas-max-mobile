using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Syncfusion.Maui.Toolkit.Hosting;

namespace NotasMax
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Inter.ttf", "Inter");
                    fonts.AddFont("Roboto.ttf", "Roboto");
                    fonts.AddFont("SpaceGrotesk.ttf", "SpaceGrotesk");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.ConfigureMauiHandlers(handlers =>
            {
                EntryHandler.Mapper.AppendToMapping("BorderlessEntry", (handler, view) =>
                {
#if WINDOWS
                    var nativeView = handler.PlatformView;
                    nativeView.Loaded += (s, e) =>
                    {
                        nativeView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
                        nativeView.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                            Microsoft.UI.Colors.Transparent);
                        nativeView.FocusVisualPrimaryThickness   = new Microsoft.UI.Xaml.Thickness(0);
                        nativeView.FocusVisualSecondaryThickness = new Microsoft.UI.Xaml.Thickness(0);
                    };
#endif
                });
            });
            return builder.Build();
        }
    }
}
