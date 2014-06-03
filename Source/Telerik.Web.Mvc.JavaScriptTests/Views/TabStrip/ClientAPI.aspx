<%@ Page Title="ClientAPI tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <h2>Client API Tests</h2>
    
	<% Html.Telerik().TabStrip()
            .Name("TabStrip")
            .Items(items =>
            {
                items.Add().Text("Item 1");
                items.Add().Text("Item 2");

                items.Add().Text("Item 3");
                items.Add().Text("Item 4")
                    .Enabled(false);
                items.Add().Text("Item 5");
                items.Add().Text("Item 6")
                    .Content(() => 
                    {
                        %>
                            <p>Content</p>
                        <%
                    });
                 items.Add().Text("Item 7")
                    .Content(() => 
                    {
                        %>
                            <p>Content</p>
                        <%
                    });
                items.Add().Text("AjaxItem");
                items.Add().Text("InputContent")
                     .Content("<input type='text' value='asdf'/>");
                items.Add().Text("NavigatingItem")
                     .Url("http://www.telerik.com");
            }
            ).ClientEvents(events =>
            {
                events.OnSelect("Select");
                events.OnLoad("Load");
            })
            .Render(); %>

     <% Html.Telerik().TabStrip()
            .Name("TabStrip1")
            .Items(items =>
            {
                items.Add().Text("Item 1");
                items.Add().Text("Item 2");
            })
            .Render(); %>
    
   <script type="text/javascript">

       var isRaised;
        
        function getRootItem(index) {
            return $('#TabStrip').find('.t-item').eq(index)
        }
        
        function getTabStrip() {
            return $("#TabStrip").data("tTabStrip");
        }

        function test_clicking_item_with_url_should_navigate() {
            
            var tabstrip = getTabStrip();
            var $item = $(getRootItem(9));

            var e = new $.Event('click');
                        
            $item.find('.t-link').trigger(e);

            assertFalse(e.isDefaultPrevented());

            //stop navigation after assert
            e.preventDefault();
        }

        function test_trigger_input_select_should_not_bubble() {

            isRaised = false;

            var tabstrip = getTabStrip();
            var content = tabstrip.getContentElement(8)

            $(content).find('input').first().trigger('select');

            assertFalse(isRaised);
        }

        function test_reload_method_should_call_ajaxRequest() { 
            var tabstrip = getTabStrip();
            var isCalled = false;
            var $item = $(getRootItem(7));

            $item.find('.t-link').data('ContentUrl', 'fake');
            tabstrip.ajaxRequest = function () { isCalled = true; };
            
            tabstrip.reload($item);

            assertTrue(isCalled);
        }
    
        function test_clicking_should_raise_onSelect_event() {

            var item = getRootItem(2);

            isRaised = false;

            item.find('> .t-link').trigger('click');

            assertTrue(isRaised);
        }

        function test_clicking_first_item_should_select_it() {
            var item = getRootItem(0);

            item.find('.t-link').trigger('click');

            assertTrue(item.hasClass('t-state-active'));
        }

        function test_select_method_should_select_second_item() {
            var tabstrip = getTabStrip();
            var item = getRootItem(1);

            tabstrip.select(item);

            assertTrue(item.hasClass('t-state-active'));
        
        }

        function test_disable_method_should_disable_item() {
            var tabstrip = getTabStrip();

            var item = getRootItem(4);

            tabstrip.disable(item);

            assertTrue(item.hasClass('t-state-disabled'));
        }

        function test_enable_method_should_enable_disabled_item() {
            var tabstrip = getTabStrip();

            var item = getRootItem(3);

            tabstrip.enable(item);

            assertTrue(item.hasClass('t-state-default'));
        }

        function test_select_method_should_show_content() {
            var tabstrip = getTabStrip();

            var item = getRootItem(5);
            tabstrip.select(item);

            var content = $(tabstrip.getContentElement(5));
            assertTrue(content.hasClass('t-state-active'));
        }

        function test_getContentElement_should_return_content_of_seveth_tab() {
            var tabstrip = getTabStrip();
            
            var expectedContent = $(tabstrip.element).find('> .t-content').eq(1); //second content under Tab-7
            
            assertEquals(expectedContent.index(), $(tabstrip.getContentElement(6)).index());
        }

        function test_getContentElement_should_not_return_tab_content_if_passed_argument_is_not_number() {
            var tabstrip = getTabStrip();

            assertEquals(undefined, tabstrip.getContentElement("a"));
        }

        function test_getContentElement_should_not_return_tab_content_if_passed_argument_is_not_in_range() {
            var tabstrip = getTabStrip();

            assertEquals(undefined, tabstrip.getContentElement(100));
        }

        function test_getSelectedTab_should_return_current_selected_tab() {
            var tabstrip = getTabStrip();

            var item = getRootItem(0);
            tabstrip.select(item);

            assertEquals($(item).index(), tabstrip.getSelectedTabIndex());
        }

        function test_getSelectedTab_should_return_negative_if_no_selected_tabs() {
            var tabstrip = $("#TabStrip1").data("tTabStrip")
            
            assertEquals(-1, tabstrip.getSelectedTabIndex());
        }

        var argsCheck = false;
        function test_click_should_raise_select_event_and_pass_corresponding_content() {
            argsCheck = true;

            var item = getRootItem(6);

            isRaised = false;

            item.find('> .t-link').trigger('click');

            assertTrue(isRaised);
        }
        
        //handlers
        function Select(e) {
            if (argsCheck) {
                if (e.contentElement) {
                    isRaised = true;
                } else {
                    isRaised = false;
                }
                argsCheck = false;
            } else {
                isRaised = true;
            }
        }

        function test_client_object_is_available_in_on_load() {
            assertNotNull(onLoadTabStrip);
            assertNotUndefined(onLoadTabStrip);
        }
    
        var onLoadTabStrip;
        function Load(e) {
            isRaised = true;
            onLoadTabStrip = $(this).data('tTabStrip');
        }
   </script>

</asp:Content>
