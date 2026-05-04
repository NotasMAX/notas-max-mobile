using System;
using System.Collections.Generic;
using System.Text;

namespace NotasMax.Services.Settings
{
    // Responsavel por armazenar o token de autenticação com a API
    public class SettingsService : ISettingsService
    {

        private const string AccessToken = "access_token";
        private readonly string AccessTokenDefault = "";

        public string AuthAccessToken 
        {
            get => Preferences.Get(AccessToken, AccessTokenDefault); 
            set => Preferences.Set(AccessToken, value); 
        }
    }
}
