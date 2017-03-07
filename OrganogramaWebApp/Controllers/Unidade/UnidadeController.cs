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

        public ActionResult Index(string guidOrganizacao)
        {
            try
            {
                var uvm = workService.Pesquisar(usuario.Organizacoes, guidOrganizacao, usuario.AccessToken);

                if (uvm.Unidades != null && uvm.Unidades.Count > 0)
                    ViewBag.GuidOrganizacao = uvm.GuidOrganizacao;

                return View(uvm);
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Erro, oe.Message);

                return View(new UnidadeViewModel());
            }
        }

        public ActionResult Consultar(UnidadeViewModel unidadeViewModel)
        {
            try
            {
                ViewBag.GuidOrganizacao = unidadeViewModel.GuidOrganizacao;
                return PartialView("Listagem", workService.Pesquisar(unidadeViewModel.GuidOrganizacao, usuario.AccessToken));
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Erro, oe.Message);

                return View(new UnidadeViewModel());
            }
        }

        public ActionResult Visualizar(string guid)
        {
            UnidadeConsultarViewModel unidade = null;
            try
            {
                unidade = workService.Consultar(guid, usuario.AccessToken);

                return PartialView("Visualizar", unidade);
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Atencao, oe.Message);

                return Json(unidade, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                AdicionarMensagem(TipoMensagem.Erro, e.Message);

                return Json(unidade, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Cadastrar(string guidOrganizacao)
        {
            try
            {
                var uivm = workService.Inserir(guidOrganizacao, usuario.AccessToken);

                uivm.Endereco = new EnderecoInsercaoViewModel();

                using (StreamReader r = new StreamReader(Server.MapPath("~/Json/uf.json")))
                {
                    string json = r.ReadToEnd();
                    var estados = JsonConvert.DeserializeObject<List<Estado>>(json);

                    uivm.Endereco.Estados = estados
                        .Select(e => new EstadoDropDownViewModel
                        {
                            Nome = e.Nome,
                            Sigla = e.Sigla
                        })
                        .OrderBy(e => e.Sigla)
                        .ToList();
                }
                uivm.Endereco.Municipios = new List<MunicipioDropDownViewModel>();

                return View(uivm);
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Erro, oe.Message);

                return View(new UnidadeViewModel());
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
            var uivm = new UnidadeInsercaoViewModel();

            uivm.Contatos = new List<ContatoInsercaoViewModel>();
            uivm.Contatos.Add(new ContatoInsercaoViewModel());

            using (StreamReader r = new StreamReader(Server.MapPath("~/Json/tiposcontato.json")))
            {
                string json = r.ReadToEnd();
                List<TipoContato> tiposContato = JsonConvert.DeserializeObject<List<TipoContato>>(json);

                uivm.Contatos[0].TiposContato = tiposContato
                    .Select(tc => new TipoContatoDropDownViewModel
                    {
                        Id = tc.id,
                        Descricao = tc.descricao
                    })
                    .OrderBy(tc => tc.Descricao)
                    .ToList();
            }

            ViewBag.i = i;

            return PartialView("_campoTelefone", uivm);
        }

        [HttpPost]
        public ActionResult Create(UnidadeInsercaoViewModel uivm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uivm.Endereco.Bairro) &&
                    string.IsNullOrWhiteSpace(uivm.Endereco.Cep) &&
                    string.IsNullOrWhiteSpace(uivm.Endereco.Complemento) &&
                    string.IsNullOrWhiteSpace(uivm.Endereco.GuidMunicipio) &&
                    string.IsNullOrWhiteSpace(uivm.Endereco.Logradouro) &&
                    string.IsNullOrWhiteSpace(uivm.Endereco.Numero))
                {
                    uivm.Endereco = null;
                }

                workService.Inserir(uivm, usuario.AccessToken);

                ModelState.Clear();
                AdicionarMensagem(TipoMensagem.Sucesso, "Unidade cadastrada com sucesso!");
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

        public ActionResult Editar(string guid)
        {
            UnidadeEditarViewModel unidade = null;
            try
            {
                unidade = workService.ConsultarEdicao(guid, usuario.AccessToken);

                return PartialView("Editar", unidade);
            }
            catch (OrganogramaException oe)
            {
                AdicionarMensagem(TipoMensagem.Atencao, oe.Message);

                return Json(unidade, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                AdicionarMensagem(TipoMensagem.Erro, e.Message);

                return Json(unidade, JsonRequestBehavior.AllowGet);
            }
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

        public ActionResult Excluir(string guid)
        {
            try
            {
                UnidadeConsultarViewModel unidade = workService.Consultar(guid, usuario.AccessToken);

                return PartialView("Excluir", unidade);
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

        [HttpPost]
        public ActionResult Delete(string guid)
        {
            try
            {
                workService.Excluir(guid, usuario.AccessToken);

                AdicionarMensagem(TipoMensagem.Sucesso, "Unidade excluída com sucesso!");
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
