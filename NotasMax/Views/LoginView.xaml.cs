using NotasMax.Exceptions;
using NotasMax.Models.Usuarios;
using NotasMax.Services.NavigationService;
using NotasMax.Services.Settings;
using NotasMax.Services.Usuarios;
using NotasMax.Views.EsqueceuSenha;
using System.Net;

namespace NotasMax.Views;

[QueryProperty(nameof(Logout), "Logout")] // rebece paremetro na rota, exemplo: "Login?Logout=true"
public partial class LoginView : ContentPage
{
    public bool Logout { get; set; } // salva o valor do parametro recebido na rota
    private readonly IUsuarioService _userService;
    private readonly ISettingsService _settingsService;
    private readonly INavigationService _navigationService;

    private bool isEyeOpen = false;
    public LoginView(IUsuarioService userService, ISettingsService settingsService, INavigationService navigationService)
    {
        _userService = userService;
        _settingsService = settingsService;
        _navigationService = navigationService;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (Logout) // se o parametro recebido for true, limpa o token de autenticação
            _settingsService.AuthAccessToken = string.Empty;
    }

    void OnLabelEsqueceuSenhaTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new InformarEmailView());
    }

    private void ToggleEye_Clicked(object sender, EventArgs e)
    {
        isEyeOpen = !isEyeOpen;
        entry_senha.IsPassword = !isEyeOpen;
        ToggleEye.Source = isEyeOpen ? "eye_closed.png" : "eye_open.png";
    }

    private async void ButtonEntrar_Clicked(object sender, EventArgs e)
    {
        var auth = new UsuarioAuth()
        {
            Email = entry_email.Text,
            Senha = entry_senha.Text,
        };

        if (string.IsNullOrWhiteSpace(auth.Email) || string.IsNullOrWhiteSpace(auth.Senha))
        {
            await DisplayAlertAsync("Atenção", "Preencha os campos E-mail e Senha.", "Ok");
            return;
        }

        try
        {
            var response = await _userService.Authenticate(auth);

            // Salvar o token e as informações do usuário usando o serviço de configurações
            _settingsService.Set(response);

            if (response.Usuario.TipoUsuario == "aluno")
            {
                await _navigationService.NavigationAsync($"HomeAluno", true);
            }

            if (response.Usuario.TipoUsuario == "professor")
            {
                await _navigationService.NavigationAsync($"HomeProfessor", true);
            }

            if (response.Usuario.TipoUsuario == "administrador")
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
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro: {ex.Message} \n\n{ex.InnerException?.Message}");

            await DisplayAlertAsync("Erro", "Erro com o servidor, Tente novamente.", "Ok");
        }

        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", $"Ocorreu um erro inesperado: {ex.Message}", "Ok");
        }
    }
}