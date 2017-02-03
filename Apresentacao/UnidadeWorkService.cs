using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ProcessoEletronicoWebApp.Apresentacao.Models;
using ProcessoEletronicoWebApp.Apresentacao.Base;

namespace ProcessoEletronicoWebApp.Apresentacao.Unidade
{
    public class UnidadeWorkService: WorkServiceBase
    {
        public UnidadeWorkService(string urlBase) : base(urlBase)
        {
        }

        public List<UnidadeGetModel> GetUnidades(string guidOrganizacao, string token)
        {
            List<UnidadeGetModel> unidades = new List<UnidadeGetModel>();
            //...
            var url = urlBase + "unidades/organizacao/" + guidOrganizacao;
            var retorno_ws = Get(url, token);

            if (retorno_ws.IsSuccessStatusCode)
            {
                unidades = JsonConvert.DeserializeObject<List<UnidadeGetModel>>(retorno_ws.result);
            }

            RetornoAjaxModel retorno = new RetornoAjaxModel()
            {
                IsSuccessStatusCode = retorno_ws.IsSuccessStatusCode,
                content = retorno_ws.content,
                statusCode = retorno_ws.statusCode
            };
            //...
            return unidades;
        }

        public RetornoAjaxModel GetUnidade(string guidOrganizacao, string token)
        {
            var url = urlBase + "unidades/" + guidOrganizacao;
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
            var url = urlBase + "organizacoes/" + guidPatriarca + "/filhas";
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

            var url = urlBase + "tipos-unidade";
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
            var url = urlBase + "unidades";
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