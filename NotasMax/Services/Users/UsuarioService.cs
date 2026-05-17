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
    }
}
