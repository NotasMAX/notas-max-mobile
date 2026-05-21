using NotasMax.Services.Simulados;
using NotasMax.ViewModels;
using System.Diagnostics;
using System.Globalization;

namespace NotasMax.Views.Aluno;

[QueryProperty(nameof(SimuladoSelecionado), "Simulado")]
public partial class ExibirSimuladoView : ContentPage
{
    public DesempenhoAlunoBySimulados.Simulado? SimuladoSelecionado { get; set; }
    private readonly ISimuladoService _simuladoService;

    public ExibirSimuladoView(ISimuladoService simuladoService)
    {
        InitializeComponent();
        _simuladoService = simuladoService;
    }
    private async Task<DesempenhoAlunoBySimulado?> fetchDadosSimulado()
    {
        try
        {
            await DisplayAlertAsync("erro", SimuladoSelecionado.Id, "ok");
            if (SimuladoSelecionado == null)
                return null;

            return await _simuladoService.GetDesempenhoAlunoBySimulado(SimuladoSelecionado.Id);
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

        var simulado = await fetchDadosSimulado();

        if (simulado == null)
            return;

        label_simulado.Text = "Simulado" + simulado.Numero;
        string dataFormatada = simulado.DataRealizacao.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("pt-BR"));
        label_data_realizacao.Text = dataFormatada;
        label_valor_media.Text = simulado.Media.ToString("N2");

        column_chart.ItemsSource = simulado.Disciplinas;
        polar_line_chart.ItemsSource = simulado.Disciplinas;

        BindableLayout.SetItemsSource(layout_disciplinas, simulado.Disciplinas);

    }

}