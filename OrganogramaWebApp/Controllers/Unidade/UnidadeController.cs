using Newtonsoft.Json;
using OrganogramaApp.WebApp.Models;
using ProcessoEletronicoWebApp.Apresentacao.Models;
using ProcessoEletronicoWebApp.Apresentacao.Unidade;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using static ProcessoEletronicoWebApp.Apresentacao.Models.Endereco;

namespace OrganogramaApp.WebApp.Controllers.Unidade
{
    public class UnidadeController : BaseController
    {
        // GET: Unidade
        public ActionResult Index()
        {
            UnidadeWorkService unidade_ws = new UnidadeWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);
            ViewBag.NomeOrgao = usuario.Orgao.nomeFantasia;
            return View(unidade_ws.GetUnidades(usuario.Orgao.guid, usuario.Token));
        }

        // GET: Unidade/Details/5
        public ActionResult Visualizar(string guid)
        {
            UnidadeWorkService unidade_ws = new UnidadeWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);
            UnidadeGetModel unidade = new UnidadeGetModel();

            var retorno = unidade_ws.GetUnidade(guid, usuario.Token);

            if (retorno.IsSuccessStatusCode)
            {
                unidade = JsonConvert.DeserializeObject<UnidadeGetModel>(retorno.content);                
            }
            else
            {
                AdicionarMensagem(TipoMensagem.Atencao, retorno.content);
            }

            return PartialView("Visualizar", unidade);
        }

        // GET: Unidade/Create
        public ActionResult Cadastrar()
        {
            TelaUnidadeModel tela = new TelaUnidadeModel();
            UnidadeWorkService unidade_ws = new UnidadeWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);

            tela.organizacao = unidade_ws.GeOrganizacoesPorPatriarca(usuario.Patriarca.guid, usuario.Token);
            tela.tipoUnidade = unidade_ws.GetTiposUnidade(usuario.Token);
            tela.unidadePai = new List<UnidadeGetModel>();

            tela.endereco = new Endereco();
            tela.endereco.municipios = new List<Municipio>();

            using (StreamReader r = new StreamReader(Server.MapPath("~/Json/uf.json")))
            {
                string json = r.ReadToEnd();
                tela.endereco.estados = JsonConvert.DeserializeObject<List<Estado>>(json);
            }
                        
            tela.listaTiposContato = new List<TipoContato>();
            using (StreamReader r = new StreamReader(Server.MapPath("~/Json/tiposcontato.json")))
            {
                string json = r.ReadToEnd();
                tela.listaTiposContato = JsonConvert.DeserializeObject<List<TipoContato>>(json);
            }

            return View(tela);
        }        

        public ActionResult IncluirCampoSite(int i)
        {
            ViewBag.i = ++i;
            return PartialView("_campoSite");
        }

        public ActionResult IncluirCampoEmail(int i)
        {
            ViewBag.i = ++i;
            return PartialView("_campoEmail");
        }

        public ActionResult IncluirCampoTelefone(int i)
        {
            TelaUnidadeModel tela = new TelaUnidadeModel();
            tela.listaTiposContato = new List<TipoContato>();
            using (StreamReader r = new StreamReader(Server.MapPath("~/Json/tiposcontato.json")))
            {
                string json = r.ReadToEnd();
                tela.listaTiposContato = JsonConvert.DeserializeObject<List<TipoContato>>(json);
            }

            ViewBag.i = ++i;

            return PartialView("_campoTelefone", tela);
        }

        // GET: Unidade/CadastrarEndereco
        public ActionResult CadastrarEndereco()
        {
            return PartialView("CadastrarEndereco");
        }

        // POST: Unidade/Create
        [HttpPost]
        public ActionResult Create(TelaUnidadeModel tela)
        //public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                UnidadePostModel unidadePost = new UnidadePostModel();

                unidadePost.contatos = new List<Contato>();
                unidadePost.emails = new List<Email>();
                unidadePost.sites = new List<Site>();
                
                foreach (var contato in tela.contatos)
                {
                    if (!string.IsNullOrEmpty(contato.telefone))
                    {
                        unidadePost.contatos.Add(contato);
                    }
                }

                foreach (var email in tela.emails)
                {
                    if (!string.IsNullOrEmpty(email.endereco))
                    {
                        unidadePost.emails.Add(email);
                    }
                }

                foreach (var site in tela.sites)
                {
                    if (!string.IsNullOrEmpty(site.url))
                    {
                        unidadePost.sites.Add(site);
                    }
                }

                //unidadePost.endereco = tela.endereco;
                unidadePost.idOrganizacao = tela.idOrganizacao;
                unidadePost.idTipoUnidade = tela.idTipoUnidade;
                unidadePost.idUnidadePai = tela.idUnidadePai;
                unidadePost.nome = tela.nome;
                unidadePost.sigla = tela.sigla;                

                UnidadeWorkService unidade_ws = new UnidadeWorkService(ConfigurationManager.AppSettings["OrganogramaAPIBase"]);
                var retorno = unidade_ws.PostUnidade(unidadePost, usuario.Token);

                if (retorno.IsSuccessStatusCode)
                {
                    ModelState.Clear();
                    AdicionarMensagem(TipoMensagem.Sucesso, "Unidade cadastrada com sucesso!");
                    return Json(retorno, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    AdicionarMensagem(TipoMensagem.Atencao, retorno.content);
                    return Json(retorno, JsonRequestBehavior.AllowGet);
                }                                
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: Unidade/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Unidade/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Unidade/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Unidade/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
