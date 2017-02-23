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

    public class UnidadeDropDownViewModel
    {
        public string Guid { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string SiglaNome {
            get
            {
                return Sigla + " - " + Nome;
            }
        }
    }

    public class UnidadeInsercaoViewModel
    {
        public string GuidOrganizacao { get; set; }
        [Display(Name = "Organização")]
        public string SiglaNomeOrganizacao { get; set; }

        [Display(Name = "Nome"), Required(ErrorMessage = "Informe o Nome da unidade.")]
        public string Nome { get; set; }

        [Display(Name = "Sigla"), Required(ErrorMessage = "Informe a Sigla da unidade.")]
        public string Sigla { get; set; }

        [Display(Name = "Tipo de Unidade"), Required(ErrorMessage = "Informe o Tipo de unidade.")]
        public int IdTipoUnidade { get; set; }
        public List<TipoUnidadeDropDownViewModel> TiposUnidade { get; set; }

        [Display(Name = "Unidade Pai")]
        public string GuidUnidadePai { get; set; }
        public List<UnidadeDropDownViewModel> UnidadesPai { get; set; }

        public EnderecoInsercaoViewModel Endereco { get; set; }

        public List<ContatoInsercaoViewModel> Contatos { get; set; }

        public List<EmailInsercaoViewModel> Emails { get; set; }

        public List<SiteInsercaoViewModel> Sites { get; set; }
    }
}
