WebApi-OrderedFilters
=====================

Installation
------------

Install from Nuget: `Install-Package RichardLawley.WebApi.OrderedFilters`

In your WebApiConfig.cs (or wherever you configure WebAPI)

    // Change the Filter Provider to one which respects ordering
    config.Services.Replace(typeof(System.Web.Http.Filters.IFilterProvider), new OrderedFilterProvider());

Usage
-----

Derive your filters from BaseActionFilter, BaseAuthorizationFilter or BaseExceptionFilter, and set the Order property.
