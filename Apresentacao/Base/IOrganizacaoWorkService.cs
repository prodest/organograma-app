using OrganogramaApp.Apresentacao.Models;
using OrganogramaApp.Apresentacao.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.Base
{
    public interface IOrganizacaoWorkService
    {
        OrganizacaoVisualizacaoViewModel Pesquisar(string guid, string accessToken);
        List<OrganizacaoListagemViewModel> Pesquisar(string accessToken);
        OrganizacaoInsercaoViewModel Inserir(List<OrganizacaoModel> organizacoes, string accessToken);
        void Inserir(OrganizacaoInsercaoViewModel organizacaoIVM, string accessToken);
        OrganizacaoModel PesquisarDadosOrganizacao(string guid, string accessToken);
        PoderEsferaViewModel PesquisaPoderEsfera(string guid, string accessToken);
    }
}
