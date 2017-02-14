using Autofac;
using Autofac.Integration.Mvc;
using OrganogramaApp.Apresentacao;
using OrganogramaApp.Apresentacao.Base;
using System.Configuration;
using System.Web.Mvc;

namespace OrganogramaApp.WebApp.Config
{
    public static class InjecaoDependencia
    {
        public static void Injetar()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            string urlBase = ConfigurationManager.AppSettings["OrganogramaAPIBase"];

            #region Injeção das classes de Apresentação
            builder.RegisterInstance(new TipoOrganizacaoWorkService(urlBase)).As<ITipoOrganizacaoWorkService>();
            builder.RegisterInstance(new TipoUnidadeWorkService(urlBase)).As<ITipoUnidadeWorkService>();
            builder.RegisterInstance(new UnidadeWorkService(urlBase)).As<IUnidadeWorkService>();
            #endregion

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


        }


    }
}
