using NotasMax.Services.Simulados;
using NotasMax.Services.NavigationService;
using NotasMax.Services.Settings;
using NotasMax.Services.Simulados;

namespace NotasMax.ContentsViews;

public partial class MeuSimuladoItemContentView : ContentView
{
    private readonly ISimuladoService _simuladoService;
    private readonly ISettingsService _settingsService;
    private readonly INavigationService _navigationService;

    public MeuSimuladoItemContentView()
	{
		InitializeComponent();
	}

    private void Button_Tapped(object sender, TappedEventArgs e)
    {

    }
}