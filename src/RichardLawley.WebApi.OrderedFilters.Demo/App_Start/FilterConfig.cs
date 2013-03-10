using System.Web;
using System.Web.Mvc;
using RichardLawley.WebApi.OrderedFilters.Demo.Filters;

namespace RichardLawley.WebApi.OrderedFilters.Demo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}