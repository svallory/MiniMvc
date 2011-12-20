namespace MiniMvc
{
	using System;
	using System.IO;
	using System.Reflection;
	using System.Web.Routing;
	using System.Web.Hosting;
	using System.Web;

	public class MiniMvcSystem
	{
		public String BasePath { get; set; }
		public String ViewsPath { get; set; }
		public String ViewExtension { get; set; }
		private static readonly Object _lock = new Object();
		private static Assembly _assembly {get;set;}

		private static MiniMvcSystem _instance;

		public static MiniMvcSystem GetInstance(String basePath) {
			lock(_lock)
			{
				if(_instance == null)
					MiniMvcSystem._instance = new MiniMvcSystem(basePath);
			
				return MiniMvcSystem._instance;
			}
		}

		public static MiniMvcSystem GetInstance()
		{
			lock (_lock)
			{
				if(_instance == null)
					_instance = new MiniMvcSystem();
			
				return _instance;
			}
		}

		static void GetInstance(Assembly assembly, String basePath)
		{
			GetInstance(basePath);
			MiniMvcSystem._assembly = assembly;
		}

		public static Assembly GetAssembly() {
			return _assembly;
		}

		private MiniMvcSystem()
		{
			ViewExtension = ".vbhtml";
			RegisterRoutes();
			HostingEnvironment.RegisterVirtualPathProvider(new MiniVirtualPathProvider());
		}

		private MiniMvcSystem(String basePath) : base()
		{
			SetBasePath(basePath);
		}

		private void RegisterRoutes() {
			RouteTable.Routes.Add(new Route("{controller}/{action}", new RouteHandler()));
		}

		public void SetBasePath(String basePath) {
			BasePath = HttpContext.Current.Server.MapPath(basePath);
			ViewsPath = Path.Combine(BasePath, "Views");
		}

		public static String GetViewsPath()
		{
			return GetInstance().ViewsPath;
		}
	}
}