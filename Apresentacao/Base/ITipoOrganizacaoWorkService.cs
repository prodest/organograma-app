using OrganogramaApp.Apresentacao.Models;

namespace OrganogramaApp.Apresentacao.Base
{
    public interface ITipoOrganizacaoWorkService
    {
        object GetTiposOrganizacao(string token);

        TelaTipoOrganizacao GetTipoOrganizacao(int id, string token);

        TelaTipoOrganizacao PutTipoOrganizacao(int id, TipoOrganizacaoModel tipoOrganizacao, string token);

        TelaTipoOrganizacao PostTipoOrganizacao(TipoOrganizacaoModel tipoOrganizacao, string token);

        TelaTipoOrganizacao DeleteTipoOrganizacao(int id, string token);
    }
}
