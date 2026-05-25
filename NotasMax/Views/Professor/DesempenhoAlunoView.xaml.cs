using NotasMax.ViewModels;
using NotasMax.Helpers;

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
        label_iniciais.Text = TextoHelper.PegarIniciais(nomeAluno);

        line_chart.ItemsSource = notaSimulados;
        BindableLayout.SetItemsSource(layout_simulados, notaSimulados);

        CalcularMedia(notaSimulados);
    }
}