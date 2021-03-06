using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Web;
using Visualisation.Core.Domain;
using Visualisation.Core.Interfaces;
using Visualisation.Core.Responsitories;
using Visualisation.Core.Services;
using Visualisation.Web.Controllers;
using Visualisation.Web.Hubs;

namespace Visualisation.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();
            container.RegisterType<IMapDisplay, MapHub>();
            container.RegisterType<GeoLocationService, GeoLocationService>();
            container.RegisterType<IRepository<TransactionRequest>, MongoDbRepository<TransactionRequest>>();
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<IOwinContext>(new InjectionFactory(c => c.Resolve<HttpContextBase>().GetOwinContext()));
            container.RegisterType<AccountController>(new InjectionConstructor());


            //Service Locator
            var unityServiceLocator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
        }
    }
}
