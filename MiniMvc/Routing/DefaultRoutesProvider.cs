using System.Web.Routing;
using MiniMvc.Configuration;
using RouteCollection = System.Web.Routing.RouteCollection;

namespace MiniMvc.Routing
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class DefaultRoutesProvider : IRoutesProvider
	{
		#region Implementation of IRoutesProvider

		public RouteCollection GetRoutes()
		{
			var rc = new RouteCollection();
			var cfg = MiniMvcSystem.Config;

			foreach (RouteElement route in cfg.Routes)
			{
				var rd = new RouteValueDictionary {
						    {"controller", route.Controller}, {"action", route.Action}
				        };

				rc.Add(route.Name, new Route(route.Url, rd, MiniMvcSystem.GetRouteHandler()));
			}

			return rc;
		}

		#endregion
	}
}
