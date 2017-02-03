using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProcessoEletronicoWebApp.Apresentacao.Models
{
    public class Contato
    {
        [Display(Name = "Telefone")]
        public string telefone { get; set; }        
        public List<TipoContato> tiposContato { get; set; }
        [Display(Name = "Tipo de Contato")]
        public int idTipoContato { get; set; }        
    }

    public class TipoContato
    {        
        public int id { get; set; }
        public string descricao { get; set; }
        public int quantidadeDigitos { get; set; }
    }
}