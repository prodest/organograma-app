using OrganogramaApp.Apresentacao.Models;
using OrganogramaApp.Apresentacao.TipoOrganizacao;
using OrganogramaApp.WebApp.Models;
using System.Configuration;
using System.Web.Mvc;

namespace OrganogramaApp.WebApp.Controllers.TipoOrganizacao
{
    public class TipoOrganizacaoController : BaseController
    {
        // GET: TipoUnidade
        public ActionResult Index()
        {
            TipoOrganizacaoWorkService tipoOrganizacao_ws = new TipoOrganizacaoWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);
            return View(tipoOrganizacao_ws.GetTiposOrganizacao(usuario.Token));
        }

        // GET: TipoUnidade/Details/5
        public ActionResult Listar()
        {
            TipoOrganizacaoWorkService tipoOrganizacao_ws = new TipoOrganizacaoWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);
            return PartialView("Consultar", tipoOrganizacao_ws.GetTiposOrganizacao(usuario.Token));
        }

        // GET: TipoUnidade/Create
        public ActionResult Criar()
        {
            TelaTipoOrganizacao tela = new TelaTipoOrganizacao();
            return PartialView(tela);
        }

        // POST: TipoUnidade/Create
        [HttpPost]
        //public ActionResult Criar(FormCollection collection)
        public ActionResult Criar(TipoOrganizacaoModel tipoOrganizacao)
        {
            try
            {
                TipoOrganizacaoWorkService tipoOrganizacao_ws = new TipoOrganizacaoWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);
                var retorno = tipoOrganizacao_ws.PostTipoOrganizacao(tipoOrganizacao, usuario.Token);

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

        // GET: TipoUnidade/Edit/5
        public ActionResult Editar(int id)
        {
            TipoOrganizacaoWorkService tipoOrganizacao_ws = new TipoOrganizacaoWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);

            TelaTipoOrganizacao tela = new TelaTipoOrganizacao();
            tela = tipoOrganizacao_ws.GetTipoOrganizacao(id, usuario.Token);

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

        // POST: TipoUnidade/Edit/5
        [HttpPost]
        public ActionResult Editar(TipoOrganizacaoModel tipoOrganizacao)
        {
            try
            {
                TipoOrganizacaoWorkService tipoOrganizacao_ws = new TipoOrganizacaoWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);

                var retorno = tipoOrganizacao_ws.PutTipoOrganizacao(tipoOrganizacao.id, tipoOrganizacao, usuario.Token);

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

        // GET: TipoUnidade/Edit/5
        public ActionResult ExcluirVerItem(int id)
        {
            TipoOrganizacaoWorkService tipoOrganizacao_ws = new TipoOrganizacaoWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);

            TelaTipoOrganizacao tela = new TelaTipoOrganizacao();
            tela = tipoOrganizacao_ws.GetTipoOrganizacao(id, usuario.Token);

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


        // Get: TipoUnidade/Delete/5
        [HttpGet]
        public ActionResult Excluir(TipoOrganizacaoModel tipoOrganizacao)
        {
            try
            {
                TipoOrganizacaoWorkService tipoOrganizacao_ws = new TipoOrganizacaoWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);
                var retorno = tipoOrganizacao_ws.DeleteTipoOrganizacao(tipoOrganizacao.id, usuario.Token);

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