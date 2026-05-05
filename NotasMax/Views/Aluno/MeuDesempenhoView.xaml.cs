using NotasMax.ViewModels;
using Syncfusion.Maui.Toolkit.Charts;

namespace NotasMax.Views.Aluno;

public partial class MeuDesempenhoView : ContentPage
{
    public MeuDesempenhoView()
    {
        InitializeComponent();
    }

    private void CalcularMedia(List<NotaSimulado> notas)
    {
        double media = notas.Average(u => u.Nota);
        label_media.Text = media.ToString("N2");
    }

    private void CalcularMenorNota(List<NotaMateria> notas)
    {
        NotaMateria? menorNota = notas.MinBy(n => n.Nota);
        if (menorNota != null)
        {
            label_menor_nota.Text = menorNota.Nota.ToString("N2");
            label_menor_materia.Text = menorNota.Materia;
        }
    }

    private void CalcularMaiorNota(List<NotaMateria> notas)
    {
        NotaMateria? maiorNota = notas.MaxBy(n => n.Nota);
        if (maiorNota != null)
        {
            label_maior_nota.Text = maiorNota.Nota.ToString("N2");
            label_maior_materia.Text = maiorNota.Materia;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var notasMaterias = new ViewModelExemplo().NotaMaterias;
        var notasSimulados = new ViewModelExemplo().NotaSimulados;

        line_chart.ItemsSource = notasSimulados;
        polar_line_chart.ItemsSource = notasMaterias;
        column_chart.ItemsSource = notasMaterias;

        CalcularMedia(notasSimulados);
        CalcularMenorNota(notasMaterias);
        CalcularMaiorNota(notasMaterias);
    }

    private void MinhasMaterias_Tapped(object sender, TappedEventArgs e)
    {
        DisplayAlertAsync("Ops!", "Essa funcionalidade ainda não está disponível.", "OK");
    }

    private void ColumnChart_LabelCreated(object sender, ChartAxisLabelEventArgs e)
    {
        string labelText = e.Label;

        if (labelText.Length > 3)
        {
            e.Label = labelText.Substring(0, 3);
        }
    }
}