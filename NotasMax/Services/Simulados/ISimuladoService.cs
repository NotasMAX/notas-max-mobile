using NotasMax.Models;

namespace NotasMax.Services.Simulados
{
    public interface ISimuladoService
    {
        Task<List<Simulado>> GetByAluno(string aluno_id, string token);

    }
}
