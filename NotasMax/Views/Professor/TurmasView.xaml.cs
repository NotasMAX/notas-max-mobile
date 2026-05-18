
using NotasMax.Models.Usuarios;
using NotasMax.Services.Disciplinas;
using NotasMax.Services.NavigationService;
using NotasMax.Services.Settings;
using NotasMax.Services.Turmas;
using NotasMax.ViewModels;
using System.Diagnostics;

namespace NotasMax.Views.Professor;

public partial class TurmasView : ContentPage
{
    private readonly ITurmaService _turmaService;
    private readonly ISettingsService _settingsService;
    private readonly INavigationService _navigationService;
    public TurmasView(ITurmaService turmaService, ISettingsService settingsService, INavigationService navigationService)
    {
		InitializeComponent();
        _turmaService = turmaService;
        _settingsService = settingsService;
        _navigationService = navigationService;
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