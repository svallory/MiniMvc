using System;
using System.ComponentModel;
using MiniMvc.Routing;

namespace MiniMvc.Configuration
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Configuration;

	// Note: Route Child Elements are not supportted yet
	// 
	//<miniMvc>
	//    <viewEngine viewsPath="" viewExtension="" />
	//    <routes router="Namespace.IMiniMvcRoutesProvider, AssemblyName">
	//        <route name="" url="" controller="" action="">
	//            <defaults>
	//                <param name="" value="" />
	//            </defaults>
	//            <constrains>
	//                <param name="" rule="" />
	//            </constrains>
	//        </route>
	//    </routes>
	//</miniMvc>

	/// <summary>
	/// Defines the main configuration section for the MiniMvc.
	/// </summary>
	public class MiniMvcConfigurationSection : ConfigurationSection
	{
		private const string SectionPath = "miniMvc";

		#region Attribute names
		private const string ViewEngineAttribute = "viewEngine";
		private const string RoutesAttribute = "routes";
		private const string RoutesProviderAttribute = "routesProvider";
		private const string DefaultControllerAttribute = "defaultController";
		private const string DefaultActionAttribute = "defaultAction";
		#endregion
		
		[ConfigurationProperty(ViewEngineAttribute, IsRequired = false)]
		public ViewEngineElement ViewEngine { 
			get { return (ViewEngineElement)this[ViewEngineAttribute]; }
			set { this[ViewEngineAttribute] = value; }
		}

		[ConfigurationProperty(RoutesAttribute, IsDefaultCollection = true, IsRequired = false)]
		public RouteCollection Routes
		{
			get { return (RouteCollection)this[RoutesAttribute]; }
			set { this[RoutesAttribute] = value; }
		}

		[TypeConverter(typeof(TypeNameConverter))]
		[ConfigurationProperty(RoutesProviderAttribute, IsRequired = false, DefaultValue = "MiniMvc.Routing.DefaultRoutesProvider, MiniMvc")]
		public Type RoutesProviderType
		{
			get { return this[RoutesProviderAttribute] as Type; }
			set { this[RoutesProviderAttribute] = value; }
		}

		private IRoutesProvider _routesProvider;
		public IRoutesProvider RoutesProvider
		{
			get { return _routesProvider ?? (_routesProvider = Activator.CreateInstance(RoutesProviderType) as IRoutesProvider); }
			set {
				_routesProvider = value;
				RoutesProviderType = value.GetType();
			}
		}

		[ConfigurationProperty(DefaultControllerAttribute, IsRequired = false, DefaultValue = "Site")]
		public string DefaultController
		{
			get { return (string)this[DefaultControllerAttribute]; }
			set { this[DefaultControllerAttribute] = value; }
		}

		[ConfigurationProperty(DefaultActionAttribute, IsRequired = false, DefaultValue = "Index")]
		public string DefaultAction
		{
			get { return (string)this[DefaultActionAttribute]; }
			set { this[DefaultActionAttribute] = value; }
		}

		#region Methods
		/// <summary>
		/// Gets an instance of <see cref="MiniMvcConfigurationSection"/> that represents the current configuration.
		/// </summary>
		/// <returns>An instance of <see cref="MiniMvcConfigurationSection"/>, or null if no configuration is specified.</returns>
		public static MiniMvcConfigurationSection GetConfiguration()
		{
			return ConfigurationManager.GetSection(SectionPath) as MiniMvcConfigurationSection;
		}
		#endregion
	}
}
