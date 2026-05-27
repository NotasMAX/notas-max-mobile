using NotasMax.Services.Usuarios;

namespace NotasMax.Views.EsqueceuSenha;

[QueryProperty(nameof(Email), "email")]
public partial class InformarCodigoRecuperacaoView : ContentPage
{
	private string _email = "exemplo@email.com"; 
	public string Email
	{
		get => _email;
		set => _email = Uri.UnescapeDataString(value ?? "");
	}

	private bool isEyeOpen = false;
	private readonly IUsuarioService _usuarioService;

	public InformarCodigoRecuperacaoView(IUsuarioService usuarioService)
	{
		InitializeComponent();
		_usuarioService = usuarioService;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		label_info.Text = $"Enviamos um código de recuperação para {Email}. Por favor, verifique seu email e insira o código abaixo.";
    }
    private void ToggleEye_Clicked(object sender, EventArgs e)
    {
        isEyeOpen = !isEyeOpen;
        entry_codigo.IsPassword = !isEyeOpen;
        ToggleEye.Source = isEyeOpen ? "eye_closed.png" : "eye_open.png";
    }

    private async Task<bool> ValidateCode()
    {
        string code = entry_codigo.Text.Trim();
        if (string.IsNullOrEmpty(code))
        {
            await DisplayAlertAsync("Erro", "Por favor, insira o código de recuperação enviado para seu email.", "OK");
            return false;
        }
        return true;
    }

    private async void CodeReceived_Clicked(object sender, EventArgs e)
    {
        if (!await ValidateCode())
            return;

        string token = entry_codigo.Text.Trim();
        await Shell.Current.GoToAsync($"///novasenha?token={Uri.EscapeDataString(token)}");
    }

    private async void BackToLogin_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Login");
    }
}