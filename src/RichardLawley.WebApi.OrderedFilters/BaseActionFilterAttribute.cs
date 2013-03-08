using System;
using System.Linq;
using System.Web.Http.Filters;

namespace RichardLawley.WebApi.OrderedFilters
{
    /// <summary>
    /// Action Filter which can have the execution order specified
    /// </summary>
    public abstract class BaseActionFilterAttribute : ActionFilterAttribute, IOrderedFilterAttribute
    {
        /// <summary>
        /// Order of execution for this filter
        /// </summary>
        public int Order { get; set; }

        public BaseActionFilterAttribute()
        {
            Order = 0;
        }

        public BaseActionFilterAttribute(int order)
        {
            Order = order;
        }
    }
}