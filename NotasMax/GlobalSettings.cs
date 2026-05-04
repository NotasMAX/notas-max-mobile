namespace NotasMax
{
    public class GlobalSettings // Classe para armazenar configurações globais da aplicação, como endpoints de API
    {
        // Define o endpoint padrão para a API, que pode ser usado em toda a aplicação para fazer requisições
        public const string DefaultEndpoint = "http://localhost:5000/NotasMax";

        // Define o endpoint específico para autenticação de usuários, que é construído a partir do endpoint padrão
        public string UsuarioEndpoint { get; set; }

        public static GlobalSettings Instance { get; } = new GlobalSettings();

        // Constutor da classe, onde o endpoint de autenticação é inicializado usando o endpoint padrão
        public GlobalSettings()
        {
            UsuarioEndpoint = $"{DefaultEndpoint}/Auth/login";
        }
    }
}
