using OrganogramaApp.Apresentacao.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganogramaApp.Apresentacao.Models;
using Newtonsoft.Json;
using OrganogramaApp.Apresentacao.Comum;
using OrganogramaApp.Apresentacao.ViewModel;
using AutoMapper;

namespace OrganogramaApp.Apresentacao
{
    public class OrganizacaoWorkService : WorkServiceBase, IOrganizacaoWorkService
    {
        private ITipoOrganizacaoWorkService tipoOrganizacaoWS;

        public OrganizacaoWorkService(string urlBaseurlOrganogramaApiBase) : base(urlBaseurlOrganogramaApiBase)
        {
            this.tipoOrganizacaoWS = new TipoOrganizacaoWorkService(urlBaseurlOrganogramaApiBase);
        }

        public OrganizacaoModel PesquisarDadosOrganizacao(string guid, string accessToken)
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

        public OrganizacaoVisualizacaoViewModel Pesquisar(string guid, string accessToken)
        {
            OrganizacaoVisualizacaoViewModel organizacao = null;

            string url = urlOrganogramaApiBase + "organizacoes/" + guid;

            RetornoAjaxModel retornoAjaxModel = Get(url, accessToken);

            if (retornoAjaxModel.IsSuccessStatusCode)
                organizacao = JsonConvert.DeserializeObject<OrganizacaoVisualizacaoViewModel>(retornoAjaxModel.result);
            else
            {
                string conteudo = retornoAjaxModel.content.Replace("-------------------------------\n", "");
                throw new OrganogramaException(retornoAjaxModel.statusCode + ": " + conteudo);
            }

            return organizacao;
        }

        public List<OrganizacaoListagemViewModel> Pesquisar(string accessToken)
        {
            List<OrganizacaoListagemViewModel> organizacoes = null;

            string url = urlOrganogramaApiBase + "organizacoes/escopo";

            RetornoAjaxModel retornoAjaxModel = Get(url, accessToken);

            if (retornoAjaxModel.IsSuccessStatusCode)
            {
                organizacoes = JsonConvert.DeserializeObject<List<OrganizacaoListagemViewModel>>(retornoAjaxModel.result);                
            }

            else
            {
                string conteudo = retornoAjaxModel.content.Replace("-------------------------------\n", "");
                throw new OrganogramaException(retornoAjaxModel.statusCode + ": " + conteudo);
            }

            return organizacoes;
        }

        public OrganizacaoInsercaoViewModel Inserir(List<OrganizacaoModel> organizacoes, string accessToken)
        {
            OrganizacaoInsercaoViewModel organizacaoIVM = new OrganizacaoInsercaoViewModel();

            var tiposOrg = (TelaTipoOrganizacao)tipoOrganizacaoWS.GetTiposOrganizacao(accessToken);

            organizacaoIVM.TiposOrganizacao = tiposOrg.tiposOrganizacao.Select(o => new TipoOrganizacaoDropDownViewModel
            {
                Id = o.id,
                Descricao = o.descricao
            })
                .ToList();


            organizacaoIVM.OrganizacoesPai = organizacoes.Select(o => new OrganizacaoDropDownList
            {
                Guid = o.guid,
                Nome = o.razaoSocial,
                Sigla = o.sigla
            })
            .ToList();

            organizacaoIVM.Poderes = PesquisaPoderes(accessToken);

            organizacaoIVM.Esferas = PesquisaEsferas(accessToken);            

            return organizacaoIVM;
        }

        public void Inserir(OrganizacaoInsercaoViewModel organizacaoIVM, string accessToken)
        {
            OrganizacaoPostModel organizacao = Mapper.Map<OrganizacaoInsercaoViewModel, OrganizacaoPostModel>(organizacaoIVM);

            var url = urlOrganogramaApiBase + "organizacoes";

            RetornoAjaxModel retornoAjaxModel = Post(organizacao, url, accessToken);

            if (!retornoAjaxModel.IsSuccessStatusCode)
            {
                string conteudo = retornoAjaxModel.content.Replace("-------------------------------\n", "");
                throw new OrganogramaException(retornoAjaxModel.statusCode + ": " + conteudo);
            }
        }

        public List<PoderViewModel> PesquisaPoderes(string accessToken)
        {
            List<PoderViewModel> poderes = null;

            string url = urlOrganogramaApiBase + "poderes";

            RetornoAjaxModel retornoAjaxModel = Get(url, accessToken);

            if (retornoAjaxModel.IsSuccessStatusCode)
            {
                poderes = JsonConvert.DeserializeObject<List<PoderViewModel>>(retornoAjaxModel.result);
            }

            else
            {
                string conteudo = retornoAjaxModel.content.Replace("-------------------------------\n", "");
                throw new OrganogramaException(retornoAjaxModel.statusCode + ": " + conteudo);
            }

            return poderes;
        }

        public List<EsferaViewModel> PesquisaEsferas(string accessToken)
        {
            List<EsferaViewModel> esferas = null;

            string url = urlOrganogramaApiBase + "esferas-organizacao";

            RetornoAjaxModel retornoAjaxModel = Get(url, accessToken);

            if (retornoAjaxModel.IsSuccessStatusCode)
            {
                esferas = JsonConvert.DeserializeObject<List<EsferaViewModel>>(retornoAjaxModel.result);
            }

            else
            {
                string conteudo = retornoAjaxModel.content.Replace("-------------------------------\n", "");
                throw new OrganogramaException(retornoAjaxModel.statusCode + ": " + conteudo);
            }

            return esferas;
        }

        public PoderEsferaViewModel PesquisaPoderEsfera(string guid, string accessToken)
        {
            PoderEsferaViewModel descricaoPoderEsfera = new PoderEsferaViewModel();

            var dadosOrganizacao = PesquisarDadosOrganizacao(guid, accessToken);

            descricaoPoderEsfera.EsferaDescricao = dadosOrganizacao.esfera.descricao;
            descricaoPoderEsfera.PoderDescricao = dadosOrganizacao.poder.descricao;            

            return descricaoPoderEsfera;
        }
    }
}
