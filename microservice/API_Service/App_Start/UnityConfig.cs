using System.Web.Http;
using Unity;
using Unity.WebApi;
using API_Service.Repository;
using API_Service.Repository.Interface;
using API_Service.Data;

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
            container.RegisterType<IContext, SampleData>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}