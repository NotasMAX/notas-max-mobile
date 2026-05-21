using NotasMax.Helpers;
using NotasMax.ViewModels;

namespace NotasMax.ContentsViews;

public partial class ExibirSimuladoItemContentView : ContentView
{
    public ExibirSimuladoItemContentView()
    {
        InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (!(this.BindingContext is DesempenhoAlunoBySimulado.Disciplina disciplina))
            return;

        label_valor_nota.TextColor = ColorHelper.DefinirCor(disciplina.Nota);
        label_nome_disciplona.Text = disciplina.Nome;
        label_valor_nota.Text = disciplina.Nota.ToString("N2");
    }

}