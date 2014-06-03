<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

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

</asp:Content>


<asp:Content ContentPlaceHolderID="TestContent" runat="server">

    <script type="text/javascript">

        function getGrid(selector) {
            return $(selector || "#Grid1").data("tGrid");
        }

        module("Grid / Binding", {
            teardown: function() {
                var grid = getGrid();
            
                $("tbody tr", grid.element).remove();
                for (var i = 0; i < 10; i++)
                    $("<tr><td/></tr>").appendTo($("tbody", grid.element));
            }
        });
        
        test('should removes rows when data length is less than page size', function() {
            var grid = getGrid();
            grid.dataBind([{}, {}]);

            equal($("tbody tr", grid.element).length, 2);
        });

        test('should create rows up to page size when they dont exist', function() {
            var grid = getGrid();
            $("tbody tr", grid.element).remove();
            grid.dataBind([{}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}]);
            equal($("tbody tr", grid.element).length, 10);
        });
        
        test('should bind columns with same name', function() {
            var grid = getGrid("#Grid2");

            grid.dataBind([{ Name: "Test"}]);
            
            equal($("tbody tr td:last", grid.element).html(), "Test");
            equal($("tbody tr:last td:first", grid.element).html(), "Test");
        });
        
        test('should apply alt style', function() {
            var grid = getGrid();
            $("tbody tr", grid.element).remove();
            grid.dataBind([{}, {}]);
            equal($("tbody tr", grid.element).length, 2);
            equal($("tbody tr:nth-child(2)", grid.element).attr("class"), "t-alt");
        });
        
        test('should serialize attributes', function() {
            var grid = getGrid("#Grid2");
            
            equal(grid.columns[0].attr, ' dir="rtl"');
        });
        
        test('should apply column html attributes', function() {
            var grid = getGrid('#Grid2');
            $('tbody tr', grid.element).remove();
            grid.dataBind([{}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}]);
            equal($('#Grid2 tbody tr:first td:first').attr('dir'), 'rtl');
        });

        test('rebind multiple arguments', function() {
            var grid = getGrid('#Grid2');
            var data;
            grid.ajaxRequest = function(additionalData) { data = additionalData; }
            grid.rebind({a:1,b:2});
            equal(data.a, 1);
            equal(data.b, 2);
        });

        test('binding to empty result clears the grid', function() {
            var grid = getGrid('#Grid3');
            grid.dataBind([]);
            equal($('tbody tr', grid.element).length, 1);
        });

        test('binding to empty result should return noRecords form localization', function() {
            var grid = getGrid('#Grid3');
            grid.dataBind([]);
            equal($('tbody td', grid.element).text(), grid.localization.noRecords);
        });

        test('binding to empty result should serialize noRecords', function() {
            var grid = getGrid('#Grid3');
            grid.dataBind([]);
            ok(grid.localization.noRecords != null);
        });

        test('binding to null result clears the grid', function() {
            var grid = getGrid('#Grid4');
            grid.dataBind(null);
            equal($('tbody tr', grid.element).length, 1);
        });

        test('date time null binding', function() {
            var grid = getGrid('#Grid5');
            ok(null === grid.columns[0].value({BirthDate:null}));
        });

        test('binding to null shows empty string', function() {
            var grid = getGrid('#Grid6');
            $("tbody tr", grid.element).remove();

            grid.dataBind([{ BirthDate: null}]);
            equal($('tbody tr:first td:first', grid.element).html(), '');
        });

        test('encoded is serialized', function() {
            var grid = getGrid('#Grid7');

            ok(undefined === grid.columns[0].encoded);
            ok(!grid.columns[1].encoded);
        });        
        
        test('should encode html when binding', function() {
            var grid = getGrid('#Grid7');
            equal(grid.displayFor(grid.columns[0])({Address:'<strong>foo</strong>'}), '&lt;strong&gt;foo&lt;/strong&gt;');
        });        

        test('should not encode html when column is not encoded', function() {
            var grid = getGrid('#Grid7');
            equal(grid.displayFor(grid.columns[1])({Name:'<strong>foo</strong>'}), '<strong>foo</strong>');
        });
        
        test('encoding and numeric columns', function() {
            var grid = getGrid('#Grid7');
            equal(grid.displayFor(grid.columns[2])({IntegerValue:1}), '1');
        });        
        
        test('encoding and zero', function() {
            var grid = getGrid('#Grid7');
            equal(grid.displayFor(grid.columns[2])({IntegerValue:0}), '0');
        });        
        
        test('encoding and null', function() {
            var grid = getGrid('#Grid7');
            equal(grid.displayFor(grid.columns[0])({Address:null}), '');
        });        
        
        test('encoding and undefined', function() {
            var grid = getGrid('#Grid7');
            equal(grid.displayFor(grid.columns[0])({}), '');
        });

        test('should encode html when format is set', function() {
            var grid = getGrid('#Grid7');
            equal(grid.displayFor(grid.columns[3])({Name:'foo'}), '&lt;strong&gt;foo&lt;/strong&gt;');
        });

        test('dataBind should raise repaint event', function() {
            var grid = getGrid('#Grid6');
            var raised = false;
            $(grid.element).bind('repaint', function() { raised = true; });
            grid.dataBind([{ BirthDate: null}]);
            equal(raised, true);
        });

    </script>

</asp:Content>