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
		if (!(this.BindingContext is MediaTurmaAlunos mediaTurmaAlunos))
			return;

        label_valor_media.TextColor = ColorHelper.DefinirCor(mediaTurmaAlunos.Media);
		label_valor_media.Text = mediaTurmaAlunos.Media.ToString("N2");
        label_quantidade_alunos.Text = mediaTurmaAlunos.QuantidadeAlunos + " alunos";
    }

	private void TurmaItem_Tapped(object sender, TappedEventArgs e)
	{
        if (!(this.BindingContext is MediaTurmaAlunos mediaTurmaAlunos))
            return;

		var turmaId = mediaTurmaAlunos.TurmaId;

        //Lógica para navegar para a página de detalhes da turma usando o id
    }
}