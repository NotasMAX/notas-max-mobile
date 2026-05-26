using NotasMax.Helpers;
using NotasMax.Services.NavigationService;
using NotasMax.Services.Settings;
using NotasMax.Services.Simulados;
using NotasMax.Services.Usuarios;
using System.Diagnostics;

namespace NotasMax.Views.Aluno;

public partial class PerfilView : ContentPage
{
    private readonly ISettingsService _settingsService;
    private readonly INavigationService _navigationService;
    private readonly IUsuarioService _usuarioService;
    private readonly ISimuladoService _simuladoService;

    public PerfilView(
        ISettingsService settingsService,
        INavigationService navigationService,
        IUsuarioService usuarioService,
        ISimuladoService simuladoService)
    {
        InitializeComponent();
        _settingsService = settingsService;
        _navigationService = navigationService;
        _usuarioService = usuarioService;
        _simuladoService = simuladoService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarDados();
    }

    private async Task CarregarDados()
    {
        try
        {
            // Dados do usuário já salvos no SettingsService
            var nome = _settingsService.UserNameKey;
            label_nome.Text = nome;
            label_iniciais.Text = TextoHelper.PegarIniciais(nome);

            // Carregar desempenho para obter nota geral, qtd simulados e matérias
            var desempenho = await _simuladoService.GetDesempenhoAluno();

            if (desempenho != null)
            {
                // Nota geral = média das médias dos simulados
                if (desempenho.Simulados?.Count > 0)
                {
                    double notaGeral = desempenho.Simulados.Average(s => s.Media);
                    label_nota_geral.Text = notaGeral.ToString("N1");
                    label_qtd_simulados.Text = desempenho.Simulados.Count.ToString();
                }
                else
                {
                    label_nota_geral.Text = "--";
                    label_qtd_simulados.Text = "0";
                }

                label_qtd_materias.Text = desempenho.Materias?.Count.ToString() ?? "0";
            }

            // Carregar turma do aluno via detalhes do usuário
            var usuario = await _usuarioService.GetUsuarioById(
                _settingsService.UserIdKey,
                _settingsService.AuthAccessToken);

            // O modelo Usuario não expõe série/turma diretamente,
            // então exibimos o tipo de usuário como fallback
            // (quando a API retornar esse dado, basta ajustar aqui)
            label_turma.Text = usuario?.TipoUsuario is not null
                ? CapitalizarPrimeira(usuario.TipoUsuario)
                : "";
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erro ao carregar perfil: {ex.Message}");
        }
    }

    private static string CapitalizarPrimeira(string texto)
    {
        if (string.IsNullOrEmpty(texto)) return texto;
        return char.ToUpper(texto[0]) + texto.Substring(1).ToLower();
    }

    private void MinhasMaterias_Tapped(object sender, TappedEventArgs e)
    {
        // Tela de Minhas Matérias ainda não implementada
        DisplayAlertAsync("Em breve", "Essa funcionalidade estará disponível em breve.", "OK");
    }

    private void Sair_Tapped(object sender, TappedEventArgs e)
    {
        _navigationService.NavigationAsync("Login?Logout=true", true);
    }
}
