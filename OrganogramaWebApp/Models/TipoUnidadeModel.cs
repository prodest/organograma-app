using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TelaTipoUnidade
    {
        public RetornoAjaxModel retornoAjax { get; set; }
        public List<TipoUnidadeModel> tiposUnidade { get; set; }
        public TipoUnidadeModel tipoUnidade { get; set; }


    }

    public class TipoUnidadeModel
    {
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Fim da Vigência")]
        public string observacaoFimVigencia { get; set; }

        [Display(Name = "Descrição"), Required(ErrorMessage = "Informe a descricão.")]
        public string descricao { get; set; }
    }    
}