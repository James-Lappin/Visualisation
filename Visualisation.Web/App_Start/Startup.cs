using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using Visualisation.Core.Domain;
using Visualisation.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace Visualisation.Web
{
	public class Startup
	{
		// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
			StartUpSignalR(app);
		}

		private void StartUpSignalR(IAppBuilder app)
		{
			// Any connection or hub wire up and configuration should go here
			app.MapSignalR();
		}

		private void ConfigureAuth(IAppBuilder app)
		{
			// Configure the db context, user manager and role manager to use a single instance per request
			app.CreatePerOwinContext<ApplicationUserManager>((options, context) => ApplicationUserManager.Create(options));
			app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

			// Enable the application to use a cookie to store information for the signed in user
			// and to use a cookie to temporarily store information about a user logging in with a third party login provider
			// Configure the sign in cookie
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Account/Login"),
				Provider = new CookieAuthenticationProvider
				{
					// Enables the application to validate the security stamp when the user logs in.
					// This is a security feature which is used when you change a password or add an external login to your account.  
					OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
						validateInterval: TimeSpan.FromMinutes(30),
						regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
				}
			});
		}
	}
}