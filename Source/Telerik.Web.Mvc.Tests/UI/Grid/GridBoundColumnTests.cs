namespace Telerik.Web.Mvc.UI.Tests.Grid
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using Telerik.Web.Mvc.UI;
    using Telerik.Web.Mvc.UI.Html;
    using Xunit;

    public class GridBoundColumnTests
    {
        public class Product
        {
            public string Name
            {
                get;
                set;
            }

            public Category Category
            {
                get;
                set;
            }
        }

        public class Category
        {
            public string Name
            {
                get;
                set;
            }

            public User Owner
            {
                get;
                set;
            }
        }

        public class User
        {
            [DisplayName("UserName")]
            [DisplayFormat(DataFormatString="{0}")]
            [ReadOnly(true)]
            public string Name
            {
                get;
                set;
            }

            public bool Active
            {
                get;
                set;
            }
        }

        [Fact]
        public void HeaderText_should_be_extracted_from_expression()
        {
            GridBoundColumn<Customer, int> column = new GridBoundColumn<Customer, int>(GridTestHelper.CreateGrid<Customer>(), c => c.Id);

            Assert.Equal("Id", column.Title);
        }

        [Fact]
        public void Name_should_be_extracted_from_expression()
        {
            GridBoundColumn<Customer, int> column = new GridBoundColumn<Customer, int>(GridTestHelper.CreateGrid<Customer>(), c => c.Id);
            Assert.Equal("Id", column.Member);
        }

        [Fact]
        public void Name_should_be_equal_to_member_when_complex_member_expression_is_supplied()
        {
            GridBoundColumn<Customer, int> column = new GridBoundColumn<Customer, int>(GridTestHelper.CreateGrid<Customer>(), c => c.RegisterAt.Day);

            Assert.Equal("RegisterAt.Day", column.Member);
        }

        [Fact]
        public void Name_should_be_extracted_correctly_from_nested_expression()
        {
            GridBoundColumn<Product, string> column = new GridBoundColumn<Product, string>(GridTestHelper.CreateGrid<Product>(), p => p.Category.Owner.Name);

            Assert.Equal("Category.Owner.Name", column.Member);
        }

        [Fact]
        public void Type_should_be_set()
        {
            GridBoundColumn<Customer, int> column = new GridBoundColumn<Customer, int>(GridTestHelper.CreateGrid<Customer>(), c => c.Id);
            Assert.Equal(typeof(int), column.MemberType);
        }

        [Fact]
        public void Throws_on_invalid_expression()
        {
            Assert.Throws<InvalidOperationException>(() => new GridBoundColumn<Customer, string>(GridTestHelper.CreateGrid<Customer>(), c => c.Id.ToString()));
        }

        [Fact]
        public void ClientTemplate_sanitized()
        {
            GridBoundColumn<User, string> column = new GridBoundColumn<User, string>(GridTestHelper.CreateGrid<User>(), u => u.Name);

            column.ClientTemplate = "&lt;#= Name #>";
            Assert.Equal("<#= Name #>", column.ClientTemplate);
        }

        [Fact]
        public void Should_set_the_value_of_the_builder()
        {
            GridBoundColumn<User, string> column = new GridBoundColumn<User, string>(GridTestHelper.CreateGrid<User>(), u => u.Name);

            var builder = column.CreateDisplayHtmlBuilder(new GridCell<User>(column, new User
                    {
                        Name = "User"
                    })) as GridDataCellHtmlBuilder<User>;

            Assert.Equal("User", builder.Value);
        }

        [Fact]
        public void Should_create_template_builder_if_template_is_set()
        {
            var column = new GridBoundColumn<User, bool>(GridTestHelper.CreateGrid<User>(), u => u.Active);
            column.Template = delegate { };
            var builder = column.CreateDisplayHtmlBuilder(new GridCell<User>(column, new User())) as GridTemplateCellHtmlBuilder<User>;
            Assert.NotNull(builder);
        }        
        
        [Fact]
        public void Should_create_template_builder_if_inline_template_is_set()
        {
            var column = new GridBoundColumn<User, bool>(GridTestHelper.CreateGrid<User>(), u => u.Active);
            column.InlineTemplate = delegate { return null; };
            var builder = column.CreateDisplayHtmlBuilder(new GridCell<User>(column, new User())) as GridTemplateCellHtmlBuilder<User>;
            Assert.NotNull(builder);
        }

        [Fact]
        public void Should_throw_if_method_call()
        {
            Assert.Throws<InvalidOperationException>(() => new GridBoundColumn<User, string>(GridTestHelper.CreateGrid<User>(), u => u.Active.ToString()));
        }
        
        [Fact]
        public void Should_support_parameter_expression()
        {
            new GridBoundColumn<User, User>(GridTestHelper.CreateGrid<User>(), u => u);
        }

#if MVC2 || MVC3
        [Fact]
        public void Readonly_is_populated_from_metadata()
        {
            GridBoundColumn<User, string> column = new GridBoundColumn<User, string>(GridTestHelper.CreateGrid<User>(), u => u.Name);

            Assert.Equal(true, column.ReadOnly);
        }

        [Fact]
        public void Should_create_displayfor_builder()
        {
            var column = new GridBoundColumn<User, bool>(GridTestHelper.CreateGrid<User>(), u => u.Active);
            var builder = column.CreateDisplayHtmlBuilder(new GridCell<User>(column, new User()));
            Assert.IsType<GridDisplayForCellHtmlBuilder<User, bool>>(builder);
        }
        
        [Fact]
        public void Should_create_boundbuilder_when_bound_to_data_row()
        {
            var column = new GridBoundColumn<DataRow, string>(GridTestHelper.CreateGrid<DataRow>(), dr => (string)dr["Foo"]);
            DataTable table = new DataTable();
            table.Columns.Add("Foo");
            table.Rows.Add(new object[]{"Bar"});
            var builder = column.CreateDisplayHtmlBuilder(new GridCell<DataRow>(column, table.Rows[0]));
            Assert.IsType<GridDataCellHtmlBuilder<DataRow>>(builder);
        }

        [Fact]
        public void Should_create_editor_builder_when_in_edit_mode()
        {
            var column = new GridBoundColumn<User, bool>(GridTestHelper.CreateGrid<User>(), u => u.Active);
            var cell = new GridCell<User>(column, new User())
            {
                InEditMode = true
            };

            var builder = column.CreateEditorHtmlBuilder(cell);
            Assert.IsType<GridEditorForCellHtmlBuilder<User, bool>>(builder);
        }

        [Fact]
        public void Should_create_editor_builder_when_in_insert_mode()
        {
            var column = new GridBoundColumn<User, bool>(GridTestHelper.CreateGrid<User>(), u => u.Active);
            var cell = new GridCell<User>(column, new User())
            {
                InInsertMode = true
            };

            var builder = column.CreateEditorHtmlBuilder(cell);
            Assert.IsType<GridEditorForCellHtmlBuilder<User, bool>>(builder);
        }
        
        [Fact]
        public void Should_return_display_builder_if_readonly()
        {
            var column = new GridBoundColumn<User, bool>(GridTestHelper.CreateGrid<User>(), u => u.Active);
            column.ReadOnly = true;

            var cell = new GridCell<User>(column, new User())
            {
                InInsertMode = true,
            };

            var builder = column.CreateEditorHtmlBuilder(cell);
            Assert.IsType<GridDisplayForCellHtmlBuilder<User, bool>>(builder);
        }

        [Fact]
        public void Should_not_use_display_for_if_format_is_set()
        {
            var column = new GridBoundColumn<User, bool>(GridTestHelper.CreateGrid<User>(), u => u.Active);
            column.Format = "{0}";
            var builder = column.CreateDisplayHtmlBuilder(new GridCell<User>(column, new User()));
            Assert.IsType<GridDataCellHtmlBuilder<User>>(builder);
        }

        [Fact]
        public void Should_not_use_display_for_if_encoded_is_false()
        {
            var column = new GridBoundColumn<User, bool>(GridTestHelper.CreateGrid<User>(), u => u.Active);
            column.Encoded = false;
            var builder = column.CreateDisplayHtmlBuilder(new GridCell<User>(column, new User()));
            Assert.IsType<GridDataCellHtmlBuilder<User>>(builder);
        }
#endif
    }
}