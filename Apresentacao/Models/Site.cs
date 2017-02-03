using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProcessoEletronicoWebApp.Apresentacao.Models
{
    public class Site
    {        
        [Display(Name = "URL")]
        public string url { get; set; }                
    }
}