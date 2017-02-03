using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProcessoEletronicoWebApp.Apresentacao.Models
{
    public class UnidadeGetModel
    {
        public int id { get; set; }
        public string guid { get; set; }
        [Display(Name = "Unidade")]
        public string nome { get; set; }
        [Display(Name = "Sigla")]
        public string sigla { get; set; }
        [Display(Name = "Tipo")]
        public Tipounidade tipoUnidade { get; set; }
        [Display(Name = "Organização")]
        public OrganizacaoModel organizacao { get; set; }
        [Display(Name = "Unidade Pai")]
        public Unidadepai unidadePai { get; set; }
        [Display(Name = "Endereço")]
        public Endereco endereco { get; set; }
        [Display(Name = "Contatos")]
        public List<Contato> contatos { get; set; }
        [Display(Name = "E-mails")]
        public List<Email> emails { get; set; }
        [Display(Name = "Sites")]
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