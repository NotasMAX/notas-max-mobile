namespace NotasMax.Views;

public partial class LoginPage : ContentPage
{
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
}