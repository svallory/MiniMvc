using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniMvc
{
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class ActionAttribute : Attribute
	{
		readonly string _name;

		public ActionAttribute (string Name) 
		{ 
			this._name = Name;        
		}

		public string Name
		{
			get { return _name; }
		}
	}

	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class ControllerAttribute : Attribute
	{
		readonly string _name;
    
	   public ControllerAttribute (string name) 
	   { 
			this._name = name;
	   }
   
		public string Name
		{
			get { return _name; }
		}
	}
}