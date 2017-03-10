using Newtonsoft.Json;
using OrganogramaApp.Apresentacao.Base;
using OrganogramaApp.Apresentacao.Comum;
using OrganogramaApp.Apresentacao.Models;
using OrganogramaApp.Apresentacao.ViewModel;
using OrganogramaApp.WebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static OrganogramaApp.Apresentacao.Models.Endereco;

namespace OrganogramaApp.WebApp.Controllers.Organizacao
{
    public class OrganizacaoController : BaseController
    {
        private IOrganizacaoWorkService workService;

        public OrganizacaoController (IOrganizacaoWorkService workService)
        {
            this.workService = workService;
        }

        // GET: Organizacao
        public ActionResult Index()
        {
            try
            {                
                return View(workService.Pesquisar(usuario.AccessToken));
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Erro, oe.Message);

                return View(new OrganizacaoViewModel());
            }
        }

        public ActionResult Consultar(string guid)
        {
            try
            {
                return PartialView(workService.Pesquisar(guid, usuario.AccessToken));
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Erro, oe.Message);

                return PartialView(new OrganizacaoVisualizacaoViewModel());
            }
        }

        public ActionResult Cadastrar(string guidOrganizacao)
        {
            try
            {   
                OrganizacaoInsercaoViewModel oivm = workService.Inserir(usuario.Organizacoes, usuario.AccessToken);

                oivm.Endereco = new EnderecoInsercaoViewModel();

                using (StreamReader r = new StreamReader(Server.MapPath("~/Json/uf.json")))
                {
                    string json = r.ReadToEnd();
                    var estados = JsonConvert.DeserializeObject<List<Estado>>(json);

                    oivm.Endereco.Estados = estados
                        .Select(e => new EstadoDropDownViewModel
                        {
                            Nome = e.Nome,
                            Sigla = e.Sigla
                        })
                        .OrderBy(e => e.Sigla)
                        .ToList();
                }
                oivm.Endereco.Municipios = new List<MunicipioDropDownViewModel>();

                return View(oivm);
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Erro, oe.Message);

                return View(new OrganizacaoInsercaoViewModel());
            }
        }

        public ActionResult IncluirCampoSite(int i)
        {
            ViewBag.i = i;
            return PartialView("_campoSite");
        }

        public ActionResult IncluirCampoEmail(int i)
        {
            ViewBag.i = i;
            return PartialView("_campoEmail");
        }

        public ActionResult IncluirCampoTelefone(int i)
        {
            var oivm = new OrganizacaoInsercaoViewModel();

            oivm.Contatos = new List<ContatoInsercaoViewModel>();
            oivm.Contatos.Add(new ContatoInsercaoViewModel());

            using (StreamReader r = new StreamReader(Server.MapPath("~/Json/tiposcontato.json")))
            {
                string json = r.ReadToEnd();
                List<TipoContato> tiposContato = JsonConvert.DeserializeObject<List<TipoContato>>(json);

                oivm.Contatos[0].TiposContato = tiposContato
                    .Select(tc => new TipoContatoDropDownViewModel
                    {
                        Id = tc.id,
                        Descricao = tc.descricao
                    })
                    .OrderBy(tc => tc.Descricao)
                    .ToList();
            }

            ViewBag.i = i;

            return PartialView("_campoTelefone", oivm);
        }

        public ActionResult IncluirPoderEsfera(string guid)
        {            
            return Json(workService.PesquisaPoderEsfera(guid, usuario.AccessToken), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(OrganizacaoInsercaoViewModel oivm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(oivm.Endereco.Bairro) &&
                    string.IsNullOrWhiteSpace(oivm.Endereco.Cep) &&
                    string.IsNullOrWhiteSpace(oivm.Endereco.Complemento) &&
                    string.IsNullOrWhiteSpace(oivm.Endereco.GuidMunicipio) &&
                    string.IsNullOrWhiteSpace(oivm.Endereco.Logradouro) &&
                    string.IsNullOrWhiteSpace(oivm.Endereco.Numero))
                {
                    oivm.Endereco = null;
                }

                oivm.Cnpj = oivm.Cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

                workService.Inserir(oivm, usuario.AccessToken);

                ModelState.Clear();
                AdicionarMensagem(TipoMensagem.Sucesso, "Organização cadastrada com sucesso!");
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Atencao, oe.Message);

                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                AdicionarMensagem(TipoMensagem.Erro, e.Message);

                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}