using OrganogramaApp.WebApp.Models;
using ProcessoEletronicoWebApp.Apresentacao.Models;
using ProcessoEletronicoWebApp.Apresentacao.TipoUnidade;
using System.Configuration;
using System.Web.Mvc;

namespace OrganogramaApp.WebApp.Controllers.TipoUnidade
{
    public class TipoUnidadeController : BaseController
    {
        // GET: TipoUnidade
        public ActionResult Index()
        {
            TipoUnidadeWorkService tipoUnidade_ws = new TipoUnidadeWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);
            return View(tipoUnidade_ws.GetTiposUnidade(usuario.Token));
        }

        // GET: TipoUnidade/Details/5
        public ActionResult Listar()
        {
            TipoUnidadeWorkService tipoUnidade_ws = new TipoUnidadeWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);
            return PartialView("Consultar", tipoUnidade_ws.GetTiposUnidade(usuario.Token));
        }

        // GET: TipoUnidade/Create
        public ActionResult Criar()
        {
            TelaTipoUnidade tela = new TelaTipoUnidade();
            return PartialView(tela);
        }

        // POST: TipoUnidade/Create
        [HttpPost]
        //public ActionResult Criar(FormCollection collection)
        public ActionResult Criar(TipoUnidadeModel tipoUnidade)
        {
            try
            {
                TipoUnidadeWorkService tipoUnidade_ws = new TipoUnidadeWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);
                var retorno = tipoUnidade_ws.PostTipoUnidade(tipoUnidade, usuario.Token);
                
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

        // GET: TipoUnidade/Edit/5
        public ActionResult Editar(int id)
        {
            TipoUnidadeWorkService tipoUnidade_ws = new TipoUnidadeWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);

            TelaTipoUnidade tela = new TelaTipoUnidade();
            tela = tipoUnidade_ws.GetTipoUnidade(id, usuario.Token);

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

        // POST: TipoUnidade/Edit/5
        [HttpPost]
        public ActionResult Editar(TipoUnidadeModel tipoUnidade)
        {
            try
            {
                TipoUnidadeWorkService tipoUnidade_ws = new TipoUnidadeWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);

                var retorno = tipoUnidade_ws.PutTipoUnidade(tipoUnidade.id, tipoUnidade, usuario.Token);

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

        // GET: TipoUnidade/Edit/5
        public ActionResult ExcluirVerItem(int id)
        {
            TipoUnidadeWorkService tipoUnidade_ws = new TipoUnidadeWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);

            TelaTipoUnidade tela = new TelaTipoUnidade();
            tela = tipoUnidade_ws.GetTipoUnidade(id, usuario.Token);

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
        public ActionResult Excluir(TipoUnidadeModel tipoUnidade)
        {
            try
            {
                TipoUnidadeWorkService tipoUnidade_ws = new TipoUnidadeWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);
                var retorno = tipoUnidade_ws.DeleteTipoUnidade(tipoUnidade.id, usuario.Token);

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
