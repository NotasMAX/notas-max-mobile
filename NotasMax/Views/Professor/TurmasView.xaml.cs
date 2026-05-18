using NotasMax.Services.NavigationService;
using NotasMax.Services.Settings;
using NotasMax.Services.Simulados;
using NotasMax.ViewModels;
using System.Diagnostics;

namespace NotasMax.Views.Professor;

public partial class TurmasView : ContentPage
{
    private readonly ISimuladoService _simuladoService;
    private readonly ISettingsService _settingsService;
    private readonly INavigationService _navigationService;
    public TurmasView(ISimuladoService simuladoService, ISettingsService settingsService, INavigationService navigationService)
    {
		InitializeComponent();
        _simuladoService = simuladoService  ;
        _settingsService = settingsService;
        _navigationService = navigationService;
    }

    private async Task<TurmasByAnoAndProfessor?> fetchDadosTurmas()
    {
        try
        {
            return await _simuladoService.GetByAnoAndProfessor();
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

        var turmas = await fetchDadosTurmas();
        if (turmas == null)
            return;

        BindableLayout.SetItemsSource(layout_turmas, turmas?.Turmas);
        label_numero_turmas_ativas.Text = turmas?.TotalTurmas.ToString();
        label_numero_total_alunos.Text = turmas?.TotalAlunos.ToString();

    }

}