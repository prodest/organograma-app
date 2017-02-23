using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.ViewModel
{
    public class ContatoViewModel
    {
    }

    public class ContatoInsercaoViewModel
    {
        [Display(Name = "Tipo de Contato")]
        public int IdTipoContato { get; set; }
        public List<TipoContatoDropDownViewModel> TiposContato { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }
    }

    public class ContatoConsultarUnidadeViewModel
    {
        public string Telefone { get; set; }
    }
}
