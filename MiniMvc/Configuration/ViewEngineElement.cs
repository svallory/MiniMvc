using System.Configuration;

namespace MiniMvc.Configuration
{
	class ViewEngineElement : ConfigurationElement
	{
		#region Attribute names
		private const string ViewsPathAttribute = "viewsPath";
		private const string ViewExtensionAttribute = "viewExtension";
		#endregion

		[ConfigurationProperty(ViewsPathAttribute, IsRequired = false, DefaultValue = "~/Views")]
		public string ViewsPath
		{
			get { return (string)this[ViewsPathAttribute]; }
			set { this[ViewsPathAttribute] = value; }
		}

		[ConfigurationProperty(ViewExtensionAttribute, IsRequired = false, DefaultValue = ".vbhtml")]
		[RegexStringValidator(@"\.[a-zA-Z0-9_]+")]
		public string ViewExtension
		{
			get { return (string)this[ViewExtensionAttribute]; }
			set { this[ViewExtensionAttribute] = value; }
		}
	}
}