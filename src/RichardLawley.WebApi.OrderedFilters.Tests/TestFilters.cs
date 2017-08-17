using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Http.Filters;

namespace RichardLawley.WebApi.OrderedFilters.Tests
{
    public class OrderedActionFilter : BaseActionFilterAttribute
    {
        public OrderedActionFilter(int order)
            : base(order) { }

        public OrderedActionFilter()
            : base() { }
    }

    public class OrderedAuthorizationFilter : BaseAuthorizationFilterAttribute
    {
        public OrderedAuthorizationFilter(int order)
            : base(order) { }

        public OrderedAuthorizationFilter() { }
    }

    public class OrderedExceptionFilter : BaseExceptionFilterAttribute
    {
        public OrderedExceptionFilter(int order)
            : base(order) { }

        public OrderedExceptionFilter() { }
    }

    public class OrderedFilter1 : OrderedActionFilter
    {
        public OrderedFilter1() : base(1)
        {
        }
    }

    public class OrderedFilter2 : OrderedActionFilter
    {
        public OrderedFilter2() : base(2)
        {
        }
    }

    public class OrderedFilter1a : OrderedActionFilter
    {
        public OrderedFilter1a() : base(1)
        {
        }
    }

    public class UnorderedFilter : ActionFilterAttribute { }

    public class UnorderedFilter2 : ActionFilterAttribute { }
}