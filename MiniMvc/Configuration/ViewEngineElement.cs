using System.Collections.Generic;
using System.Configuration;
using RazorEngine.Configuration.Xml;

namespace MiniMvc.Configuration
{
	public class ViewEngineElement : ConfigurationElement
	{
		#region Attribute names
		private const string ViewsFolderAttribute = "viewsFolder";
		private const string ViewExtensionAttribute = "viewExtension";
		#endregion

		[ConfigurationProperty(ViewsFolderAttribute, IsRequired = false, DefaultValue = "~/Views")]
		public string ViewsFolder
		{
			get { return (string)this[ViewsFolderAttribute]; }
			set { this[ViewsFolderAttribute] = value; }
		}

		[ConfigurationProperty(ViewExtensionAttribute, IsRequired = false, DefaultValue = ".vbhtml")]
		[RegexStringValidator(@"\.[a-zA-Z0-9_]+")]
		public string ViewExtension
		{
			get { return (string)this[ViewExtensionAttribute]; }
			set { this[ViewExtensionAttribute] = value; }
		}

		[ConfigurationProperty("namespaces", IsDefaultCollection = true, IsRequired = false)]
		public NamespacesCollection Namespaces
		{
			get { return (NamespacesCollection)this["namespaces"]; }
			internal set { this["namespaces"] = value; }
		}
	}

	public class NamespaceElement : ConfigurationElement
	{
		[ConfigurationProperty("namespace", IsRequired = true)]
		public string Namespace
		{
			get { return (string)this["namespace"]; }
			set { this["namespace"] = value; }
		}
	}

	[ConfigurationCollection(typeof(NamespaceElement))]
	public class NamespacesCollection : ConfigurationElementCollection
	{
		#region Overrides of ConfigurationElementCollection

		/// <summary>
		/// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement"/>.
		/// </summary>
		/// <returns>
		/// A new <see cref="T:System.Configuration.ConfigurationElement"/>.
		/// </returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new NamespaceElement();
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
			return ((NamespaceElement) element).Namespace.Replace(".","_");
		}

		#endregion
	}
}