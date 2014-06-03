<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>DropDown Rendering</h2>

    <script type="text/javascript">
        var dropDownObject;
        var $component;
        var position;
        var $input;
        var $t;

        function setUp() {
            $t = $.telerik;

            dropDownObject = new $t.dropDown({
                attr: 'width: 200px',
                effects: $t.fx.toggle.defaults()
            });

            dropDownObject.dataBind([{ Text: 'text1', Value: '1' },
                                     { Text: 'text2', Value: '2' },
                                     { Text: 'text3', Value: '3'}]);

            $input = $('#testInput');

            position = {
                offset: $input.offset(),
                outerHeight: $input.outerHeight(),
                outerWidth: $input.outerWidth(),
                zIndex: $t.getElementZIndex($input[0])
            }
        }

        function test_dropDown_should_create_$element_with_passed_attributes() {
            
            var dropDown = new $t.dropDown({
                attr: 'style="width: 300px; overflow-y: visible"'
            });
            
            dropDown.$element.appendTo(document.body);
            assertEquals('300px', dropDown.$element.css('width'));
            assertEquals('visible', dropDown.$element.css('overflow-y'));
        }


        function test_dropDown_should_create_$element_with_passed_attr_and_preserve_default_classes() {

            var dropDown = new $t.dropDown({
                attr: 'class="bob"'
            });

            assertEquals('bob t-popup t-group', dropDown.$element.attr('class'));
        }

        function test_dropDown_should_set_width_property_and_overflow_auto_when_open() {

            var position1 = {
                offset: $input.offset(),
                outerHeight: $input.outerHeight(),
                outerWidth: 200, //2 pixels are always extracted in the constructor of dropDown
                zIndex: $t.getElementZIndex($input[0])
            }

            dropDownObject.open(position1)

            assertEquals('198px', dropDownObject.$element.css('width'));
            assertEquals('auto', dropDownObject.$element.css('overflow-y'));
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
            
            dropDownObject.$element.appendTo(document.body);

            assertEquals('42px', dropDownObject.$element.css('height'));
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

            dropDown.$element.appendTo(document.body);
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

             dropDown.$element.appendTo(document.body);
             assertEquals('300px', dropDown.$element.css('height'));
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

        function test_highlight_method_should_select_second_item() {
            
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

            dropDownObject.open(position);

            assertTrue(isCalled);
        }

        function test_open_should_apply_offset_and_outerHeight_to_the_animation_container() {
            var position1 = {
                offset: {top:180, left:100},
                outerHeight: 20,
                outerWidth: $input.outerWidth(),
                zIndex: 10
            }

            dropDownObject.open(position1);

            var animationContainer = dropDownObject.$element.parent();

            assertEquals('10', animationContainer.css('zIndex').toString())
            assertEquals(200, animationContainer.offset().top) //outerHeight + offset.Top
            assertEquals(100, animationContainer.offset().left)
        }

        function test_open_method_should_call_scrollTo_item_if_there_is_selected_item() {
            var isCalled = false;
            var scrollTo = dropDownObject.scrollTo;

            dropDownObject.highlight(dropDownObject.$items.last());
            dropDownObject.scrollTo = function () { isCalled = true; }
            dropDownObject.open(position);

            assertTrue(isCalled);

            dropDownObject.scrollTo = scrollTo;
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
            dropDown.open(position);

            dropDown.$items.last().trigger('click');

            assertEquals('text3', $(item).text());
        }

    </script>

    <input id="testInput" />

    <% Html.Telerik().ScriptRegistrar()
           .Scripts(scripts => scripts
               .Add("telerik.common.js")
               .Add("telerik.list.js")); %>
</asp:Content>