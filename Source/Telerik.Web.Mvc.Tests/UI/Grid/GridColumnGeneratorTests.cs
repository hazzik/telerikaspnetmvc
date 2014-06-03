namespace Telerik.Web.Mvc.UI.Tests
{
    using Xunit;
    
    public class GridColumnGeneratorTests
    {
        private GridColumnGenerator<Customer> generator;

        public GridColumnGeneratorTests()
        {
            generator = new GridColumnGenerator<Customer>(GridTestHelper.CreateGrid<Customer>());
        }

        [Fact]
        public void Should_create_bound_column_from_column_settings()
        {
            var columnSettings = new GridColumnSettings<Customer>
            {
                Member = "Name"
            };

            var column = (GridBoundColumn<Customer, string>)generator.CreateColumn(columnSettings);
            Assert.Equal("Name", column.Member);
        }
        
        [Fact]
        public void Should_set_bound_column_properties()
        {
            var settings = new GridColumnSettings<Customer>
            {
                Member = "Name",
                Sortable = false,
                ClientTemplate = "foo",
                Encoded = false,
                Filterable = false,
                Format = "{0}",
                Groupable = false,
                HeaderHtmlAttributes = { },
                Hidden = true,
                HtmlAttributes = { },
#if MVC2                
                ReadOnly = true,
#endif
                Title = "foo",
                Visible = false,
                Width = "100"
            };

            var column = (GridBoundColumn<Customer, string>)generator.CreateColumn(settings);
            Assert.Equal(column.Sortable, settings.Sortable);
            Assert.Equal(column.ClientTemplate, settings.ClientTemplate);
            Assert.Equal(column.Encoded, settings.Encoded);
            Assert.Equal(column.Filterable, settings.Filterable);
            Assert.Equal(column.Format, settings.Format);
            Assert.Equal(column.Groupable, settings.Groupable);
            Assert.Equal(column.HeaderHtmlAttributes, settings.HeaderHtmlAttributes);
            Assert.Equal(column.Hidden, settings.Hidden);
            Assert.Equal(column.HtmlAttributes, settings.HtmlAttributes);
#if MVC2                            
            Assert.Equal(column.ReadOnly, settings.ReadOnly);
#endif
            Assert.Equal(column.Title, settings.Title);
            Assert.Equal(column.Visible, settings.Visible);
            Assert.Equal(column.Width, settings.Width);
        }

        [Fact]
        public void Member_should_set_title()
        {
            var settings = new GridColumnSettings<Customer>
            {
                Member = "Name"
            };

            var column = (GridBoundColumn<Customer, string>)generator.CreateColumn(settings);
            Assert.Equal(column.Title, "Name");
        }
        
        [Fact]
        public void Should_set_template()
        {
            var settings = new GridColumnSettings<Customer>
            {
                Member = "Name",
                Template = delegate { }
            };

            var column = (GridBoundColumn<Customer, string>)generator.CreateColumn(settings);
            Assert.Equal(column.Template, settings.Template);
        }
    }
}
