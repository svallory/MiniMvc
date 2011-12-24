namespace MiniMvc.Routing
{
	using System.Web.Routing;
	public interface IRoutesProvider
	{
		RouteCollection GetRoutes(); 
	}
}
