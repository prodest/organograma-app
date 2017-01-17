using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Endereco
    {
        [Display(Name = "Logradouro"), Required(ErrorMessage = "Informe o Logradouro.")]
        public string logradouro { get; set; }
        [Display(Name = "Número")]
        public string numero { get; set; }
        [Display(Name = "Complemento")]
        public string complemento { get; set; }
        [Display(Name = "Bairro"), Required(ErrorMessage = "Informe o Bairro.")]
        public string bairro { get; set; }
        [Display(Name = "CEP"), Required(ErrorMessage = "Informe o CEP.")]
        public string cep { get; set; }
        [Display(Name = "Município"), Required(ErrorMessage = "Informe o Município.")]
        public string guidMunicipio { get; set; }
        [Display(Name = "Estado")]
        public List<Estado> estados { get; set; }
        public List<Municipio> municipios { get; set; }
        public Municipio municipio { get; set; }

        public class Estado
        {
            public int ID { get; set; }
            public string Sigla { get; set; }
            public string Nome { get; set; }            
        }
    }
}