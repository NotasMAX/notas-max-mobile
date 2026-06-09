using NotasMax.Services.Settings;

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
                var role = _settingsService.UserRoleKey.Trim().ToLowerInvariant();

                if (role == "aluno")
                    route = "aluno/inicio/homealunoview";
                else if (role == "professor")
                    route = "professor/inicio/homeprofessor";
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
