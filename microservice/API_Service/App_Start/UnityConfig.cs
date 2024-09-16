using System.Web.Http;
using Unity;
using Unity.WebApi;
using API_Service.Repository;
using API_Service.Repository.Interface;

namespace API_Service
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<ITaskRepository, TaskRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}