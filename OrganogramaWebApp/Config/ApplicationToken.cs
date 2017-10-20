using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrganogramaApp.WebApp.Config
{
    public static class ApplicationToken
    {
        private static string _Authority = "https://acessocidadao.es.gov.br/is/connect/token";
        private static string _clientId = ConfigurationManager.AppSettings["ClientIdOrganogramaApp"];
        private static string _secret = ConfigurationManager.AppSettings["SecretOrganogramaApp"];

        private static DateTime ExpiryTime { get; set; }

        private static TokenResponse _Token { get; set; }

        public async static Task<TokenResponse> GetToken()
        {
            if (_Token != null)
            {
                if (ExpiryTime > DateTime.UtcNow)
                {
                    return _Token;
                }                
            }

            TokenClient tokenClient = new TokenClient(_Authority, _clientId, _secret);
            string scope = "ApiOrganograma";
            _Token = await tokenClient.RequestClientCredentialsAsync(scope);

            if (_Token.IsError)
            {
                throw new SecurityTokenException($"Não foi possível gerar Token.");
            }
            
            ExpiryTime = DateTime.UtcNow.AddSeconds(_Token.ExpiresIn);


            return _Token;
        }
    }

}