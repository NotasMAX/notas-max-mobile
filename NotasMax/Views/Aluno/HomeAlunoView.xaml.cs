using NotasMax.Models;
using NotasMax.Models.Usuarios;
using NotasMax.Services.NavigationService;
using NotasMax.Services.Settings;
using NotasMax.Services.Simulados;
using NotasMax.Services.Usuarios;

namespace NotasMax.Views.Aluno;

public partial class HomeAlunoView : ContentPage
{
    private readonly INavigationService _navigationService;
    private readonly ISettingsService  _settingsService;
    private readonly IUsuarioService  _usuarioService;
    private readonly ISimuladoService _simuladoService;

    private Usuario? _usuario;
    private List<Simulado> _simulados = new();

    public HomeAlunoView(INavigationService navigationService, ISettingsService settingsService, IUsuarioService usuarioService, ISimuladoService simuladoService)
	{
        _navigationService = navigationService;
        _settingsService = settingsService;
        _usuarioService = usuarioService;
        _simuladoService = simuladoService;

		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            _usuario = await _usuarioService.GetUsuarioById(
                _settingsService.UserIdKey,
                _settingsService.AuthAccessToken
            );

            _simulados = await _simuladoService.GetByAluno(
                _settingsService.UserIdKey,
                _settingsService.AuthAccessToken
             );

            txt_nomeUsuario.Text = _usuario?.Nome ?? "Usuário";
        }
        catch (HttpRequestException ex)
        {
            await DisplayAlertAsync("Erro", $"Sem conexão com o servidor. \nMessage:{ex.Message}", "Ok");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", $"Erro inesperado: {ex.Message}", "Ok");
        }
    }
    private void OnLabelVerTudoMateria(object sender, TappedEventArgs e)
    {

    }

    private void TapGestureRecognizer_VerMateria(object sender, TappedEventArgs e)
    {

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        _navigationService.NavigationAsync("Login?Logout=true", true);
    }
}