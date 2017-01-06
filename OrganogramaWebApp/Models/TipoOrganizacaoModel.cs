using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TelaTipoOrganizacao
    {
        public RetornoAjaxModel retornoAjax { get; set; }
        public List<TipoOrganizacaoModel> tiposOrganizacao { get; set; }
        public TipoOrganizacaoModel tipoOrganizacao { get; set; }
    }

    public class TipoOrganizacaoModel
    {
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Fim da Vigência")]
        public string observacaoFimVigencia { get; set; }

        [Display(Name = "Tipo de Organização"), Required(ErrorMessage = "Informe o Tipo de Organização.")]
        public string descricao { get; set; }
    }    
}