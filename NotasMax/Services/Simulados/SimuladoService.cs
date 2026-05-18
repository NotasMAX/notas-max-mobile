using NotasMax.Models.Usuarios;
using NotasMax.Services.RequestProvider;
using NotasMax.ViewModels;

namespace NotasMax.Services.Simulados

{

    public interface ISimuladoService
    {
        Task<DesempenhoByDisciplina> getDesempenhoByDisciplina(string disciplinaId);
        Task<DesempenhoAluno> GetDesempenhoAluno();

        Task<SimuladosByAluno> GetSimuladosAluno();

        Task<TurmasByAnoAndProfessor> GetByAnoAndProfessor();

    }
    public class SimuladoService : ISimuladoService
    {
        Usuario _aluno;
        Usuario _professor;
        private readonly IRequestProvider _requestProvider;

        public SimuladoService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
            _aluno = new Usuario
            {
                Id = "6a0b76383d9ea915916d1840",
                Nome = "Aluno Exemplo"
            }; //Necessário substituir pelo usuário logado
            _professor = new Usuario
            {
                Id = "6a0b76383d9ea915916d1831",
                Nome = "Professor Exemplo"
            }; //Necessário substituir pelo usuário logado
        }

        public async Task<DesempenhoByDisciplina> getDesempenhoByDisciplina(string disciplinaId)
        {
            if (string.IsNullOrEmpty(disciplinaId))
            {
                throw new ArgumentNullException(nameof(disciplinaId));
            }

            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Simulado/Disciplina/id={disciplinaId}";

            return await _requestProvider.GetAsync<DesempenhoByDisciplina>(endpoint);
        }

        public async Task<DesempenhoAluno> GetDesempenhoAluno()
        {
            string ano = DateTime.UtcNow.Year.ToString();

            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Desempenho/ano={ano}/aluno={_aluno.Id}";

            return await _requestProvider.GetAsync<DesempenhoAluno>(endpoint);
        }

        public async Task<SimuladosByAluno> GetSimuladosAluno()
        {
            string ano = DateTime.UtcNow.Year.ToString();

            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Simulados/ano={ano}/aluno={_aluno.Id}";

            return await _requestProvider.GetAsync<SimuladosByAluno>(endpoint);
        }
        public async Task<TurmasByAnoAndProfessor> GetByAnoAndProfessor()
        {
            string ano = DateTime.UtcNow.Year.ToString();

            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Turmas/ano={ano}/professor={_professor.Id}";

            return await _requestProvider.GetAsync<TurmasByAnoAndProfessor>(endpoint);
        }

    }


}
