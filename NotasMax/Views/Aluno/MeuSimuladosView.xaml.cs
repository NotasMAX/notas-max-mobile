using NotasMax.Services.NavigationService;
using NotasMax.Services.Settings;
using NotasMax.Services.Simulados;
using NotasMax.ViewModels;
using System.Diagnostics;

namespace NotasMax.Views.Aluno;

public partial class MeuSimuladosView : ContentPage
{
    private readonly ISettingsService _settingsService;
    private readonly INavigationService _navigationService;
    private readonly ISimuladoService _simuladoService;

    public MeuSimuladosView(ISettingsService settingsService, INavigationService navigationService, ISimuladoService simuladoService)
    {

        InitializeComponent();
        _settingsService = settingsService;
        _navigationService = navigationService;
        _simuladoService = simuladoService;
    }

    private async Task<List<DesempenhoAlunoBySimulados.Simulado>?> FetchSimuladosByAluno()
    {
        try
        {
            return (await _simuladoService.GetSimuladosAluno()).Simulados;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erro: {ex.Message}");
            return null;
        }
    }

    private void CalcularSimuladosRealizados(List<DesempenhoAlunoBySimulados.Simulado> simulados)
    {
        if (simulados == null)
            return;

        label_numero_simulados_realizados.Text = simulados.Where(s => s.DataRealizacao <= DateTime.Now).Count().ToString();
    }

    private void CalcularSimuladosPendentes(List<DesempenhoAlunoBySimulados.Simulado> simulados)
    {
        if (simulados == null)
            return;
        label_numero_simulados_pendentes.Text = simulados.Where(s => s.DataRealizacao > DateTime.Now).Count().ToString();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var simulados = await FetchSimuladosByAluno();

        if (simulados == null)
            return;

        CalcularSimuladosRealizados(simulados);
        CalcularSimuladosPendentes(simulados);

        BindableLayout.SetItemsSource(layout_simulados, simulados);
    }
}