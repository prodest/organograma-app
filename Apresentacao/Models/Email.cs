using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganogramaApp.Apresentacao.Models
{   
    public class Email
    {
        [Display(Name = "E-mail")]
        public string endereco { get; set; }
    }
}