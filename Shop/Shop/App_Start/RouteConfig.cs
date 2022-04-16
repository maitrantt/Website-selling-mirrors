using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // register
            routes.MapRoute(
                name: "Register_action",
                url: "Register/{action}",
                defaults: new { controller = "Register" }
            );
            routes.MapRoute(
                name: "Register",
                url: "Register",
                defaults: new { controller = "Register", action = "Index" }
            );
            // Login
            routes.MapRoute(
                name: "Login_Action",
                url: "Login/{action}",
                defaults: new { controller = "Login" }
            );
            routes.MapRoute(
               name: "Login",
               url: "Login",
               defaults: new { controller = "Login", action = "Login" }
           );

            // Home

            routes.MapRoute(
                name: "Home_Action",
                url: "Home/{action}",
                defaults: new { controller = "Home" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            // cart
            routes.MapRoute(
               name: "Cart_Action",
               url: "cart/{action}",
               defaults: new { controller = "Cart" }
           );
            routes.MapRoute(
                name: "Cart",
                url: "cart",
                defaults: new { controller = "Cart", action = "Index" }
            );
            //order
            routes.MapRoute(
                name: "Order_Action",
                url: "Order/{action}",
                defaults: new { controller = "Order" }
            );
            routes.MapRoute(
                name: "Order",
                url: "Order",
                defaults: new { controller = "Order", action = "Index" }
            );
            // name 
            //routes.MapRoute(
            //    name: "Product",
            //    url: "Product_{id}",
            //    defaults: new { controller = "Product", action = "Index", id = @"\w\gm" }
            //);
            //routes.MapRoute(
            //    name: "Product_Action",
            //    url: "Product/{action}",
            //    defaults: new { controller = "Product" }
            //);
            //routes.MapRoute(
            //    name: "Product_Action_id",
            //    url: "Product/{id}",
            //    defaults: new { controller = "Product", action = "Index" }
            //);
            // name 
            //routes.MapRoute(
            //    name: "Product",
            //    url: "Product_{id}",
            //    defaults: new { controller = "Product", action = "Index", id = @"\w\gm" }
            //);
            //routes.MapRoute(
            //    name: "ProductItem_Action",
            //    url: "ProductItem/{action}",
            //    defaults: new { controller = "ProductItem" }
            //);
            //routes.MapRoute(
            //    name: "ProductItem_Action_id",
            //    url: "ProductItem/{id}",
            //    defaults: new { controller = "ProductItem", action = "Index", id = @"\w\gm" }
            //);


        }
    }
}
