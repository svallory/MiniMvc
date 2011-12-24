using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace MiniMvc.Configuration
{
	public class MiniConfigHandler : IConfigurationSectionHandler
	{
		#region Implementation of IConfigurationSectionHandler

		/// <summary>
		/// Creates a configuration section handler.
		/// </summary>
		/// <returns>
		/// The created section handler object.
		/// </returns>
		/// <param name="parent">Parent object.</param><param name="configContext">Configuration context object.</param><param name="section">Section XML node.</param><filterpriority>2</filterpriority>
		public object Create(object parent, object configContext, XmlNode section)
		{
			return new MiniMvcConfigurationSection();
		}

		#endregion
	}
}
