using NotasMax.Models.Usuarios.Response;
using NotasMax.Services.NavigationService;

namespace NotasMax.Views.Aluno;


[QueryProperty(nameof(Aluno), "aluno")]
public partial class HomeAlunoView : ContentPage
{
    private readonly INavigationService _navigationService;
    private UserToken? Aluno {  get; set; }

    public HomeAlunoView(INavigationService navigationService)
	{
        _navigationService = navigationService;
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();


        txt_nomeUsuario.Text = Aluno.Usuario.Nome;

    }
    private void OnLabelVerTudoMateria(object sender, TappedEventArgs e)
    {

    }

    private void TapGestureRecognizer_VerMateria(object sender, TappedEventArgs e)
    {

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        _navigationService.NavigationAsync("Login?Logout=true");
    }
}