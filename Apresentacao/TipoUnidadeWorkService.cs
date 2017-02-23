using Newtonsoft.Json;
using OrganogramaApp.Apresentacao.Base;
using OrganogramaApp.Apresentacao.Models;
using System.Collections.Generic;
using System;
using OrganogramaApp.Apresentacao.Comum;

namespace OrganogramaApp.Apresentacao
{
    public class TipoUnidadeWorkService: WorkServiceBase, ITipoUnidadeWorkService
    {
        public TipoUnidadeWorkService(string urlBase) : base(urlBase)
        {
        }

        public List<TipoUnidadeModel> Listar(string accessToken)
        {
            List<TipoUnidadeModel> tiposUnidade = null;

            var url = urlOrganogramaApiBase + "tipos-unidade";

            var retornoAjaxModel = Get(url, accessToken);

            if (retornoAjaxModel.IsSuccessStatusCode)
                tiposUnidade = JsonConvert.DeserializeObject<List<TipoUnidadeModel>>(retornoAjaxModel.result);
            else
            {
                string conteudo = retornoAjaxModel.content.Replace("-------------------------------\n", "");
                throw new OrganogramaException(retornoAjaxModel.statusCode + ": " + conteudo);
            }

            return tiposUnidade;
        }

        public object GetTiposUnidade(string accessToken)
        {
            List<TipoUnidadeModel> tiposUnidade = new List<TipoUnidadeModel>();
            
            var url = urlOrganogramaApiBase + "tipos-unidade";
            var retorno_ws = Get(url, accessToken);

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

        public TelaTipoUnidade GetTipoUnidade(int id, string accessToken)
        {
            TipoUnidadeModel tipoUnidade = new TipoUnidadeModel();

            var url = urlOrganogramaApiBase + "tipos-unidade/" + id;
            var retorno_ws = Get(url, accessToken);

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

        public TelaTipoUnidade PutTipoUnidade(int id, TipoUnidadeModel tipoUnidade, string accessToken)
        {
            var url = urlOrganogramaApiBase + "tipos-unidade/" + id;
            var retorno_ws = Put(tipoUnidade, url, accessToken);         

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

        public TelaTipoUnidade PostTipoUnidade(TipoUnidadeModel tipoUnidade, string accessToken)
        {
            var url = urlOrganogramaApiBase + "tipos-unidade";
            var retorno_ws = Post(tipoUnidade, url, accessToken);

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
            var url = urlOrganogramaApiBase + "tipos-unidade/" + id;
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