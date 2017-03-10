using OrganogramaApp.Apresentacao.Base;
using OrganogramaApp.Apresentacao.Comum;
using OrganogramaApp.Apresentacao.ViewModel;
using OrganogramaApp.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganogramaApp.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private IOrganogramaWorkService workService;

        public HomeController(IOrganogramaWorkService workService)
        {
            this.workService = workService;
        }

        public ActionResult Index()
        {
            try
            {
                var ovm = workService.Pesquisar(usuario.Organizacoes[0].guid, usuario.AccessToken);                
                return View(ovm);
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Erro, oe.Message);

                return View(new OrganogramaViewModel());
            }
        }

        public ActionResult Organograma(string guid)
        {
            try
            {
                var ovm = workService.Pesquisar(guid, usuario.AccessToken);                
                return Json(ovm, JsonRequestBehavior.AllowGet);
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Erro, oe.Message);

                return Json(new OrganogramaViewModel(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetUsuarioLogado()
        {
            return PartialView("_usuario", usuario);
        }        
    }
}