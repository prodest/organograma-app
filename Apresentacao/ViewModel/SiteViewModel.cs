using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.ViewModel
{
    public class SiteViewModel
    {
    }

    public class SiteInsercaoViewModel
    {
        [Display(Name = "URL")]
        public string Url { get; set; }
    }

    public class SiteConsultarUnidadeViewModel
    {
        public string Url { get; set; }
    }
}
