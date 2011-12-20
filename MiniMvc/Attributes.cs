using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniMvc
{
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	sealed class ActionAttribute : Attribute
	{
		readonly string name;

		public ActionAttribute (string Name) 
		{ 
			this.name = Name;        
		}

		public string Name
		{
			get { return name; }
		}
	}

	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	sealed class ControllerAttribute : Attribute
	{
		readonly string name;
    
	   public ControllerAttribute (string name) 
	   { 
			this.name = name;
	   }
   
		public string Name
		{
			get { return name; }
		}
	}
}