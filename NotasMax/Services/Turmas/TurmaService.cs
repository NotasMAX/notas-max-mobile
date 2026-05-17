using NotasMax.Models.Usuarios;
using NotasMax.Services.RequestProvider;
using NotasMax.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotasMax.Services.Turmas
{

    public interface ITurmaService
    {
        Task<TurmasByAnoAndProfessor> GetByAnoAndProfessor(int ano, Usuario professor);
    }

    public class TurmaService : ITurmaService
    {
        private readonly IRequestProvider _requestProvider;

        public TurmaService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<TurmasByAnoAndProfessor> GetByAnoAndProfessor(int ano = 0, Usuario? professor = null)
        {
            if (professor == null)
            {
                throw new ArgumentNullException(nameof(professor));
            }

            if (ano == 0)
            {
                ano = DateTime.Now.Year; 
            }

            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Turmas/ano={ano}/professor={professor.Id}";

            return await _requestProvider.GetAsync<TurmasByAnoAndProfessor>(endpoint);
        }
    }
}

