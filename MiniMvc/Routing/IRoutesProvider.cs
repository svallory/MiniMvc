﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace MiniMvc
{
	interface IRoutesProvider
	{
		RouteCollection GetRoutes(); 
	}
}