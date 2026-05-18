using NotasMax.Services.Disciplinas;
using NotasMax.Services.NavigationService;
using NotasMax.Services.Settings;
using NotasMax.Services.Turmas;
using NotasMax.ViewModels;
using System.Diagnostics;
namespace NotasMax.Views.Aluno;

public partial class MeuDesempenhoView : ContentPage
{
    private readonly ITurmaService _turmaService;
    private readonly ISettingsService _settingsService;
    private readonly INavigationService _navigationService;
    private readonly IDisciplinaService _disciplinaService;
    public MeuDesempenhoView(ITurmaService turmaService, ISettingsService settingsService, INavigationService navigationService, IDisciplinaService disciplinaService)
    {
        InitializeComponent();
        _turmaService = turmaService;
        _settingsService = settingsService;
        _navigationService = navigationService;
        _disciplinaService = disciplinaService;
    }

    private void CalcularMedia(List<DesempenhoAluno.Simulado> simulados)
    {
        double media = simulados.Average(u => u.Media);
        label_media.Text = media.ToString("N2");
    }

    private void CalcularMenorNota(List<DesempenhoAluno.Materia> materias)
    {
        var menorNota = materias.MinBy(n => n.Media);
        if (menorNota != null)
        {
            label_menor_nota.Text = menorNota.Media.ToString("N2");
            label_menor_materia.Text = menorNota.Nome;
        }
    }

    private void CalcularMaiorNota(List<DesempenhoAluno.Materia> materias)
    {
        var maiorNota = materias.MaxBy(n => n.Media);
        if (maiorNota != null)
        {
            label_maior_nota.Text = maiorNota.Media.ToString("N2");
            label_maior_materia.Text = maiorNota.Nome;
        }
    }

    private async Task<DesempenhoAluno?> FetchDesempenhoAluno()
    {
        try
        {
            return await _disciplinaService.GetDesempenhoAluno();
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

        DesempenhoAluno? desempenhoAluno = await FetchDesempenhoAluno();
        if (desempenhoAluno == null)
            return;

        var simulados = desempenhoAluno.Simulados;
        var materias = desempenhoAluno.Materias;

        line_chart.ItemsSource = simulados;
        polar_line_chart.ItemsSource = materias;
        column_chart.ItemsSource = materias;

        CalcularMedia(simulados);
        CalcularMenorNota(materias);
        CalcularMaiorNota(materias);
    }

    private void MinhasMaterias_Tapped(object sender, TappedEventArgs e)
    {
        DisplayAlertAsync("Ops!", "Essa funcionalidade ainda não está disponível.", "OK");
    }
}