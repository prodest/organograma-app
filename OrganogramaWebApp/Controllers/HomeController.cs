using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganogramaApp.WebApp.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            ViewBag.Message = usuario.SiglaOrganizacao;
            return View();
        }

        public ActionResult GetUsuarioLogado()
        {
            return PartialView("_usuario", usuario);
        }        
    }
}