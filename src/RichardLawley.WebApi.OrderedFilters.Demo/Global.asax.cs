using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RichardLawley.WebApi.OrderedFilters.Demo.Filters;

namespace RichardLawley.WebApi.OrderedFilters.Demo
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Filters.Add(new TestOrderedFilter(4));
            GlobalConfiguration.Configuration.Filters.Add(new TestOrderedFilter(0));

            // Enable the ordered filter provider
            GlobalConfiguration.Configuration.Services.Replace(typeof(System.Web.Http.Filters.IFilterProvider), new OrderedFilterProvider());
        }
    }
}