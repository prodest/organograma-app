using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.ViewModel
{
    public class MunicipioViewModel
    {
    }

    public class MunicipioDropDownViewModel
    {
        public string Guid { get; set; }
        public string Nome { get; set; }
    }

    public class MunicipioConsultarUnidadeViewModel
    {
        public string Nome { get; set; }
        public string Uf { get; set; }
    }
}
