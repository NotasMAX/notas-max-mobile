using NotasMax.Models;
using NotasMax.Models.Simulados;
using NotasMax.ViewModels;

namespace NotasMax.Services.Simulados
{
    public interface ISimuladoService
    {
        Task<DesempenhoAlunoByDisciplina> getDesempenhoByDisciplina(string disciplinaId);
        Task<DesempenhoAluno> GetDesempenhoAluno();
        Task<CalendarioByAluno> GetCalendarioByAluno();
        Task<DesempenhoAlunoBySimulado> GetDesempenhoAlunoBySimulado(string simuladoId);
        Task<DesempenhoAlunoBySimulados> GetSimuladosAluno();
        Task<TurmasByAnoAndProfessor> GetByAnoAndProfessor();
        Task<DesempenhoAlunoBySimulados> GetSimuladosByAluno(string alunoId);
        Task<SimuladoAlunoResponse> GetByAluno(string aluno_id, string token);

    }
}
