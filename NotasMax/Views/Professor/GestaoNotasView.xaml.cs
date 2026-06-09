using NotasMax.Services.NavigationService;
using NotasMax.Services.Settings;
using NotasMax.Services.Simulados;
using NotasMax.ViewModels;
using System;
using System.Diagnostics;
using static NotasMax.ViewModels.GestaoNotasSimuladoViewModel;

namespace NotasMax.Views.Professor;

public partial class GestaoNotasView : ContentPage
{
    private GestaoNotasViewModel? _gestaoNotas;
    private GestaoNotasSimuladoViewModel? _gestaoNotasSimulado;
    private readonly ISettingsService _settingsService;
    private readonly INavigationService _navigationService;
    private readonly ISimuladoService _simuladoService;
    private bool isTurmaLoading = false;
    private bool isDisciplinaLoading = false;
    private bool isSimuladoLoading = false;

    public GestaoNotasView(ISettingsService settingsService, INavigationService navigationService, ISimuladoService simuladoService)
    {
        InitializeComponent();
        _settingsService = settingsService;
        _navigationService = navigationService;
        _simuladoService = simuladoService;
    }

    private async Task<GestaoNotasViewModel?> fetchDadosGestaoNotas()
    {
        try
        {
            return await _simuladoService.GetGestaoNotas();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erro: {ex.Message}");
            return null;
        }
    }

    private async Task<GestaoNotasSimuladoViewModel?> fetchDadosGestaoNotasSimulado(string disciplinaId, string simuladoId)
    {
        try
        {
            return await _simuladoService.GetGestaoNotasSimulado(simuladoId, disciplinaId);
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

        GestaoNotasViewModel? gestaoNotas = await fetchDadosGestaoNotas();

        if (gestaoNotas == null)
            return;

        gestaoNotas.Turmas = gestaoNotas.Turmas.OrderBy(t => t.Serie).ToList();
        _gestaoNotas = gestaoNotas;

        chirp_serie.ItemsSource = gestaoNotas.Turmas;

        chirp_serie.SelectedItem = gestaoNotas.Turmas.MinBy(t => t.Serie);
    }

    private async void chirp_serie_SelectionChanged(object sender, Syncfusion.Maui.Toolkit.Chips.SelectionChangedEventArgs e)
    {

        if (isTurmaLoading)
            return;

        isTurmaLoading = true;

        if (!(e.AddedItem is GestaoNotasViewModel.Turma turma))
            return;

        List<GestaoNotasViewModel.Disciplina> disciplinas = turma.Disciplinas.OrderBy(d => d.MateriaNome).ToList();

        chirp_disciplinas.ItemsSource = disciplinas;
        var primeiraDisciplina = disciplinas.FirstOrDefault();
        chirp_disciplinas.SelectedItem = primeiraDisciplina;

        isTurmaLoading = false;
    }

    private async void chirp_disciplinas_SelectionChanged(object sender, Syncfusion.Maui.Toolkit.Chips.SelectionChangedEventArgs e)
    {
        if (!(e.AddedItem is GestaoNotasViewModel.Disciplina disciplina))
            return;

        if (isDisciplinaLoading)
            return;
        isDisciplinaLoading = true;


        List<GestaoNotasViewModel.Simulado> simulados = disciplina.Simulados.OrderBy(s => s.Numero).ToList();

        chirp_simulados.ItemsSource = simulados;
        var primeiroSimulado = simulados.FirstOrDefault();
        chirp_simulados.SelectedItem = primeiroSimulado;

        isDisciplinaLoading = false;
    }

    private async void chirp_simulados_SelectionChanged(object sender, Syncfusion.Maui.Toolkit.Chips.SelectionChangedEventArgs e)
    {
        if (!(e.AddedItem is GestaoNotasViewModel.Simulado simulado))
            return;

        if (!(chirp_disciplinas.SelectedItem is GestaoNotasViewModel.Disciplina disciplina))
            return;

        if (isSimuladoLoading)
            return;
        isSimuladoLoading = true;

        try
        {
            _gestaoNotasSimulado = await fetchDadosGestaoNotasSimulado(disciplina.Id, simulado.Id);

            if (_gestaoNotasSimulado == null)
            {
                Debug.WriteLine("chirp_simulados_SelectionChanged: _gestaoNotasSimulado is null");
                return;
            }

            List<GestaoNotasSimuladoViewModel.Aluno> alunos = _gestaoNotasSimulado.Alunos.OrderBy(a => a.Nome).ToList();
            _gestaoNotasSimulado.Alunos = alunos;

            BindableLayout.SetItemsSource(layout_alunos, alunos);
        }
        finally
        {
            isSimuladoLoading = false;
        }
    }

    private async void button_salvar_Clicked(object sender, EventArgs e)
    {
        Debug.WriteLine($"button_salvar_Clicked: _gestaoNotasSimulado = {(_gestaoNotasSimulado == null ? "NULL" : "OK")}");

        if (_gestaoNotasSimulado == null)
        {
            Debug.WriteLine("Salvar cancelado: _gestaoNotasSimulado é nulo.");
            return;
        }

        Debug.WriteLine($"Salvando {_gestaoNotasSimulado.Alunos.Count} alunos. SimuladoId={_gestaoNotasSimulado.SimuladoId} DisciplinaId={_gestaoNotasSimulado.DisciplinaId}");

        try
        {
            await _simuladoService.SaveGestaoNotas(_gestaoNotasSimulado.SimuladoId, _gestaoNotasSimulado.DisciplinaId, _gestaoNotasSimulado.Alunos);
            Debug.WriteLine("Notas salvas com sucesso.");
            await DisplayAlert("Sucesso", "Notas salvas com sucesso!", "OK");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erro ao salvar notas: {ex.Message}");
            await DisplayAlert("Erro", $"Não foi possível salvar as notas: {ex.Message}", "OK");
        }
    }
}