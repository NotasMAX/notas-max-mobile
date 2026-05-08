using NotasMax.Helpers;
using NotasMax.Models;
using NotasMax.ViewModels;
using Syncfusion.Maui.Toolkit.Charts;

namespace NotasMax.Views.Professor;

public partial class DesempenhoTurmaView : ContentPage
{
    private List<Brush> PaleteBrushes = new();
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
            label_menor_aluno.Text = TextoHelper.ReduzirTexto(menorNota.Nome);
        }
    }

    private void CalcularMaiorNota(List<NotaAluno> notas)
    {
        NotaAluno? maiorNota = notas.MaxBy(n => n.Nota);
        if (maiorNota == null)
            return;

        label_maior_nota.Text = maiorNota.Nota.ToString("N2");
        label_maior_aluno.Text = TextoHelper.ReduzirTexto(maiorNota.Nome);

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
        var turmas = (new ViewModelExemplo().Turmas).OrderBy(t => t.Serie).ToList();



        DefinirCoresDistribuicao(distribuicaoNotas);

        line_chart.ItemsSource = notasSimulados;
        column_chart.ItemsSource = notasAlunos;
        chirp_serie.ItemsSource = turmas;
        chirp_serie.SelectedItem = turmas.MinBy(t => t.Serie);

        CalcularMedia(notasSimulados);
        CalcularMenorNota(notasAlunos);
        CalcularMaiorNota(notasAlunos);
    }

    private void DefinirCoresDistribuicao(List<DistribuicaoNotas> distribuicaoNotas)
    {
        DistribuicaoNotas primeiraFaixa = new();
        DistribuicaoNotas segundaFaixa = new();
        DistribuicaoNotas terceiraFaixa = new();
        List<DistribuicaoNotas> notasDistribuidas = new();

        foreach (var item in distribuicaoNotas)
        {
            switch (item.Faixa)
            {
                case "<5":
                    primeiraFaixa = item;
                    break;

                case "5-7":
                    segundaFaixa = item;
                    break;

                case ">7":
                    terceiraFaixa = item;
                    break;
            }
        };

        // Definir as cores para cada faixa usando o ColorHelper
        // Faixa <5: Vermelho, Faixa 5-7: Amarelo, Faixa >7: Verde
        Color vermelho = ColorHelper.DefinirCor(0);
        Color amarelo = ColorHelper.DefinirCor(6);
        Color verde = ColorHelper.DefinirCor(10);

        notasDistribuidas.Add(primeiraFaixa);

        PaleteBrushes.Add(new SolidColorBrush(vermelho));
        notasDistribuidas.Add(segundaFaixa);
        PaleteBrushes.Add(new SolidColorBrush(amarelo));
        notasDistribuidas.Add(terceiraFaixa);
        PaleteBrushes.Add(new SolidColorBrush(verde));

        doughnut_chart.ItemsSource = notasDistribuidas;
        doughnut_chart.PaletteBrushes = PaleteBrushes;

        DefinirDestaqueDistribuicao(notasDistribuidas);
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

        e.Label = TextoHelper.ReduzirTexto(labelText);
    }

    private void chirp_serie_SelectionChanged(object sender, Syncfusion.Maui.Toolkit.Chips.SelectionChangedEventArgs e)
    {

        if (!(e.AddedItem is Turma turma))
            return;

    }
}