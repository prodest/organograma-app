using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProcessoEletronicoWebApp.Apresentacao.Models
{
    public class UnidadePostModel
    {   
        public string nome { get; set; }        
        public string sigla { get; set; }        
        public int idOrganizacao { get; set; }
        public int idTipoUnidade { get; set; }
        public int idUnidadePai { get; set; }
        public Endereco endereco { get; set; }
        public List<Contato> contatos { get; set; }
        public List<Email> emails { get; set; }
        public List<Site> sites { get; set; }
    }
}