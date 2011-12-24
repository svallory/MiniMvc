using System.Diagnostics;
using System.Linq;
using Microsoft.VisualBasic;
using System.Reflection;
using System.Web.Routing;
using System.Web;
using System.Collections.Generic;
using System;
using Exceptions;

namespace MiniMvc.Routing
{
	public class HttpHandler :  IHttpHandler
	{

		public bool IsReusable
		{
			get { return true; }
		} 

		public void ProcessRequest(HttpContext context)
		{
			RouteData routeData = context.Request.RequestContext.RouteData;
			var cfg = MiniMvcSystem.Config;
			var mvc = MiniMvcSystem.GetInstance();

			// Find the controller
			var ctrlName = (string)routeData.Values["controller"] ?? cfg.DefaultController;

			if (!mvc.Controllers.ContainsKey(ctrlName))
				throw new ControllerNotFoundException(ctrlName);

			var ctrl = Activator.CreateInstance(mvc.Controllers[ctrlName]) as Controller;

			// Find the action
			ctrl.LoadActions();

			var action = (string)routeData.Values["action"] ?? cfg.DefaultAction;

			if (!ctrl.Actions.ContainsKey(action))
				throw new ActionNotFoundException(ctrlName, action);

			ctrl.RunAction(action);	
		}
	}
}