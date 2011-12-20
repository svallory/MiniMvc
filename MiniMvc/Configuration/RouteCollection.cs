using System.Configuration;

namespace MiniMvc.Configuration
{
	[ConfigurationCollection(typeof(RouteElement), AddItemName = "route")]
	class RouteCollection : ConfigurationElementCollection
	{
		#region Attribute Names
		private const string RouteProviderAttribute = "routeProvider";
		private const string RouteAttribute = "route";
		#endregion

		[ConfigurationProperty(RouteProviderAttribute, IsRequired = false)]
		[SubclassTypeValidator(typeof(IRoutesProvider))]
		public string ViewsPath
		{
			get { return (string)this[RouteProviderAttribute]; }
			set { this[RouteProviderAttribute] = value; }
		}

		protected override string ElementName
		{
			get { return RouteAttribute; }
		}

		#region Overrides of ConfigurationElementCollection

		/// <summary>
		/// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement"/>.
		/// </summary>
		/// <returns>
		/// A new <see cref="T:System.Configuration.ConfigurationElement"/>.
		/// </returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new RouteElement(); 
		}

		/// <summary>
		/// Gets the element key for a specified configuration element when overridden in a derived class.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Object"/> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement"/>.
		/// </returns>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement"/> to return the key for. </param>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((RouteElement) element).Name;
		}

		#endregion
	}
}