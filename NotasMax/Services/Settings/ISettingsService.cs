using NotasMax.Models.Usuarios.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotasMax.Services.Settings
{
    public interface ISettingsService
    {
        string AuthAccessToken { get; set; }
        string UserIdKey { get; set; }
        string UserNameKey { get; set; }
        string UserRoleKey { get; set; }
        string UserEmailKey { get; set; }
        string UserNomeResponsavelKey { get; set; }
        string UserTelefoneResponsavelKey { get; set; }

        void Set(UserToken userToken);
    }
}
