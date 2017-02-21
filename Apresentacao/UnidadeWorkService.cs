using Newtonsoft.Json;
using OrganogramaApp.Apresentacao.Base;
using OrganogramaApp.Apresentacao.Comum;
using OrganogramaApp.Apresentacao.Models;
using OrganogramaApp.Apresentacao.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace OrganogramaApp.Apresentacao
{
    public class UnidadeWorkService : WorkServiceBase, IUnidadeWorkService
    {
        public UnidadeWorkService(string urlBase) : base(urlBase)
        {
        }

        public UnidadeViewModel Pesquisar(List<OrganizacaoModel> organizacoes, string token)
        {
            UnidadeViewModel unidadeViewModel = new UnidadeViewModel();
            unidadeViewModel.Organizacoes = organizacoes.Select(o => new OrganizacaoDropDownList
            {
                Guid = o.guid,
                Nome = o.razaoSocial,
                Sigla = o.sigla
            })
            .ToList();

            if (unidadeViewModel.Organizacoes != null && unidadeViewModel.Organizacoes.Count == 1)
            {
                string guidOrganizacao = organizacoes[0].guid;

                unidadeViewModel.GuidOrganizacao = guidOrganizacao;

                unidadeViewModel.Unidades = PesquisarPorOrganizacao(guidOrganizacao, token);
            }

            return unidadeViewModel;
        }

        public List<UnidadeListagemViewModel> Pesquisar(string guidOrganizacao, string token)
        {
            List<UnidadeListagemViewModel> unidades = null;

            if (!string.IsNullOrWhiteSpace(guidOrganizacao))
                unidades = PesquisarPorOrganizacao(guidOrganizacao, token);

            return unidades;
        }

        private List<UnidadeListagemViewModel> PesquisarPorOrganizacao(string guidOrganizacao, string token)
        {
            List<UnidadeListagemViewModel> unidades = null;

            string url = urlOrganogramaApiBase + "unidades/organizacao/" + guidOrganizacao;

            RetornoAjaxModel retornoAjaxModel = Get(url, token);

            if (retornoAjaxModel.IsSuccessStatusCode)
            {
                List<UnidadeGetModel> ugm = JsonConvert.DeserializeObject<List<UnidadeGetModel>>(retornoAjaxModel.result);
                unidades = ugm.Select(u => new UnidadeListagemViewModel
                {
                    Guid = u.guid,
                    Nome = u.nome,
                    Sigla = u.sigla,
                    Tipo = u.tipoUnidade.descricao,
                    UnidadePai = u.unidadePai != null ? u.unidadePai.nome : ""
                })
                .ToList();
            }
            else
            {
                string conteudo = retornoAjaxModel.content.Replace("-------------------------------\n", "");
                throw new OrganogramaException(retornoAjaxModel.statusCode + ": " + conteudo);
            }

            return unidades;
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

        public List<OrganizacaoModel> GeOrganizacoesPorPatriarca(string guidPatriarca, string token)
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
    }
}