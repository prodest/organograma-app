using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Controllers.TipoUnidade
{
    public class TipoUnidadeWorkService: WorkServiceBase
    {
        public object GetTiposUnidade(string token)
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

            return new TelaTipoUnidade {
                tiposUnidade = tiposUnidade,
                retornoAjax = retorno
            };            
        }

        public TelaTipoUnidade GetTipoUnidade(int id, string token)
        {
            TipoUnidadeModel tipoUnidade = new TipoUnidadeModel();

            var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "tipos-unidade/" + id;
            var retorno_ws = Get(url, token);

            if (retorno_ws.IsSuccessStatusCode)
            {
                tipoUnidade = JsonConvert.DeserializeObject<TipoUnidadeModel>(retorno_ws.result);
            }

            RetornoAjaxModel retorno = new RetornoAjaxModel()
            {
                IsSuccessStatusCode = retorno_ws.IsSuccessStatusCode,
                content = retorno_ws.content,
                statusCode = retorno_ws.statusCode
            };

            return new TelaTipoUnidade
            {
                tipoUnidade = tipoUnidade,
                retornoAjax = retorno
            };
        }

        public TelaTipoUnidade PutTipoUnidade(int id, TipoUnidadeModel tipoUnidade, string token)
        {
            var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "tipos-unidade/" + id;
            var retorno_ws = Put(tipoUnidade, url, token);         

            RetornoAjaxModel retorno = new RetornoAjaxModel()
            {
                IsSuccessStatusCode = retorno_ws.IsSuccessStatusCode,
                content = retorno_ws.content,
                statusCode = retorno_ws.statusCode
            };

            return new TelaTipoUnidade
            {            
                retornoAjax = retorno
            };
        }

        public TelaTipoUnidade PostTipoUnidade(TipoUnidadeModel tipoUnidade, string token)
        {
            var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "tipos-unidade";
            var retorno_ws = Post(tipoUnidade, url, token);

            RetornoAjaxModel retorno = new RetornoAjaxModel()
            {
                IsSuccessStatusCode = retorno_ws.IsSuccessStatusCode,
                content = retorno_ws.content,
                statusCode = retorno_ws.statusCode
            };

            return new TelaTipoUnidade
            {
                retornoAjax = retorno
            };
        }

        public TelaTipoUnidade DeleteTipoUnidade(int id, string token)
        {
            var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "tipos-unidade/" + id;
            var retorno_ws = Delete(url, token);

            RetornoAjaxModel retorno = new RetornoAjaxModel()
            {
                IsSuccessStatusCode = retorno_ws.IsSuccessStatusCode,
                content = retorno_ws.content,
                statusCode = retorno_ws.statusCode
            };

            return new TelaTipoUnidade
            {
                retornoAjax = retorno
            };
        }
    }
}