using NotasMax.Views;
using NotasMax.Views.Aluno;

namespace NotasMax
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeRouting();
            InitializeComponent();
        }

        // Método para registrar rotas de Navegação.
        public static void InitializeRouting()
        {
            Routing.RegisterRoute("Login", typeof(LoginPage));
            Routing.RegisterRoute("HomeAluno", typeof(HomeAlunoView));
        }
    }
}
