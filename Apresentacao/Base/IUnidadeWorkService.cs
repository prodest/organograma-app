using OrganogramaApp.Apresentacao.Models;
using System.Collections.Generic;

namespace OrganogramaApp.Apresentacao.Base
{
    public interface IUnidadeWorkService
    {
        List<UnidadeGetModel> GetUnidades(string guidOrganizacao, string token);
        RetornoAjaxModel GetUnidade(string guidOrganizacao, string token);
        List<OrganizacaoModel> GeOrganizacoesPorPatriarca(string guidPatriarca, string token);
        List<TipoUnidadeModel> GetTiposUnidade(string token);
        RetornoAjaxModel PostUnidade(UnidadePostModel unidade, string token);
    }
}
