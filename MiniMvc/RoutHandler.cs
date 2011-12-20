using System.Web.Routing;
using System.Web;

public class RouteHandler : IRouteHandler
{
	public IHttpHandler GetHttpHandler(System.Web.Routing.RequestContext reqCtx)
	{
		return new HttpHandler();
	}
}