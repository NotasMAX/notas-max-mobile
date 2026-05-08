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

        DefinirCorMedia(mediaTurmaAlunos.Media);
		label_quantidade_alunos.Text = mediaTurmaAlunos.QuantidadeAlunos + " alunos";
    }

	private void TurmaItem_Tapped(object sender, TappedEventArgs e)
	{
        if (!(this.BindingContext is MediaTurmaAlunos mediaTurmaAlunos))
            return;

		var id = mediaTurmaAlunos.Id;

        //Lógica para navegar para a página de detalhes da turma usando o id
    }

    private void DefinirCorMedia(double media)
	{
			if (media < 5)
			{
				label_valor_media.TextColor = Colors.Firebrick;
			}
			else if (media >= 5 && media < 7)
			{
				label_valor_media.TextColor = Colors.Gold;
			}
			else
			{
				label_valor_media.TextColor = Colors.ForestGreen;
			}
		
	}
}