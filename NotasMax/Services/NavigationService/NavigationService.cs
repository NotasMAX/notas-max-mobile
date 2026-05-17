using NotasMax.Services.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotasMax.Services.NavigationService
{
    // Serviço de navegação para gerenciar a navegação entre as páginas do aplicativo
    internal class NavigationService(ISettingsService settingsService) : INavigationService
    {
        private readonly ISettingsService _settingsService = settingsService;
        public Task Initializeasync()
        {
            var route = "Login";

            if (!string.IsNullOrEmpty(_settingsService.AuthAccessToken))
            {
                string path = "Login";

                if (_settingsService.UserRoleKey == "Aluno")
                    path = "HomeAluno";

                if (_settingsService.UserRoleKey == "Professor")
                    path = "HomeProfessor";

                route = path;
            }
            return NavigationAsync(route, absolute: true);
        }

        public Task NavigationAsync(string route, bool absolute)
        {
            var path = absolute ? $"//{route.TrimStart('/')}" : route;

            return Shell.Current.GoToAsync(path);
        }
    }
}
