using NotasMax.Models.Simulados;
using NotasMax.Services.Simulados;
using NotasMax.Services.Settings;
using NotasMax.ViewModels;

namespace NotasMax.Views.Aluno;

[QueryProperty(nameof(MateriaId), "materiaId")]
[QueryProperty(nameof(MateriaNome), "nome")]
public partial class ExibirMateriaView : ContentPage
{
    private readonly ISimuladoService _simuladoService;
    private readonly ISettingsService _settingsService;

    public string? MateriaId { get; set; }
    public string? MateriaNome { get; set; }

    public ExibirMateriaView(ISimuladoService simuladoService, ISettingsService settingsService)
    {
        InitializeComponent();
        _simuladoService = simuladoService;
        _settingsService = settingsService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        label_titulo_materia.Text = MateriaNome ?? "Matéria";
        await CarregarDados();
    }

    private async Task CarregarDados()
    {
        try
        {
            var alunoId = _settingsService.UserIdKey;
            var token = _settingsService.AuthAccessToken;

            var response = await _simuladoService.GetByAluno(alunoId, token);

            var notaSimulados = response.Simulados
                .Where(s => s.Materias.Any(m => m.MateriaId.ToString() == MateriaId))
                .OrderBy(s => s.Numero)
                .Select(s => new NotaSimulado
                {
                    Numero = s.Numero.ToString(),
                    Nota = s.Materias
                        .First(m => m.MateriaId.ToString() == MateriaId)
                        .Nota,
                    Data = s.DataRealizacao
                })
                .ToList();

            if (notaSimulados.Count == 0)
            {
                label_valor_media.Text = "0,00";
                return;
            }

            line_chart.ItemsSource = notaSimulados;
            BindableLayout.SetItemsSource(layout_materias, notaSimulados);
            CalcularMedia(notaSimulados);
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "OK");
        }
    }

    private void CalcularMedia(List<NotaSimulado> notas)
    {
        double media = notas.Average(n => n.Nota);
        label_valor_media.Text = media.ToString("N2");
    }
}