using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Controllers.TipoOrganizacao
{
    public class TipoOrganizacaoWorkService : WorkServiceBase
    {

        public object GetTiposOrganizacao(string token)
        {
            List<TipoOrganizacaoModel> tiposOrganizacao = new List<TipoOrganizacaoModel>();

            var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "tipos-organizacao";
            var retorno_ws = Get(url, token);

            if (retorno_ws.IsSuccessStatusCode)
            {
                tiposOrganizacao = JsonConvert.DeserializeObject<List<TipoOrganizacaoModel>>(retorno_ws.result);
            }

            RetornoAjaxModel retorno = new RetornoAjaxModel()
            {
                IsSuccessStatusCode = retorno_ws.IsSuccessStatusCode,
                content = retorno_ws.content,
                statusCode = retorno_ws.statusCode
            };

            return new TelaTipoOrganizacao
            {
                tiposOrganizacao = tiposOrganizacao,
                retornoAjax = retorno
            };
        }

        public TelaTipoOrganizacao GetTipoOrganizacao(int id, string token)
        {
            TipoOrganizacaoModel tipoOrganizacao = new TipoOrganizacaoModel();

            var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "tipos-organizacao/" + id;
            var retorno_ws = Get(url, token);

            if (retorno_ws.IsSuccessStatusCode)
            {
                tipoOrganizacao = JsonConvert.DeserializeObject<TipoOrganizacaoModel>(retorno_ws.result);
            }

            RetornoAjaxModel retorno = new RetornoAjaxModel()
            {
                IsSuccessStatusCode = retorno_ws.IsSuccessStatusCode,
                content = retorno_ws.content,
                statusCode = retorno_ws.statusCode
            };

            return new TelaTipoOrganizacao
            {
                tipoOrganizacao = tipoOrganizacao,
                retornoAjax = retorno
            };
        }

        public TelaTipoOrganizacao PutTipoOrganizacao(int id, TipoOrganizacaoModel tipoOrganizacao, string token)
        {
            var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "tipos-organizacao/" + id;
            var retorno_ws = Put(tipoOrganizacao, url, token);

            RetornoAjaxModel retorno = new RetornoAjaxModel()
            {
                IsSuccessStatusCode = retorno_ws.IsSuccessStatusCode,
                content = retorno_ws.content,
                statusCode = retorno_ws.statusCode
            };

            return new TelaTipoOrganizacao
            {
                retornoAjax = retorno
            };
        }

        public TelaTipoOrganizacao PostTipoOrganizacao(TipoOrganizacaoModel tipoOrganizacao, string token)
        {
            var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "tipos-organizacao";
            var retorno_ws = Post(tipoOrganizacao, url, token);

            RetornoAjaxModel retorno = new RetornoAjaxModel()
            {
                IsSuccessStatusCode = retorno_ws.IsSuccessStatusCode,
                content = retorno_ws.content,
                statusCode = retorno_ws.statusCode
            };

            return new TelaTipoOrganizacao
            {
                retornoAjax = retorno
            };
        }

        public TelaTipoOrganizacao DeleteTipoOrganizacao(int id, string token)
        {
            var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "tipos-organizacao/" + id;
            var retorno_ws = Delete(url, token);

            RetornoAjaxModel retorno = new RetornoAjaxModel()
            {
                IsSuccessStatusCode = retorno_ws.IsSuccessStatusCode,
                content = retorno_ws.content,
                statusCode = retorno_ws.statusCode
            };

            return new TelaTipoOrganizacao
            {
                retornoAjax = retorno
            };
        }

    }
}