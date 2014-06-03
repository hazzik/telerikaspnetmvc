<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" Culture="de-DE"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.Telerik().ScriptRegistrar().Globalization(true); %>

    <h2>ModelBinder</h2>

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
                .MinValue( double.MinValue)
                .MaxValue( double.MaxValue)
                .Value(1231234.12)
        %>
    </div>

    <div id="integer">
        <%= Html.Telerik().IntegerTextBox()
                .Name("IntegerTextBox")
                .MinValue( int.MinValue)
                .MaxValue( int.MaxValue)
                .Value(1231234)
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
                .SelectedIndex(1)
        %>
    </div>
    
    <script type="text/javascript">
    
    function getGrid(selector) {
        return $(selector || "#Grid1").data("tGrid");
    }
    
    var ModelBinder;
    var binder;
    
    function setUp() {
        ModelBinder = $.telerik.grid.ModelBinder;
        binder = new ModelBinder();
    }
    
    function test_bind_textbox() {
        var $ui = $('<div><input type="text" name="foo" value="bar" /></div>');
        
        var model = binder.bind($ui);

        assertEquals('bar', model.foo);
    }

    function test_bind_skips_disabled_elements() {
        var $ui = $('<div><input type="text" name="foo" value="bar" disabled="disabled" /></div>');
        
        var model = binder.bind($ui);

        assertUndefined('bar', model.foo);
    }

    function test_bind_checked_checkbox_yields_true() {
        var $ui = $('<div><input type="checkbox" name="foo" checked="checked" /></div>');
        
        var model = binder.bind($ui);

        assertEquals(true, model.foo);
    }

    function test_bind_unchecked_checkbox_yields_false() {
        var $ui = $('<div><input type="checkbox" name="foo" /></div>');
        
        var model = binder.bind($ui);

        assertEquals(false, model.foo);
    }

    function test_bind_textarea() {
        var $ui = $('<div><textarea name="foo">bar</textarea></div>');
        
        var model = binder.bind($ui);

        assertEquals('bar', model.foo);
    }

    function test_bind_select() {
        var $ui = $('<div><select name="foo"><option>foo</option><option selected="selected">bar</option></select></div>');
        
        var model = binder.bind($ui);

        assertEquals('bar', model.foo);
    }

    function test_bind_populates_numeric_text_box() {
        var $ui = $('#numeric');
        
        var model = binder.bind($ui);

        assertEquals('<%= 1231234.12 %>', model.NumericTextBox);
    }

    function test_bind_populates_integer_text_box()
    {
        var $ui = $('#integer');
        
        var model = binder.bind($ui);

        assertEquals('<%= 1231234 %>', model.IntegerTextBox);
    } 

    function test_bind_dropdownlist() {
        var $ui = $('#dropdown');
        
        var model = binder.bind($ui);

        assertEquals('foo', model.DropDownList);
    }
    
    function test_bind_combobox() {
        var $ui = $('#combobox');
        
        var model = binder.bind($ui);
        assertEquals('bar', model.ComboBox);
    }

    function test_bind_nested() {
        var $ui = $('<div><input name="foo.bar" value="baz" /></div>');
        
        var model = binder.bind($ui);

        assertEquals('baz', model['foo.bar']);
    }

    </script>
</asp:Content>
