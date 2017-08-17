using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace RichardLawley.WebApi.OrderedFilters.Tests
{
    public class BaseFilterTest
    {
        [Test]
        public void BaseActionFilter_DefaultConstructor_SetsOrderToZero()
        {
            BaseActionFilterAttribute attribute = new OrderedActionFilter();
            attribute.Order.ShouldBe(0);
        }

        [Test]
        public void BaseActionFilter_ConstructorWithOrder_SetsOrderProperty()
        {
            const int value = 42;

            BaseActionFilterAttribute attribute = new OrderedActionFilter(value);
            attribute.Order.ShouldBe(value);
        }

        [Test]
        public void BaseAuthorizationFilter_DefaultConstructor_SetsOrderToZero()
        {
            BaseAuthorizationFilterAttribute attribute = new OrderedAuthorizationFilter();
            attribute.Order.ShouldBe(0);
        }

        [Test]
        public void BaseAuthorizationFilter_ConstructorWithOrder_SetsOrderProperty()
        {
            const int value = 42;

            BaseAuthorizationFilterAttribute attribute = new OrderedAuthorizationFilter(value);
            attribute.Order.ShouldBe(value);
        }

        [Test]
        public void BaseExceptionFilter_DefaultConstructor_SetsOrderToZero()
        {
            BaseExceptionFilterAttribute attribute = new OrderedExceptionFilter();
            attribute.Order.ShouldBe(0);
        }

        [Test]
        public void BaseExceptionFilter_ConstructorWithOrder_SetsOrderProperty()
        {
            const int value = 42;

            BaseExceptionFilterAttribute attribute = new OrderedExceptionFilter(value);
            attribute.Order.ShouldBe(value);
        }
    }
}