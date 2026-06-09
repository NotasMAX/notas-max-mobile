using NotasMax.Models;
using NotasMax.Models.Simulados;
using NotasMax.ViewModels;

namespace NotasMax.Services.Simulados
{
    public interface ISimuladoService
    {
        // Save edited student notes for a given simulado and disciplina
        Task SaveGestaoNotas(string simuladoId, string disciplinaId, List<GestaoNotasSimuladoViewModel.Aluno> alunos);
        Task<DesempenhoAlunoByDisciplina> getDesempenhoByDisciplina(string disciplinaId);
        Task<DesempenhoAluno> GetDesempenhoAluno();
        Task<CalendarioByAluno> GetCalendarioByAluno();
        Task<DesempenhoAlunoBySimulado> GetDesempenhoAlunoBySimulado(string simuladoId);
        Task<DesempenhoAlunoBySimulados> GetSimuladosAluno();
        Task<TurmasByAnoAndProfessor> GetByAnoAndProfessor();
        Task<DesempenhoAlunoBySimulados> GetSimuladosByAluno(string alunoId);
        Task<SimuladoAlunoResponse> GetByAluno(string aluno_id, string token);
        Task<SimuladoProfessorResponse> GetByProfessor(string professor_id, string token);
        Task<GestaoNotasViewModel> GetGestaoNotas();
        Task<GestaoNotasSimuladoViewModel> GetGestaoNotasSimulado(string simuladoId, string disciplinaId);

    }
}
