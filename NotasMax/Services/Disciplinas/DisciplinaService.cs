using NotasMax.Models.Usuarios;
using NotasMax.Services.RequestProvider;
using NotasMax.Services.Turmas;
using NotasMax.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using static NotasMax.ViewModels.TurmasByAnoAndProfessor;

namespace NotasMax.Services.Disciplinas
{
    public interface IDisciplinaService
    {
        Task<DesempenhoByDisciplina> getDesempenhoByDisciplina(string disciplinaId);
        Task<DesempenhoAluno> GetDesempenhoAluno();
    }
    public class DisciplinaService : IDisciplinaService
    {
        private readonly IRequestProvider _requestProvider;

        public DisciplinaService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
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
            Usuario usuario = new();
            usuario.Id = "6a0a2d90a7adea15004c46c8"; // Substitua pelo ID  do usuário logado

            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Simulado/ano={ano}/aluno={usuario.Id}";

            return await _requestProvider.GetAsync<DesempenhoAluno>(endpoint);
        }
    }


}
