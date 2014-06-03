// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions.UnitTest
{
    using System.Collections.Generic;

    using Xunit;

    public class EnumerableExtensionsTests
    {
        [Fact]
        public void Each_should_call_the_provided_action()
        {
            IEnumerable<int> list = new[]{ 4 };
            bool isCalled = false;

            list.Each(i=> isCalled = true);

            Assert.True(isCalled);
        }
    }
}