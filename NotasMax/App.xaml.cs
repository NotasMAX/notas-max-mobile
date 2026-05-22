using NotasMax.Services.NavigationService;
using Syncfusion.Maui.Toolkit.Localization;
using System.Globalization;
using System.Resources;

namespace NotasMax
{
    public partial class App : Application
    {
        private readonly INavigationService _navigationService;

        public App(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeComponent();

            CultureInfo culture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            UserAppTheme = AppTheme.Light;

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var w = new Window(new AppShell(_navigationService));
            w.Width = 448;
            w.Height = 700;

            return w;
        }
    }
}