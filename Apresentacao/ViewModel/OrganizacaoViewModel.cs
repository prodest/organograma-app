using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.ViewModel
{
    public class OrganizacaoViewModel
    {
    }

    public class OrganizacaoDropDownList
    {
        public string Guid { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string SiglaNome
        {
            get
            {
                return Sigla + " - " + Nome;
            }
        }
    }
}
