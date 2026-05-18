using NotasMax.Helpers;
using NotasMax.Models;
using NotasMax.Models.Usuarios;
using NotasMax.Services.Disciplinas;
using NotasMax.Services.NavigationService;
using NotasMax.Services.Settings;
using NotasMax.Services.Turmas;
using NotasMax.Services.Usuarios;
using NotasMax.ViewModels;
using Syncfusion.Maui.Toolkit.Charts;
using System.Diagnostics;

namespace NotasMax.Views.Professor;

[QueryProperty(nameof(TurmaSelecionada), "Turma")]
public partial class DesempenhoTurmaView : ContentPage
{
    public TurmasByAnoAndProfessor.Turma? TurmaSelecionada { get; set; }

    private List<Brush> PaleteBrushes = new();
    private readonly ITurmaService _turmaService;
    private readonly ISettingsService _settingsService;
    private readonly INavigationService _navigationService;
    private readonly IDisciplinaService _disciplinaService;

    public DesempenhoTurmaView(ITurmaService turmaService, ISettingsService settingsService, INavigationService navigationService, IDisciplinaService disciplinaService )
    {
        InitializeComponent();
        _turmaService = turmaService;
        _settingsService = settingsService;
        _navigationService = navigationService;
        _disciplinaService = disciplinaService;
    }

    private void CalcularMenorNota(List<DesempenhoByDisciplina.DesempenhoAluno> alunos)
    {
        var menorNota = alunos.MinBy(n => n.Media);
        if (menorNota != null)
        {
            label_menor_nota.Text = menorNota.Media.ToString("N2");
            label_menor_aluno.Text = TextoHelper.ReduzirNome(menorNota.Nome);
        }
    }

    private void CalcularMaiorNota(List<DesempenhoByDisciplina.DesempenhoAluno> alunos)
    {
        var maiorNota = alunos.MaxBy(n => n.Media);
        if (maiorNota == null)
            return;

        label_maior_nota.Text = maiorNota.Media.ToString("N2");
        label_maior_aluno.Text = TextoHelper.ReduzirNome(maiorNota.Nome);

    }
    private void DefinirDestaqueDistribuicao(List<DesempenhoByDisciplina.DesempenhoDistribuicao> distribuicaoNotas)
    {
        int indexNotasBaixas = distribuicaoNotas.FindIndex(item => item.Faixa == "<5");
        doughnut_chart.ExplodeIndex = indexNotasBaixas;
    }

    private async Task<TurmasByAnoAndProfessor?> fetchDadosTurmas()
    {
        try
        {
            return await _turmaService.GetByAnoAndProfessor();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erro: {ex.Message}");
            return null;
        }
    }


    private async Task<DesempenhoByDisciplina?> fetchDadosDisciplinas(string disciplinaId)
    {
        try
        {
            return await _disciplinaService.getDesempenhoByDisciplina(disciplinaId);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erro: {ex.Message}");
            return null;
        }
    }


    protected async override void OnAppearing()
    {
        base.OnAppearing();

        TurmasByAnoAndProfessor? dadosTurmas = await fetchDadosTurmas();

        if (dadosTurmas == null)
            return;

        dadosTurmas.Turmas = dadosTurmas.Turmas.OrderBy(t => t.Serie).ToList();

        chirp_serie.ItemsSource = dadosTurmas.Turmas;

        if (TurmaSelecionada != null)
        {
            chirp_serie.SelectedItem = dadosTurmas.Turmas.Where(t => t.Id == TurmaSelecionada.Id).FirstOrDefault();
        }
        else
        {
            chirp_serie.SelectedItem = dadosTurmas.Turmas.MinBy(t => t.Serie);
        }
    }
    private void DefinirCoresDistribuicao(List<DesempenhoByDisciplina.DesempenhoDistribuicao> distribuicaoNotas)
    {
        DesempenhoByDisciplina.DesempenhoDistribuicao primeiraFaixa = new();
        DesempenhoByDisciplina.DesempenhoDistribuicao segundaFaixa = new();
        DesempenhoByDisciplina.DesempenhoDistribuicao terceiraFaixa = new();
        List<DesempenhoByDisciplina.DesempenhoDistribuicao> notasDistribuidas = new();

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

        e.Label = TextoHelper.ReduzirNome(labelText);
    }

    private async void chirp_serie_SelectionChanged(object sender, Syncfusion.Maui.Toolkit.Chips.SelectionChangedEventArgs e)
    {

        if (!(e.AddedItem is TurmasByAnoAndProfessor.Turma turma))
            return;

        List<TurmasByAnoAndProfessor.Disciplina> disciplinas = turma.Disciplinas.OrderBy(d => d.MateriaNome).ToList();

        chirp_disciplinas.ItemsSource = disciplinas;
        var primeiraDisciplina = disciplinas.FirstOrDefault();
        chirp_disciplinas.SelectedItem = primeiraDisciplina;

        if (primeiraDisciplina != null)
        {
            await CarregarDadosDisciplinaAsync(primeiraDisciplina);
        }
    }

    private async void chirp_disciplinas_SelectionChanged(object sender, Syncfusion.Maui.Toolkit.Chips.SelectionChangedEventArgs e)
    {
        if (!(e.AddedItem is TurmasByAnoAndProfessor.Disciplina disciplina))
            return;
        await CarregarDadosDisciplinaAsync(disciplina);
    }

    private async Task CarregarDadosDisciplinaAsync(TurmasByAnoAndProfessor.Disciplina disciplina)
    {

        Debug.WriteLine($"Disciplina selecionada: {disciplina.MateriaNome} (ID: {disciplina.Id})");

        var dadosDisciplinas = await fetchDadosDisciplinas(disciplina.Id);

        if (dadosDisciplinas == null)
            return;

        var alunos = dadosDisciplinas.Alunos.OrderBy(a => a.Nome).ToList();
        var simulados = dadosDisciplinas.SimuladosContainer.Simulados.OrderBy(s => s.NumeroSimulado).ToList();
        var distribuicaoNotas = dadosDisciplinas.Distribuicao;
        var media = dadosDisciplinas.SimuladosContainer.Media;
        DefinirCoresDistribuicao(distribuicaoNotas);

        line_chart.ItemsSource = simulados;
        column_chart.ItemsSource = alunos;

        CalcularMenorNota(alunos);
        CalcularMaiorNota(alunos);

        label_media.Text = media.ToString("N2");


        label_subtitulo_alunos.Text = $"Alunos ({alunos.Count})";

        BindableLayout.SetItemsSource(layout_alunos, alunos);
    }
}