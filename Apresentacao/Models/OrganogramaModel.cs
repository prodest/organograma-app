using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.Models
{
    public class OrganogramaModel
    {
        public string guid { get; set; }
        public string cnpj { get; set; }
        public string razaoSocial { get; set; }
        public string nomeFantasia { get; set; }
        public string sigla { get; set; }
        public string nomeCurto { get; set; }
        public Esfera esfera { get; set; }
        public Poder poder { get; set; }
        public List<OrganogramaModel> organizacoesFilhas { get; set; }
        public List<UnidadeFilhaModel> unidades { get; set; }
        public List<ChildrenModel> children { get; set; }
    }

    public class UnidadeFilhaModel
    {
        public string guid { get; set; }
        public string nome { get; set; }
        public string sigla { get; set; }
        public string nomeCurto { get; set; }
        public List<UnidadeFilhaModel> unidadesFilhas { get; set; }
    }

    public class ChildrenModel
    {
        public string guid { get; set; }
        public string nome { get; set; }
        public string sigla { get; set; }
        public string nomeCurto { get; set; }
    }

}
