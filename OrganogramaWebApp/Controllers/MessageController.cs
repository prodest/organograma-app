using OrganogramaApp.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganogramaApp.WebApp.Controllers
{
    public class MessageController : Controller
    {
        [NonAction]
        protected void AdicionarMensagem(TipoMensagem tipo, string texto, string titulo = null)
        {
            ListaMensagens.AdicionarMensagem(tipo, texto, titulo);
        }

        public ActionResult MensagensAjax()
        {
            DesabilitarCache();
            return PartialView("MensagensAjax");
        }

        [NonAction]
        protected void DesabilitarCache()
        {
            //Desabilitando cache deste response para evitar duplicação de mensagens pelo cache do navegador
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.Now);
        }
    }
}