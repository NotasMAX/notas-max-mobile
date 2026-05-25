using Microsoft.Extensions.DependencyInjection;

namespace NotasMax
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            UserAppTheme = AppTheme.Light;

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
			Window window = new Window(new AppShell());

			window.Height = 600;
			window.Width = 350;

			return window;
		}
    }
}