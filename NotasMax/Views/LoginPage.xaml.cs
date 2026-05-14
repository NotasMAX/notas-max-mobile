using NotasMax.Exceptions;
using NotasMax.Models.Usuarios;
using NotasMax.Services.NavigationService;
using NotasMax.Services.Settings;
using NotasMax.Services.Usuarios;
using System.Net;

namespace NotasMax.Views;

[QueryProperty(nameof(Logout), "Logout")]
public partial class LoginPage : ContentPage
{

    public bool Logout { get; set; }
    private readonly IUsuarioService _userService;
    private readonly ISettingsService _settingsService;
    private readonly INavigationService _navigationService;
    
    public LoginPage(IUsuarioService userService, ISettingsService settingsService, INavigationService navigationService)
    {
        _userService = userService;
        _settingsService = settingsService;
        _navigationService = navigationService;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if(Logout)
            _settingsService.AuthAccessToken = string.Empty;
    }

    private async void ButtonEntrarClicked(object sender, EventArgs e)
    {

        var auth = new UsuarioAuth()
        {
            Email = entry_email.Text,
            Senha = entry_senha.Text,
        };

        if(string.IsNullOrWhiteSpace(auth.Email) || string.IsNullOrWhiteSpace(auth.Senha))
            throw new Exception("Preencha os campos email e senha.");

        try
        {
            var response = await _userService.Authenticate(auth);

            _settingsService.AuthAccessToken = response.Token;

            if (response.Usuario.TipoUsuario == "aluno")
            {
                await SecureStorage.Default.SetAsync("id_usuario", response.Usuario.Id);
                await _navigationService.NavigationAsync($"HomeAluno?aluno={response}");
            }

            if (response.Usuario.TipoUsuario == "professor")
            {
                await SecureStorage.Default.SetAsync("id_usuario", response.Usuario.Id);
                await _navigationService.NavigationAsync($"HomeProfessor?professor={response}");
            }

            if(response.Usuario.TipoUsuario == "administrador")
                await DisplayAlertAsync("Aviso", "Você esta realizando o login como administrador \nNo momento o aplicativo não conta com tela para administrador", "Ok");

        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            await DisplayAlertAsync("Erro", "E-mail ou senha inválidos.", "Ok");
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
        {
            await DisplayAlertAsync("Erro", "Requisição inválida. Verifique os campos.", "Ok");
        }
        catch (HttpRequestException)
        {
            await DisplayAlertAsync("Erro", "Sem conexão com o servidor. Tente novamente.", "Ok");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", $"Ocorreu um erro inesperado: {ex.Message}", "Ok");
        }

    }

    void OnLabelEsqueceuSenhaTapped(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("HomeAluno");
    }
}