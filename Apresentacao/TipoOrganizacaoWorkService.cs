using Newtonsoft.Json;
using OrganogramaApp.Apresentacao.Base;
using OrganogramaApp.Apresentacao.Models;
using System.Collections.Generic;

namespace OrganogramaApp.Apresentacao
{
    public class TipoOrganizacaoWorkService : WorkServiceBase, ITipoOrganizacaoWorkService
    {
        public TipoOrganizacaoWorkService(string urlOrganogramaApiBase) : base (urlOrganogramaApiBase)
        {
        }

        public object GetTiposOrganizacao(string accessToken)
        {
            List<TipoOrganizacaoModel> tiposOrganizacao = new List<TipoOrganizacaoModel>();

            var url = urlOrganogramaApiBase + "tipos-organizacao";
            var retorno_ws = Get(url, accessToken);

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

        public TelaTipoOrganizacao GetTipoOrganizacao(int id, string accessToken)
        {
            TipoOrganizacaoModel tipoOrganizacao = new TipoOrganizacaoModel();

            var url = urlOrganogramaApiBase + "tipos-organizacao/" + id;
            var retorno_ws = Get(url, accessToken);

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
            var url = urlOrganogramaApiBase + "tipos-organizacao/" + id;
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
            var url = urlOrganogramaApiBase + "tipos-organizacao";
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
            var url = urlOrganogramaApiBase + "tipos-organizacao/" + id;
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