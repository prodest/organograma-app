using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Controllers.Unidade
{
    public class UnidadeWorkService: WorkServiceBase
    {
        public List<UnidadeGetModel> GetUnidades(string guidOrganizacao, string token)
        {
            List<UnidadeGetModel> unidades = new List<UnidadeGetModel>();
            //...
            var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "unidades/organizacao/" + guidOrganizacao;
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
            var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "unidades/" + guidOrganizacao;
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
            var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "organizacoes/" + guidPatriarca + "/filhas";
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

            var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "tipos-unidade";
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
            var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "unidades";
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