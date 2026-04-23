namespace NotasMax
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var w = new Window(new AppShell());
            w.Width = 448;
            w.Height = 700;

            return w;
        }
    }
}