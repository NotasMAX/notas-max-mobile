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

        line_chart.ItemsSource = new ViewModelExemplo().NotaSimulados;
        polar_line_chart.ItemsSource = new ViewModelExemplo().NotaMaterias;

    }

    private void MinhasMaterias_Tapped(object sender, TappedEventArgs e)
    {
        DisplayAlertAsync("Ops!","Essa funcionalidade ainda não está disponível.", "OK");
    }

}