using NotasMax.Models;
using NotasMax.Models.Simulados;

namespace NotasMax.Services.Simulados
{
    public interface ISimuladoService
    {
        Task<SimuladoAlunoResponse> GetByAluno(string aluno_id, string token);

    }
}
