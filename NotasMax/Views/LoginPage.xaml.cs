namespace NotasMax.Views;

public partial class LoginPage : ContentPage
{
    private bool isEyeOpen = false;
    public LoginPage()
    {
        InitializeComponent();
    }

    private void ButtonEntrarClicked(object sender, EventArgs e)
    {
        DisplayAlertAsync("teste", "Clicou em Entrar", "Continuar");
    }

    void OnLabelEsqueceuSenhaTapped(object sender, EventArgs e)
    {
        DisplayAlertAsync("teste", "Clicou em Esqueceu a senha", "Continuar");
    }

    private void ToggleEye_Clicked(object sender, EventArgs e)
    {
        isEyeOpen = !isEyeOpen;
        entry_senha.IsPassword = !isEyeOpen;
        ToggleEye.Source = isEyeOpen ? "eye_closed.png" : "eye_open.png";
    }
}