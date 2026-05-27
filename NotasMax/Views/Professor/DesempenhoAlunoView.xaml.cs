using NotasMax.Helpers;
using NotasMax.Services.Simulados;
using NotasMax.ViewModels;
using System.Diagnostics;

namespace NotasMax.Views.Professor;

[QueryProperty(nameof(AlunoId), "AlunoId")]
[QueryProperty(nameof(AlunoNome), "AlunoNome")]
public partial class DesempenhoAlunoView : ContentPage
{
    private readonly ISimuladoService _simuladoService;

    public string AlunoId { get; set; }
    public string AlunoNome { get; set; }

    public DesempenhoAlunoView(ISimuladoService simuladoService)
    {
        InitializeComponent();
        _simuladoService = simuladoService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarDados();
    }

    private async Task CarregarDados()
    {
        string nome = AlunoNome ?? "Aluno";
        label_nome_aluno.Text = nome;
        label_iniciais.Text = TextoHelper.PegarIniciais(nome);

        try
        {
            var resultado = await _simuladoService.GetSimuladosByAluno(AlunoId);

            if (resultado?.Simulados == null || resultado.Simulados.Count == 0)
                return;

            var simulados = resultado.Simulados.OrderBy(s => s.Numero).ToList();

            var notasSimulado = simulados.Select(s => new NotaSimulado
            {
                Numero = s.Numero.ToString(),
                Nota = s.Media,
                Data = s.DataRealizacao
            }).ToList();

            line_chart.ItemsSource = notasSimulado;
            BindableLayout.SetItemsSource(layout_simulados, notasSimulado);

            double media = simulados.Average(s => s.Media);
            label_valor_media.Text = media.ToString("N2");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erro ao carregar desempenho do aluno: {ex.Message}");
        }
    }
}