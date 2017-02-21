using OrganogramaApp.Apresentacao.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.ViewModel
{
    public class UnidadeViewModel
    {
        [Display(Name = "Organização")]
        public string GuidOrganizacao { get; set; }

        public List<UnidadeListagemViewModel> Unidades { get; set; }
        public List<OrganizacaoDropDownList> Organizacoes { get; set; }
    }

    public class UnidadeListagemViewModel
    {
        public string Guid { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name = "Sigla")]
        public string Sigla { get; set; }
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
        [Display(Name = "Unidade Pai")]
        public string UnidadePai { get; set; }
    }
}
