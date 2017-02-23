using Newtonsoft.Json;
using OrganogramaApp.Apresentacao.Base;
using OrganogramaApp.Apresentacao.Comum;
using OrganogramaApp.Apresentacao.Models;
using OrganogramaApp.Apresentacao.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System;
using AutoMapper;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao
{
    public class UnidadeWorkService : WorkServiceBase, IUnidadeWorkService
    {
        private IOrganizacaoWorkService organizacaoWS;
        private ITipoUnidadeWorkService tipoUnidadeWS;

        public UnidadeWorkService(string urlBaseurlOrganogramaApiBase) : base(urlBaseurlOrganogramaApiBase)
        {
            this.organizacaoWS = new OrganizacaoWorkService(urlBaseurlOrganogramaApiBase);
            this.tipoUnidadeWS = new TipoUnidadeWorkService(urlBaseurlOrganogramaApiBase);
        }

        public UnidadeViewModel Pesquisar(List<OrganizacaoModel> organizacoes, string guidOrganizacao, string token)
        {
            UnidadeViewModel unidadeViewModel = new UnidadeViewModel();
            unidadeViewModel.Organizacoes = organizacoes.Select(o => new OrganizacaoDropDownList
            {
                Guid = o.guid,
                Nome = o.razaoSocial,
                Sigla = o.sigla
            })
            .ToList();

            if (!string.IsNullOrWhiteSpace(guidOrganizacao) && unidadeViewModel.Organizacoes.Where(o => o.Guid.Equals(guidOrganizacao)).SingleOrDefault() != null)
            {
                unidadeViewModel.GuidOrganizacao = guidOrganizacao;

                unidadeViewModel.Unidades = ListarPorOrganizacao(guidOrganizacao, token);
            }
            else if(unidadeViewModel.Organizacoes != null && unidadeViewModel.Organizacoes.Count == 1)
            {
                guidOrganizacao =  organizacoes[0].guid;

                unidadeViewModel.GuidOrganizacao = guidOrganizacao;

                unidadeViewModel.Unidades = ListarPorOrganizacao(guidOrganizacao, token);
            }

            return unidadeViewModel;
        }

        public List<UnidadeListagemViewModel> Pesquisar(string guidOrganizacao, string token)
        {
            List<UnidadeListagemViewModel> unidades = null;

            if (!string.IsNullOrWhiteSpace(guidOrganizacao))
                unidades = ListarPorOrganizacao(guidOrganizacao, token);

            return unidades;
        }

        public UnidadeInsercaoViewModel Inserir(string guidOrganizacao, string accessToken)
        {
            UnidadeInsercaoViewModel unidadeIVM = new UnidadeInsercaoViewModel();

            OrganizacaoModel organizacao = organizacaoWS.Pesquisar(guidOrganizacao, accessToken);
            unidadeIVM.GuidOrganizacao = organizacao.guid;
            unidadeIVM.SiglaNomeOrganizacao = organizacao.sigla + " - " + organizacao.razaoSocial;

            unidadeIVM.TiposUnidade = tipoUnidadeWS.Listar(accessToken)
                                                   .Select(tu => new TipoUnidadeDropDownViewModel
                                                   {
                                                       Id = tu.id,
                                                       Descricao = tu.descricao
                                                   })
                                                   .OrderBy(tu => tu.Descricao)
                                                   .ToList();

            unidadeIVM.UnidadesPai = PesquisarPorOrganizacao(guidOrganizacao, accessToken)
                .Select(u => new UnidadeDropDownViewModel
                {
                    Guid = u.guid,
                    Nome = u.nome,
                    Sigla = u.sigla
                })
                .OrderBy(u => u.Nome)
                .ToList();

            return unidadeIVM;
        }

        public void Inserir(UnidadeInsercaoViewModel unidadeIVM, string accessToken)
        {
            UnidadePostModel unidade = Mapper.Map<UnidadeInsercaoViewModel, UnidadePostModel>(unidadeIVM);

            string a = unidade.guidOrganizacao;

            var url = urlOrganogramaApiBase + "unidades";

            RetornoAjaxModel retornoAjaxModel = Post(unidade, url, accessToken);

            if (!retornoAjaxModel.IsSuccessStatusCode)
            {
                string conteudo = retornoAjaxModel.content.Replace("-------------------------------\n", "");
                throw new OrganogramaException(retornoAjaxModel.statusCode + ": " + conteudo);
            }
        }

        public UnidadeConsultarViewModel Consultar(string guid, string accessToken)
        {
            var url = urlOrganogramaApiBase + "unidades/" + guid;
            UnidadeConsultarViewModel unidade = null;

            RetornoAjaxModel retornoAjaxModel = Get(url, accessToken);

            if (retornoAjaxModel.IsSuccessStatusCode)
                unidade = JsonConvert.DeserializeObject<UnidadeConsultarViewModel>(retornoAjaxModel.result);
            else
            {
                string conteudo = retornoAjaxModel.content.Replace("-------------------------------\n", "");
                throw new OrganogramaException(retornoAjaxModel.statusCode + ": " + conteudo);
            }

            return unidade;
        }

        public RetornoAjaxModel GetUnidade(string guidOrganizacao, string token)
        {
            var url = urlOrganogramaApiBase + "unidades/" + guidOrganizacao;
            var retorno_ws = Get(url, token);

            RetornoAjaxModel retorno = new RetornoAjaxModel()
            {
                IsSuccessStatusCode = retorno_ws.IsSuccessStatusCode,
                content = retorno_ws.content,
                statusCode = retorno_ws.statusCode
            };

            return retorno;
        }

        public List<OrganizacaoModel> GetOrganizacoesPorPatriarca(string guidPatriarca, string token)
        {
            List<OrganizacaoModel> organizacoes = new List<OrganizacaoModel>();
            //...
            var url = urlOrganogramaApiBase + "organizacoes/" + guidPatriarca + "/filhas";
            var retorno_ws = Get(url, token);

            if (retorno_ws.IsSuccessStatusCode)
            {
                organizacoes = JsonConvert.DeserializeObject<List<OrganizacaoModel>>(retorno_ws.result);
            }

            RetornoAjaxModel retorno = new RetornoAjaxModel()
            {
                IsSuccessStatusCode = retorno_ws.IsSuccessStatusCode,
                content = retorno_ws.content,
                statusCode = retorno_ws.statusCode
            };
            //...
            return organizacoes;
        }

        public List<TipoUnidadeModel> GetTiposUnidade(string token)
        {
            List<TipoUnidadeModel> tiposUnidade = new List<TipoUnidadeModel>();

            var url = urlOrganogramaApiBase + "tipos-unidade";
            var retorno_ws = Get(url, token);

            if (retorno_ws.IsSuccessStatusCode)
            {
                tiposUnidade = JsonConvert.DeserializeObject<List<TipoUnidadeModel>>(retorno_ws.result);
            }

            RetornoAjaxModel retorno = new RetornoAjaxModel()
            {
                IsSuccessStatusCode = retorno_ws.IsSuccessStatusCode,
                content = retorno_ws.content,
                statusCode = retorno_ws.statusCode
            };

            return tiposUnidade;
        }

        public RetornoAjaxModel PostUnidade(UnidadePostModel unidade, string token)
        {
            var url = urlOrganogramaApiBase + "unidades";
            var retorno_ws = Post(unidade, url, token);

            RetornoAjaxModel retorno = new RetornoAjaxModel()
            {
                IsSuccessStatusCode = retorno_ws.IsSuccessStatusCode,
                content = retorno_ws.content,
                statusCode = retorno_ws.statusCode
            };
            return retorno;
        }

        private List<UnidadeListagemViewModel> ListarPorOrganizacao(string guidOrganizacao, string accessToken)
        {
            List<UnidadeListagemViewModel> unidades = null;

            List<UnidadeGetModel> ugm = PesquisarPorOrganizacao(guidOrganizacao, accessToken);

            if (ugm != null)
                unidades = ugm.Select(u => new UnidadeListagemViewModel
                {
                    Guid = u.guid,
                    Nome = u.nome,
                    Sigla = u.sigla,
                    Tipo = u.tipoUnidade.descricao,
                    UnidadePai = u.unidadePai != null ? u.unidadePai.nome : ""
                })
                .OrderBy(u => u.Nome)
                .ToList();

            return unidades;
        }

        private List<UnidadeGetModel> PesquisarPorOrganizacao(string guidOrganizacao, string accessToken)
        {
            List<UnidadeGetModel> unidades = null;

            string url = urlOrganogramaApiBase + "unidades/organizacao/" + guidOrganizacao;

            RetornoAjaxModel retornoAjaxModel = Get(url, accessToken);

            if (retornoAjaxModel.IsSuccessStatusCode)
                unidades = JsonConvert.DeserializeObject<List<UnidadeGetModel>>(retornoAjaxModel.result);
            else
            {
                string conteudo = retornoAjaxModel.content.Replace("-------------------------------\n", "");
                throw new OrganogramaException(retornoAjaxModel.statusCode + ": " + conteudo);
            }

            return unidades;
        }

        public void Excluir(string guid, string accessToken)
        {
            string url = urlOrganogramaApiBase + "unidades/" + guid;

            RetornoAjaxModel retornoAjaxModel = Delete(url, accessToken);

            if (!retornoAjaxModel.IsSuccessStatusCode)
            {
                string conteudo = retornoAjaxModel.content.Replace("-------------------------------\n", "");
                throw new OrganogramaException(retornoAjaxModel.statusCode + ": " + conteudo);
            }
        }
    }
}