using NotasMax.Helpers;
using NotasMax.Services.Settings;

namespace NotasMax.Views.Aluno;

public partial class DadosPessoaisAlunoView : ContentPage
{
    private readonly ISettingsService _settingsService;

    public DadosPessoaisAlunoView(ISettingsService settingsService)
    {
        InitializeComponent();
        _settingsService = settingsService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregarDados();
    }

    private void CarregarDados()
    {
        string nome = _settingsService.UserNameKey;

        label_iniciais.Text = TextoHelper.PegarIniciais(nome);
        label_nome.Text = nome;
        label_email.Text = _settingsService.UserEmailKey;
        label_nome_responsavel.Text = _settingsService.UserNomeResponsavelKey;
        label_telefone_responsavel.Text = _settingsService.UserTelefoneResponsavelKey;
    }
}