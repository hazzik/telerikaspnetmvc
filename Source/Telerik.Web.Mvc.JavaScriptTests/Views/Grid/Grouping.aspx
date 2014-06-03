<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <%= Html.Telerik().Grid(Model)
            .Name("Grid1")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate);
                columns.Bound(c => c.Active);
            })
            .Scrollable()
            .Groupable(grouping => grouping.Groups(groups => 
                {
                    groups.Add(c => c.Name);
                    groups.Add(c => c.BirthDate);
                }))
            .DataBinding(dataBinding => dataBinding.Ajax().Select("Foo", "Bar"))
        %>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid2")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate);
                columns.Bound(c => c.Active);
                columns.Bound(c => c.Active).Title("Ungroupable").Groupable(false);
            })
            .Scrollable()
            .Groupable()
            .DataBinding(dataBinding => dataBinding.Ajax().Select("Foo", "Bar"))
        %>
    
        <%= Html.Telerik().Grid(Model)
            .Name("Grid3")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate).Hidden(true);
                columns.Bound(c => c.Active);
            })
            .Scrollable()
            .Groupable()
            .DataBinding(dataBinding => dataBinding.Ajax().Select("Foo", "Bar"))
        %>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid4")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate);
                columns.Bound(c => c.Active);
            })
            .Scrollable()
            .Groupable(grouping => grouping.Groups(groups => 
                {
                    groups.Add(c => c.Name);
                    groups.Add(c => c.BirthDate);
                }))
            .DataBinding(dataBinding => dataBinding.Ajax().Select("Foo", "Bar"))
        %>
               
</asp:Content>


<asp:Content ContentPlaceHolderID="TestContent" runat="server">

<script type="text/javascript">

        function getGrid(selector) {
            return $(selector).data("tGrid");
        }

        module("Grid / Grouping", {
            setup: function() {
                getGrid('#Grid1').ajaxRequest = function() {};
                getGrid('#Grid2').ajaxRequest = function() {};
                getGrid('#Grid3').ajaxRequest = function() {};
                getGrid('#Grid4').ajaxRequest = function() {};
            }
        });

        test('ungrouping removes grouping columns', function() {
            var grid = getGrid('#Grid1');
            grid.unGroup('Birth Date');
            grid.normalizeColumns(grid.groups.length + grid.columns.length);
            
            equal($('table:first col', grid.element).length, 4)
            equal($('tr:has(th):first .t-group-cell', grid.element).length, 1)
        });

        test('ungrouping removes grouping columns hidden', function() {
            var grid = getGrid('#Grid4');
            grid.unGroup('Birth Date');
            grid.normalizeColumns(grid.groups.length + grid.columns.length);

            equal($('table:first col', grid.element).length, 4)
            equal($('tr:has(th):first .t-group-cell', grid.element).length, 1)
        });
        
        test('grouping creates grouping columns', function() {
            var grid = getGrid('#Grid2');
            grid.group('Name');
            grid.normalizeColumns(grid.groups.length + grid.columns.length);

            equal($('table:first col', grid.element).length, 5)
            equal($('tr:has(th):first .t-group-cell', grid.element).length, 1)
        });
        
        test('grouping creates grouping hidden', function() {
            var grid = getGrid('#Grid3');
            grid.group('Name');
            grid.normalizeColumns(grid.groups.length + grid.columns.length);

            equal($('table:first col', grid.element).length, 4)
            equal($('tr:has(th):first .t-group-cell', grid.element).length, 1)
        });

        test('ungroupable columns groupable serialized', function() {
            var grid = getGrid('#Grid2');
            ok(!grid.columns[grid.columns.length - 1].groupable);
        });

</script>

</asp:Content>