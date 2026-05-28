using NotasMax.Helpers;
using NotasMax.Services.NavigationService;
using NotasMax.Services.Settings;
using NotasMax.Services.Simulados;
using NotasMax.Services.Usuarios;
using NotasMax.ViewModels;
using System.Diagnostics;

namespace NotasMax.Views.Professor;

public partial class PerfilProfessorView : ContentPage
{
    private readonly ISettingsService _settingsService;
    private readonly INavigationService _navigationService;
    private readonly IUsuarioService _usuarioService;
    private readonly ISimuladoService _simuladoService;

    private bool _materiasExpandidas = true;
    private bool _turmasExpandidas = true;

    public PerfilProfessorView(
        ISettingsService settingsService,
        INavigationService navigationService,
        IUsuarioService usuarioService,
        ISimuladoService simuladoService)
    {
        _settingsService = settingsService;
        _navigationService = navigationService;
        _usuarioService = usuarioService;
        _simuladoService = simuladoService;

        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarDados();
    }

    private async Task CarregarDados()
    {
        try
        {
            // Nome e iniciais via SettingsService (já em cache após login)
            var nome = _settingsService.UserNameKey;
            label_nome.Text = nome;
            label_iniciais.Text = TextoHelper.PegarIniciais(nome);

            // Email e demais dados via API
            var usuario = await _usuarioService.GetUsuarioById(
                _settingsService.UserIdKey,
                _settingsService.AuthAccessToken);

            label_email.Text = usuario?.Email ?? string.Empty;

            // Turmas, matérias e alunos via endpoint do professor
            var dados = await _simuladoService.GetByAnoAndProfessor();

            if (dados != null)
            {
                label_qtd_turmas.Text = dados.TotalTurmas.ToString();
                label_qtd_alunos.Text = dados.TotalAlunos.ToString();

                // Deduplica matérias a partir das disciplinas de todas as turmas
                var materias = dados.Turmas
                    .SelectMany(t => t.Disciplinas)
                    .GroupBy(d => d.MateriaId)
                    .Select(g => g.First())
                    .ToList();

                label_qtd_materias.Text = materias.Count.ToString();

                // Chips de matérias
                layout_chips_materias.Children.Clear();
                foreach (var materia in materias)
                    layout_chips_materias.Children.Add(CriarChip(materia.MateriaNome, "{StaticResource BluePrimary}", Colors.White));

                // Chips de turmas (séries únicas)
                var turmasUnicas = dados.Turmas
                    .OrderBy(t => t.Serie)
                    .ToList();

                layout_chips_turmas.Children.Clear();
                foreach (var turma in turmasUnicas)
                    layout_chips_turmas.Children.Add(CriarChip(turma.SerieFormatada, "{StaticResource YellowSecudary}", Color.FromArgb("#B8860B")));
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erro ao carregar perfil do professor: {ex.Message}");
        }
    }

    private static Border CriarChip(string texto, string bgColorKey, Color textColor)
    {
        // Resolve a cor de fundo dinamicamente pelos recursos da aplicação
        Color bgColor = Color.FromArgb("#E6F0FA"); // Blue100 como fallback
        if (Application.Current?.Resources.TryGetValue(bgColorKey.Replace("{StaticResource ", "").Replace("}", ""), out var resource) == true
            && resource is Color cor)
            bgColor = cor;

        return new Border
        {
            StrokeThickness = 0,
            StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 20 },
            BackgroundColor = bgColor,
            Padding = new Thickness(12, 6),
            Margin = new Thickness(0, 0, 6, 6),
            Content = new Label
            {
                Text = texto,
                FontFamily = "Inter",
                FontSize = 13,
                FontAttributes = FontAttributes.Bold,
                TextColor = textColor
            }
        };
    }

    private void ToggleMaterias_Tapped(object sender, TappedEventArgs e)
    {
        _materiasExpandidas = !_materiasExpandidas;
        layout_chips_materias.IsVisible = _materiasExpandidas;
        label_toggle_materias.Text = _materiasExpandidas ? "∨" : "∧";
    }

    private void ToggleTurmas_Tapped(object sender, TappedEventArgs e)
    {
        _turmasExpandidas = !_turmasExpandidas;
        layout_chips_turmas.IsVisible = _turmasExpandidas;
        label_toggle_turmas.Text = _turmasExpandidas ? "∨" : "∧";
    }

    private void DadosPessoais_Tapped(object sender, TappedEventArgs e)
    {
        // Tela de Dados Pessoais ainda não implementada
        DisplayAlertAsync("Em breve", "Essa funcionalidade estará disponível em breve.", "OK");
    }

    private void Sair_Tapped(object sender, TappedEventArgs e)
    {
        _navigationService.NavigationAsync("Login?Logout=true", true);
    }
}
