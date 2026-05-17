using NotasMax.Helpers;
using NotasMax.ViewModels;

namespace NotasMax.ContentsViews;

public partial class MateriaItemContentView : ContentView
{
	public MateriaItemContentView()
	{
		InitializeComponent();
	}
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (!(this.BindingContext is NotaSimulado mediaTurmaAlunos))
            return;

        label_valor_nota.TextColor = ColorHelper.DefinirCor(mediaTurmaAlunos.Nota);
        label_nome_simulado.Text = "Simulado " + mediaTurmaAlunos.Numero;
        label_valor_nota.Text = mediaTurmaAlunos.Nota.ToString("N2");
    }

}