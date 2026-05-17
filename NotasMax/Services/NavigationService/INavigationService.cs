using System;
using System.Collections.Generic;
using System.Text;

namespace NotasMax.Services.NavigationService
{
    public interface INavigationService
    {
        Task Initializeasync();
        Task NavigationAsync(string route);
    }
}
