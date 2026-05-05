namespace NotasMax.Views.ForgottenPassword;

public partial class PasswordCodeRequestView : ContentPage
{
	private string _email = "exemplo@email.com"; 
    public string Email
    {
        get => _email;
        set => _email = value;
    }

	private bool isEyeOpen = false;
    public PasswordCodeRequestView()
	{
		InitializeComponent();
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
        // Aqui você pode adicionar a lógica para verificar o código inserido pelo usuário
        // Se o código for válido, navegue para a página de criação de nova senha
        await Navigation.PushAsync(new NewPasswordView());
    }
}