using NotasMax.Helpers;
using NotasMax.ViewModels;

namespace NotasMax.ContentsViews;

public partial class AlunoNotaItemContentView : ContentView
{
    public AlunoNotaItemContentView()
    {
        InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (!(this.BindingContext is GestaoNotasSimuladoViewModel.Aluno aluno))
            return;

        entry_acertos.Text = aluno.Acertos.ToString("D2");
        label_quantidade_questoes.Text = $"/ {aluno.QuantidadeQuestoes.ToString("D2")}";
        label_nome_aluno.Text = TextoHelper.ReduzirTexto(aluno.Nome, 20);
        label_iniciais_aluno.Text = TextoHelper.PegarIniciais(aluno.Nome);

        // Wire up TextChanged so edits propagate back to the model
        entry_acertos.TextChanged -= Entry_acertos_TextChanged;
        entry_acertos.TextChanged += Entry_acertos_TextChanged;
    }

    private void Entry_acertos_TextChanged(object? sender, TextChangedEventArgs e)
    {
        if (!(this.BindingContext is GestaoNotasSimuladoViewModel.Aluno aluno))
            return;

        if (int.TryParse(e.NewTextValue, out int newAcertos))
        {
            if (newAcertos < 0)
            {
                entry_acertos.TextChanged -= Entry_acertos_TextChanged;
                entry_acertos.Text = "00";
                entry_acertos.TextChanged += Entry_acertos_TextChanged;
                newAcertos = 0;
            }
            else if (newAcertos > aluno.QuantidadeQuestoes)
            {
                entry_acertos.TextChanged -= Entry_acertos_TextChanged;
                entry_acertos.Text = aluno.QuantidadeQuestoes.ToString("D2");
                entry_acertos.TextChanged += Entry_acertos_TextChanged;
                newAcertos = aluno.QuantidadeQuestoes;
            }

            aluno.Acertos = newAcertos;

            // Recalculate nota proportionally
            if (aluno.QuantidadeQuestoes > 0)
                aluno.Nota = Math.Round((double)newAcertos / aluno.QuantidadeQuestoes * 10, 2);
        }
    }
}