using OrganogramaApp.Apresentacao.Models;
using System.Collections.Generic;
using System.Linq;

namespace OrganogramaApp.Apresentacao.ViewModel
{
    public class OrganogramaViewModel
    {
        public string guid { get; set; }
        public string cnpj { get; set; }
        public string title { get; set; }
        public string nomeFantasia { get; set; }
        public string name { get; set; }
        public Esfera esfera { get; set; }
        public Poder poder { get; set; }
        public List<OrganogramaViewModel> organizacoesFilhas { get; set; }
        public List<UnidadeFilhaViewModel> children { get; set; }
        public List<Children> c
        {
            get
            {
                List<Children> organizacoes = null;
                if (organizacoesFilhas != null)
                    organizacoes = organizacoesFilhas.Select(of => new Children { guid = of.guid, title = of.title, name = of.name }).ToList();

                List<Children> unidades = null;
                if (children != null)
                    unidades = children.Select(u => new Children { guid = u.guid, title = u.title, name = u.name }).ToList();

                if (organizacoes != null && unidades != null)
                    return organizacoes.Union(unidades).ToList();
                else if (organizacoes != null)
                    return organizacoes;
                else if (unidades != null)
                    return unidades;
                else
                    return null;
            }
        }
    }

    public class UnidadeFilhaViewModel
    {
        public string guid { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public List<UnidadeFilhaViewModel> children { get; set; }
        public List<Children> c
        {
            get
            {
                List<Children> unidades = null;
                if (children != null)
                    unidades = children.Select(u => new Children { guid = u.guid, title = u.title, name = u.name }).ToList();

                if (unidades != null)
                    return unidades;
                else
                    return null;
            }
        }
    }

    public class Children
    {
        public string guid { get; set; }
        public string title { get; set; }
        public string name { get; set; }
    }

    public class ChartViewModel
    {
        public string guid { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public string tipo { get; set; }
        public string className
        {
            get
            {
                if (!string.IsNullOrEmpty(tipo))                
                    return "organizacao";
                else
                    return "unidade";
            }
        }
        public List<ChartViewModel> organizacoes { get; set; }
        public List<ChartViewModel> unidades { get; set; }
        public List<ChartViewModel> children
        {
            get
            {
                if (organizacoes != null && unidades != null)
                    return organizacoes.Union(unidades).ToList();
                else if (organizacoes != null)
                    return organizacoes;
                else if (unidades != null)
                    return unidades;
                else
                    return null;
            }
        }
    }
}
