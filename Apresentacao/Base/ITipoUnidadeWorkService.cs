using OrganogramaApp.Apresentacao.Models;
using System.Collections.Generic;

namespace OrganogramaApp.Apresentacao.Base
{
    public interface ITipoUnidadeWorkService
    {
        List<TipoUnidadeModel> Listar(string accessToken);
        object GetTiposUnidade(string token);
        TelaTipoUnidade GetTipoUnidade(int id, string token);
        TelaTipoUnidade PutTipoUnidade(int id, TipoUnidadeModel tipoUnidade, string token);
        TelaTipoUnidade PostTipoUnidade(TipoUnidadeModel tipoUnidade, string token);
        TelaTipoUnidade DeleteTipoUnidade(int id, string token);
    }
}
