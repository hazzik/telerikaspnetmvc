<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>FormView</h2>
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

    <div id="numeric">
        <%= Html.Telerik().NumericTextBox()
                .Name("NumericTextBox")
        %>
    </div>    
    
    <div id="dropdown">
        <%= Html.Telerik().DropDownList()
                .Name("DropDownList")
                .Items(items =>
                {
                    items.Add().Text("foo");
                    items.Add().Text("bar");
                }) 
        %>
    </div>    
    <div id="combobox">
        <%= Html.Telerik().ComboBox()
                .Name("ComboBox")
                .Items(items =>
                {
                    items.Add().Text("foo");
                    items.Add().Text("bar");
                }) 
        %>
    </div>
    <script type="text/javascript">
    
    function getGrid(selector) {
        return $(selector || "#Grid1").data("tGrid");
    }
    
    var FormViewBinder;
    var binder;
    
    function setUp() {
        FormViewBinder = $.telerik.grid.FormViewBinder;
        binder = new FormViewBinder();
    }

    function test_bind_populates_textbox() {
        var $ui = $('<div><input type="text" name="foo" /></div>');
        
        binder.bind($ui, {foo:'bar'});

        assertEquals('bar', $ui.find(':input').val());
    }
    
    function test_bind_checkes_checkboxes() {
        var $ui = $('<div><input type="checkbox" name="foo" /></div>');
        
        binder.bind($ui, {foo:true});

        assertEquals(true, $ui.find(':checkbox').attr('checked'));
    }
    
    function test_bind_does_not_set_null() {
        var $ui = $('<div><input type="text" name="foo" /></div>');
        
        binder.bind($ui, {foo:null});

        assertEquals('', $ui.find(':input').val());
    }    
    
    function test_bind_populates_text_area() {
        var $ui = $('<div><textarea name="foo" ></textarea></div>');
        
        binder.bind($ui, {foo:'bar'});

        assertEquals('bar', $ui.find('textarea').val());
    }

    function test_bind_populates_select() {
        var $ui = $('<div><select name="foo"><option>foo</option><option>bar</option></select></div>');
        
        binder.bind($ui, {foo:'bar'});

        assertEquals('bar', $ui.find('select').val());
    }    
    
    function test_bind_populates_numeric_text_box() {
        var $ui = $('#numeric');
        
        binder.bind($ui, {NumericTextBox: 42});

        assertEquals(42, $('#NumericTextBox').data('tTextBox').value());
        assertEquals('42.00', $('#NumericTextBox-input-text').val());
    }
    
    function test_bind_skips_inputs_whose_name_does_not_match() {
        var $ui = $('<div><input type="text" name="bar" /></div>');
        
        binder.bind($ui, {foo:'baz'});

        assertEquals('', $ui.find(':input').val());
    }

    function test_eval_for_prefixed_column() {
        assertEquals('Customer1', binder.evaluate({Name:'Customer1'}, 'm.Name'));
    }

    function test_eval_uses_converters() {
        var binder = new FormViewBinder({'number':function(name,value){return -value;}});
        
        assertEquals(-42, binder.evaluate({foo:42}, 'foo'));
    }
        
    function test_eval_for_nested_property() {
        assertEquals('foo', binder.evaluate({Address:{Street:'foo'}}, 'Address.Street'));
    }
        
    function test_eval_for_property_which_does_not_have_a_column() {
        assertEquals(true, binder.evaluate({Active:true}, 'Active'));
    }

    function test_eval_for_nested_property_with_prefix() {
        assertEquals('foo', binder.evaluate({Address:{Street:'foo'}}, 'm.Address.Street'));
    }

    function test_eval_returns_undefined_with_invalid_expression_with_valid_parts() {
        assertUndefined(binder.evaluate({Address:{Street:'foo'}}, 'm.Address.Foo'));
    }
        
    function test_eval_with_more_than_one_prefix() {
        assertEquals('foo', binder.evaluate({Address:{Street:'foo'}}, 'foo.bar.Address.Street'));
    }
        
    function test_eval_returns_undefined_with_invalid_expression() {
        assertUndefined(binder.evaluate({}, 'foo'));
    }
    
    function test_eval_returns_date_time_for_serialized_dates() {
        var value = binder.evaluate({ Date: "/Date(315525600000)/" }, 'Date');
        assertEquals(1, value.getDate());
        assertEquals(1, value.getMonth() + 1);
        assertEquals(1980, value.getFullYear());
    }

    function test_eval_with_non_existent_name() {
        var value = binder.evaluate({ }, 'Foo');
        assertUndefined(value);
    }    
    
    function test_eval_nested_member_of_null() {
        var value = binder.evaluate({ Foo: null }, 'Foo.Bar');
        assertUndefined(value);
    }
    
    function test_eval_nested_member_of_primitive_type() {
        var value = binder.evaluate({ Foo: true}, 'Foo.Bar');
        assertUndefined(value);
    }
    
    function test_eval_returns_undefined_when_expression_is_null() {
        var value = binder.evaluate({}, null);
        assertUndefined(value);
    }
    
    function test_eval_returns_undefined_when_expression_is_undefined() {
        var value = binder.evaluate({}, undefined);
        assertUndefined(value);
    }

    function test_eval_returns_undefined_when_expression_is_empty_string() {
        var value = binder.evaluate({}, '');
        assertUndefined(value);
    }    
    
    function test_eval_returns_undefined_when_model_is_null() {
        var value = binder.evaluate(null, 'foo');
        assertUndefined(value);
    }

    function test_evaluate_returns_undefined_for_non_primitive_values() {
        var value = binder.evaluate({ Foo: {} }, 'Foo');
        assertUndefined(value);
    }
    
    function test_eval_returns_undefined_when_model_is_undefined() {
        var value = binder.evaluate(undefined, 'foo');
        assertUndefined(value);
    }

    function test_bind_populates_dropdownlist() {
        var $ui = $('#dropdown');
        binder.bind($ui, {DropDownList: 'bar'});

        assertEquals('bar', $('#DropDownList').data('tDropDownList').value());
        assertEquals('bar', $('#DropDownList .t-input').text());
    }    
    
    function test_bind_populates_combobox() {
        var $ui = $('#combobox');
        
        binder.bind($ui, {ComboBox: 'bar'});

        assertEquals('bar', $('#ComboBox').data('tComboBox').value());
        assertEquals('bar', $('#ComboBox .t-input').val());
    }    
    
    </script>
</asp:Content>
