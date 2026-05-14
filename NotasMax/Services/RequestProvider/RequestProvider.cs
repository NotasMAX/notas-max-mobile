using NotasMax.Exceptions;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace NotasMax.Services.RequestProvider
{
    public class RequestProvider : IRequestProvider
    {

        private readonly Lazy<HttpClient> _httpClient =
            new(() =>
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                return httpClient;
            });

        // Metodo auxiliador para obter ou criar o HttpClient com token de autenticação
        private HttpClient GetOrCreateHttpClient(string token)
        {
            var httpClient = _httpClient.Value;
            httpClient.DefaultRequestHeaders.Clear(); // Limpa o cabeçalho para evitar acúmulo de tokens

            // Adiciona o token, fazendo uma verificação para garantir que ele não seja nulo ou vazio
            httpClient.DefaultRequestHeaders.Authorization =
                !string.IsNullOrEmpty(token) ?
                new System.Net.Http.Headers.AuthenticationHeaderValue(token) :
                null;

            return httpClient;
        }

        // Métodos para realizar requisições HTTP (GET, POST, PUT, DELETE) usando o HttpClient

        // Método POST - Dinâmico para enviar dados e receber uma resposta do mesmo tipo
        public async Task<TResult> PostAsync<TResult, TSend>(string uri, TSend data, string token = "")
        {
            var httpClient = GetOrCreateHttpClient(token);

            // Serializa o objeto de dados "data" para JSON e cria um StringContent para o corpo da requisição
            var body = new StringContent(JsonSerializer.Serialize(data));
            body.Headers.ContentType = new MediaTypeHeaderValue("application/json"); // Define o tipo de conteúdo como JSON

            // Envia a requisição POST para a URI especificada e aguarda a resposta
            var response = await httpClient.PostAsync(uri, body).ConfigureAwait(false);

            // Verifica se a resposta foi bem-sucedida, caso contrario lança uma exceção 
            if (!response.IsSuccessStatusCode)
                throw new ApiException(response.StatusCode, "Erro ao realizar a requisição com a API");

            // Lê o conteúdo da resposta e desserializa para o tipo TSend, retornando o resultado
            var result = await response.Content.ReadFromJsonAsync<TResult>();
            return result;
        }    
        
        public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
        {
            var httpClient = GetOrCreateHttpClient(token);
            
            var response = await httpClient.GetAsync(uri).ConfigureAwait(false);
            
            if (!response.IsSuccessStatusCode)
                throw new ApiException(response.StatusCode, "Erro ao realizar a requisição com a API");


            var result = await response.Content.ReadFromJsonAsync<TResult>();
            return result;
        }
    }
}
