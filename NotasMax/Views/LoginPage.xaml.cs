using NotasMax.Exceptions;
using NotasMax.Models.Usuarios;
using NotasMax.Services.Settings;
using NotasMax.Services.Usuarios;
using System.Net;

namespace NotasMax.Views;

public partial class LoginPage : ContentPage
{
    private readonly IUsuarioService _userService;
    private readonly ISettingsService _settingsService;

    public LoginPage(IUsuarioService userService, ISettingsService settingsService)
    {
        _userService = userService;
        _settingsService = settingsService;
        InitializeComponent();
    }

    private async void ButtonEntrarClicked(object sender, EventArgs e)
    {
        // Fazer verificao se campos estao preenchidos
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
            await DisplayAlertAsync("Login", "Login realizado com sucesso", "Ok");

            await Shell.Current.GoToAsync("HomeAluno");

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