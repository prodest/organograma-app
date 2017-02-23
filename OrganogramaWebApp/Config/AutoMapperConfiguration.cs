using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganogramaApp.WebApp.Config
{
    public static class AutoMapperConfiguration
    {
        public static void CreateMap()
        {
            OrganogramaApp.Apresentacao.Config.AutoMapperConfiguration.CreateMap();
        }
    }
}