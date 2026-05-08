using NotasMax.ViewModels;

namespace NotasMax.Views.Professor;

public partial class TurmasView : ContentPage
{
	public TurmasView()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var mediaTurmaAlunos = new ViewModelExemplo().MediaTurmaAlunos;
        collectionView_turmas.ItemsSource = mediaTurmaAlunos;

        CalcularAlunos(mediaTurmaAlunos);
        CalcularTurmas(mediaTurmaAlunos);
    }

    private void CalcularTurmas(List<MediaTurmaAlunos> mediaTurmaAlunos)
    {
        var soma = mediaTurmaAlunos.Count();
        label_numero_turmas_ativas.Text = soma.ToString();
    }

    private void CalcularAlunos(List<MediaTurmaAlunos> mediaTurmaAlunos)
    {
        var soma = mediaTurmaAlunos.Sum(i => i.QuantidadeAlunos);
        label_numero_total_alunos.Text = soma.ToString();
    }
}