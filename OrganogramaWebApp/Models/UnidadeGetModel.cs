using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class UnidadeGetModel
    {
        public int id { get; set; }
        public string guid { get; set; }
        public string nome { get; set; }
        public string sigla { get; set; }
        public Tipounidade tipoUnidade { get; set; }
        public OrganizacaoModel organizacao { get; set; }
        public Unidadepai unidadePai { get; set; }
        public Endereco endereco { get; set; }
        public List<Contato> contatos { get; set; }
        public List<Email> emails { get; set; }
        public List<Site> sites { get; set; }
    }

    public class Tipounidade
    {
        public int id { get; set; }
        public string descricao { get; set; }
    }

    //public class Organizacao
    //{
    //    public string guid { get; set; }
    //    public string razaoSocial { get; set; }
    //    public string nomeFantasia { get; set; }
    //    public string sigla { get; set; }
    //}

    public class Unidadepai
    {
        public string guid { get; set; }
        public string nome { get; set; }
        public string sigla { get; set; }
    }    

    
}