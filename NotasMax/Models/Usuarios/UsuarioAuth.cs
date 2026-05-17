using System.Text.Json.Serialization;

namespace NotasMax.Models.Usuarios
{
    // Este modelo armazena os dados de autenticação, contendo as propriedades necessárias para realizar o login. Ele é utilizado para enviar as credenciais do usuário para a API de autenticação e receber um token de acesso em resposta, que são armazenadas no modelo Response/UserToken.cs
    public class UsuarioAuth 
    {

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("senha")]
        public string Senha { get; set; }
    }
}
