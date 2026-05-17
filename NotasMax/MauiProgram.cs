using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Syncfusion.Maui.Toolkit.Hosting;
using NotasMax.Services.NavigationService;
using NotasMax.Services.RequestProvider;
using NotasMax.Services.Settings;
using NotasMax.Services.Usuarios;
using NotasMax.Views;
using NotasMax.Views.Aluno;
using NotasMax.Services.Turmas;
using NotasMax.Services.Disciplinas;

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
                     fonts.AddFont("RobotoMono.ttf", "RobotoMono");
                })
                .RegisterAppServices()
                .RegisterViews();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Configuração personalizada para remover bordas de Entry no Windows
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



        // Registrar os serviços da aplicação
        public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder app)
        {
            app.Services.AddSingleton<IRequestProvider, RequestProvider>();
            app.Services.AddSingleton<IUsuarioService, UsuarioService>();
            app.Services.AddSingleton<ISettingsService, SettingsService>();
            app.Services.AddSingleton<INavigationService, NavigationService>();
            app.Services.AddSingleton<ITurmaService, TurmaService>();
            app.Services.AddSingleton<IDisciplinaService, DisciplinaService>();
            return app;
        }

        // Registrar as Views que usarão os serviços da aplicação
        public static MauiAppBuilder RegisterViews(this MauiAppBuilder app)
        {
            app.Services.AddSingleton<LoginView>();
            app.Services.AddSingleton<HomeAlunoView>();
            return app;
        }

    }
}
