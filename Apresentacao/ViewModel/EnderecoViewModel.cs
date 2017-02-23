using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.ViewModel
{
    public class EnderecoViewModel
    {
    }

    public class EnderecoInsercaoViewModel
    {
        [Display(Name = "Logradouro"), Required(ErrorMessage = "Informe o Logradouro.")]
        public string Logradouro { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Display(Name = "Bairro"), Required(ErrorMessage = "Informe o Bairro.")]
        public string Bairro { get; set; }

        [Display(Name = "CEP"), Required(ErrorMessage = "Informe o CEP.")]
        public string Cep { get; set; }

        [Display(Name = "Estado")]
        public List<EstadoDropDownViewModel> Estados { get; set; }

        [Display(Name = "Município"), Required(ErrorMessage = "Informe o Município.")]
        public string GuidMunicipio { get; set; }
        public List<MunicipioDropDownViewModel> Municipios { get; set; }
    }

    public class EnderecoConsultarUnidadeViewModel
    {
        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }
        [Display(Name = "Número")]
        public string Numero { get; set; }
        [Display(Name = "Complemento")]
        public string Complemento { get; set; }
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }
        [Display(Name = "CEP")]
        public string Cep { get; set; }
        public MunicipioConsultarUnidadeViewModel Municipio { get; set; }
    }
}
