using NotasMax.ViewModels;
using Syncfusion.Maui.Toolkit.Charts;

namespace NotasMax.Views.Professor;

public partial class DesempenhoTurmaView : ContentPage
{
	public DesempenhoTurmaView()
	{
		InitializeComponent();
	}
    private void CalcularMedia(List<NotaSimulado> notas)
    {
        double media = notas.Average(u => u.Nota);
        label_media.Text = media.ToString("N2");
    }

    private void CalcularMenorNota(List<NotaAluno> notas)
    {
        NotaAluno? menorNota = notas.MinBy(n => n.Nota);
        if (menorNota != null)
        {
            label_menor_nota.Text = menorNota.Nota.ToString("N2");
            label_menor_materia.Text = menorNota.Nome;
        }
    }

    private void CalcularMaiorNota(List<NotaAluno> notas)
    {
        NotaAluno? maiorNota = notas.MaxBy(n => n.Nota);
        if (maiorNota != null)
        {
            label_maior_nota.Text = maiorNota.Nota.ToString("N2");
            label_maior_materia.Text = maiorNota.Nome;
        }
    }
    private void DefinirDestaqueDistribuicao(List<DistribuicaoNotas> distribuicaoNotas)
    {
        int indexNotasBaixas = distribuicaoNotas.FindIndex(item => item.Faixa == "<5");
        doughnut_chart.ExplodeIndex = indexNotasBaixas;
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();

        var notasSimulados = (new ViewModelExemplo().NotaSimulados).OrderBy(n => n.Numero).ToList();
        var notasAlunos = (new ViewModelExemplo().NotaAlunos).OrderBy(n => n.Nome).ToList();
        var distribuicaoNotas = new ViewModelExemplo().DistribuicaoNotas;

        line_chart.ItemsSource = notasSimulados;
        doughnut_chart.ItemsSource = distribuicaoNotas;
        column_chart.ItemsSource = notasAlunos;

        DefinirDestaqueDistribuicao(distribuicaoNotas);
        CalcularMedia(notasSimulados);
        CalcularMenorNota(notasAlunos);
        CalcularMaiorNota(notasAlunos);
    }

    private void MinhasMaterias_Tapped(object sender, TappedEventArgs e)
    {
        DisplayAlertAsync("Ops!", "Essa funcionalidade ainda não está disponível.", "OK");
    }

    private void ColumnChart_LabelCreated(object sender, ChartAxisLabelEventArgs e)
    {
        string? labelText = e.Label;

        if (labelText == null)
            return;

        if (labelText.Length > 3)
        {
            e.Label = labelText.Substring(0, 3);
        }
    }
}