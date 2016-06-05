using System.Web.Mvc;

namespace Visualisation.Web.Controllers
{
	public class DisplayController : Controller
	{
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

