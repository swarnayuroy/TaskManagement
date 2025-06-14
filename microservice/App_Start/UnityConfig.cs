using System.Web.Http;
using Unity;
using Unity.WebApi;
using microservice.Repository;
using microservice.Sample_Data;
using microservice.Sample_Data.Context;
using microservice.Service;

namespace microservice
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IAccountService, AccountRepository>();
            container.RegisterType<IAccountContext, AccountData>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}