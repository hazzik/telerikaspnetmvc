<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ModelState</h2>

    <%= Html.Telerik().Grid(Model)
            .Name("Grid1")
            .DataKeys(keys => keys.Add(c => c.Name))
            .ToolBar(toolbar => toolbar.Insert())
            .Columns(columns => 
                {
                    columns.Bound(c => c.Name);
                    columns.Bound(c => c.BirthDate).Format("{0:d}");
                    columns.Bound(c => c.ReadOnly);
                    columns.Command(commands =>
                    {
                        commands.Edit();
                        commands.Delete();
                    });
                })
            .DataBinding(binding => binding.Ajax()
                .Select("Select", "Dummy")
                .Insert("Insert", "Dummy")
                .Delete("Delete", "Dummy")
                .Update("Update", "Dummy")
            )
            .Pageable()
    %>

    <script type="text/javascript">
    var grid;
    var originalHasErrors;
    var originalAjax;
    var originalDisplayErrors;

    function setUp() {
        grid = getGrid();
        originalHasErrors = grid.hasErrors;
        originalAjax = $.ajax;
        originalDisplayErrors = grid.displayErrors;
    }

    function tearDown() {
        grid.hasErrors = originalHasErrors;
        $.ajax = originalAjax;
        grid.displayErrors = originalDisplayErrors;
    }

    function getGrid(selector) {
        return $(selector || "#Grid1").data("tGrid");
    }    
    
    function test_grid_does_not_bind_if_hasErrors_returns_true() {
        $.ajax = function(options) {
            options.success('{data:[], modelState:{foo:{errors:[]}}}')
        }

        grid.hasErrors = function() { return true }
        
        var dataBound = false;
            
        $(grid.element).bind('dataBound', function() {
            dataBound = true;
        });

        grid.sendValues({}, 'updateUrl');

        assertFalse(dataBound);
    }     
    
    function test_grid_displays_errors_if_hasErrors_returns_true() {
        $.ajax = function(options) {
            options.success('{data:[], modelState:{foo:{errors:[]}}}')
        }

        var displayed = false;
        
        grid.displayErrors = function() { displayed = true };
        grid.sendValues({}, 'updateUrl');

        assertTrue(displayed);
    }    
    
    function test_grid_does_not_display_errors_if_hasErrors_returns_false() {
        $.ajax = function(options) {
            options.success('{data:[]}')
        }

        var displayed = false;
        
        grid.displayErrors = function() { displayed = true };
        grid.sendValues({}, 'updateUrl');

        assertFalse(displayed);
    }    
    
    function test_grid_binds_if_hasErrors_returns_false() {
        $.ajax = function(options) {
            options.success('{data:[]}')
        }
    
        grid.hasErrors = function() { return false }
        var dataBound = false;
            
        $(grid.element).bind('dataBound', function() {
            dataBound = true;
        });

        grid.sendValues({}, 'updateUrl');

        assertTrue(dataBound);
    }

    function test_hasErrors_returns_true_if_modelState_has_errors() {
        assertTrue(grid.hasErrors({modelState:{foo:{errors:[]}}}));
    }

    function test_hasErrors_returns_false_if_modelState_has_no_errors() {
        assertFalse(grid.hasErrors({modelState:{}}));
    }

    function test_hasErrors_returns_false_if_modelState_is_not_present() {
        assertFalse(grid.hasErrors({}));
    }

    </script>
</asp:Content>
