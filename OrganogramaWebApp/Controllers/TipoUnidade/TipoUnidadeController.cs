using OrganogramaApp.Apresentacao.Base;
using OrganogramaApp.Apresentacao.Models;
using OrganogramaApp.WebApp.Models;
using System.Web.Mvc;

namespace OrganogramaApp.WebApp.Controllers.TipoUnidade
{
    public class TipoUnidadeController : BaseController
    {
        private ITipoUnidadeWorkService workService;

        public TipoUnidadeController(ITipoUnidadeWorkService workService)
        {
            this.workService = workService;
        }

        public ActionResult Index()
        {
            return View(workService.GetTiposUnidade(usuario.AccessToken));
        }

        public ActionResult Listar()
        {
            return PartialView("Consultar", workService.GetTiposUnidade(usuario.AccessToken));
        }

        public ActionResult Criar()
        {
            TelaTipoUnidade tela = new TelaTipoUnidade();
            return PartialView(tela);
        }

        [HttpPost]
        public ActionResult Criar(TipoUnidadeModel tipoUnidade)
        {
            try
            {
                var retorno = workService.PostTipoUnidade(tipoUnidade, usuario.AccessToken);
                
                if (retorno.retornoAjax.IsSuccessStatusCode)
                {
                    ModelState.Clear();                    
                    AdicionarMensagem(TipoMensagem.Sucesso, "Tipo de Unidade cadastrado com sucesso!");
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
            TelaTipoUnidade tela = new TelaTipoUnidade();
            tela = workService.GetTipoUnidade(id, usuario.AccessToken);

            if (tela.retornoAjax.IsSuccessStatusCode)
            {                
                return PartialView("Editar",tela);
            }
            else
            {
                AdicionarMensagem(TipoMensagem.Atencao, tela.retornoAjax.content);
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult Editar(TipoUnidadeModel tipoUnidade)
        {
            try
            {
                var retorno = workService.PutTipoUnidade(tipoUnidade.id, tipoUnidade, usuario.AccessToken);

                if (retorno.retornoAjax.IsSuccessStatusCode)
                {
                    AdicionarMensagem(TipoMensagem.Sucesso, "Tipo de Unidade atualizado com sucesso!");
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
            TelaTipoUnidade tela = new TelaTipoUnidade();
            tela = workService.GetTipoUnidade(id, usuario.AccessToken);

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
        public ActionResult Excluir(TipoUnidadeModel tipoUnidade)
        {
            try
            {
                var retorno = workService.DeleteTipoUnidade(tipoUnidade.id, usuario.AccessToken);

                if (retorno.retornoAjax.IsSuccessStatusCode)
                {
                    AdicionarMensagem(TipoMensagem.Sucesso, "Tipo de Unidade excluído com sucesso!");
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
