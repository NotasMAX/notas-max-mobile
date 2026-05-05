using NotasMax.ViewModels;
using Syncfusion.Maui.Toolkit.Charts;

namespace NotasMax.Views.Aluno;

public partial class MeuDesempenhoView : ContentPage
{
	public MeuDesempenhoView()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        line_chart.ItemsSource = new ViewModelExemplo().DadosSimulados;

    }

    private void MinhasMaterias_Tapped(object sender, TappedEventArgs e)
    {
        DisplayAlertAsync("Ops!","Essa funcionalidade ainda não está disponível.", "OK");
    }
}