using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Visualisation.Core.Domain;
using Visualisation.Core.Interfaces;
using Visualisation.Core.Responsitories;
using Visualisation.Core.Services;
using Visualisation.Web.Hubs;

namespace Visualisation.Web
{
	public class Bootstrapper
	{
		public static IUnityContainer Initialise()
		{
			IUnityContainer container = new UnityContainer();
			container.RegisterType<IMapDisplay, MapHub>();
			container.RegisterType<GeoLocationService, GeoLocationService>();
			container.RegisterType<IRepository<TransactionRequest>, MongoDbRepository<TransactionRequest>>();

			//Service Locator
			var unityServiceLocator = new UnityServiceLocator(container);
			ServiceLocator.SetLocatorProvider(() => unityServiceLocator);

			return container;
		}
	}
}