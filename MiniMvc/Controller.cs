using System.Linq;
using MiniMvc.Configuration;
using RazorEngine;
using System.IO;
using System.Web;
using System.Reflection;
using System.Collections.Generic;
using System;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace MiniMvc
{
	public class Controller
	{
		public Dictionary<String, MethodInfo> Actions { get; set; }

		private static ITemplateService GetTemplateService(ViewEngineElement conf)
		{
			var ts = new TemplateService();

			foreach (NamespaceElement ns in conf.Namespaces)
			{
				ts.AddNamespace(ns.Namespace);
			}

			return ts;
		}

		protected static void RenderView(String viewPath, Object model)
		{
			ViewEngineElement conf = MiniMvcSystem.Config.ViewEngine;
			var ctx = HttpContext.Current;

			String source = File.ReadAllText(Path.Combine(ctx.Server.MapPath(conf.ViewsFolder), viewPath) + conf.ViewExtension);

			var ts = GetTemplateService(conf);
			var result = ts.Parse(source, model);

			HttpContext.Current.Response.Write(result);
		}

		protected static void RenderView<T>(String viewPath, T model)
		{
			ViewEngineElement conf = MiniMvcSystem.Config.ViewEngine;
			var ctx = HttpContext.Current;
			String source = File.ReadAllText(Path.Combine(ctx.Server.MapPath(conf.ViewsFolder), viewPath) + conf.ViewExtension);

			var ts = GetTemplateService(conf);
			var result = ts.Parse(source, model);
			
			HttpContext.Current.Response.Write(result);
		}

		internal void LoadActions()
		{
			if (Actions == null)
			{
				Actions = new Dictionary<string, MethodInfo>();

				var actions = from act in GetType().GetMethods()
				              where
				              	act.Name.EndsWith("Action") || (0 < act.GetCustomAttributes(typeof (ActionAttribute), true).Count())
				              select act;


				foreach (MethodInfo m in actions)
				{
					var custom = m.GetCustomAttributes(typeof (ActionAttribute), true).FirstOrDefault() as ActionAttribute;
					var name = custom != null ? custom.Name : m.Name.Replace("Action", "").ToLower();

					Actions[name] = m;
				}
			}
		}

		internal void RunAction(string action)
		{
			Actions[action].Invoke(this, null);
		}
	}
}