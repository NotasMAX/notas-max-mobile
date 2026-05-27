using NotasMax.Models.Usuarios.Response;

namespace NotasMax.Services.Settings
{
    // Responsavel por armazenar o token de autenticação com a API
    public class SettingsService : ISettingsService
    {

        private const string AccessToken = "access_token";

        private const string UserId = "user_id";
        private const string UserName = "user_name";
        private const string UserRole = "user_type";
        private const string UserEmail = "user_email";
        private const string UserNomeResponsavel = "user_nome_responsavel";
        private const string UserTelefoneResponsavel = "user_telefone_responsavel";

        private readonly string _defaultString = "";

        public string AuthAccessToken 
        {
            get => Preferences.Get(AccessToken, _defaultString); 
            set => Preferences.Set(AccessToken, value); 
        }

        public string UserIdKey
        {
            get => Preferences.Get(UserId, _defaultString);
            set => Preferences.Set(UserId, value);
        }

        public string UserNameKey
        {
            get => Preferences.Get(UserName, _defaultString);
            set => Preferences.Set(UserName, value);
        }

        public string UserRoleKey
        {
            get => Preferences.Get(UserRole, _defaultString);
            set => Preferences.Set(UserRole, value);
        }
        public string UserEmailKey
        {
            get => Preferences.Get(UserEmail, _defaultString);
            set => Preferences.Set(UserEmail, value);
        }

        public string UserNomeResponsavelKey
        {
            get => Preferences.Get(UserNomeResponsavel, _defaultString);
            set => Preferences.Set(UserNomeResponsavel, value);
        }

        public string UserTelefoneResponsavelKey
        {
            get => Preferences.Get(UserTelefoneResponsavel, _defaultString);
            set => Preferences.Set(UserTelefoneResponsavel, value);
        }

        public void Set(UserToken userToken)
        {
            if (userToken?.Usuario is null)
                throw new InvalidOperationException("A resposta de autenticação não retornou os dados do usuário.");

            AuthAccessToken = userToken.Token ?? string.Empty;
            UserIdKey = userToken.Usuario.Id ?? string.Empty;
            UserNameKey = userToken.Usuario.Nome ?? string.Empty;
            UserRoleKey = userToken.Usuario.TipoUsuario ?? string.Empty;
            UserEmailKey = userToken.Usuario.Email ?? string.Empty;
            UserNomeResponsavelKey = userToken.Usuario.NomeResponsavel ?? string.Empty;
            UserTelefoneResponsavelKey = userToken.Usuario.TelefoneResponsavel ?? string.Empty;
        }
    }
}
