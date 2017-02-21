using Newtonsoft.Json;
using OrganogramaApp.Apresentacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace OrganogramaApp.WebApp.Autorizacao
{
    public class UsuarioLogado : ClaimsPrincipal
    {
        private List<OrganizacaoModel> _Organizacoes;
        private List<OrganizacaoModel> _OrganizacoesPatriarcas;

        public UsuarioLogado()
        {
            if (HttpContext.Current.User != null)
            {
                var identity = HttpContext.Current.User.Identity as ClaimsIdentity;

                if (identity == null)
                    throw new Exception("Não foi encontrada uma Identity para o usuário autenticado.");

                this.AddIdentity(identity);

                if (this.HasClaim(c => c.Type == "organizacao"))
                {
                    _Organizacoes = this.Claims.Where(x => x.Type == "organizacao")
                                              .Select(x => JsonConvert.DeserializeObject<OrganizacaoModel>(x.Value))
                                              .ToList();
                }
                //else
                //    throw new Exception("Não foi possível encontrar uma organização para o usuário autenticado.");

                if (this.HasClaim(c => c.Type == "organizacao_patriarca"))
                {
                    _OrganizacoesPatriarcas = this.Claims.Where(x => x.Type == "organizacao_patriarca")
                                                        .Select(x => JsonConvert.DeserializeObject<OrganizacaoModel>(x.Value))
                                                        .ToList();
                }
                //else
                //    throw new Exception("Não foi possível encontrar uma organização patriarca para o usuário autenticado.");
            }
        }

        public bool Autenticado
        {
            get { return this.Identity.IsAuthenticated; }
        }

        public string Token
        {
            get { return this.FindFirst("access_token").Value; }
        }

        public string Nome
        {
            get { return this.FindFirst("nome").Value; }
        }

        public string CPF
        {
            get { return this.FindFirst("cpf").Value; }
        }

        public string Email
        {
            get { return this.FindFirst("email").Value; }
        }

        public List<OrganizacaoModel> Organizacoes
        {
            get
            {
                return _Organizacoes;
            }
        }
    }
}