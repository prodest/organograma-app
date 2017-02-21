using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.Comum
{
    public class OrganogramaException : Exception
    {
        public OrganogramaException() : base() { }
        public OrganogramaException(string mensagem) : base(mensagem) { }
        public OrganogramaException(string mensagem, Exception inner) : base(mensagem, inner) { }
    }
}
