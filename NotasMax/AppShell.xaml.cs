using NotasMax.Services.NavigationService;
using NotasMax.Views;
using NotasMax.Views.Aluno;
using NotasMax.Views.Professor;

namespace NotasMax
{
    public partial class AppShell : Shell
    {
        private readonly INavigationService _navigationService;
        public AppShell(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeRouting();
            InitializeComponent();
        }

        protected override async void OnHandlerChanged()
        {
            base.OnHandlerChanged();

            if (Handler is not null)
            {
                await _navigationService.Initializeasync();
            }
        }

        // Método para registrar rotas de Navegação.
        public static void InitializeRouting()
        {
            Routing.RegisterRoute("Login", typeof(LoginView));
            Routing.RegisterRoute("HomeAluno", typeof(HomeAlunoView));
            Routing.RegisterRoute("HomeProfessor", typeof(HomeProfessorView));
            Routing.RegisterRoute("DesempenhoTurma", typeof(DesempenhoTurmaView));
            Routing.RegisterRoute("Turmas", typeof(TurmasView));
            Routing.RegisterRoute("MeuDesempenho", typeof(MeuDesempenhoView));
            Routing.RegisterRoute("Simulado", typeof(ExibirSimuladoView));
            Routing.RegisterRoute("CalendarioAluno", typeof(CalendarioAlunoView));
        }
    }
}
