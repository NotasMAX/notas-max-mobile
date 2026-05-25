using NotasMax.Models.Usuarios;
using NotasMax.Services.RequestProvider;
using NotasMax.ViewModels;

namespace NotasMax.Services.Simulados

{
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
                Id = "6a0b76383d9ea915916d1833",
                Nome = "Aluno Exemplo"
            }; //Necessário substituir pelo usuário logado
            _professor = new Usuario
            {
                Id = "6a0b76383d9ea915916d1831",
                Nome = "Professor Exemplo"
            }; //Necessário substituir pelo usuário logado
        }

        public async Task<List<Simulado>> GetByAluno(string aluno_id, string token)
        {
            var uri = $"{GlobalSettings.Instance.SimuladoEndpoint}/getByAluno/{aluno_id}";
            var response = await _requestProvider.GetAsync<SimuladoListResponse>(uri, token);

            return response?.Simulados ?? new List<Simulado>();
        }
        public async Task<DesempenhoAlunoByDisciplina> getDesempenhoByDisciplina(string disciplinaId)
        {
            if (string.IsNullOrEmpty(disciplinaId))
            {
                throw new ArgumentNullException(nameof(disciplinaId));
            }

            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Simulado/Disciplina/id={disciplinaId}";

            return await _requestProvider.GetAsync<DesempenhoAlunoByDisciplina>(endpoint);
        }

        public async Task<DesempenhoAluno> GetDesempenhoAluno()
        {
            string ano = DateTime.UtcNow.Year.ToString();

            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Desempenho/ano={ano}/aluno={_aluno.Id}";

            return await _requestProvider.GetAsync<DesempenhoAluno>(endpoint);
        }

        public async Task<DesempenhoAlunoBySimulado> GetDesempenhoAlunoBySimulado(string simuladoId)
        {
            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Desempenho/aluno={_aluno.Id}/simulado={simuladoId}";

            return await _requestProvider.GetAsync<DesempenhoAlunoBySimulado>(endpoint);
        }

        public async Task<DesempenhoAlunoBySimulados> GetSimuladosAluno()
        {
            string ano = DateTime.UtcNow.Year.ToString();

            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Simulados/ano={ano}/aluno={_aluno.Id}";

            return await _requestProvider.GetAsync<DesempenhoAlunoBySimulados>(endpoint);
        }
        public async Task<TurmasByAnoAndProfessor> GetByAnoAndProfessor()
        {
            string ano = DateTime.UtcNow.Year.ToString();

            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Turmas/ano={ano}/professor={_professor.Id}";

            return await _requestProvider.GetAsync<TurmasByAnoAndProfessor>(endpoint);
        }

        public async Task<CalendarioByAluno> GetCalendarioByAluno()
        {
            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Calendario/aluno={_aluno.Id}";
            return await _requestProvider.GetAsync<CalendarioByAluno>(endpoint);
        }
    }


}
