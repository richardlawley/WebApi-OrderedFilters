using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace RichardLawley.WebApi.OrderedFilters.Tests
{
    [ExcludeFromCodeCoverage]
    public class OrderedActionFilter : BaseActionFilterAttribute
    {
        public OrderedActionFilter(int order)
            : base(order) { }
        public OrderedActionFilter()
            : base() { }
    }

    [ExcludeFromCodeCoverage]
    public class OrderedAuthorizationFilter : BaseAuthorizationFilterAttribute
    {
        public OrderedAuthorizationFilter(int order)
            : base(order) { }
        public OrderedAuthorizationFilter()
            : base() { }
    }

    [ExcludeFromCodeCoverage]
    public class OrderedExceptionFilter : BaseExceptionFilterAttribute
    {
        public OrderedExceptionFilter(int order)
            : base(order) { }
        public OrderedExceptionFilter()
            : base() { }
    }

    [ExcludeFromCodeCoverage]
    public class OrderedFilter1 : OrderedActionFilter
    {
        public OrderedFilter1() : base(1) { }
    }

    [ExcludeFromCodeCoverage]
    public class OrderedFilter2 : OrderedActionFilter
    {
        public OrderedFilter2() : base(2) { }
    }

    [ExcludeFromCodeCoverage]
    public class OrderedFilter1a : OrderedActionFilter
    {
        public OrderedFilter1a() : base(1) { }
    }

    [ExcludeFromCodeCoverage]
    public class UnorderedFilter : ActionFilterAttribute { }

    [ExcludeFromCodeCoverage]
    public class UnorderedFilter2 : ActionFilterAttribute { }
}
