using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Compilation;
using MiniMvc.Routing;

namespace MiniMvc
{
	using System;
	using System.IO;
	using System.Reflection;
	using System.Web.Routing;
	using System.Web.Hosting;
	using System.Web;
	using System.Configuration;
	using MiniMvc.Configuration;

	public class MiniMvcSystem
	{
		private static readonly Object _lock = new Object();
		
		private static MiniMvcSystem _instance;

		private static Assembly _assembly { get; set; }
		public static Assembly GetAssembly() {
			return _assembly;
		}

		public static MiniMvcConfigurationSection Config { get; set; }
		
		public static MiniMvcSystem GetInstance()
		{
			lock (_lock)
			{
				return _instance ?? (_instance = new MiniMvcSystem(Assembly.GetCallingAssembly()));
			}
		}

		public Dictionary<String, Type> Controllers { get; set; }

		private MiniMvcSystem(Assembly callingAssembly)
		{
			_assembly = Assembly.Load("App_Code") ?? callingAssembly;

			// Fazer os tratamentos para pegar os valores default do que não estiver na configuração
			// Dar uma olhada no razor
			Config = MiniMvcConfigurationSection.GetConfiguration();

			LoadControllers();

			RegisterRoutes();

			HostingEnvironment.RegisterVirtualPathProvider(new MiniVirtualPathProvider());
		}

		private static RouteHandler _routeHandler;
		public static RouteHandler GetRouteHandler()
		{
			return _routeHandler ?? (_routeHandler = new RouteHandler());
		}

		private void RegisterRoutes() {
			var rc = Config.RoutesProvider.GetRoutes();

			foreach(var rt in rc)
				RouteTable.Routes.Add(rt);
		}

		private void LoadControllers()
		{
			Controllers = new Dictionary<string, Type>();
			Type[] types = _assembly.GetTypes();

			var ctrls = (from type in types
						 where type.IsSubclassOf(typeof(Controller))
						 select type);


			foreach (var t in ctrls)
			{
				var custom = t.GetCustomAttributes(typeof (ControllerAttribute), false).FirstOrDefault() as ControllerAttribute;
				var name = custom != null ? custom.Name : t.Name.Replace("Controller", "").ToLower();
				Controllers[name] = t;
			}
		}
	}
}