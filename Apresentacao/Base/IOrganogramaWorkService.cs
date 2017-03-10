using OrganogramaApp.Apresentacao.Models;
using OrganogramaApp.Apresentacao.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.Base
{
    public interface IOrganogramaWorkService
    {
        //OrganogramaViewModel Pesquisar(string guid, string accessToken);
        ChartViewModel Pesquisar(string guid, string accessToken);
    }
}
