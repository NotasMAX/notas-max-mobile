using System;
using System.Collections.Generic;
using System.Text;

namespace NotasMax.Services.Settings
{
    public interface ISettingsService
    {
        string AuthAccessToken { get; set; }
    }
}
