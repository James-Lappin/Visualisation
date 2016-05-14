using System.Web.Mvc;
using System.Web.Routing;

namespace Visualisation.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Display", action = "Map", id = UrlParameter.Optional }
			);
		}
	}
}
