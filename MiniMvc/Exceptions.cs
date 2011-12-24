using System;

namespace Exceptions
{
	public class MiniMVCException : Exception
	{
		public MiniMVCException(String message) : base(message) {
		}
	}

	public class ControllerNotFoundException : MiniMVCException
	{
		public ControllerNotFoundException(String controller) : 
			base("The controller '" + controller + "' could not be found. Make sure you are extending MiniMvc.Controller and your route is properly set . ")
		{
		}
	}

	public class ActionNotFoundException : MiniMVCException
	{
		public ActionNotFoundException(String controller, String action) : 
			base("The action '" + action + "' from controller '" + controller + 
			"' could not be found. Make sure you added the <ActionAttribute(\"action_name\")> to the method")
		{
		}
	}
}