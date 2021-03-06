﻿using System;
using System.Linq;
using System.Web.Http.Filters;

namespace RichardLawley.WebApi.OrderedFilters
{
    /// <summary>
    /// Authorization Filter which can have the execution order specified
    /// </summary>
    public abstract class BaseAuthorizationFilterAttribute : AuthorizationFilterAttribute, IOrderedFilterAttribute
    {
        /// <summary>
        /// Order of execution for this filter
        /// </summary>
        public int Order { get; set; }

        protected BaseAuthorizationFilterAttribute()
        {
            Order = 0;
        }

        protected BaseAuthorizationFilterAttribute(int order)
        {
            Order = order;
        }
    }
}