using NotasMax.Models.Simulados;
using NotasMax.Services.Simulados;
using NotasMax.Services.Settings;
using System.Diagnostics;

namespace NotasMax.Views.Aluno;

public partial class MinhasMateriasView : ContentPage
{
    private readonly ISimuladoService _simuladoService;
    private readonly ISettingsService _settingsService;

    public MinhasMateriasView(ISimuladoService simuladoService, ISettingsService settingsService)
    {
        _simuladoService = simuladoService;
        _settingsService = settingsService;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarMaterias();
    }

    private async Task CarregarMaterias()
    {
        try
        {
            var alunoId = _settingsService.UserIdKey;
            var token = _settingsService.AuthAccessToken;

            var response = await _simuladoService.GetByAluno(alunoId, token);

            if (response?.MediaPorMateria == null || response.MediaPorMateria.Count == 0)
                return;

            grid_materias.RowDefinitions.Clear();
            grid_materias.Children.Clear();

            var materias = response.MediaPorMateria;

            int totalLinhas = (int)Math.Ceiling(materias.Count / 2.0);
            for (int i = 0; i < totalLinhas; i++)
                grid_materias.RowDefinitions.Add(new RowDefinition { Height = 100 });

            for (int i = 0; i < materias.Count; i++)
            {
                var materia = materias[i];
                int linha = i / 2;
                int coluna = i % 2;

                var card = CriarCardMateria(materia.MateriaId.ToString(), materia.Materia);
                Grid.SetRow(card, linha);
                Grid.SetColumn(card, coluna);
                grid_materias.Children.Add(card);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erro ao carregar matérias: {ex.Message}");
        }
    }

    private Border CriarCardMateria(string materiaId, string nomeMateria)
    {
        var cores = new[]
        {
            Color.FromArgb("#2EB867"),
            Color.FromArgb("#FFBB0F"),
            Color.FromArgb("#EF4343"),
            Color.FromArgb("#6C63FF"),
            Color.FromArgb("#1685F3"),
            Color.FromArgb("#FF7043"),
        };

        var cor = cores[Math.Abs(nomeMateria.GetHashCode()) % cores.Length];

        var card = new Border
        {
            StrokeThickness = 0,
            StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 16 },
            BackgroundColor = cor,
            Content = new Label
            {
                Text = nomeMateria,
                FontFamily = "SpaceGrotesk",
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(12)
            }
        };

        var tap = new TapGestureRecognizer();
        tap.Tapped += (s, e) => Navegar_Materia(materiaId, nomeMateria);
        card.GestureRecognizers.Add(tap);

        return card;
    }

    private async void Navegar_Materia(string materiaId, string nomeMateria)
    {
        await Shell.Current.GoToAsync(
            $"Materia?materiaId={Uri.EscapeDataString(materiaId)}&nome={Uri.EscapeDataString(nomeMateria)}");
    }
}