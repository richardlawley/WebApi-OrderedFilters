using System;
using System.Linq;
using System.Web.Http.Filters;

namespace RichardLawley.WebApi.OrderedFilters
{
    /// <summary>
    /// Exception Filter which can have the execution order specified
    /// </summary>
    public abstract class BaseExceptionFilterAttribute : ExceptionFilterAttribute, IOrderedFilterAttribute
    {
        /// <summary>
        /// Order of execution for this filter
        /// </summary>
        public int Order { get; set; }

        protected BaseExceptionFilterAttribute()
        {
            Order = 0;
        }

        protected BaseExceptionFilterAttribute(int order)
        {
            Order = order;
        }
    }
}