<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ErrorView</h2>

    <%= Html.Telerik().Grid(Model)
            .Name("Grid1")
            .DataKeys(keys => keys.Add(c => c.Name))
            .ToolBar(toolbar => toolbar.Insert())
            .Columns(columns => 
                {
                    columns.Bound(c => c.Name).Format("<strong>{0}</strong>");
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
            .Pageable(pager => pager.PageSize(10))
    %>
<script type="text/javascript">
    
    function getGrid(selector) {
        return $(selector || "#Grid1").data("tGrid");
    }

    var errorView;
    
    function setUp() {
        errorView = new $.telerik.grid.ErrorView();
    }

    function test_should_set_inner_text_of_validation_span() {
        var $ui = $('<div><span id="ProductName_validationMessage" class="field-validation-valid"></span></div>')
        errorView.bind($ui, {ProductName:{errors:['Error']}});
        assertEquals('Error', $ui.find('span').html());
    }
    
    function test_should_set_the_className_of_the_validation_span() {
        var $ui = $('<div><span id="ProductName_validationMessage" class="field-validation-valid"></span></div>')
        errorView.bind($ui, {ProductName:{errors:['Error']}});
        assertEquals('field-validation-error', $ui.find('span').attr('className'));
    }

    function test_should_set_all_validators() {
        var $ui = $('<div><span id="ProductName_validationMessage" class="field-validation-valid"></span><span id="ProductID_validationMessage" class="field-validation-valid"></span></div>')
        
        errorView.bind($ui, {ProductName:{errors:['Error']}, ProductID:{errors:['Error']}});
        
        assertEquals('Error', $ui.find('span:eq(0)').html());
        assertEquals('field-validation-error', $ui.find('span:eq(0)').attr('className'));
        assertEquals('Error', $ui.find('span:eq(1)').html());
        assertEquals('field-validation-error', $ui.find('span:eq(1)').attr('className'));
    }

    function test_should_not_update_validators_which_dont_have_errors() {
        var $ui = $('<div><span id="ProductName_validationMessage" class="field-validation-valid"></span></div>')
        errorView.bind($ui, {ProductName:{errors:[]}});
        assertEquals('', $ui.find('span').html());
        assertEquals('field-validation-valid', $ui.find('span').attr('className'));
    }    
    
    function test_should_not_update_validators_when_there_are_no_errors() {
        var $ui = $('<div><span id="ProductName_validationMessage" class="field-validation-valid"></span></div>')
        errorView.bind($ui, {ProductName:{}});
        assertEquals('', $ui.find('span').html());
        assertEquals('field-validation-valid', $ui.find('span').attr('className'));
    }

    function test_should_clear_previous_validation_errors() {
        var $ui = $('<div><span id="ProductName_validationMessage" class="field-validation-error">Error</span></div>');
        errorView.bind($ui, {ProductName:{}});
        assertEquals('', $ui.find('span').html());
        assertEquals('field-validation-valid', $ui.find('span').attr('className'));
    }

    function test_should_set_className_of_textbox() {
        var $ui = $('<div><input id="ProductName" /></div>');
        
        errorView.bind($ui, {ProductName:{errors:['Error']}});
        
        assertEquals('input-validation-error', $ui.find('input').attr('className'));
    }
    
    function test_should_remove_valid_from_the_className_of_textbox() {
        var $ui = $('<div><input id="ProductName" class="textbox valid" /></div>');
        
        errorView.bind($ui, {ProductName:{errors:['Error']}});
        
        assertFalse('valid', $ui.find('input').hasClass('valid'));
    }

    function test_should_clear_invalid_class_and_restore_valid_class() {
        var $ui = $('<div><input id="ProductName" class="textbox input-validation-error" /></div>');
        
        errorView.bind($ui, {ProductName:{}});
        
        assertTrue($ui.find('input').hasClass('valid'));
        assertFalse($ui.find('input').hasClass('input-validation-error'));
    }

    function test_display_errors_calls_error_view() {
        var $form;
        var modelState;
        
        var grid = getGrid();

        grid.errorView.bind = function() {
            $form = arguments[0];
            modelState = arguments[1];
        }
        
        $('#Grid1 .t-grid-edit:first').click();
        grid.displayErrors({modelState:{}});
        
        assertNotUndefined(modelState);
        assertEquals('Grid1form', $form.attr('id'));
    }

</script>
</asp:Content>

