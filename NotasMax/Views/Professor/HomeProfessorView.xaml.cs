using NotasMax.Services.NavigationService;

namespace NotasMax.Views.Professor;

public partial class HomeProfessorView : ContentPage
{
    private readonly INavigationService _navigationService;

    public HomeProfessorView(INavigationService navigationService)
	{
        _navigationService = navigationService;
        InitializeComponent();
	}

    private void Logout_Clicked(object sender, EventArgs e)
    {
        _navigationService.NavigationAsync("Login?Logout=true", true);
    }

    private void Avatar_Clicked(object sender, EventArgs e)
    {

    }
}