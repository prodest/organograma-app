using OrganogramaApp.Apresentacao.Models;

namespace OrganogramaApp.Apresentacao.Base
{
    public interface ITipoUnidadeWorkService
    {
        object GetTiposUnidade(string token);
        TelaTipoUnidade GetTipoUnidade(int id, string token);
        TelaTipoUnidade PutTipoUnidade(int id, TipoUnidadeModel tipoUnidade, string token);
        TelaTipoUnidade PostTipoUnidade(TipoUnidadeModel tipoUnidade, string token);
        TelaTipoUnidade DeleteTipoUnidade(int id, string token);
    }
}
