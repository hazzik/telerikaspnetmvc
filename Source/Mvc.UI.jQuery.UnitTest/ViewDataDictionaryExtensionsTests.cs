// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System.Web.Mvc;
    
    using Telerik.Web.Mvc.Infrastructure;

    using Xunit;

    public class ViewDataDictionaryExtensionsTests
    {
        private readonly ViewDataDictionary _viewData;

        public ViewDataDictionaryExtensionsTests()
        {
            _viewData = new ViewDataDictionary();
        }

        [Fact]
        public void GetConvertedModelStateValue_should_convert_successfully()
        {
            _viewData.ModelState.Add("test", new ModelState { Value = new ValueProviderResult("5", "5", Culture.Invariant) });

            Assert.NotNull(_viewData.GetConvertedModelStateValue("test", typeof(int)));
        }

        [Fact]
        public void HasModeStateError_should_return_false_when_error_collection_is_empty()
        {
            _viewData.ModelState.Add("test", new ModelState { Value = new ValueProviderResult("5", "5", Culture.Invariant) });

            Assert.False(_viewData.HasModeStateError("test"));
        }
    }
}