using NotasMax.Models;
using NotasMax.ViewModels;

namespace NotasMax.Services.Simulados
{
    public interface ISimuladoService
    {
        Task<List<Simulado>> GetByAluno(string aluno_id, string token);
        Task<DesempenhoAlunoByDisciplina> getDesempenhoByDisciplina(string disciplinaId);
        Task<DesempenhoAluno> GetDesempenhoAluno();
        Task<CalendarioByAluno> GetCalendarioByAluno();
        Task<DesempenhoAlunoBySimulado> GetDesempenhoAlunoBySimulado(string simuladoId);
        Task<DesempenhoAlunoBySimulados> GetSimuladosAluno();
        Task<TurmasByAnoAndProfessor> GetByAnoAndProfessor();

    }
}
