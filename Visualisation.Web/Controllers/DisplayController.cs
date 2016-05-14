using System.Web.Mvc;

namespace Visualisation.Web.Controllers
{
	[AllowAnonymous]
	public class DisplayController : Controller
	{
		[AllowAnonymous]
		public ActionResult Map()
		{
			return View();
		}

		[AllowAnonymous]
		public ActionResult Error()
		{
			return View();
		}
	}
}

