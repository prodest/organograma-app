using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganogramaApp.Apresentacao.Models
{
    public class UnidadePostModel
    {   
        public string nome { get; set; }        
        public string sigla { get; set; }        
        public string guidOrganizacao { get; set; }
        public int idTipoUnidade { get; set; }
        public string guidUnidadePai { get; set; }
        public Endereco endereco { get; set; }
        public List<Contato> contatos { get; set; }
        public List<Email> emails { get; set; }
        public List<Site> sites { get; set; }
    }
}