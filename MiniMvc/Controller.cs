using RazorEngine;
using System.IO;
using System.Web;
using System.Reflection;
using System.Collections.Generic;
using System;

namespace MiniMvc
{
	public class Controller
	{
		protected static void RenderView(String viewPath, Object model)
		{
			MiniMvcSystem mvc = MiniMvcSystem.GetInstance();
			String source = File.ReadAllText(Path.Combine(mvc.ViewsPath, viewPath) + mvc.ViewExtension);
			var result = Razor.Parse(source, model);
			HttpContext.Current.Response.Write(result);
		}

		protected static void RenderView<T>(String viewPath, T model)
		{
			MiniMvcSystem mvc = MiniMvcSystem.GetInstance();
			String source = File.ReadAllText(Path.Combine(mvc.ViewsPath, viewPath) + mvc.ViewExtension);
			var result = Razor.Parse<T>(source, model);
			HttpContext.Current.Response.Write(result);
		}

	}
}