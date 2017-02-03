using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessoEletronicoWebApp.Apresentacao.Models
{
    public class Municipio
    {
        public string guid { get; set; }
        public int codigoIbge { get; set; }
        public string nome { get; set; }
        public string uf { get; set; }
    }
}