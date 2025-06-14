using client.API_Service;
using client.API_Service.DomainService;
using client.DataLayer;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace client
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IService, Service>();
            container.RegisterType<IDataAccessLayer, DataAccessLayer>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}