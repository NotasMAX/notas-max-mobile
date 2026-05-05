using System.Text.RegularExpressions;

namespace NotasMax.Views.ForgottenPassword;

public partial class EmailRequestPage : ContentPage
{

    public EmailRequestPage()
    {
        InitializeComponent();
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
        await Navigation.PushAsync(new PasswordCodeRequestPage { Email = entry_email.Text.Trim() });
    }
}