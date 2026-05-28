using NotasMax.Services.Simulados;
using System.Diagnostics;

namespace NotasMax.Views.Aluno;

public partial class MinhasMateriasView : ContentPage
{
    private readonly ISimuladoService _simuladoService;

    public MinhasMateriasView(ISimuladoService simuladoService)
    {
        _simuladoService = simuladoService;
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
            var desempenho = await _simuladoService.GetDesempenhoAluno();
            if (desempenho?.Materias == null || desempenho.Materias.Count == 0)
                return;

            grid_materias.RowDefinitions.Clear();
            grid_materias.Children.Clear();

            var materias = desempenho.Materias;

            // Calcula quantas linhas o grid precisará
            int totalLinhas = (int)Math.Ceiling(materias.Count / 2.0);
            for (int i = 0; i < totalLinhas; i++)
                grid_materias.RowDefinitions.Add(new RowDefinition { Height = 100 });

            for (int i = 0; i < materias.Count; i++)
            {
                var materia = materias[i];
                int linha = i / 2;
                int coluna = i % 2;

                var card = CriarCardMateria(materia.Nome);
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

    private Border CriarCardMateria(string nomeMateria)
    {
        // Paleta de cores fixa para os cards, ciclando entre elas
        var cores = new[]
        {
            Color.FromArgb("#2EB867"), // verde
            Color.FromArgb("#FFBB0F"), // amarelo
            Color.FromArgb("#EF4343"), // vermelho
            Color.FromArgb("#6C63FF"), // roxo
            Color.FromArgb("#1685F3"), // azul
            Color.FromArgb("#FF7043"), // laranja
        };

        // Usa o hash do nome para sempre associar a mesma cor à mesma matéria
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
        tap.Tapped += (s, e) => Navegar_Materia(nomeMateria);
        card.GestureRecognizers.Add(tap);

        return card;
    }

    private async void Navegar_Materia(string nomeMateria)
    {
        // Navega para ExibirMateriaView passando o nome da matéria como query parameter.
        await Shell.Current.GoToAsync($"Materia?nome={Uri.EscapeDataString(nomeMateria)}");
    }
}
