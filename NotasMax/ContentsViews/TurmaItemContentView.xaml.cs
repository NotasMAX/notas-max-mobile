using NotasMax.Helpers;
using NotasMax.ViewModels;

namespace NotasMax.ContentsViews;

public partial class TurmaItemContentView : ContentView
{
    public TurmaItemContentView()
    {
        InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (!(this.BindingContext is TurmasByAnoAndProfessor.Turma turma))
            return;

        label_valor_media.TextColor = ColorHelper.DefinirCor(turma.Media);
        label_valor_media.Text = turma.Media.ToString("N2");
        label_quantidade_alunos.Text = turma.QuantidadeAlunos + " alunos";
    }

    private async void TurmaItem_Tapped(object sender, TappedEventArgs e)
    {
        if (this.BindingContext is not TurmasByAnoAndProfessor.Turma turma)
            return;

        await Shell.Current.GoToAsync("DesempenhoTurma", new Dictionary<string, object>
        {
            { "Turma", turma }
        });
    }
}