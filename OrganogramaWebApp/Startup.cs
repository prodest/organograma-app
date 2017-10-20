using IdentityModel.Client;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrganogramaApp.Apresentacao.Base;
using OrganogramaApp.Apresentacao.Models;
using OrganogramaApp.WebApp.Autorizacao;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(WebApp.Startup))]

namespace WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string localApp = ConfigurationManager.AppSettings["LocalApp"];

            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies",
                ExpireTimeSpan = TimeSpan.FromMinutes(30),
                SlidingExpiration = true,

            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = ConfigurationManager.AppSettings["ClientIdOrganogramaApp"],
                Authority = "https://acessocidadao.es.gov.br/is",
                RedirectUri = localApp,
                PostLogoutRedirectUri = localApp,
                ResponseType = "code id_token",
                Scope = "openid profile offline_access cpf email nome permissoes ApiOrganograma",


                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "nome",
                    RoleClaimType = "role"
                },

                SignInAsAuthenticationType = "Cookies",

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    AuthorizationCodeReceived = async n =>
                    {
                        // use the code to get the access and refresh token
                        var tokenClient = new TokenClient(
                            "https://acessocidadao.es.gov.br/is/connect/token",
                            ConfigurationManager.AppSettings["ClientIdOrganogramaApp"],
                            ConfigurationManager.AppSettings["SecretOrganogramaApp"]);

                        var tokenResponse = await tokenClient.RequestAuthorizationCodeAsync(
                                        n.Code, n.RedirectUri);

                        if (tokenResponse.IsError)
                        {
                            throw new Exception(tokenResponse.Error);
                        }

                        // use the access token to retrieve claims from userinfo
                        var userInfoClient = new UserInfoClient("https://acessocidadao.es.gov.br/is/connect/userinfo");

                        var userInfoResponse = await userInfoClient.GetAsync(tokenResponse.AccessToken);

                        // create new identity
                        var id = new ClaimsIdentity(n.AuthenticationTicket.Identity.AuthenticationType);

                        var userInfoList = userInfoResponse.Claims.ToList();
                        foreach (var ui in userInfoList)
                        {
                            if (ui.Type != "permissao" && ui.Type != "orgao")
                            {
                                id.AddClaim(new Claim(ui.Type, ui.Value));
                            }
                        }

                        //Adicionando as Claims de organização
                        var organizacaoClaims = userInfoResponse.Claims.Where(x => x.Type == "orgao").ToList();
                        foreach (var organizacaoClaim in organizacaoClaims)
                        {
                            string organizacaoSigla = organizacaoClaim.Value;

                            FillOrgaoEPatriarca(organizacaoSigla, id, tokenResponse.AccessToken);
                        }

                        //Adicionando as Claims de permissão
                        var permissaoClaims = userInfoResponse.Claims.Where(x => x.Type == "permissao").ToList();
                        foreach (var permissaoClaim in permissaoClaims)
                        {
                            dynamic objetoPermissao = JsonConvert.DeserializeObject(permissaoClaim.Value.ToString());
                            string recurso = objetoPermissao.Recurso;
                            id.AddClaim(new Claim("Recurso", recurso));
                            var listaAcoes = ((JArray)objetoPermissao.Acoes).Select(x => x.ToString()).ToList();
                            foreach (var acao in listaAcoes)
                            {
                                id.AddClaim(new Claim("Acao$" + recurso, acao));
                            }
                        }

                        id.AddClaim(new Claim("access_token", tokenResponse.AccessToken));
                        id.AddClaim(new Claim("expires_at", DateTime.Now.AddSeconds(tokenResponse.ExpiresIn).ToLocalTime().ToString()));
                        id.AddClaim(new Claim("refresh_token", tokenResponse.RefreshToken));
                        id.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));
                        //id.AddClaim(new Claim("sid", n.AuthenticationTicket.Identity.FindFirst("sid").Value));

                        n.AuthenticationTicket = new AuthenticationTicket(
                            new ClaimsIdentity(id.Claims, n.AuthenticationTicket.Identity.AuthenticationType, "nome", "role"),
                            n.AuthenticationTicket.Properties);
                    },

                    RedirectToIdentityProvider = n =>
                    {
                        // if signing out, add the id_token_hint
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                            if (idTokenHint != null)
                            {
                                n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                            }

                        }

                        return Task.FromResult(0);
                    }
                }
            });

            app.UseResourceAuthorization(new AuthorizationManager());
        }

        private void FillOrgaoEPatriarca(string organizacaoSigla, ClaimsIdentity id, string token)
        {
            var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "organizacoes/sigla/" + organizacaoSigla;
            var organizacaoString = WorkServiceBase.Get(url, token);

            id.AddClaim(new Claim("organizacao", organizacaoString.result));

            var organizacao = JsonConvert.DeserializeObject<OrganizacaoModel>(organizacaoString.result);

            var organizacoesPatriarcas = id.Claims.Where(x => x.Type == "organizacao_patriarca")
                                                      .Select(x => JsonConvert.DeserializeObject<OrganizacaoModel>(x.Value))
                                                      .ToList();

            OrganizacaoModel organizacaoPatriarca = null;

            if (organizacoesPatriarcas.Count > 0)
                organizacaoPatriarca = organizacoesPatriarcas.Where(op => op.guid.Equals(organizacao.organizacaoPai.guid)).SingleOrDefault();

            if (organizacaoPatriarca == null)
            {
                url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "organizacoes/" + organizacao.guid + "/patriarca";
                var patriarcaString = WorkServiceBase.Get(url, token);
                id.AddClaim(new Claim("organizacao_patriarca", patriarcaString.result));
            }
        }
    }
}
