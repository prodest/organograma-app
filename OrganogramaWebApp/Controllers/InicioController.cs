using IdentityModel.Client;
using OrganogramaApp.Apresentacao.Base;
using OrganogramaApp.Apresentacao.Comum;
using OrganogramaApp.Apresentacao.ViewModel;
using OrganogramaApp.WebApp.Config;
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

    //public class InicioController : BaseController
    public class InicioController: MessageController
    {
        private IOrganogramaWorkService _workServiceOrganograma;
        private IUnidadeWorkService _workServiceunidade;
        private IOrganizacaoWorkService _workServiceOrganizacao; 

        public InicioController(IOrganogramaWorkService workServiceOrganograma, IUnidadeWorkService workServiceunidade, IOrganizacaoWorkService workServiceOrganizacao)
        {
            this._workServiceOrganograma = workServiceOrganograma;
            this._workServiceunidade = workServiceunidade;
            this._workServiceOrganizacao = workServiceOrganizacao;
        }
        // GET: Inicio
        public async Task<ActionResult> Index()
        {
            try
            {
                TokenResponse token = await ApplicationToken.GetToken();
                ViewBag.teste = token;

                return View(_workServiceOrganograma.PesquisarFilhas("fe88eb2a-a1f3-4cb1-a684-87317baf5a57", token.AccessToken));                
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Erro, oe.Message);
                return View(new List<OrganogramaViewModel>());
            }
        }

        public async Task<ActionResult> Organograma(string guid)
        {
            try
            {
                TokenResponse token = await ApplicationToken.GetToken();

                var ovm = _workServiceOrganograma.Pesquisar(guid, token.AccessToken);
                return Json(ovm, JsonRequestBehavior.AllowGet);
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Erro, oe.Message);
                return Json(new OrganogramaViewModel(), JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Consultar(string guid)
        {
            try
            {
                TokenResponse token = await ApplicationToken.GetToken();
                return PartialView(_workServiceOrganizacao.Pesquisar(guid, token.AccessToken));
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Erro, oe.Message);
                return PartialView(new OrganizacaoVisualizacaoViewModel());
            }
        }

        public async Task<ActionResult> VisualizarUnidade(string guid)
        {
            UnidadeConsultarViewModel unidade = null;
            try
            {
                TokenResponse token = await ApplicationToken.GetToken();
                unidade = _workServiceunidade.Consultar(guid, token.AccessToken);
                unidade.Responsavel = _workServiceunidade.ConsultarResponsavel(guid, token.AccessToken);

                return PartialView("VisualizarUnidade", unidade);
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Erro, oe.Message);
                return Json(unidade, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                AdicionarMensagem(TipoMensagem.Erro, e.Message);
                return Json(unidade, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> VisualizarOrganizacao(string guid)
        {
            OrganizacaoVisualizacaoViewModel organizacao = null;

            try
            {
                TokenResponse token = await ApplicationToken.GetToken();
                organizacao = _workServiceOrganizacao.Pesquisar(guid, token.AccessToken);
                return PartialView("VisualizarOrganizacao", organizacao);
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Erro, oe.Message);
                return Json(organizacao, JsonRequestBehavior.AllowGet);
            }
        }

    }
}