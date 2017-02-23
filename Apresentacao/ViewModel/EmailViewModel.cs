using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.ViewModel
{
    public class EmailViewModel
    {
    }

    public class EmailInsercaoViewModel
    {
        [Display(Name = "E-mail")]
        public string Endereco { get; set; }
    }
}
