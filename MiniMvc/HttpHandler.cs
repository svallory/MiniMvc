using Microsoft.VisualBasic;
using System.Reflection;
using System.Web.Routing;
using System.Web;
using System.Collections.Generic;
using System;
using MiniMvc;
using Exceptions;

public class HttpHandler :  IHttpHandler
{

	private Dictionary<String, Type> Controllers { get; set; }
	private Dictionary<String, Dictionary<String, MethodInfo>> Actions {get;set;}

	public bool IsReusable
	{
		get { return true; }
		set {}
	} 

	public void ProcessRequest(System.Web.HttpContext context)
	{
		LoadControllers();
		RouteData routeData = context.Request.RequestContext.RouteData;
		String ctrlName = (String)routeData.Values["controller"];
		String actName = (String)routeData.Values["action"];

		if(Controllers.ContainsKey(ctrlName)) {
			LoadActionsFor(ctrlName);

			if(Actions[ctrlName].ContainsKey(actName)) {
				RunAction(ctrlName, actName);
			} else {
				throw new ActionNotFoundException(ctrlName, actName);
			}
		}
		else {
			throw new ControllerNotFoundException(ctrlName);
		}
	}

	void RunAction(String ctrl, String action)
	{
		Controller c = (Controller) Activator.CreateInstance(Controllers[ctrl]);

		Actions[ctrl][action.ToLower()].Invoke(c, null);
	} // Sub

	private void LoadControllers()
	{
		Module[] moduleArray = MiniMvcSystem.GetAssembly().GetModules(false);

		// In a simple project with only one module, the module at index
		// 0 will be the module containing these classes.
		Module myModule = moduleArray[0];

		Type[] tArray;

		tArray = myModule.FindTypes((tp, c) => tp.Name.EndsWith("Controller"), null);

		foreach (Type t in tArray)
		{
			object[] cas = t.GetCustomAttributes(typeof(ControllerAttribute), false);

			if(cas.Length > 0)
				Controllers[((ControllerAttribute)cas[0]).Name] = t;
		}
	}

	private void LoadActionsFor(String ctrlName)
	{
		Dictionary<String, MethodInfo> cActs = new Dictionary<String, MethodInfo>();

		MemberInfo[] methods = Controllers[ctrlName].GetMethods();

		foreach(MethodInfo m in methods)
		{
			Object[] customAtts = m.GetCustomAttributes(typeof(ActionAttribute), true);

			if(customAtts.Length > 0)
			{
				String actionName = ((ActionAttribute)customAtts[0]).Name;
				cActs[actionName] = m;
			}
		}

		Actions[ctrlName] = cActs;
	}
}