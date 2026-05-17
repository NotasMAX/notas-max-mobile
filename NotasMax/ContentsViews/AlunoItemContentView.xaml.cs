using NotasMax.Helpers;
using NotasMax.ViewModels;

namespace NotasMax.ContentsViews;

public partial class AlunoItemContentView : ContentView
{
    public AlunoItemContentView()
    {
        InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (!(this.BindingContext is NotaAluno notaAluno))
            return;

        label_nota_aluno.TextColor = ColorHelper.DefinirCor(notaAluno.Nota);
        label_nota_aluno.Text = notaAluno.Nota.ToString("N2");
        label_nome_aluno.Text = TextoHelper.ReduzirTexto(notaAluno.Nome, 20);
        label_iniciais_aluno.Text = TextoHelper.PegarIniciais(notaAluno.Nome);
    }

    private void TurmaItem_Tapped(object sender, TappedEventArgs e)
    {
        if (!(this.BindingContext is MediaTurmaAlunos mediaTurmaAlunos))
            return;

        var turmaId = mediaTurmaAlunos.TurmaId;

        //Lógica para navegar para a página de detalhes do aluno usando o id
    }
}