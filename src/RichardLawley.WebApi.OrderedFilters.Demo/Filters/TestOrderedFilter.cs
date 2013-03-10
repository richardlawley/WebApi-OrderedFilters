using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace RichardLawley.WebApi.OrderedFilters.Demo.Filters
{
    public class TestOrderedFilter : BaseActionFilterAttribute, IActionFilter
    {
        public TestOrderedFilter(int order) : base(order) { }
        public const string KEY = "TestFilter";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);


            string value = String.Empty;
            if (actionContext.Request.Properties.ContainsKey(KEY))
            {
                value = actionContext.Request.Properties[KEY] as string ?? String.Empty;
            }
            value += (Order.ToString() + ",");
            actionContext.Request.Properties[KEY] = value;
        }
    }
}