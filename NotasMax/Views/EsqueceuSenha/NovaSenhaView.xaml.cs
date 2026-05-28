using NotasMax.Services.Usuarios;

namespace NotasMax.Views.EsqueceuSenha;

[QueryProperty(nameof(Token), "token")]
public partial class NovaSenhaView : ContentPage
{
    private bool isPasswordEyeOpen = false;
    private bool isConfirmPasswordEyeOpen = false;
    private readonly IUsuarioService _usuarioService;

    private string _token = string.Empty;
    public string Token
    {
        get => _token;
        set => _token = Uri.UnescapeDataString(value ?? "");
    }

    public NovaSenhaView(IUsuarioService usuarioService)
    {
        InitializeComponent();
        _usuarioService = usuarioService;
    }

    private async Task<bool> ValidatePassword()
    {
        string password = entry_password.Text;
        string confirmPassword = entry_confirm_password.Text;
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            await DisplayAlertAsync("Erro", "Por favor, preencha ambos os campos de senha.", "OK");
            return false;
        }
        if (password != confirmPassword)
        {
            await DisplayAlertAsync("Erro", "As senhas não coincidem. Por favor, tente novamente.", "OK");
            return false;
        }
        if (password.Length < 6)
        {
            await DisplayAlertAsync("Erro", "A senha deve conter pelo menos 6 caracteres.", "OK");
            return false;
        }
        return true;
    }

    private async void SavePassword_Clicked(object sender, EventArgs e)
    {

        if (!await ValidatePassword())
            return;

        button_save_password.IsEnabled = false;
        button_save_password.Text = "Salvando...";

        try
        {
            if (_usuarioService == null)
            {
                await DisplayAlertAsync("Erro", "Serviço não disponível. Por favor, tente novamente.", "OK");
                return;
            }

            string novaSenha = entry_password.Text.Trim();
            var response = await _usuarioService.ResetPassword(Token, novaSenha);

            if (response != null)
            {
                await DisplayAlertAsync("Sucesso", response.Message ?? "Senha alterada com sucesso!", "OK");
                await Shell.Current.GoToAsync("///Login");
            }
            else
            {
                await DisplayAlertAsync("Erro", "Não foi possível alterar a senha. Tente novamente.", "OK");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao alterar senha: {ex.Message}");
            await DisplayAlertAsync("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
        }
        finally
        {
            button_save_password.IsEnabled = true;
            button_save_password.Text = "Salvar Nova Senha";
        }
    }

    private async void BackToLogin_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Login");
    }
    private void ToggleEyePassword_Clicked(object sender, EventArgs e)
    {
        isPasswordEyeOpen = !isPasswordEyeOpen;
        entry_password.IsPassword = !isPasswordEyeOpen;
        ToggleEye.Source = isPasswordEyeOpen ? "eye_closed.png" : "eye_open.png";
    }

    private void ToggleEyeConfirmPassword_Clicked(object sender, EventArgs e)
    {
        isConfirmPasswordEyeOpen = !isConfirmPasswordEyeOpen;
        entry_confirm_password.IsPassword = !isConfirmPasswordEyeOpen;
        ToggleEyeConfirmPassword.Source = isConfirmPasswordEyeOpen ? "eye_closed.png" : "eye_open.png";
    }
}