using NotasMax.ViewModels;

namespace NotasMax.Views.Professor;

public partial class DesempenhoAlunoView : ContentPage
{
    public DesempenhoAlunoView()
    {
        InitializeComponent();
    }

    private void CalcularMedia(List<NotaSimulado> notas)
    {
        double media = notas.Average(n => n.Nota);
        label_valor_media.Text = media.ToString("N2");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var notaSimulados = new ViewModelExemplo().NotaSimulados.OrderBy(s => s.Numero).ToList();

        string nomeAluno = "Lucas Oliveira";
        label_nome_aluno.Text = nomeAluno;
        label_iniciais.Text = ObterIniciais(nomeAluno);

        line_chart.ItemsSource = notaSimulados;
        BindableLayout.SetItemsSource(layout_simulados, notaSimulados);

        CalcularMedia(notaSimulados);
    }

    private static string ObterIniciais(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome)) return "??";
        var partes = nome.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (partes.Length == 1)
            return partes[0][..Math.Min(2, partes[0].Length)].ToUpper();
        return $"{partes[0][0]}{partes[^1][0]}".ToUpper();
    }
}