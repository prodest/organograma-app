using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessoEletronicoWebApp.Apresentacao.Models
{
    public class RetornoAjaxModel
    {
        public bool IsSuccessStatusCode { get; set; }
        public string statusCode { get; set; }
        public string content { get; set; }
        public string result { get; set; }
    }
}