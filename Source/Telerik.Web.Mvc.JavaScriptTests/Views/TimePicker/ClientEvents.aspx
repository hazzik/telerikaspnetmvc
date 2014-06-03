<%@ Page Title="SingleExpandItem ClientAPI tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Client API Tests</h2>
    
   <script type="text/javascript">

       function getTimePicker() {
           return $('#TimePicker').data('tTimePicker');
       }

       function test_client_object_is_available_in_on_load() {
           assertNotNull(onLoadTimePicker);
           assertNotUndefined(onLoadTimePicker);
       }

       var isChanged;
       var isRaised;
              
       function test_focusing_input_should_raise_onOpen_event() {
           getTimePicker().close();

           var input = $('#TimePicker').find('.t-input');

           isRaised = false;

           input.focus();

           assertTrue(isRaised);
       }       

       function test_clicking_tab_should_raise_onClose() {

           getTimePicker().open();

           
           var input = $('#TimePicker').find('.t-input');
           input[0].focus();

           isRaised = false;
           input.trigger({ type: "keydown", keyCode: 9});        

           assertTrue(isRaised);
       }

       function test_clicking_escape_should_raise_onClose() {

           getTimePicker().open();

           var input = $('#TimePicker').find('.t-input');
           input[0].focus();

           isRaised = false;
           input.trigger({ type: "keydown", keyCode: 27 });

           assertTrue(isRaised);
       }

       function test_clicking_enter_should_raise_onClose() {

           getTimePicker().open();

           var input = $('#TimePicker').find('.t-input');
           input[0].focus();

           isRaised = false;
           input.trigger({ type: "keydown", keyCode: 13 });

           assertTrue(isRaised);
       }

       function test_clicking_alt_and_down_arrow_should_raise_onOpen() {
           var timepicker = getTimePicker();

           timepicker.close();
           isRaised = false;
           timepicker.$input.trigger({ type: "keydown", keyCode: 40, altKey: true });

           assertTrue(isRaised);
       }

       function test_clicking_alt_and_up_arrow_should_raise_onClose() {
           var timepicker = getTimePicker();

           timepicker.open();
           isRaised = false;
           timepicker.$input.trigger({ type: "keydown", keyCode: 38, altKey: true });

           assertTrue(isRaised);
       }

       function test_clicking_document_should_raise_close_event() {
           getTimePicker().open();

           isRaised = false;
           $(document.documentElement).trigger('mousedown');

           assertTrue(isRaised);
       }

       function test_clicking_item_should_raise_close_event() {
           var timepicker = getTimePicker();
           timepicker.open();

           var $item = $(timepicker.timeView.dropDown.$items[0]);

           isRaised = false;
           $item.trigger('click');

           assertTrue(isRaised);
       }
       
       function test_clicking_enter_should_raise_onChange() {

           var timepicker = getTimePicker();
           timepicker.open();

           var input = $('#TimePicker').find('.t-input');
           input[0].focus();
           input.val('10:30 AM');

           isChanged = false;
           input.trigger({ type: "keydown", keyCode: 13 });

           assertTrue(isChanged);
       }

       function test_clicking_tab_should_raise_onChange() {

           var timepicker = getTimePicker();
           timepicker.open();

           var input = $('#TimePicker').find('.t-input');
           input[0].focus();
           input.val('10:31 AM');

           isChanged = false;
           input.trigger({ type: "keydown", keyCode: 9 });

           assertTrue(isChanged);
       }

       function test_clicking_escape_should_raise_onChange() {

           var timepicker = getTimePicker();
           timepicker.open();

           var input = $('#TimePicker').find('.t-input');
           input[0].focus();
           input.val('10:32 AM');

           isChanged = false;
           input.trigger({ type: "keydown", keyCode: 27 });

           assertTrue(isChanged);
       }

       function test_clicking_up_arrow_should_raise_onChange_if_dropDown_is_closed() {
           var input = $('#TimePicker').find('.t-input');
           input[0].focus();
           getTimePicker().close();
           
           isChanged = false;
           input.trigger({ type: "keydown", keyCode: 38 });

           assertTrue(isChanged);
       }

       function test_clicking_document_should_raise_change_event() {
           getTimePicker().open();
           
           var input = $('#TimePicker').find('.t-input');
           input[0].focus();
           input.val('11:33 AM');
           
           isChanged = false;
           $(document.documentElement).trigger('mousedown');

           assertTrue(isChanged);
       }

       function test_typing_same_time_should_not_raise_change_event() {
           var timepicker = getTimePicker();

           timepicker.value('10:35 AM');

           var input = timepicker.$input;
           input[0].focus();
           input.val('10:35 AM');
           
           isChanged = false;
           input.trigger({ type: "keydown", keyCode: 13 });

           assertFalse(isChanged);
       }

       function test_not_able_to_parse_text_should_add_error_state() {
           var input = $('#TimePicker').find('.t-input');
           input[0].focus();
           input.val('Not valid time');

           input.trigger({ type: "keydown", keyCode: 13 });

           assertTrue("t-error-state was not added!", input.hasClass('t-state-error'));
           
       }

       function test_datepicker_should_call_change_and_close_internal_methods_if_document_mousedown() {
           var isCloseCalled = false;
           var isChangeCalled = false;
           var timepicker = getTimePicker();

           var oldClose = timepicker._close;
           var oldChange = timepicker._change;

           timepicker._close = function () { isCloseCalled = true; };
           timepicker._change = function () { isChangeCalled = true; };

           timepicker.open();

           $(document.documentElement).trigger('mousedown');

           assertTrue('_close was not called', isCloseCalled);
           assertTrue('_change was not called', isChangeCalled);

           timepicker._close = oldClose;
           timepicker._change = oldChange;
       }

       //handlers
       function onLoad(e) {
           isRaised = true;
       }

       function onChange(e) {
           isChanged = true;
       }

       function onClose(e) {
           isRaised = true;
       }

       function onOpen(e) {
           isRaised = true;
       }

       var onLoadTimePicker;

       function onLoad() {
           onLoadTimePicker = $(this).data('tTimePicker');
       }
   </script>

 <%= Html.Telerik().TimePicker().Name("TimePicker")
                   .Effects( e => e.Toggle())
                   .ClientEvents(events => events.OnLoad("onLoad")
                                                 .OnChange("onChange")
                                                 .OnClose("onClose")
                                                 .OnOpen("onOpen"))
 %>

</asp:Content>