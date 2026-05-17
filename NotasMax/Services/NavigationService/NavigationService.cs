using NotasMax.Services.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotasMax.Services.NavigationService
{
    internal class NavigationService(ISettingsService settingsService) : INavigationService
    {
        private readonly ISettingsService _settingsService = settingsService;
        public Task Initializeasync()
        {
            var route = "Login";

            if (!string.IsNullOrEmpty(_settingsService.AuthAccessToken))
            {
                route = "HomeAluno";
            }
            return NavigationAsync(route);
        }

        public Task NavigationAsync(string route)
        {
            return Shell.Current.GoToAsync(route);
        }
    }
}
