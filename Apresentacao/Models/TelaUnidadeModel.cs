using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProcessoEletronicoWebApp.Apresentacao.Models
{
    public class TelaUnidadeModel
    {
        [Display(Name = "Nome"), Required(ErrorMessage = "Informe o Nome da unidade.")]
        public string nome { get; set; }
        [Display(Name = "Sigla"), Required(ErrorMessage = "Informe a Sigla da unidade.")]
        public string sigla { get; set; }
        [Display(Name = "Tipo de Unidade"), Required(ErrorMessage = "Informe o Tipo de unidade.")]
        public List<TipoUnidadeModel> tipoUnidade { get; set; }
        [Display(Name = "Tipo de Unidade"), Required(ErrorMessage = "Informe o Tipo de unidade.")]
        public int idTipoUnidade { get; set; }
        public List<OrganizacaoModel> organizacao { get; set; }
        [Display(Name = "Organização"), Required(ErrorMessage = "Informe a Organização da unidade.")]
        public int idOrganizacao { get; set; }        
        public List<UnidadeGetModel> unidadePai { get; set; }
        [Display(Name = "Unidade Pai"), Required(ErrorMessage = "Informe a Unidade Pai da unidade.")]
        public int idUnidadePai { get; set; }
        public Endereco endereco { get; set; }
        public List<TipoContato> listaTiposContato { get; set; }
        public List<Contato> contatos { get; set; }
        public List<Email> emails { get; set; }
        public List<Site> sites { get; set; }        
    }    
}