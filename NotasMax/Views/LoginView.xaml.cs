using NotasMax.Views.EsqueceuSenha;

namespace NotasMax.Views;

public partial class LoginView : ContentPage
{
    private bool isEyeOpen = false;
    public LoginView()
    {
        InitializeComponent();
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
        //Lógica de Login
    }
}