using System.Configuration;

namespace MiniMvc.Configuration
{
	class RouteElement : ConfigurationElement
	{
		#region Attribute Names
		private const string NameAttribute = "name";
		private const string UrlAttribute = "url";
		private const string ModuleAttribute = "module";
		private const string ControllerAttribute = "controller";
		private const string ActionAttribute = "action";
		#endregion
		
		[ConfigurationProperty(NameAttribute, IsRequired = false)]
		public string Name
		{
			get { return (string)this[NameAttribute]; }
			set { this[NameAttribute] = value; }
		}

		[ConfigurationProperty(UrlAttribute, IsRequired = false)]
		public string Url
		{
			get { return (string)this[UrlAttribute]; }
			set { this[UrlAttribute] = value; }
		}

		[ConfigurationProperty(ModuleAttribute, IsRequired = false)]
		public string Module
		{
			get { return (string)this[ModuleAttribute]; }
			set { this[NameAttribute] = value; }
		}

		[ConfigurationProperty(ControllerAttribute, IsRequired = false)]
		public string Controller
		{
			get { return (string)this[ControllerAttribute]; }
			set { this[ControllerAttribute] = value; }
		}

		[ConfigurationProperty(ActionAttribute, IsRequired = false)]
		public string Action
		{
			get { return (string)this[ActionAttribute]; }
			set { this[ActionAttribute] = value; }
		}
	}
}