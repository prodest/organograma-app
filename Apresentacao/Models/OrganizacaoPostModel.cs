using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.Models
{
    public class OrganizacaoPostModel
    {
        public string cnpj { get; set; }
        public string razaoSocial { get; set; }
        public string nomeFantasia { get; set; }
        public string sigla { get; set; }
        public int idTipoOrganizacao { get; set; }
        public string guidOrganizacaoPai { get; set; }
        public Endereco endereco { get; set; }
        public List<Contato> contatos { get; set; }
        public List<Email> emails { get; set; }
        public List<Site> sites { get; set; }
    }
}