using NotasMax.Services.NavigationService;
using NotasMax.Services.Settings;
using Syncfusion.Maui.Toolkit.Localization;
using System.Globalization;
using System.Resources;

namespace NotasMax
{
    public partial class App : Application
    {
        private readonly INavigationService _navigationService;
        private readonly ISettingsService _settingsService;

        public App(INavigationService navigationService, ISettingsService settingsService)
        {
            _navigationService = navigationService;
            _settingsService = settingsService;
            InitializeComponent();

            CultureInfo culture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            UserAppTheme = AppTheme.Light;

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var w = new Window(new AppShell(_navigationService, _settingsService));
            w.Width = 448;
            w.Height = 700;

            return w;
        }
    }
}