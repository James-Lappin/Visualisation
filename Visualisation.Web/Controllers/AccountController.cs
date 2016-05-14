using System.Web.Mvc;
using test.Models;
using Visualisation.Core.Domain;

namespace Visualisation.Web.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		//
		// GET: /Account/Login
		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		//
		// POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginViewModel model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			// This doesn't count login failures towards account lockout
			// To enable password failures to trigger account lockout, change to shouldLockout: true
			var user = VisualisationUser.GetByUsernameAndPassword(model.Email, model.Password);
			if (user == null)
			{
				ModelState.AddModelError("", "Invalid login attempt.");
				return View(model);
			}

			LoginManager.Login(user);
			return RedirectToLocal(returnUrl);
		}

		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			return RedirectToAction("Map", "Display");
		}

		//
		// GET: /Account/Register
		[AllowAnonymous]
		public ActionResult Register()
		{
			return View();
		}

		//
		// POST: /Account/Register
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = VisualisationUser.Create(model.Email, model.Password);
			if (user == null)
			{
				ModelState.AddModelError("", "Sorry Something went wrong");
				return View(model);
			}


			LoginManager.Login(user);
			return RedirectToAction("Map", "Display");
		}

		//
		// POST: /Account/LogOff
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			//AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			return RedirectToAction("Login");
		}

		//
		// GET: /Account/ExternalLoginFailure
		[AllowAnonymous]
		public ActionResult ExternalLoginFailure()
		{
			return View();
		}

	}
}