<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>DropDown Rendering</h2>

    <script type="text/javascript">
        var dropDownObject;
        var $component;
        var $t;

        function setUp() {
            $t = $.telerik;

            dropDownObject = new $t.dropDown({
                offset: {left: 100, top: 100},
                outerHeight: 100,
                outerWidth: 200,
                zIndex: 10,
                attr: 'width: 200px',
                effects: $t.fx.toggle.defaults()
            });

            dropDownObject.dataBind([{ Text: 'text1', Value: '1' },
                                     { Text: 'text2', Value: '2' },
                                     { Text: 'text3', Value: '3'}]);
        }

        function test_dropDown_should_create_$element_with_passed_attributes() {
            
            var dropDown = new $t.dropDown({
                attr: 'style="width: 300px; overflow-y: visible"'
            });

            assertEquals('300px', dropDown.$element.css('width'));
            assertEquals('visible', dropDown.$element.css('overflow-y'));
        }

        function test_dropDown_should_create_$element_with_width_equal_to_outerWidth_property_and_overflow_auto() {

            var dropDown = new $t.dropDown({
                outerWidth: 200 //2 pixels are always extracted in the constructor of dropDown
            });

            assertEquals('198px', dropDown.$element.css('width'));
            assertEquals('auto', dropDown.$element.css('overflow-y'));
        }

        function test_dataBind_should_create_li_items_depending_on_passed_data() {
            
            dropDownObject.dataBind([{ Text: 'text1', Value: '1' },
                                     { Text: 'text2', Value: '2' }, 
                                     { Text: 'text3', Value: '3'}]);
                                     
            assertEquals(3, dropDownObject.$items.length);
        }

        function test_dataBind_should_fill_list_with_2_li_items() {
            
            dropDownObject.dataBind([{ Text: 'text1', Value: '1' },
                                     { Text: 'text2', Value: '2' }]);

            assertEquals(2, dropDownObject.$element.find('.t-item').length);
        }

        function test_dataBind_should_height_to_auto_if_items_are_less_then_10() {

            dropDownObject.dataBind([{ Text: 'text1', Value: '1' },
                                     { Text: 'text2', Value: '2'}]);

            assertEquals('auto', dropDownObject.$element.css('height'));
        }

        function test_dataBind_should_height_to_200px_if_items_are_more_than_10_and_dropDown_does_not_have_set_height() {

            var dropDown = new $t.dropDown({
                outerWidth: 200
            });

            dropDown.dataBind([{ Text: 'text1', Value: '1' },
                                     { Text: 'text2', Value: '2' },
                                     { Text: 'text2', Value: '2' },
                                     { Text: 'text2', Value: '2' },
                                     { Text: 'text2', Value: '2' },
                                     { Text: 'text2', Value: '2' },
                                     { Text: 'text2', Value: '2' },
                                     { Text: 'text2', Value: '2' },
                                     { Text: 'text2', Value: '2' },
                                     { Text: 'text2', Value: '2' },
                                     { Text: 'text2', Value: '2' },
                                     { Text: 'text2', Value: '2' },
                                     { Text: 'text2', Value: '2' },
                                     { Text: 'text2', Value: '2' } ]);

            assertEquals('200px', dropDown.$element.css('height'));
        }

        function test_dataBind_should_set_height_to_the_one_from_attr() {

            var dropDown = new $t.dropDown({
                attr: 'style="height: 300px"'
            });

             dropDown.dataBind([{ Text: 'text1', Value: '1' },
                                { Text: 'text2', Value: '2' },
                                { Text: 'text2', Value: '2' },
                                { Text: 'text2', Value: '2' },
                                { Text: 'text2', Value: '2' },
                                { Text: 'text2', Value: '2' },
                                { Text: 'text2', Value: '2' },
                                { Text: 'text2', Value: '2' },
                                { Text: 'text2', Value: '2' },
                                { Text: 'text2', Value: '2' },
                                { Text: 'text2', Value: '2' },
                                { Text: 'text2', Value: '2' },
                                { Text: 'text2', Value: '2' },
                                { Text: 'text2', Value: '2'}]);

             assertEquals('300px', dropDown.$element.css('height'));
        }

        function test_dataBinding_method_should_be_called() {
            var isCalled = false;

            dropDownObject.onDataBinding = function () {
                isCalled = true;
            }

            dropDownObject.dataBind([{ Text: 'text1', Value: '1' },
                                     { Text: 'text2', Value: '2'}]);

            assertEquals(true, isCalled);
        }

        function test_itemCrate_method_should_be_called_twice() {
            var count = 0;
            
            dropDownObject.onItemCreate = function () {
                count += 1;
            }

            dropDownObject.dataBind([{ Text: 'text1', Value: '1' },
                                     { Text: 'text2', Value: '2'}]);

            assertEquals(2, count);
        }

        function test_itemCrate_method_should_be_called_twice() {
            
            dropDownObject.dataBind([{ Text: 'text1', Value: '1' },
                                     { Text: 'text2', Value: '2' },
                                     { Text: 'text3', Value: '3' }]);

            dropDownObject.highlight(dropDownObject.$items[1]);

            var $selected = dropDownObject.$items.filter('.t-state-selected');

            assertEquals(1, $selected.length);
            assertEquals('text2', $selected.first().text());
        }

        function test_open_method_should_call_open_callback() {
            var isCalled = false;

            dropDownObject.onOpen = function () { isCalled = true; }

            dropDownObject.open();

            assertTrue(isCalled);
        }

        function test_open_method_should_be_prevented_by_open_callback() {
            
            var isCalled = false;
            var play = $t.fx.play;
            $t.fx.play = function () { isCalled = true; }

            dropDownObject.onOpen = function () { return false; }
            dropDownObject.open();

            assertFalse(isCalled);

            dropDownObject.onOpen = null;
            $t.fx.play = play;
        }

        function test_open_should_apply_offset_and_outerHeight_to_the_animation_container() {
            dropDownObject.open();

            var animationContainer = dropDownObject.$element.parent();

            assertEquals('10', animationContainer.css('zIndex'))
            assertEquals(200, animationContainer.offset().top) //outerHeight + offset.Top
            assertEquals(100, animationContainer.offset().left)
        }

        function test_open_method_should_call_scrollTo_item_if_there_is_selected_item() {
            var isCalled = false;
            var scrollTo = dropDownObject.scrollTo;

            dropDownObject.highlight(dropDownObject.$items.last());
            dropDownObject.scrollTo = function () { isCalled = true; }
            dropDownObject.open();

            assertTrue(isCalled);

            dropDownObject.scrollTo = scrollTo;
        }

        function test_close_method_should_call_close_callback() {
            var isCalled = false;

            dropDownObject.onClose = function () { isCalled = true; }

            dropDownObject.close();

            assertTrue(isCalled);
        }

        function test_close_method_should_be_prevented_by_close_callback() {

            var isCalled = false;
            var rewind = $t.fx.rewind;
            $t.fx.rewind = function () { isCalled = true; }

            dropDownObject.onClose = function () { return false; }
            dropDownObject.close();

            assertFalse(isCalled);

            $t.fx.rewind = rewind;
        }

        function test_click_item_should_call_click_callback() {
            var isCalled = false;
            var dropDown = new $t.dropDown({
                offset: { left: 100, top: 100 },
                outerHeight: 100,
                outerWidth: 200,
                zIndex: 10,
                effects: $t.fx.toggle.defaults()
            });

            dropDown.dataBind([{ Text: 'text1', Value: '1' },
                               { Text: 'text2', Value: '2' },
                               { Text: 'text3', Value: '3'}]);

            dropDown.onClick = function () { isCalled = true; }
            dropDown.open();

            dropDown.$items.first().trigger('click');

            assertTrue(isCalled);
        }

        function test_click_item_should_pass_clicked_item_to_click_callback() {
            var item;

            var dropDown = new $t.dropDown({
                offset: { left: 100, top: 100 },
                outerHeight: 100,
                outerWidth: 200,
                zIndex: 10,
                effects: $t.fx.toggle.defaults()
            });

            dropDown.dataBind([{ Text: 'text1', Value: '1' },
                               { Text: 'text2', Value: '2' },
                               { Text: 'text3', Value: '3'}]);

            dropDown.onClick = function (e) { item = e.item; }
            dropDown.open();

            dropDown.$items.last().trigger('click');

            assertEquals('text3', $(item).text());
        }

        function test_position_should_set_offset_object() {
            var dropDown = new $t.dropDown({
                offset: { left: 100, top: 100 },
                outerHeight: 100,
                outerWidth: 200,
                zIndex: 10,
                effects: $t.fx.toggle.defaults()
            });

            dropDown.dataBind([{ Text: 'text1', Value: '1' },
                               { Text: 'text2', Value: '2' },
                               { Text: 'text3', Value: '3'}]);

            dropDown.position(5, 10);

            assertEquals(5, dropDown.offset.top)
            assertEquals(10, dropDown.offset.left)
        }

    </script>

    <% Html.Telerik().ScriptRegistrar()
           .Scripts(scripts => scripts
               .Add("telerik.common.js")
               .Add("telerik.list.js")); %>
</asp:Content>