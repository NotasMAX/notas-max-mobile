using NotasMax.ViewModels;

namespace NotasMax.Views.Aluno;

public partial class ExibirMateriaView : ContentPage
{
	public ExibirMateriaView()
	{
		InitializeComponent();
	}
    private void CalcularMedia(List<NotaSimulado> notas)
    {
        double media = notas.Average(u => u.Nota);
        label_valor_media.Text = media.ToString("N2");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var notaSimulados = (new ViewModelExemplo().NotaSimulados).OrderBy(s => s.Numero).ToList();
        line_chart.ItemsSource = notaSimulados;
        collectionView_simulados.ItemsSource = notaSimulados;
        CalcularMedia(notaSimulados);
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

    }
}