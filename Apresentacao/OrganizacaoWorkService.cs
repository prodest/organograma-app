using OrganogramaApp.Apresentacao.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganogramaApp.Apresentacao.Models;
using Newtonsoft.Json;
using OrganogramaApp.Apresentacao.Comum;

namespace OrganogramaApp.Apresentacao
{
    public class OrganizacaoWorkService : WorkServiceBase, IOrganizacaoWorkService
    {
        public OrganizacaoWorkService(string urlBaseurlOrganogramaApiBase) : base(urlBaseurlOrganogramaApiBase)
        {
        }

        public OrganizacaoModel Pesquisar(string guid, string accessToken)
        {
            OrganizacaoModel organizacao = null;

            string url = urlOrganogramaApiBase + "organizacoes/" + guid;

            RetornoAjaxModel retornoAjaxModel = Get(url, accessToken);

            if (retornoAjaxModel.IsSuccessStatusCode)
                organizacao = JsonConvert.DeserializeObject<OrganizacaoModel>(retornoAjaxModel.result);
            else
            {
                string conteudo = retornoAjaxModel.content.Replace("-------------------------------\n", "");
                throw new OrganogramaException(retornoAjaxModel.statusCode + ": " + conteudo);
            }

            return organizacao;
        }
    }
}
