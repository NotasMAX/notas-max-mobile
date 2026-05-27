using NotasMax.Models.Usuarios;
using NotasMax.Models.Usuarios.Response;
using NotasMax.Services.RequestProvider;

namespace NotasMax.Services.Usuarios
{
    // Classe de serviço para lidar com as requisições feitas na API relacionadas ao usuário, como login e autenticação
    //                         (Primary Consntructor             )
    public class UsuarioService(IRequestProvider resquestProvider) : IUsuarioService
    {
        private readonly IRequestProvider _requestProvider = resquestProvider;


        // Metodo de login 
        public async Task<Usuario> Login(Usuario usuario) 
        {
            var uri = GlobalSettings.Instance.UsuarioLoginAuthEndpoint;
            return await _requestProvider.PostAsync<Usuario, Usuario>(uri, usuario);
        }
        public async Task<UserToken> Authenticate(UsuarioAuth auth)
        {
            var uri = GlobalSettings.Instance.UsuarioLoginAuthEndpoint;
            return await _requestProvider.PostAsync<UserToken, UsuarioAuth>(uri, auth);
        }

        public async Task<Usuario> GetUsuarioById(string id, string token)
        {
            var uri = $"{GlobalSettings.Instance.UsuarioEndPonint}/Detalhes/{id}";
            return await _requestProvider.GetAsync<Usuario>(uri, token);
        }

        public async Task<Models.Usuarios.Response.ForgotPasswordResponse> ForgotPassword(string email)
        {
            var uri = GlobalSettings.Instance.ForgotPasswordEndpoint;
            var request = new Models.Usuarios.ForgotPasswordRequest { Email = email };
            return await _requestProvider.PostAsync<Models.Usuarios.Response.ForgotPasswordResponse, Models.Usuarios.ForgotPasswordRequest>(uri, request);
        }

        public async Task<Models.Usuarios.Response.ResetPasswordResponse> ResetPassword(string token, string novaSenha)
        {
            var uri = GlobalSettings.Instance.ResetPasswordEndpoint;
            var request = new Models.Usuarios.ResetPasswordRequest { Token = token, NovaSenha = novaSenha };
            return await _requestProvider.PostAsync<Models.Usuarios.Response.ResetPasswordResponse, Models.Usuarios.ResetPasswordRequest>(uri, request);
        }
    }
}
