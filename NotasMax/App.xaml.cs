using NotasMax.Services.NavigationService;

namespace NotasMax
{
    public partial class App : Application
    {
        private readonly INavigationService _navigationService;

        public App(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeComponent();

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