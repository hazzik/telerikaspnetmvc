namespace Telerik.Web.Mvc.UI.Tests
{
    using Xunit;

    public class AutoCompleteHtmlBuilderTests
    {

        private AutoComplete AutoComplete;
        private AutoCompleteHtmlBuilder renderer;

        public AutoCompleteHtmlBuilderTests()
        {
            AutoComplete = AutoCompleteTestHelper.CreateAutocomplete();
            renderer = new AutoCompleteHtmlBuilder(AutoComplete);

            AutoComplete.Name = "Test";
        }

        [Fact]
        public void Build_should_output_input_tag()
        {
            IHtmlNode tag = renderer.Build();

            Assert.Equal("input", tag.TagName);
        }

        [Fact]
        public void Build_should_output_input_with_id_attribute_same_as_component_id()
        {
            AutoComplete.Name = "test.test";

            IHtmlNode tag = renderer.Build();

            Assert.Equal(AutoComplete.Id, tag.Attribute("id"));
        }

        [Fact]
        public void Build_should_output_input_with_name_attribute_same_as_component_name()
        {
            AutoComplete.Name = "test.test";

            IHtmlNode tag = renderer.Build();

            Assert.Equal(AutoComplete.Name, tag.Attribute("name"));
        }

        [Fact]
        public void Build_should_output_input_title_attribute_same_as_component_name()
        {
            AutoComplete.Name = "test.test";

            IHtmlNode tag = renderer.Build();

            Assert.Equal(AutoComplete.Name, tag.Attribute("title"));
        }

        [Fact]
        public void Build_should_output_input_with_Autocomplete_off()
        {
            IHtmlNode tag = renderer.Build();

            Assert.Equal("off", tag.Attribute("autocomplete"));
        }

        [Fact]
        public void Build_should_output_input_with_class_attribute()
        {
            IHtmlNode tag = renderer.Build();

            Assert.Equal(UIPrimitives.Widget + " t-autocomplete " + UIPrimitives.Input, tag.Attribute("class"));
        }

        [Fact]
        public void Build_should_output_html_attributes()
        {
            AutoComplete.HtmlAttributes.Add("style", "width: 100px;");

            IHtmlNode tag = renderer.Build();

            Assert.Equal("width: 100px;", tag.Attribute("style"));
        }
    }
}
