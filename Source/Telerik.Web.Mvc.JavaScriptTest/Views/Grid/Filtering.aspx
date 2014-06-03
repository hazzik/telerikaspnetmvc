<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTest.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Filtering</h2>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid1")
            .Columns(columns =>
            {
                columns.Add(c => c.Name);
                columns.Add(c => c.BirthDate.Date);
                columns.Add(c => c.BirthDate.Day);
                columns.Add(c => c.Active);
            })
            .Ajax(settings => { })
            .Pageable()
            .Filterable()
    %>
    
    <script type="text/javascript">
    
        function getGrid(selector) {
            return $(selector || "#Grid1").data("tGrid");
        }
    
        function tearDown() {
            var grid = getGrid();
            $.each(grid.columns, function() {this.filters = [];});
        }
        
        function test_single_filter_expr_for_one_column() {
            var grid = getGrid();
            
            grid.columns[0].filters = [
            {
                operator: "eq",
                value:"10"
            }];
            
            assertEquals("Name~eq~'10'", grid.filterExpr())
        }
        
        function test_filter_expr_for_bool_column() {
            var grid = getGrid();
            
            grid.columns[3].filters = [
            {
                operator: "eq",
                value:true
            }];
            
            assertEquals("Active~eq~true", grid.filterExpr())
        }
        
        function test_single_filter_expr_for_one_column_when_operator_is_function() {
            var grid = getGrid();
            
            grid.columns[0].filters = [
            {
                operator: "startswith",
                value:"10"
            }];
            
            assertEquals("startswith(Name,'10')", grid.filterExpr())
        }
        
        function test_two_filters_for_one_column() {
            var grid = getGrid();
            
            grid.columns[0].filters = [{
                operator: "startswith",
                value:"10"
            },
            {
                operator: "eq",
                value:"10"
            }];
            
            assertEquals("startswith(Name,'10')~and~Name~eq~'10'", grid.filterExpr())
        }
        
        function test_two_filters_for_one_column() {
            var grid = getGrid();
            
            grid.columns[0].filters = [
            {
                operator: "startswith",
                value:"10"
            },
            {
                operator: "eq",
                value:"10"
            }];
            
            assertEquals("startswith(Name,'10')~and~Name~eq~'10'", grid.filterExpr())
        }
        
        function test_encode_number() {
            var grid = getGrid();
            var encoded = grid.encodeFilterValue(2, 10);
            assertEquals(10, encoded);
        }
        
        function test_encode_string() {
            var grid = getGrid();
            var encoded = grid.encodeFilterValue(0, "test");
            assertEquals("'test'", encoded);
        }
        
        function test_escape_quotes_in_string() {
            var grid = getGrid();
            var encoded = grid.encodeFilterValue(0, "t'est");
            assertEquals("'t''est'", encoded);
        }
        
        function test_encode_date() {
            var grid = getGrid();
            
            var encoded = grid.encodeFilterValue(1, '10/11/2000');
            assertEquals("datetime'2000-10-11T12-00-00'", encoded);
        }
        
        function test_validate_number_fails_when_string() {
            var grid = getGrid();
            assertFalse(grid.isValidFilterValue(grid.columns[2], "string"));
        }
        
        function test_validate_number_succeeds_when_whole_number() {
            var grid = getGrid();
            assertTrue(grid.isValidFilterValue(grid.columns[2], "1"));
        }
        
        function test_validate_number_succeeds_when_decimal_number() {
            var grid = getGrid();
            assertTrue(grid.isValidFilterValue(grid.columns[2], "3.14"));
        }
        
        function test_validate_number_succeeds_when_decimal_number_without_whole_part() {
            var grid = getGrid();
            assertTrue(grid.isValidFilterValue(grid.columns[2], ".14"));
        }
        
        function test_validate_number_succeeds_when_positive_number() {
            var grid = getGrid();
            assertTrue(grid.isValidFilterValue(grid.columns[2], "+1"));
        }
        
        function test_validate_number_succeeds_when_negative_number() {
            var grid = getGrid();
            assertTrue(grid.isValidFilterValue(grid.columns[2], "-1"));
        }
        
        function test_validate_string_succeeds_always() {
            var grid = getGrid();
            assertTrue(grid.isValidFilterValue(grid.columns[0], "anything"));
        }
    </script>
    
</asp:Content>
