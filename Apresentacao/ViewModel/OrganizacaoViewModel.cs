using OrganogramaApp.Apresentacao.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.ViewModel
{
    public class OrganizacaoViewModel
    {
        List<OrganizacaoListagemViewModel> Organizacoes { get; set; }
    }

    public class OrganizacaoListagemViewModel
    {
        public string Guid { get; set; }

        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }
        [Display(Name = "Sigla")]
        public string Sigla { get; set; }        
        [Display(Name = "Organização Pai")]
        public Organizacaopai organizacaoPai { get; set; }
    }

    public class OrganizacaoDropDownList
    {
        public string Guid { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string SiglaNome
        {
            get
            {
                return Sigla + " - " + Nome;
            }
        }
    }

    public class OrganizacaoConsultarUnidadeViewModel
    {
        public string RazaoSocial { get; set; }
        public string Sigla { get; set; }
        public string SiglaRazaoSocial
        {
            get
            {
                return Sigla + " - " + RazaoSocial;
            }
        }
    }

    public class OrganizacaoInsercaoViewModel
    {        
        [Display(Name = "CNPJ"), Required(ErrorMessage = "Informe o CNPJ.")]
        public string Cnpj { get; set; }

        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Display(Name = "Esfera"), Required(ErrorMessage = "Informe o Esfera.")]
        public string Esfera { get; set; }

        [Display(Name = "Poder"), Required(ErrorMessage = "Informe o Poder.")]
        public string Poder { get; set; }

        [Display(Name = "Razão Social"), Required(ErrorMessage = "Informe o Nome da organização.")]
        public string RazaoSocial { get; set; }

        [Display(Name = "Sigla"), Required(ErrorMessage = "Informe a Sigla da organização.")]
        public string Sigla { get; set; }

        [Display(Name = "Tipo de organização"), Required(ErrorMessage = "Informe o Tipo de organização.")]
        public int IdTipoOrganizacao { get; set; }
        public List<TipoOrganizacaoDropDownViewModel> TiposOrganizacao { get; set; }

        [Display(Name = "Organização Pai")]
        public string GuidOrganizacaoPai { get; set; }
        public List<OrganizacaoDropDownList> OrganizacoesPai { get; set; }

        public EnderecoInsercaoViewModel Endereco { get; set; }

        public List<ContatoInsercaoViewModel> Contatos { get; set; }

        public List<EmailInsercaoViewModel> Emails { get; set; }

        public List<SiteInsercaoViewModel> Sites { get; set; }

        public List<PoderViewModel> Poderes { get; set; }
        public List<EsferaViewModel> Esferas { get; set; }        
    }


    public class OrganizacaoVisualizacaoViewModel
    {
        public int id { get; set; }        
        public string guid { get; set; }
        [Display(Name = "CNPJ")]
        public string cnpj { get; set; }
        [Display(Name = "Razão Social")]
        public string razaoSocial { get; set; }
        [Display(Name = "Nome Fantasia")]
        public string nomeFantasia { get; set; }
        [Display(Name = "Sigla")]
        public string sigla { get; set; }
        [Display(Name = "Contato")]
        public List<Contato> contatos { get; set; }
        [Display(Name = "E-mail")]
        public List<Email> emails { get; set; }
        [Display(Name = "Endereço")]
        public Endereco endereco { get; set; }
        [Display(Name = "Esfera")]
        public Esfera esfera { get; set; }
        [Display(Name = "Poder")]
        public Poder poder { get; set; }
        [Display(Name = "Organização Pai")]
        public Organizacaopai organizacaoPai { get; set; }
        [Display(Name = "Site")]
        public List<Site> sites { get; set; }
        [Display(Name = "Tipo")]
        public Tipoorganizacao tipoOrganizacao { get; set; }
    }

    public class EsferaViewModel
    {
        public int id { get; set; }
        [Display(Name = "Descrição")]
        public string descricao { get; set; }
    }

    public class PoderViewModel
    {
        public int id { get; set; }
        [Display(Name = "Descrição")]
        public string descricao { get; set; }
    }

    public class Organizacaopai
    {
        public string guid { get; set; }
        [Display(Name = "Razão Social")]
        public string razaoSocial { get; set; }
        [Display(Name = "Sigla")]
        public string sigla { get; set; }
    }

    public class Tipoorganizacao
    {
        [Display(Name = "Descrição")]
        public string descricao { get; set; }
    }
    
    public class PoderEsferaViewModel
    {        
        public string PoderDescricao { get; set; }
        public string EsferaDescricao { get; set; }        
    }
}
