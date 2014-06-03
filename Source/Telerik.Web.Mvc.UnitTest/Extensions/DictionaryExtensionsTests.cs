// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions.UnitTest
{
    using System.Collections.Generic;

    using Xunit;

    public class DictionaryExtensionsTests
    {
        [Fact]
        public void Should_be_able_to_merge_item()
        {
            IDictionary<string, object> target = new Dictionary<string, object>();

            target.Merge("key", "value", true);

            Assert.True(target.ContainsKey("key"));
        }

        [Fact]
        public void Should_be_able_to_append_in_value()
        {
            IDictionary<string, object> target = new Dictionary<string, object>{{"key", "value"}};

            target.AppendInValue("key", " ", "value2");

            Assert.Equal("value value2", target["key"]);
        }

        [Fact]
        public void Should_be_able_to_get_as_attribute_string()
        {
            IDictionary<string, object> target = new Dictionary<string, object> { { "key1", "value1" }, { "key2", "value2" } };

            string attributeString = target.ToAttributeString();

            Assert.Equal(" key1=\"value1\" key2=\"value2\"", attributeString);
        }

        [Fact]
        public void Merge_with_dictionary_should_add_specified_items()
        {
            IDictionary<string, object> target = new Dictionary<string, object>();

            target.Merge(new Dictionary<string, object> { { "foo", "bar" } });

            Assert.Equal("bar", target["foo"]);
        }

        [Fact]
        public void Merge_with_dictionary_should_not_replace_the_existing_items()
        {
            IDictionary<string, object> target = new Dictionary<string, object> { { "foo", "bar" } };

            target.Merge(new Dictionary<string, object> { { "foo", "bar2" } }, false);

            Assert.Equal("bar", target["foo"]);
        }

        [Fact]
        public void Merge_with_object_should_add_specified_items()
        {
            IDictionary<string, object> target = new Dictionary<string, object>();

            target.Merge(new { foo = "bar" });

            Assert.Equal("bar", target["foo"]);
        }

        [Fact]
        public void Merge_with_object_should_not_replace_the_existing_items()
        {
            IDictionary<string, object> target = new Dictionary<string, object> { { "foo", "bar" } };

            target.Merge(new { foo = "bar2" }, false);

            Assert.Equal("bar", target["foo"]);
        }
    }
}