using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using static WebApp.Models.Endereco;

namespace WebApp.Controllers.Unidade
{
    public class UnidadeController : BaseController
    {
        // GET: Unidade
        public ActionResult Index()
        {
            UnidadeWorkService unidade_ws = new UnidadeWorkService();
            return View(unidade_ws.GetUnidades(usuario.Orgao.guid, usuario.Token));
        }

        // GET: Unidade/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Unidade/Create
        public ActionResult Cadastrar()
        {
            TelaUnidadeModel tela = new TelaUnidadeModel();
            UnidadeWorkService unidade_ws = new UnidadeWorkService();            

            tela.organizacao = unidade_ws.GeOrganizacoesPorPatriarca(usuario.Patriarca.guid, usuario.Token);
            tela.tipoUnidade = unidade_ws.GetTiposUnidade(usuario.Token);
            tela.unidadePai = unidade_ws.GetUnidades(usuario.Orgao.guid, usuario.Token);

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
            ViewBag.count = i++;
            return PartialView("_campoSite");
        }

        public ActionResult IncluirCampoEmail()
        {
            return PartialView("_campoEmail");
        }

        public ActionResult IncluirCampoTelefone()
        {
            TelaUnidadeModel tela = new TelaUnidadeModel();
            tela.listaTiposContato = new List<TipoContato>();
            using (StreamReader r = new StreamReader(Server.MapPath("~/Json/tiposcontato.json")))
            {
                string json = r.ReadToEnd();
                tela.listaTiposContato = JsonConvert.DeserializeObject<List<TipoContato>>(json);
            }

            ViewBag.i = 10;

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
                unidadePost.contatos = tela.contatos;
                unidadePost.emails = tela.emails;
                //unidadePost.endereco = tela.endereco;
                unidadePost.idOrganizacao = tela.idOrganizacao;
                unidadePost.idTipoUnidade = tela.idTipoUnidade;
                unidadePost.idUnidadePai = tela.idUnidadePai;
                unidadePost.nome = tela.nome;
                unidadePost.sigla = tela.sigla;
                unidadePost.sites = tela.sites;

                UnidadeWorkService unidade_ws = new UnidadeWorkService();
                var retorno = unidade_ws.PostUnidade(unidadePost, usuario.Token);

                if (retorno.IsSuccessStatusCode)
                {
                    ModelState.Clear();
                    AdicionarMensagem(TipoMensagem.Sucesso, "Unidade cadastrada com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    AdicionarMensagem(TipoMensagem.Atencao, retorno.content);
                    return View();
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
