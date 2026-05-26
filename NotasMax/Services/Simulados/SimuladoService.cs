using NotasMax.Models;
using NotasMax.Models.Simulados;
using NotasMax.Services.RequestProvider;

namespace NotasMax.Services.Simulados
{
    public class SimuladoService(IRequestProvider resquestProvider) : ISimuladoService
    {
        private readonly IRequestProvider _requestProvider = resquestProvider;
        public async Task<SimuladoAlunoResponse> GetByAluno(string aluno_id, string token)
        {
            var uri = $"{GlobalSettings.Instance.SimuladoEndpoint}/getByAluno/{aluno_id}";
            var response = await _requestProvider.GetAsync<SimuladoAlunoResponse>(uri, token);

            return response;
        }
    }
}
