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
        Task<TurmasByAnoAndProfessor> GetByAnoAndProfessor();
    }

    public class TurmaService : ITurmaService
    {
        private readonly IRequestProvider _requestProvider;

        public TurmaService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<TurmasByAnoAndProfessor> GetByAnoAndProfessor()
        {
            var professor = new Usuario
            {
                Id = "6a0a2d90a7adea15004c46ba",
                Nome = "Professor Exemplo"
            };
            string ano = DateTime.UtcNow.Year.ToString();

            string endpoint = $"{GlobalSettings.DefaultEndpoint}/Turmas/ano={ano}/professor={professor.Id}";

            return await _requestProvider.GetAsync<TurmasByAnoAndProfessor>(endpoint);
        }
    }
}

