<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Editing</h2>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid1")
            .DataKeys(keys => keys.Add(c => c.Name))
            .Columns(columns => 
                {
                    columns.Bound(c => c.Name)
                        .ClientTemplate("<strong><#= Name #></strong>");
                    
                    columns.Bound(c => c.BirthDate);
                    
                    columns.Command(commands =>
                    {
                        commands.Edit();
                        commands.Delete();
                    });

                    columns.Template(delegate { }).ClientTemplate("<strong><#= Name #></strong>");
                })
            .DataBinding(binding => binding.Ajax()
                .Select("Select", "Dummy")
                .Insert("Insert", "Dummy")
                .Delete("Delete", "Dummy")
                .Update("Update", "Dummy")
            )
            .Pageable(pager => pager.PageSize(10))
    %>

    <script type="text/javascript">

        function getGrid(selector) {
            return $(selector || "#Grid1").data("tGrid");
        }

        function test_client_template_is_serialized_for_bound_column() {
            assertEquals('<strong><#= Name #></strong>', getGrid().columns[0].template);
        }

        function test_client_template_is_serialized_for_template_column() {
            assertEquals('<strong><#= Name #></strong>', getGrid().columns[3].template);
            assertNotUndefined(getGrid().columns[3].display);
        }

        function test_template_is_applied_for_bound_column() {
            var grid = getGrid();
            var column = grid.columns[0];
            assertEquals("<strong>test</strong>", grid.displayFor(column)({ Name: 'test' }));
        }

        function test_template_is_applied_for_template_column() {
            var grid = getGrid();
            var column = grid.columns[3];
            assertEquals("<strong>test</strong>", grid.displayFor(column)({ Name: 'test' }));
        }

        function test_value_of_serialized_date() {
            var grid = getGrid();
            var column = grid.columns[1];
            assertTrue(grid.valueFor(column)(grid.data[0]) instanceof Date);
        }
    </script>

</asp:Content>
