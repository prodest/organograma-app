using Newtonsoft.Json;
using OrganogramaApp.Apresentacao.Base;
using OrganogramaApp.Apresentacao.Models;
using OrganogramaApp.WebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using static OrganogramaApp.Apresentacao.Models.Endereco;

namespace OrganogramaApp.WebApp.Controllers.Unidade
{
    public class UnidadeController : BaseController
    {
        private IUnidadeWorkService workService;

        public UnidadeController(IUnidadeWorkService workService)
        {
            this.workService = workService;
        }

        public ActionResult Index()
        {
            ViewBag.NomeOrgao = usuario.Orgao.nomeFantasia;
            return View(workService.GetUnidades(usuario.Orgao.guid, usuario.Token));
        }

        public ActionResult Visualizar(string guid)
        {
            UnidadeGetModel unidade = new UnidadeGetModel();

            var retorno = workService.GetUnidade(guid, usuario.Token);

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

        public ActionResult Cadastrar()
        {
            TelaUnidadeModel tela = new TelaUnidadeModel();

            tela.organizacao = workService.GeOrganizacoesPorPatriarca(usuario.Patriarca.guid, usuario.Token);
            tela.tipoUnidade = workService.GetTiposUnidade(usuario.Token);
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

        public ActionResult CadastrarEndereco()
        {
            return PartialView("CadastrarEndereco");
        }

        [HttpPost]
        public ActionResult Create(TelaUnidadeModel tela)
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

                var retorno = workService.PostUnidade(unidadePost, usuario.Token);

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

        public ActionResult Edit(int id)
        {
            return View();
        }

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

        public ActionResult Delete(int id)
        {
            return View();
        }

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
