using AutoMapper;
using Newtonsoft.Json;
using OrganogramaApp.Apresentacao.Base;
using OrganogramaApp.Apresentacao.Comum;
using OrganogramaApp.Apresentacao.Models;
using OrganogramaApp.Apresentacao.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao
{
    public class OrganogramaWorkService: WorkServiceBase, IOrganogramaWorkService
    {
        public OrganogramaWorkService(string urlBaseurlOrganogramaApiBase) : base(urlBaseurlOrganogramaApiBase)
        {            
        }

        public ChartViewModel Pesquisar(string guid, string accessToken)
        {
            ChartViewModel organograma = null;

            string url = urlOrganogramaApiBase + "organizacoes/organograma/" + guid;

            RetornoAjaxModel retornoAjaxModel = Get(url, accessToken);

            if (retornoAjaxModel.IsSuccessStatusCode)
            {
                OrganogramaModel organogramamd = JsonConvert.DeserializeObject<OrganogramaModel>(retornoAjaxModel.result);

                organograma = Mapper.Map<OrganogramaModel, ChartViewModel>(organogramamd);
            }
                
            else
            {
                string conteudo = retornoAjaxModel.content.Replace("-------------------------------\n", "");
                throw new OrganogramaException(retornoAjaxModel.statusCode + ": " + conteudo);
            }

            return organograma;
        }

        //public OrganogramaViewModel Pesquisar(string guid, string accessToken)
        //{
        //    OrganogramaViewModel organograma = null;

        //    string url = urlOrganogramaApiBase + "organizacoes/organograma/" + guid;

        //    RetornoAjaxModel retornoAjaxModel = Get(url, accessToken);

        //    if (retornoAjaxModel.IsSuccessStatusCode)
        //    {
        //        OrganogramaModel organogramamd = JsonConvert.DeserializeObject<OrganogramaModel>(retornoAjaxModel.result);

        //        organograma = Mapper.Map<OrganogramaModel, OrganogramaViewModel>(organogramamd);
        //    }

        //    else
        //    {
        //        string conteudo = retornoAjaxModel.content.Replace("-------------------------------\n", "");
        //        throw new OrganogramaException(retornoAjaxModel.statusCode + ": " + conteudo);
        //    }

        //    return organograma;
        //}
    }
}
