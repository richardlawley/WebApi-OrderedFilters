using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace RichardLawley.WebApi.OrderedFilters.Tests
{
    public class OrderedFilterProviderTest
    {
        private IFilterProvider _filterProvider;

        private HttpConfiguration _configuration;

        private List<IFilter> _lstActionFilters;
        private Mock<HttpActionDescriptor> _mockActionDescriptor;

        private List<IFilter> _lstControllerFilters;
        private Mock<HttpControllerDescriptor> _mockControllerDescriptor;

        [SetUp]
        public void Setup()
        {
            _configuration = new HttpConfiguration();

            _lstActionFilters = new List<IFilter>();
            _lstControllerFilters = new List<IFilter>();

            _mockControllerDescriptor = new Mock<HttpControllerDescriptor>();
            _mockControllerDescriptor.Setup(d => d.GetFilters()).Returns(() => new Collection<IFilter>(_lstControllerFilters));

            _mockActionDescriptor = new Mock<HttpActionDescriptor>();
            _mockActionDescriptor.Setup(d => d.GetFilters()).Returns(() => new Collection<IFilter>(_lstActionFilters));
            _mockActionDescriptor.Object.ControllerDescriptor = _mockControllerDescriptor.Object;

            _filterProvider = new OrderedFilterProvider();
        }

        [Test]
        [TestCaseSource("GetFilters_OrderedTestCase")]
        public void GetFilters_ReturnsFiltersInCorrectOrder(IEnumerable<IFilter> input)
        {
            foreach (IFilter filter in input)
            {
                _configuration.Filters.Add(filter);
            }

            IEnumerable<FilterInfo> filters = _filterProvider.GetFilters(_configuration, _mockActionDescriptor.Object);

            filters.Count().ShouldBe(2);
            filters.ElementAt(0).Instance.ShouldBeOfType<OrderedFilter1>();
            filters.ElementAt(1).Instance.ShouldBeOfType<OrderedFilter2>();
        }

        private static object[] GetFilters_OrderedTestCase = {
                new object[] {new IFilter[] { new OrderedFilter1(), new OrderedFilter2()}},
                new object[] {new IFilter[] { new OrderedFilter2(), new OrderedFilter1()}},
            };

        [Test]
        [TestCaseSource("GetFiltersTestCase")]
        public void GetFilters_ReturnsOrderedFiltersBeforeUnorderedFilters(IEnumerable<IFilter> input)
        {
            foreach (IFilter filter in input)
            {
                _configuration.Filters.Add(filter);
            }

            IEnumerable<FilterInfo> filters = _filterProvider.GetFilters(_configuration, _mockActionDescriptor.Object);

            filters.Count().ShouldBe(2);
            filters.ElementAt(0).Instance.ShouldBeOfType<OrderedFilter1>();
            filters.ElementAt(1).Instance.ShouldBeOfType<UnorderedFilter>();
        }

        private static object[] GetFiltersTestCase = {
                new object[] {new IFilter[] { new OrderedFilter1(), new UnorderedFilter()}},
                new object [] { new IFilter[] {new UnorderedFilter(), new OrderedFilter1()}},
            };

        [Test]
        public void GetFilters_ChecksArguments()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                _filterProvider.GetFilters(null, _mockActionDescriptor.Object);
            });
            Should.Throw<ArgumentNullException>(() =>
            {
                _filterProvider.GetFilters(_configuration, null);
            });
        }

        [Test]
        public void WithDefaultConstructor_GlobalAndActionDescriptorFilterProvidersAreUsed()
        {
            IFilter testGlobalFilter = new UnorderedFilter();
            _configuration.Filters.Add(testGlobalFilter);

            IEnumerable<FilterInfo> filters = _filterProvider.GetFilters(_configuration, _mockActionDescriptor.Object);

            filters.ShouldContain(f => f.Instance == testGlobalFilter);
            _mockControllerDescriptor.Verify(d => d.GetFilters(), Times.Once(), "Controller Filters were not requested");
            _mockActionDescriptor.Verify(d => d.GetFilters(), Times.Once(), "Action Filters were not requested");
        }

        [Test]
        public void WithCustomConstructor_OnlyCustomActionDescriptorFilterProvidersAreUsed()
        {
            var mockFilterProvider = new Mock<IFilterProvider>();
            mockFilterProvider.Setup(p => p.GetFilters(It.IsAny<HttpConfiguration>(), It.IsAny<HttpActionDescriptor>())).Returns(() => Enumerable.Empty<FilterInfo>());

            IFilter testGlobalFilter = new UnorderedFilter();
            _configuration.Filters.Add(testGlobalFilter);

            _filterProvider = new OrderedFilterProvider(new[] { mockFilterProvider.Object });
            IEnumerable<FilterInfo> filters = _filterProvider.GetFilters(_configuration, _mockActionDescriptor.Object);

            filters.ShouldNotContain(f => f.Instance == testGlobalFilter);
            _mockControllerDescriptor.Verify(d => d.GetFilters(), Times.Never(), "Controller Filters should not have been requested");
            _mockActionDescriptor.Verify(d => d.GetFilters(), Times.Never(), "Action Filters should not have been requested");
        }
    }
}