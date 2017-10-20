using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.ViewModel
{
    public class ResponsavelViewModel
    {
        [Display(Name = "CPF")]
        public string Cpf { get; set; }
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }   
}
