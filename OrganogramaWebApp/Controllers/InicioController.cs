using IdentityModel.Client;
using OrganogramaApp.Apresentacao.Base;
using OrganogramaApp.Apresentacao.Comum;
using OrganogramaApp.Apresentacao.ViewModel;
using OrganogramaApp.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OrganogramaApp.WebApp.Controllers
{
    //Trocar BaseController por Controller

    public class InicioController : BaseController
    {
        private IOrganogramaWorkService workService;

        public InicioController(IOrganogramaWorkService workService)
        {
            this.workService = workService;
        }
        // GET: Inicio
        public ActionResult Index()
        {   
            try
            {
                //var ovm = workService.Pesquisar(usuario.Organizacoes[0].guid, usuario.AccessToken);
                //var ovm = workService.Pesquisar(usuario.Organizacoes[0].guid, usuario.AccessToken);
                var ovm = workService.PesquisarFilhas(usuario.Organizacoes[0].organizacaoPai.guid, usuario.AccessToken);
                return View(ovm);
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Erro, oe.Message);

                return View(new OrganogramaViewModel());
            }
        }

        private async Task<object> GetToken()
        {
            TokenResponse tokenResponse = await GetOrganogramaAccessTokenAsync();
            return tokenResponse;
        }

        private async Task<TokenResponse> GetOrganogramaAccessTokenAsync()
        {
            //var _clientId = ConfigurationManager.AppSettings["clientApiOrganograma"];
            //var _secret = ConfigurationManager.AppSettings["SecretApiOrganograma"];

            var _clientId = ConfigurationManager.AppSettings["ClientIdOrganogramaApp"];
            var _secret = ConfigurationManager.AppSettings["SecretOrganogramaApp"];

            var authority = "https://acessocidadao.es.gov.br/is//connect/token";

            TokenClient tokenClient = new TokenClient(authority, _clientId, _secret);

            return await tokenClient.RequestClientCredentialsAsync("ApiOrganograma");            
        }
    }
}