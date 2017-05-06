﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC5Course
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //甚麼情況下不要走路由
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //路由是可自由修改、被設計的
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{data}",
                defaults: new {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    data= UrlParameter.Optional
                }
            );
        }
    }
}
