using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.ViewModel
{
    public class TipoUnidadeViewModel
    {
    }

    public class TipoUnidadeDropDownViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }

    public class TipoUnidadeConsultarUnidadeViewModel
    {
        public string Descricao { get; set; }
    }
}
