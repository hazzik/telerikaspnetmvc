namespace Telerik.Web.Mvc.UI.UnitTest.Grid
{
    using System;

    using Xunit;

    using UI;

    public class GridColumnTests
    {
        [Fact]
        public void HeaderText_should_be_extracted_from_expression()
        {
            GridColumn<Customer> column = new GridColumn<Customer>(c => c.Id);

            Assert.Equal("Id", column.Title);
        }

        [Fact]
        public void Name_should_be_extracted_from_expression()
        {
            GridColumn<Customer> column = new GridColumn<Customer>(c => c.Id);
            Assert.Equal("Id", column.Name);
        }

        [Fact]
        public void Name_should_be_equal_to_member_when_complex_member_expression_is_supplied()
        {
            GridColumn<Customer> column = new GridColumn<Customer>(c => c.RegisterAt.Day);

            Assert.Equal("RegisterAt.Day", column.Name);
        }

        [Fact]
        public void HeaderText_should_be_null_in_case_of_invalid_expression()
        {
            Func<Customer, object> func = (c) => c.Id;

            GridColumn<Customer> column = new GridColumn<Customer>(c => func(c));

            Assert.Null(column.Title);
        }

        [Fact]
        public void Type_should_be_set()
        {
            GridColumn<Customer> column = new GridColumn<Customer>(c => c.Id);
            Assert.Equal(typeof(int), column.DataType);
        }
    }
}