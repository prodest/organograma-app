using OrganogramaApp.Apresentacao.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganogramaApp.Apresentacao.Models;
using OrganogramaApp.Apresentacao.Comum;
using Newtonsoft.Json;

namespace OrganogramaApp.Apresentacao
{
    public class ComumWorkService : WorkServiceBase, IComumWorkService
    {
        public ComumWorkService(string urlBaseurlOrganogramaApiBase) : base(urlBaseurlOrganogramaApiBase)
        {
        }

        public List<TipoContato> Listar(string accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
