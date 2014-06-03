<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        DropDown Rendering</h2>
    <script type="text/javascript">

        function getDropDownList(id) {
            return $('#' + (id || 'DropDownList')).data('tDropDownList');
        }

        function test_dataBind_method_should_preserve_selectedItem_depending_on_Selected_property_even_selectedIndex_is_present() {
            var ddl = getDropDownList();
            var data = [{ Text: 'Item1', Value: '1', Selected: false },
                        { Text: 'Item2', Value: '2', Selected: false },
                        { Text: 'Item3', Value: '3', Selected: false },
                        { Text: 'Item4', Value: '4', Selected: false },
                        { Text: 'Item5', Value: '5', Selected: true }];
            ddl.index = 1;
            
            ddl.dataBind(data);

            assertEquals(4, ddl.index);
        }

        function test_dataBind_method_should_override_selectedIndex_there_is_no_Selected_true() {
            var ddl = getDropDownList();
            var data = [{ Text: 'Item1', Value: '1', Selected: false },
                        { Text: 'Item2', Value: '2', Selected: false },
                        { Text: 'Item3', Value: '3', Selected: false },
                        { Text: 'Item4', Value: '4', Selected: false },
                        { Text: 'Item5', Value: '5', Selected: false}];

            ddl.index = 1;

            ddl.dataBind(data);

            assertEquals(1, ddl.index);
        }

        function test_dataBind_method_should_override_selectedIndex_there_is_no_Selected_defined() {
            var ddl = getDropDownList();
            var data = [{ Text: 'Item1', Value: '1' },
                        { Text: 'Item2', Value: '2' },
                        { Text: 'Item3', Value: '3' },
                        { Text: 'Item4', Value: '4' },
                        { Text: 'Item5', Value: '5' }];

            ddl.index = 2;

            ddl.dataBind(data);

            assertEquals(2, ddl.index);
        }

        function test_fill_method_should_call_component_dataBind_method() {
            var ddl = getDropDownList();
            ddl.data = [{ Text: 'Item1', Value: '1', Selected: false },
                        { Text: 'Item2', Value: '2', Selected: false },
                        { Text: 'Item3', Value: '3', Selected: true },
                        { Text: 'Item4', Value: '4', Selected: false },
                        { Text: 'Item5', Value: '5', Selected: false}];

            ddl.index = 1;
            ddl.dropDown.$items = null;
            ddl.fill();
            
            assertEquals(2, ddl.index);
            assertTrue('item is not selected', ddl.dropDown.$items.eq(2).hasClass('t-state-selected'));
        }

        function test_dataBind_method_should_clear_current_component_status() {
            var ddl = getDropDownList();
            var data = [{ Text: 'Item1', Value: '1', Selected: false },
                        { Text: 'Item2', Value: '2', Selected: false },
                        { Text: 'Item3', Value: '3', Selected: true },
                        { Text: 'Item4', Value: '4', Selected: false },
                        { Text: 'Item5', Value: '5', Selected: false}];

            ddl.select(1);
            
            ddl.dataBind(data);

            assertEquals('&nbsp;', ddl.text());
            assertEquals('', ddl.value());
        }

        function test_dataBind_method_can_preserve_component_status() {
            var ddl = getDropDownList();
            var data = [{ Text: 'Item1', Value: '1', Selected: false },
                        { Text: 'Item2', Value: '2', Selected: false },
                        { Text: 'Item3', Value: '3', Selected: true },
                        { Text: 'Item4', Value: '4', Selected: false },
                        { Text: 'Item5', Value: '5', Selected: false}];

            ddl.select(1);

            ddl.dataBind(data, true /*should preserve status*/);

            assertEquals('Item2', ddl.text());
            assertEquals('2', ddl.value());
        }

        function test_value_method_should_select_item_depending_on_its_value() {
            var ddl = getDropDownList('DDL2');
            
            ddl.value('2');
            
            assertEquals(ddl.dropDown.$items.eq(1).text(), ddl.text())
            assertEquals(ddl.data[1].Value, ddl.value())
        }

        function test_value_method_should_select_item_depending_on_its_text_if_item_value_is_not_set() {
            var ddl = getDropDownList('DDL2');

            ddl.value('Item1');

            assertEquals(null, ddl.data[0].Value)
            assertEquals(ddl.data[0].Text, ddl.text())
            assertEquals(ddl.data[0].Text, ddl.value())
        }

        function test_value_method_should_not_select_item_if_no_such_text_or_value() {
            var ddl = getDropDownList('DDL2');

            ddl.value('3');

            ddl.value('Illegal');

            assertEquals(ddl.data[2].Text, ddl.text())
            assertEquals(ddl.data[2].Value, ddl.value())
        }

        function test_enable_method_should_enable_dropDownList()
        {
            var ddl = getDropDownList('DDL2');

            ddl.enable();
            ddl.disable();
            
            assertTrue($('#DDL2').hasClass('t-state-disabled'));
        }

        function test_enable_method_should_disable_dropDownList()
        {
            var ddl = getDropDownList('DDL2');

            ddl.disable();
            ddl.enable();
            
            assertFalse($('#DDL2').hasClass('t-state-disabled'));
        }

    </script>

    <%= Html.Telerik().DropDownList()
            .Name("DropDownList")
            .Items(items =>
            {
                items.Add().Text("Item1").Value("1");
                items.Add().Text("Item2").Value("2");
                items.Add().Text("Item3").Value("3");
                items.Add().Text("Item4").Value("4");
                items.Add().Text("Item5").Value("5");
                items.Add().Text("Item6").Value("6");
                items.Add().Text("Item7").Value("7");
                items.Add().Text("Item8").Value("8");
                items.Add().Text("Item9").Value("9");
                items.Add().Text("Item10").Value("10");
                items.Add().Text("Item11").Value("11");
                items.Add().Text("Item12").Value("12");
                items.Add().Text("Item13").Value("13");
                items.Add().Text("Item14").Value("14");
            })
            .Effects(effects => effects.Toggle())
    %>

    <%= Html.Telerik().DropDownList()
            .Name("DDL2")
            .Items(items =>
            {
                items.Add().Text("Item1");
                items.Add().Text("Item2").Value("2");
                items.Add().Text("Item3").Value("3");
                items.Add().Text("Item4").Value("4");
                items.Add().Text("Item5").Value("5");
                items.Add().Text("Item6").Value("6");
                items.Add().Text("Item7").Value("7");
                items.Add().Text("Item8").Value("8");
                items.Add().Text("Item9").Value("9");
                items.Add().Text("Item10").Value("10");
                items.Add().Text("Item11").Value("11");
                items.Add().Text("Item12").Value("12");
                items.Add().Text("Item13").Value("13");
                items.Add().Text("Item14").Value("14");
            })
            .Effects(effects => effects.Toggle())
    %>

    <% Html.Telerik().ScriptRegistrar()
           .Scripts(scripts => scripts
           .Add("telerik.common.js")
           .Add("telerik.list.js")); 
    %>

    <br />
</asp:Content>
