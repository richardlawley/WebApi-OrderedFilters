using System;
using System.Linq;
using NUnit.Framework;

namespace RichardLawley.WebApi.OrderedFilters.Tests
{
    public class BaseFilterTest
    {
        [Test]
        public void BaseActionFilter_DefaultConstructor_SetsOrderToZero()
        {
            BaseActionFilterAttribute attribute = new OrderedActionFilter();
            Assert.AreEqual(0, attribute.Order);
        }

        [Test]
        public void BaseActionFilter_ConstructorWithOrder_SetsOrderProperty()
        {
            const int value = 42;

            BaseActionFilterAttribute attribute = new OrderedActionFilter(value);
            Assert.AreEqual(value, attribute.Order);
        }

        [Test]
        public void BaseAuthorizationFilter_DefaultConstructor_SetsOrderToZero()
        {
            BaseAuthorizationFilterAttribute attribute = new OrderedAuthorizationFilter();
            Assert.AreEqual(0, attribute.Order);
        }

        [Test]
        public void BaseAuthorizationFilter_ConstructorWithOrder_SetsOrderProperty()
        {
            const int value = 42;

            BaseAuthorizationFilterAttribute attribute = new OrderedAuthorizationFilter(value);
            Assert.AreEqual(value, attribute.Order);
        }

        [Test]
        public void BaseExceptionFilter_DefaultConstructor_SetsOrderToZero()
        {
            BaseExceptionFilterAttribute attribute = new OrderedExceptionFilter();
            Assert.AreEqual(0, attribute.Order);
        }

        [Test]
        public void BaseExceptionFilter_ConstructorWithOrder_SetsOrderProperty()
        {
            const int value = 42;

            BaseExceptionFilterAttribute attribute = new OrderedExceptionFilter(value);
            Assert.AreEqual(value, attribute.Order);
        }
    }
}