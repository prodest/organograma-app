using OrganogramaApp.Apresentacao.Base;
using OrganogramaApp.Apresentacao.Models;
using OrganogramaApp.WebApp.Models;
using System.Web.Mvc;

namespace OrganogramaApp.WebApp.Controllers.TipoOrganizacao
{
    public class TipoOrganizacaoController : BaseController
    {
        private ITipoOrganizacaoWorkService workService;

        public TipoOrganizacaoController(ITipoOrganizacaoWorkService workService)
        {
            this.workService = workService;
        }

        public ActionResult Index()
        {
            return View(workService.GetTiposOrganizacao(usuario.AccessToken));
        }

        public ActionResult Listar()
        {
            return PartialView("Consultar", workService.GetTiposOrganizacao(usuario.AccessToken));
        }

        public ActionResult Criar()
        {
            TelaTipoOrganizacao tela = new TelaTipoOrganizacao();
            return PartialView(tela);
        }

        [HttpPost]
        public ActionResult Criar(TipoOrganizacaoModel tipoOrganizacao)
        {
            try
            {
                var retorno = workService.PostTipoOrganizacao(tipoOrganizacao, usuario.AccessToken);

                if (retorno.retornoAjax.IsSuccessStatusCode)
                {
                    ModelState.Clear();
                    AdicionarMensagem(TipoMensagem.Sucesso, "Tipo de Organização cadastrado com sucesso!");
                }
                else
                {
                    AdicionarMensagem(TipoMensagem.Atencao, retorno.retornoAjax.content);
                }

                return PartialView("Criar", retorno);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Editar(int id)
        {
            TelaTipoOrganizacao tela = new TelaTipoOrganizacao();
            tela = workService.GetTipoOrganizacao(id, usuario.AccessToken);

            if (tela.retornoAjax.IsSuccessStatusCode)
            {
                return PartialView("Editar", tela);
            }
            else
            {
                AdicionarMensagem(TipoMensagem.Atencao, tela.retornoAjax.content);
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult Editar(TipoOrganizacaoModel tipoOrganizacao)
        {
            try
            {
                var retorno = workService.PutTipoOrganizacao(tipoOrganizacao.id, tipoOrganizacao, usuario.AccessToken);

                if (retorno.retornoAjax.IsSuccessStatusCode)
                {
                    AdicionarMensagem(TipoMensagem.Sucesso, "Tipo de Organização atualizado com sucesso!");
                }
                else
                {
                    AdicionarMensagem(TipoMensagem.Atencao, retorno.retornoAjax.content);
                }


                return PartialView("Editar", retorno);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult ExcluirVerItem(int id)
        {
            TelaTipoOrganizacao tela = new TelaTipoOrganizacao();
            tela = workService.GetTipoOrganizacao(id, usuario.AccessToken);

            if (tela.retornoAjax.IsSuccessStatusCode)
            {
                return PartialView("ExcluirVerItem", tela);
            }
            else
            {
                AdicionarMensagem(TipoMensagem.Atencao, tela.retornoAjax.content);
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public ActionResult Excluir(TipoOrganizacaoModel tipoOrganizacao)
        {
            try
            {
                var retorno = workService.DeleteTipoOrganizacao(tipoOrganizacao.id, usuario.AccessToken);

                if (retorno.retornoAjax.IsSuccessStatusCode)
                {
                    AdicionarMensagem(TipoMensagem.Sucesso, "Tipo de Organização excluído com sucesso!");
                }
                else
                {
                    AdicionarMensagem(TipoMensagem.Atencao, retorno.retornoAjax.content);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}