<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    
    <h2>TimeView</h2>
    
    <script type="text/javascript">
        var $t;
        var tv;
        var $input;
        var position;
        var itemValue;
        var passedValue;

        function changeCallBack(pass) {
            passedValue = pass;
        }

        function navigateWithOpenPopupCallBack(pass) {
            itemValue = pass;
        }

        function setUp() {
            $t = $.telerik;
            $input = $('#testInput');

            tv = new $t.timeView({
                effects: new $t.fx.toggle.defaults(),
                dropDownAttr: '',
                format: $t.cultureInfo.shortTime,
                interval: 30,
                isRtl: $input.closest('.t-rtl').length,
                minValue: new $t.datetime(2010, 10, 10, 12, 0, 0),
                maxValue: new $t.datetime(2010, 10, 10, 12, 0, 0),
                onChange: changeCallBack,
                onNavigateWithOpenPopup: navigateWithOpenPopupCallBack
            });

            position = {
                offset: $input.offset(),
                outerHeight: $input.outerHeight(),
                outerWidth: $input.outerWidth(),
                zIndex: $t.getElementZIndex($input[0])
            }
        }

        function test_timeView_should_create_dropDown_on_its_creating() {
            
            var timeView = new $t.timeView({
                effects: new $t.fx.slide.defaults(),
                dropDownAttr: 'width:100px',
                isRtl: 0
            });

            assertNotUndefined(timeView.dropDown);
            assertNotUndefined(timeView.dropDown.onClick);
            assertEquals('not correct effects are set', timeView.effects.list[0].name, timeView.dropDown.effects.list[0].name);
            assertEquals('not correct effects are set', timeView.dropDownAttr, timeView.dropDown.attr);
        }

        function test_ensure_items_will_call_bind_if_not_items_are_created() {
            var isCalled = false;
            var oldM = tv.bind;
            tv.bind = function () { isCalled = true; }

            tv.dropDown.$items = null;

            tv._ensureItems();

            assertTrue('bind method was not called', isCalled);

            tv.bind = oldM;
        }

        function test_open_method_should_call_ensureItems() {
            var isCalled = false;
            var oldM = tv._ensureItems;
            tv._ensureItems = function () { isCalled = true; }

            tv.open(position);

            assertTrue('_ensureItems was not called', isCalled);

            tv._ensureItems = oldM;
        }

        function test_open_method_should_call_dropDown_open_method_with_position_data() {
            var passedPos;
            var isCalled = false;
            var oldM = tv.dropDown.open;
            tv.dropDown.open = function (pos) { isCalled = true; passedPos = pos; }

            tv.open(position);

            assertTrue('open method was not called', isCalled);
            assertEquals('passed position is not correct', position.offset.top, passedPos.offset.top); //just chekc one property

            tv.dropDown.open = oldM;
        }

        function test_open_method_should_call_dropDown_close_method() {
            var isCalled = false;
            var oldM = tv.dropDown.close;
            tv.dropDown.close = function () { isCalled = true; }

            tv.close();

            assertTrue('close method was not called', isCalled);

            tv.dropDown.close = oldM;
        }

        function test_bind_method_will_call_dataBind_with_correct_array_of_available_hours() {
            var timeView = new $t.timeView({
                effects: new $t.fx.slide.defaults(),
                dropDownAttr: 'width:100px',
                isRtl: 0,
                format: 'H:mm',
                interval: 30,
                isRtl: $input.closest('.t-rtl').length,
                minValue: new $t.datetime(2010, 10, 10, 23, 0, 0),
                maxValue: new $t.datetime(2010, 10, 10, 2, 0, 0)
            });

            var availableHours;
            var oldM = timeView.dropDown.dataBind;
            timeView.dropDown.dataBind = function (hours) { availableHours = hours; }

            timeView.bind();

            assertNotUndefined(availableHours);
            assertEquals('renderedItems are not correct number', 7, availableHours.length);
            assertEquals('hours is not formatted correctly', "23:00", availableHours[0]);

            timeView.dropDown.dataBind = oldM;
        }

        function test_bind_method_should_add_maxValue_to_items_list() {
            var timeView = new $t.timeView({
                effects: new $t.fx.slide.defaults(),
                dropDownAttr: 'width:100px',
                isRtl: 0,
                format: 'H:mm',
                interval: 30,
                isRtl: $input.closest('.t-rtl').length,
                minValue: new $t.datetime(2010, 10, 10, 23, 0, 0),
                maxValue: new $t.datetime(2010, 10, 10, 2, 15, 0)
            });

            var availableHours;
            var oldM = timeView.dropDown.dataBind;
            timeView.dropDown.dataBind = function (hours) { availableHours = hours; }

            timeView.bind();

            assertEquals('renderedItems are not correct number', 8, availableHours.length);
            assertEquals('hours is not formatted correctly', "2:15", availableHours[7]);

            timeView.dropDown.dataBind = oldM;
        }

        function test_value_method_should_highlight_item_depending_on_the_passed_date() {
            tv.dropDown.$items = null
            tv.minValue = new $t.datetime(2010, 10, 10, 12, 0, 0);
            tv.maxValue = new $t.datetime(2010, 10, 10, 12, 0, 0);
            
            tv.value("3:00 PM");

            var $item = tv.dropDown.$items.filter('.t-state-selected');

            assertEquals('Selected item is not one', 1, $item.length);
            assertEquals('selected item is not correct', "3:00 PM", $item.text());
        }

        function test_value_method_should_deselect_all_items_if_value_is_null() {
            tv.dropDown.$items = null
            tv.minValue = new $t.datetime(2010, 10, 10, 12, 0, 0);
            tv.maxValue = new $t.datetime(2010, 10, 10, 12, 0, 0);

            tv.value(null);

            var $item = tv.dropDown.$items.filter('.t-state-selected');

            assertEquals('Selected item is not 0', 0, $item.length);
        }

        function test_value_method_should_return_text_of_the_selected_item() {
            tv._ensureItems();
            var $item = tv.dropDown.$items.filter('.t-state-selected');
            assertEquals($item.text(), tv.value());
        }

        function test_max_method_should_rebind_items_list() {
            var date = new Date(2000, 1, 1, 3, 0, 0);

            tv.max(date)

            var $items = tv.dropDown.$items;

            assertEquals('maxValue is not set correctly', 0, tv.maxValue.toDate() - date);
            assertEquals($t.datetime.format(tv.maxValue.toDate(), tv.format), $($items[$items.length - 1]).text());
        }

        function test_max_method_should_return_maxValue() {
            var date = tv.max();

            assertEquals(0, date - tv.maxValue.toDate());
        }  

        function test_min_method_should_rebind_items_list() {
            var date = new Date(2000, 1, 1, 10, 0, 0)

            tv.min(date)

            var $items = tv.dropDown.$items;

            assertEquals('minValue is not set correctly', 0, tv.minValue.toDate() - date);
            assertEquals($t.datetime.format(tv.minValue.toDate(), tv.format), $($items[0]).text());
        }

        function test_min_method_should_return_minValue() {
            var date = tv.min();

            assertEquals(0, date - tv.minValue.toDate());
        }

        function test_clicking_item_should_raise_onChange_callback() {
            tv.open(position);

            var $item = tv.dropDown.$items.eq(1);

            $item.click();

            assertNotUndefined(passedValue);
            assertEquals('passed value is not correct', $item.text(), passedValue);
        }

        function test_pressing_down_arrow_should_select_next_item() {
            tv.open(position);

            var $initialSelectedItem = tv.dropDown
                                            .$items
                                            .removeClass('t-state-selected')
                                            .first()
                                            .addClass('t-state-selected');

            tv.navigate({ keyCode: 40, preventDefault: function () { } });

            var $selectedItem = tv.dropDown.$items.filter('.t-state-selected').eq(0);

            assertEquals('correct item is not selected', $initialSelectedItem.next().index(), $selectedItem.index());
            assertEquals($selectedItem.text(), itemValue);
        }

        function test_pressing_up_arrow_should_select_prev_item() {
            tv.open(position);

            tv.dropDown.highlight(tv.dropDown.$items[1]);

            tv.navigate({ keyCode: 38, preventDefault: function () { } });

            var $selectedItem = tv.dropDown.$items.filter('.t-state-selected').eq(0);

            assertEquals('correct item is not selected', 0, $selectedItem.index());
            assertEquals($selectedItem.text(), itemValue);
        }

        function test_keydown_should_create_$items_if_they_do_not_exist() {
            tv.dropDown.$items = null;

            tv.navigate({ keyCode: 38, preventDefault: function () { } });

            assertTrue(tv.dropDown.$items.length > 0);
        }

        function test_pressing_up_arrow_should_select_first_item_if_no_selected_item() {
            tv.open(position);

            tv.dropDown.$items.removeClass('t-state-selected');

            tv.navigate({ keyCode: 38, preventDefault: function () { } });

            assertEquals(0, tv.dropDown.$items.filter('.t-state-selected').index())
        }

        function test_pressing_up_arrow_should_raise_OnChange_if_popup_is_closed() {
            tv.close(position);

            tv._ensureItems();

            tv.dropDown.highlight(tv.dropDown.$items[1]);

            tv.navigate({ keyCode: 38, preventDefault: function () { } });

            var $selectedItem = tv.dropDown.$items.filter('.t-state-selected').eq(0);

            assertEquals('correct item is not selected', 0, $selectedItem.index());
            assertEquals($selectedItem.text(), passedValue);
        }

        function test_navigate_should_not_call_preventDefault_if_key_is_not_arrow() {

            var isCalled = false;

            tv.navigate({ keyCode: 49, preventDefault: function () { isCalled = true; } });

            assertFalse('preventDefault was called', isCalled);
        }

        function test_navigate_should_call_preventDefault_if_key_is_arrow() {

            var isCalled = false;

            tv.navigate({ keyCode: 38, preventDefault: function () { isCalled = true; } });

            assertTrue('preventDefault was not called', isCalled);
        }

    </script>

    <input id="testInput" />

    <% Html.Telerik().ScriptRegistrar()
           .DefaultGroup(group => group.Add("telerik.common.js")
                                       .Add("telerik.timepicker.js")); 
    %>

</asp:Content>
