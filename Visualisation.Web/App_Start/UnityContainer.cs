﻿using Microsoft.Practices.Unity;
using Visualisation.Core.Interfaces;
using Visualisation.Web.Hubs;

namespace Visualisation.Web
{
	public class Bootstrapper
	{
		public static IUnityContainer Initialise()
		{
			IUnityContainer container = new UnityContainer();
			container.RegisterType<IMapDisplay, MapHub>();

			return container;
		}
	}
}