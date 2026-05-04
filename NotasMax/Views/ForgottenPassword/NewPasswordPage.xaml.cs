namespace NotasMax.Views.ForgottenPassword;

public partial class NewPasswordPage : ContentPage
{
	private bool isPasswordEyeOpen = false;
    private bool isConfirmPasswordEyeOpen = false;
    public NewPasswordPage()
	{
		InitializeComponent();
	}

    private async Task<bool> ValidatePassword()
    {
        string password = entry_password.Text;
        string confirmPassword = entry_confirm_password.Text;
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
           await  DisplayAlertAsync("Erro", "Por favor, preencha ambos os campos de senha.", "OK");
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

            await DisplayAlertAsync("Sucesso", "Senha alterada com sucesso! Redirecionando para o login...", "OK");
            await Navigation.PopToRootAsync(); 
        
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