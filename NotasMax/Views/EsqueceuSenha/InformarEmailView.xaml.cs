using System.Text.RegularExpressions;
using NotasMax.Services.Usuarios;

namespace NotasMax.Views.EsqueceuSenha;

public partial class InformarEmailView : ContentPage
{
    private readonly IUsuarioService _usuarioService;

    public InformarEmailView(IUsuarioService usuarioService)
    {
        InitializeComponent();
        _usuarioService = usuarioService;
    }

    private async Task<bool> ValidateEmail()
    {
        string email = entry_email.Text;
        if (string.IsNullOrEmpty(email))
        {
            await DisplayAlertAsync("Erro", "Por favor, insira um email válido.", "OK");
            return false;
        }
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
        {
            await DisplayAlertAsync("Erro", "Por favor, insira um email válido.", "OK");
            return false;
        }
        return true;
    }

    private async void SendPasswordRecoverCode_Clicked(object sender, EventArgs e)
    {
        if (!await ValidateEmail())
            return;

        button_entrar.IsEnabled = false;
        button_entrar.Text = "Enviando...";

        try
        {
            string email = entry_email.Text.Trim();
            var response = await _usuarioService.ForgotPassword(email);

            if (response != null)
            {
                await DisplayAlertAsync("Sucesso", response.Message ?? "Código de recuperação enviado para seu email!", "OK");
                await Shell.Current.GoToAsync($"///informarcodigo?email={Uri.EscapeDataString(email)}");
            }
            else
            {
                await DisplayAlertAsync("Erro", "Não foi possível enviar o código de recuperação. Verifique o email e tente novamente.", "OK");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao enviar código: {ex.Message}");
            await DisplayAlertAsync("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
        }
        finally
        {
            button_entrar.IsEnabled = true;
            button_entrar.Text = "Enviar Código de Recuperação";
        }
    }

    private async void BackToLogin_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///login");
    }
}