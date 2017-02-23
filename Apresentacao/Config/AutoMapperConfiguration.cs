using AutoMapper;
using OrganogramaApp.Apresentacao.Models;
using OrganogramaApp.Apresentacao.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganogramaApp.Apresentacao.Config
{
    public static class AutoMapperConfiguration
    {
        public static void CreateMap()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AllowNullCollections = true;

                #region Contato
                cfg.CreateMap<ContatoInsercaoViewModel, Contato>()
                    //.ForMember(dest => dest.telefone, opt => opt.MapFrom(s => s.Telefone))
                    //.ForMember(dest => dest.idTipoContato, opt => opt.MapFrom(s => s.IdTipoContato))
                ;
                #endregion

                #region Email
                cfg.CreateMap<EmailInsercaoViewModel, Email>();
                #endregion

                #region Endereco
                cfg.CreateMap<EnderecoInsercaoViewModel, Endereco>()
                    //.ForMember(dest => dest.logradouro, opt => opt.MapFrom(s => s.Logradouro))
                    //.ForMember(dest => dest.numero, opt => opt.MapFrom(s => s.Numero))
                    //.ForMember(dest => dest.complemento, opt => opt.MapFrom(s => s.Complemento))
                    //.ForMember(dest => dest.bairro, opt => opt.MapFrom(s => s.Bairro))
                    //.ForMember(dest => dest.cep, opt => opt.MapFrom(s => s.Cep))
                    //.ForMember(dest => dest.guidMunicipio, opt => opt.MapFrom(s => s.GuidMunicipio))
                ;
                #endregion

                #region Unidade
                cfg.CreateMap<UnidadeInsercaoViewModel, UnidadePostModel>()
                   //.ForMember(dest => dest.nome, opt => opt.MapFrom(s => s.Nome))
                   //.ForMember(dest => dest.sigla, opt => opt.MapFrom(s => s.Sigla))
                   //.ForMember(dest => dest.guidOrganizacao, opt => opt.MapFrom(s => s.GuidOrganizacao))
                   //.ForMember(dest => dest.idTipoUnidade, opt => opt.MapFrom(s => s.IdTipoUnidade))
                   //.ForMember(dest => dest.guidUnidadePai, opt => opt.MapFrom(s => !string.IsNullOrWhiteSpace(s.GuidUnidadePai) ? s.GuidUnidadePai : null ))
                   ;
                #endregion

                #region Site
                cfg.CreateMap<SiteInsercaoViewModel, Site>();
                #endregion
            });
        }

    }
}
