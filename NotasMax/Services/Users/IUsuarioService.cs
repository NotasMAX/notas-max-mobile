using NotasMax.Models.Usuarios;
using NotasMax.Models.Usuarios.Response;

namespace NotasMax.Services.Usuarios
{
    public interface IUsuarioService
    {
        Task<Usuario> Login(Usuario usuario);

        Task<UserToken> Authenticate(UsuarioAuth auth);
        Task<Usuario> GetUsuarioById(string id, string token);
        Task<ForgotPasswordResponse> ForgotPassword(string email);
        Task<ResetPasswordResponse> ResetPassword(string token, string novaSenha);
    }
}
