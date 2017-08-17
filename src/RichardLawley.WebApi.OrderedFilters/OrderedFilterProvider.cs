using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RichardLawley.WebApi.OrderedFilters
{
    /// <summary>
    /// Combines Action Filters from multiple sources
    /// </summary>
    public class OrderedFilterProvider : IFilterProvider
    {
        private readonly List<IFilterProvider> _filterProviders = new List<IFilterProvider>();

        /// <summary>
        /// Constructor using default filter providers
        /// </summary>
        public OrderedFilterProvider()
        {
            _filterProviders.Add(new ConfigurationFilterProvider());
            _filterProviders.Add(new ActionDescriptorFilterProvider());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="innerProviders">The inner providers.</param>
        public OrderedFilterProvider(IEnumerable<IFilterProvider> innerProviders)
        {
            _filterProviders = innerProviders.ToList();
        }

        /// <summary>
        /// Returns all appropriate Filters for the specified action, sorted by their Order property if they have one
        /// </summary>
        public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            if (configuration == null) { throw new ArgumentNullException(nameof(configuration)); }
            if (actionDescriptor == null) { throw new ArgumentNullException(nameof(actionDescriptor)); }

            var filters = new List<OrderedFilterInfo>();

            foreach (IFilterProvider fp in _filterProviders)
            {
                filters.AddRange(
                    fp.GetFilters(configuration, actionDescriptor)
                        .Select(fi => new OrderedFilterInfo(fi.Instance, fi.Scope)));
            }

            IEnumerable<FilterInfo> orderedFilters = filters.OrderBy(i => i).Select(i => i.ConvertToFilterInfo());
            return orderedFilters;
        }
    }
}