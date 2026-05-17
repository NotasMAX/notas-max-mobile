using NotasMax.Models.Usuarios;
using NotasMax.Services.RequestProvider;
using NotasMax.Services.Turmas;
using NotasMax.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotasMax.Services.Disciplinas
{
    public interface IDisciplinaService
    {
        Task<DesempenhoByDisciplina> getDesempenhoByDisciplina(string disciplinaId);
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
    }


}
