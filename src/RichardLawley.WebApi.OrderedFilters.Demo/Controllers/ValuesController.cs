﻿using System;
using System.Linq;
using System.Web.Http;
using RichardLawley.WebApi.OrderedFilters.Demo.Filters;

namespace RichardLawley.WebApi.OrderedFilters.Demo.Controllers
{
    [TestOrderedFilter(2)]
    [TestOrderedFilter(1)]
    public class ValuesController : ApiController
    {
        // GET api/values
        [TestOrderedFilter(3)]
        public string Get()
        {
            return ControllerContext.Request.Properties[TestOrderedFilter.KEY].ToString();
        }
    }
}