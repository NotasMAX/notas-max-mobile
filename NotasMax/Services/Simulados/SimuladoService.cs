using NotasMax.Models;
using NotasMax.Models.Simulados;
using NotasMax.Models.Usuarios;
using NotasMax.Services.RequestProvider;
using NotasMax.Services.Settings;
using NotasMax.ViewModels;

namespace NotasMax.Services.Simulados

{
    public class SimuladoService : ISimuladoService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly ISettingsService _settingsService;
        private Usuario? _usuario { get; set; } = null;

        public SimuladoService(IRequestProvider requestProvider, ISettingsService settingsService)
        {
            _requestProvider = requestProvider;
            _settingsService = settingsService;
        }

        private async void getUsuario()
        {
            _usuario = new Usuario();
            _usuario.Id = _settingsService.UserIdKey;
            _usuario.Nome = _settingsService.UserNameKey;
            _usuario.TipoUsuario = _settingsService.UserRoleKey;
        }

        public async Task<SimuladoAlunoResponse> GetByAluno(string aluno_id, string token)
        {
            var uri = $"{GlobalSettings.Instance.SimuladoEndpoint}/getByAluno/{aluno_id}";
            var response = await _requestProvider.GetAsync<SimuladoAlunoResponse>(uri, token);

            return response;
        }

        public async Task<DesempenhoAlunoByDisciplina> getDesempenhoByDisciplina(string disciplinaId)
        {
            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Simulado/Disciplina/id={disciplinaId}";

            return await _requestProvider.GetAsync<DesempenhoAlunoByDisciplina>(endpoint);
        }

        public async Task<DesempenhoAluno> GetDesempenhoAluno()
        {
            string ano = DateTime.UtcNow.Year.ToString();
            getUsuario();
            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Desempenho/ano={ano}/aluno={_usuario?.Id}";

            return await _requestProvider.GetAsync<DesempenhoAluno>(endpoint);
        }

        public async Task<DesempenhoAlunoBySimulado> GetDesempenhoAlunoBySimulado(string simuladoId)
        {
            getUsuario();
            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Desempenho/aluno={_usuario?.Id}/simulado={simuladoId}";

            return await _requestProvider.GetAsync<DesempenhoAlunoBySimulado>(endpoint);
        }

        public async Task<DesempenhoAlunoBySimulados> GetSimuladosAluno()
        {
            string ano = DateTime.UtcNow.Year.ToString();
            getUsuario();   
            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Simulados/ano={ano}/aluno={_usuario?.Id}";

            return await _requestProvider.GetAsync<DesempenhoAlunoBySimulados>(endpoint);
        }
        public async Task<TurmasByAnoAndProfessor> GetByAnoAndProfessor()
        {
            string ano = DateTime.UtcNow.Year.ToString();
            getUsuario();
            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Turmas/ano={ano}/professor={_usuario?.Id}";

            return await _requestProvider.GetAsync<TurmasByAnoAndProfessor>(endpoint);
        }

        public async Task<CalendarioByAluno> GetCalendarioByAluno()
        {
            getUsuario();
            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Calendario/aluno={_usuario?.Id}";
            return await _requestProvider.GetAsync<CalendarioByAluno>(endpoint);
        }

        public async Task<DesempenhoAlunoBySimulados> GetSimuladosByAluno(string alunoId)
        {
            string ano = DateTime.UtcNow.Year.ToString();
            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Simulados/ano={ano}/aluno={alunoId}";
            return await _requestProvider.GetAsync<DesempenhoAlunoBySimulados>(endpoint);
        }
    }


}
