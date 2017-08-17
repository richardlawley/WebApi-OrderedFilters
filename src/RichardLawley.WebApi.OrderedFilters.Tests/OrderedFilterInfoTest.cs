using System;
using System.Linq;
using System.Web.Http.Filters;
using NUnit.Framework;
using Shouldly;

namespace RichardLawley.WebApi.OrderedFilters.Tests
{
    public class OrderedFilterInfoTest
    {
        [Test]
        public void OrderedFilterInfo_Constructor_ChecksArguments()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OrderedFilterInfo(null, FilterScope.Action);
            });
        }

        [Test]
        public void OrderedFilterInfo_CompareTo_SortsByOrderField()
        {
            var filter1 = new OrderedFilterInfo(new OrderedActionFilter(1), FilterScope.Action);
            var filter2 = new OrderedFilterInfo(new OrderedActionFilter(2), FilterScope.Action);
            var filter3 = new OrderedFilterInfo(new OrderedActionFilter(3), FilterScope.Action);

            filter1.CompareTo(filter2).ShouldBeLessThan(0);
            filter1.CompareTo(filter3).ShouldBeLessThan(0);

            filter2.CompareTo(filter1).ShouldBeGreaterThan(0);
        }

        [Test]
        public void OrderedFilterInfo_ThrowsException_GivenIncorrectType()
        {
            var filter1 = new OrderedFilterInfo(new OrderedFilter1(), FilterScope.Action);
            var filter2 = new FilterInfo(new UnorderedFilter(), FilterScope.Action);

            Assert.Throws<ArgumentException>(() =>
            {
                filter1.CompareTo(filter2);
            });
        }

        [Test]
        public void CompareTo_BothOrderedAttributes_SortsByOrder()
        {
            var filter1 = new OrderedFilterInfo(new OrderedFilter1(), FilterScope.Action);
            var filter2 = new OrderedFilterInfo(new OrderedFilter2(), FilterScope.Action);

            filter1.CompareTo(filter2).ShouldBeLessThan(0);
            filter2.CompareTo(filter1).ShouldBeGreaterThan(0);
        }

        [Test]
        public void CompareTo_BothOrderedAttributesSameOrder_SortsByName()
        {
            var filter1 = new OrderedFilterInfo(new OrderedFilter1(), FilterScope.Action);
            var filter2 = new OrderedFilterInfo(new OrderedFilter1a(), FilterScope.Action);

            filter1.CompareTo(filter2).ShouldBeLessThan(0);
            filter2.CompareTo(filter1).ShouldBeGreaterThan(0);
        }

        [Test]
        public void CompareTo_OneOrderedAttribute_OrderedAttributeIsLower()
        {
            var filter1 = new OrderedFilterInfo(new OrderedFilter1(), FilterScope.Action);
            var filter2 = new OrderedFilterInfo(new UnorderedFilter(), FilterScope.Action);

            filter1.CompareTo(filter2).ShouldBeLessThan(0);
            filter2.CompareTo(filter1).ShouldBeGreaterThan(0);
        }

        [Test]
        public void CompareTo_BothUnorderedAttributes_SortsByName()
        {
            var filter1 = new OrderedFilterInfo(new UnorderedFilter(), FilterScope.Action);
            var filter2 = new OrderedFilterInfo(new UnorderedFilter2(), FilterScope.Action);

            filter1.CompareTo(filter2).ShouldBeLessThan(0);
            filter2.CompareTo(filter1).ShouldBeGreaterThan(0);
        }

        [Test]
        public void CompareTo_SortsGlobalFiltersFirst()
        {
            var filter1 = new OrderedFilterInfo(new UnorderedFilter(), FilterScope.Action);
            var filter2 = new OrderedFilterInfo(new UnorderedFilter2(), FilterScope.Global);

            filter1.CompareTo(filter2).ShouldBeGreaterThan(0);
            filter2.CompareTo(filter1).ShouldBeLessThan(0);
        }
    }
}