using NotasMax.Models.Simulados;
using NotasMax.Models.Usuarios;
using NotasMax.Services.NavigationService;
using NotasMax.Services.Settings;
using NotasMax.Services.Simulados;
using NotasMax.Services.Usuarios;

namespace NotasMax.Views.Professor;

public partial class HomeProfessorView : ContentPage
{
    private readonly INavigationService _navigationService;
    private readonly ISettingsService _settingsService;
    private readonly IUsuarioService _usuarioService;
    private readonly ISimuladoService _simuladoService;

    private Usuario? _usuario;
    private SimuladoProfessorResponse _simulados = new();
    private List<TurmaResumo> _todasTurmas = new();
    private List<int> _anosDisponiveis = new();

    public HomeProfessorView(INavigationService navigationService, ISettingsService settingsService, IUsuarioService usuarioService, ISimuladoService simuladoService)
	{
        _navigationService = navigationService;
        _settingsService = settingsService;
        _usuarioService = usuarioService;
        _simuladoService = simuladoService;

        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            _usuario = await _usuarioService.GetUsuarioById(
                _settingsService.UserIdKey,
                _settingsService.AuthAccessToken
            );

            await CarregarDados(_settingsService.UserIdKey);
        }
        catch (HttpRequestException ex)
        {
            await DisplayAlertAsync("Erro", $"Sem conexão com o servidor. \nMessage:{ex.Message}", "Ok");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", $"Erro inesperado: {ex.Message}", "Ok");
        }
    }

    private async void Avatar_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//professor/perfil/perfilprofessor");
    }

    private async void TapGestureRecognizer_VerTurma(object sender, TappedEventArgs e)
    {
        var turmaId = e.Parameter?.ToString() ?? string.Empty;

        if (string.IsNullOrEmpty(turmaId)) return;

        await Shell.Current.GoToAsync($"DesempenhoTurma?turmaId={turmaId}");
    }

    private async void OnLabelVerTudoTurmas(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//professor/turmas/turmasview");
    }

    private async Task CarregarDados(string professorId)
    {
        var resposta = await _simuladoService.GetByProfessor(
            professorId, _settingsService.AuthAccessToken);

        if (resposta is null) return;

        string nameUser = _usuario?.Nome ?? "Usuário";
        txt_nomeUsuario.Text = nameUser;
        txt_inicialUsuario.Text = !string.IsNullOrEmpty(nameUser)
            ? nameUser[0].ToString()
            : "?";

        // Insights
        txt_totalTurmas.Text = resposta.TotalTurmas.ToString();
        txt_totalSimulados.Text = resposta.TotalSimuladosAplicados.ToString();
        txt_notaGeral.Text = resposta.MediaGeralTodasTurmas?.ToString("F1") ?? "-";

        img_seta.Source = resposta.MediaGeralTodasTurmas >= 7.0
            ? "seta_subindo_desempenho.png"
            : "seta_descendo_desempenho.png";

        _todasTurmas = resposta.Turmas ?? new();

        collection_materias.ItemsSource = _todasTurmas;

        _anosDisponiveis = _todasTurmas
            .Select(t => t.Ano)
            .Distinct()
            .OrderByDescending(a => a)
            .ToList();

        picker_ano.ItemsSource = _anosDisponiveis
            .Select(a => a.ToString())
            .ToList();

        picker_ano.SelectedIndex = 0;

        if (_anosDisponiveis.Count == 1)
            AtualizarGrafico(_anosDisponiveis[0]);
    }

    // Metodos grafico
    private void Picker_Ano_Changed(object sender, EventArgs e)
    {
        if (picker_ano.SelectedIndex < 0) return;
        int anoSelecionado = _anosDisponiveis[picker_ano.SelectedIndex];
        AtualizarGrafico(anoSelecionado);
    }

    private void AtualizarGrafico(int ano)
    {
        var dadosGrafico = _todasTurmas
            .Where(t => t.Ano == ano)
            .OrderBy(t => t.Serie)
            .ToList();

        bool temDados = dadosGrafico.Any();

        grafico_turmas.IsVisible = temDados;
        lbl_grafico_vazio.IsVisible = !temDados;

        if (temDados)
            serie_turmas.ItemsSource = dadosGrafico;
    }

}