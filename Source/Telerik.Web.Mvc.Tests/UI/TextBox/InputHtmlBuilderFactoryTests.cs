namespace Telerik.Web.Mvc.UI.Tests
{
    using Xunit;


    public class InputHtmlBuilderFactoryTests
    {
        [Fact]
        public void Should_be_able_to_create_renderer()
        {
            TextboxBaseHtmlBuilderFactory<int> factory = new TextboxBaseHtmlBuilderFactory<int>();

            ITextBoxBaseHtmlBuilder renderer = factory.Create(TextBoxBaseTestHelper.CreateInput<int>(null, null));

            Assert.IsType<TextboxBaseHtmlBuilder<int>>(renderer);
        }
    }
}
