using OrganogramaApp.Apresentacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.Base
{
    public interface IOrganizacaoWorkService
    {
        OrganizacaoModel Pesquisar(string guid, string accessToken);
    }
}
