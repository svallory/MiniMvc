namespace MiniMvc.Configuration
{
	using System.Configuration;

	[ConfigurationCollection(typeof(RouteElement), AddItemName = "route")]
	public class RouteCollection : ConfigurationElementCollection
	{
		protected RouteElement Item(int i)
		{
			return (RouteElement)this.BaseGet(i);
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