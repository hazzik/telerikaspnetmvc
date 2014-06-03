<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Binding</h2>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid1")
            .Columns(columns =>columns.Bound(c => c.Name))
            .Ajax(settings => { })
            .Pageable(pager => pager.PageSize(10))
    %>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid2")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name).HtmlAttributes(new { dir="rtl" });
                columns.Bound(c => c.Name);
            })
            .Ajax(settings => { })
            .Pageable(pager => pager.PageSize(10))
    %>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid3")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
            })
            .Ajax(settings => { })
            .Pageable(pager => pager.PageSize(10))
    %>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid4")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
            })
            .Ajax(settings => { })
            .Pageable(pager => pager.PageSize(10))
    %>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid5")
            .Columns(columns =>
            {
                columns.Bound(c => c.BirthDate);
            })
            .Ajax(settings => { })
            .Pageable(pager => pager.PageSize(10))
    %>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid6")
            .Columns(columns =>
            {
                columns.Bound(c => c.BirthDate);
            })
            .Ajax(settings => { })
            .Pageable(pager => pager.PageSize(10))
    %>    
    
    <%= Html.Telerik().Grid(Model)
            .Name("Grid7")
            .Columns(columns =>
            {
                columns.Bound(c => c.Address);
                columns.Bound(c => c.Name).Encoded(false);
                columns.Bound(c => c.IntegerValue);
                columns.Bound(c => c.Name).Format("<strong>{0}</strong>");
            })
            .Ajax(settings => { })
            .Pageable(pager => pager.PageSize(10))
    %>
    <script type="text/javascript">
        
        function tearDown() {
            var grid = getGrid();
            
            $("tbody tr", grid.element).remove();
            for (var i = 0; i < 10; i++)
                $("<tr><td/></tr>").appendTo($("tbody", grid.element));
        }

        function getGrid(selector) {
            return $(selector || "#Grid1").data("tGrid");
        }

        function test_should_removes_rows_when_data_length_is_less_than_page_size() {
            var grid = getGrid();
            grid.dataBind([{}, {}]);

            assertEquals(2, $("tbody tr", grid.element).length);
        }

        function test_should_create_rows_up_to_page_size_when_they_dont_exist() {
            var grid = getGrid();
            $("tbody tr", grid.element).remove();
            grid.dataBind([{}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}]);
            assertEquals(10, $("tbody tr", grid.element).length);
        }
        
        function test_should_bind_columns_with_same_name() {
            var grid = getGrid("#Grid2");

            grid.dataBind([{ Name: "Test"}]);
            
            assertEquals("Test", $("tbody tr td:last", grid.element).html());
            assertEquals("Test", $("tbody tr:last td:first", grid.element).html());
        }
        
        function test_should_apply_alt_style() {
            var grid = getGrid();
            $("tbody tr", grid.element).remove();
            grid.dataBind([{}, {}]);
            assertEquals(2, $("tbody tr", grid.element).length);
            assertEquals("t-alt", $("tbody tr:nth-child(2)", grid.element).attr("class"));
        }
        
        function test_should_serialize_attributes() {
            var grid = getGrid("#Grid2");
            
            assertEquals(' dir="rtl"', grid.columns[0].attr);
        }
        
        function test_should_apply_column_html_attributes() {
            var grid = getGrid('#Grid2');
            $('tbody tr', grid.element).remove();
            grid.dataBind([{}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}]);
            assertEquals('rtl', $('#Grid2 tbody tr:first td:first').attr('dir'));
        }

        function test_rebind_multiple_arguments() {
            var grid = getGrid('#Grid2');
            grid.ajaxRequest = function() { }
            grid.rebind({a:1,b:2});
            assertEquals(location.href.replace(/(http:\/\/.*?\/)/, "/").toLowerCase() + '&a=1&b=2', grid.ajax.selectUrl.toLowerCase());
        }

        function test_binding_to_empty_result_clears_the_grid() {
            var grid = getGrid('#Grid3');
            grid.dataBind([]);
            assertEquals(1, $('tbody tr', grid.element).length);
        }

        function test_binding_to_empty_result_should_return_noRecords_form_localization() {
            var grid = getGrid('#Grid3');
            grid.dataBind([]);
            assertEquals(grid.localization.noRecords, $('tbody td', grid.element).text());
        }

        function test_binding_to_empty_result_should_serialize_noRecords() {
            var grid = getGrid('#Grid3');
            grid.dataBind([]);
            assertTrue(grid.localization.noRecords != null);
        }

        function test_binding_to_null_result_clears_the_grid() {
            var grid = getGrid('#Grid4');
            grid.dataBind(null);
            assertEquals(1, $('tbody tr', grid.element).length);
        }

        function test_date_time_null_binding() {
            var grid = getGrid('#Grid5');
            assertNull(grid.columns[0].value({BirthDate:null}));
        }

        function test_binding_to_null_shows_empty_string() {
            var grid = getGrid('#Grid6');
            $("tbody tr", grid.element).remove();

            grid.dataBind([{ BirthDate: null}]);
            assertEquals('', $('tbody tr:first td:first', grid.element).html());
        }

        function test_encoded_is_serialized() {
            var grid = getGrid('#Grid7');

            assertUndefined(grid.columns[0].encoded);
            assertFalse(grid.columns[1].encoded);
        }        
        
        function test_should_encode_html_when_binding() {
            var grid = getGrid('#Grid7');
            assertEquals('&lt;strong&gt;foo&lt;/strong&gt;', grid.displayFor(grid.columns[0])({Address:'<strong>foo</strong>'}));
        }        

        function test_should_not_encode_html_when_column_is_not_encoded() {
            var grid = getGrid('#Grid7');
            assertEquals('<strong>foo</strong>', grid.displayFor(grid.columns[1])({Name:'<strong>foo</strong>'}));
        }
        
        function test_encoding_and_numeric_columns() {
            var grid = getGrid('#Grid7');
            assertEquals('1', grid.displayFor(grid.columns[2])({IntegerValue:1}));
        }        
        
        function test_encoding_and_zero() {
            var grid = getGrid('#Grid7');
            assertEquals('0', grid.displayFor(grid.columns[2])({IntegerValue:0}));
        }        
        
        function test_encoding_and_null() {
            var grid = getGrid('#Grid7');
            assertEquals('', grid.displayFor(grid.columns[0])({Address:null}));
        }        
        
        function test_encoding_and_undefined() {
            var grid = getGrid('#Grid7');
            assertEquals('', grid.displayFor(grid.columns[0])({}));
        }

        function test_should_encode_html_when_format_is_set() {
            var grid = getGrid('#Grid7');
            assertEquals('&lt;strong&gt;foo&lt;/strong&gt;', grid.displayFor(grid.columns[3])({Name:'foo'}));
        }        

    </script>

</asp:Content>
