<%@ Page Title="SingleExpandItem ClientAPI tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Client API Tests</h2>
    
   <script type="text/javascript">

       function getTimePicker() {
           return $('#TimePicker').data('tTimePicker');
       }

       var isChanged,
           isRaised;

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

<asp:Content ContentPlaceHolderID="TestContent" runat="server">

<script type="text/javascript">



       test('client object is available in on load', function() {
           ok(null !== onLoadTimePicker);
           ok(undefined !== onLoadTimePicker);
       });

       test('focusing input should raise onOpen event', function () {
           var timepicker = getTimePicker();
           timepicker.openOnFocus = true;
           timepicker.close();

           var input = $('#TimePicker');

           isRaised = false;

           input.focus();

           timepicker.openOnFocus = false;

           ok(isRaised);
       });

       test('focusing input should not raise onOpen event if openOnFocus is set to false', function () {
           var timepicker = getTimePicker();
           timepicker.close();
           timepicker.openOnFocus = false;

           var input = $('#TimePicker');

           isRaised = false;

           input.focus();

           ok(!isRaised);

           timepicker.openOnFocus = true;
       });         

       test('clicking tab should raise onClose', function() {

           getTimePicker().open();

           
           var input = $('#TimePicker');
           input[0].focus();

           isRaised = false;
           input.trigger({ type: "keydown", keyCode: 9});        

           ok(isRaised);
       });

       test('clicking escape should raise onClose', function() {

           getTimePicker().open();

           var input = $('#TimePicker');
           input[0].focus();

           isRaised = false;
           input.trigger({ type: "keydown", keyCode: 27 });

           ok(isRaised);
       });

       test('clicking enter should raise onClose', function() {

           getTimePicker().open();

           var input = $('#TimePicker');
           input[0].focus();

           isRaised = false;
           input.trigger({ type: "keydown", keyCode: 13 });

           ok(isRaised);
       });

       test('clicking alt and down arrow should raise onOpen', function() {
           var timepicker = getTimePicker();

           timepicker.close();
           isRaised = false;
           timepicker.$element.trigger({ type: "keydown", keyCode: 40, altKey: true });

           ok(isRaised);
       });

       test('clicking alt and up arrow should raise onClose', function() {
           var timepicker = getTimePicker();

           timepicker.open();
           isRaised = false;
           timepicker.$element.trigger({ type: "keydown", keyCode: 38, altKey: true });

           ok(isRaised);
       });

       test('clicking document should raise close event', function() {
           getTimePicker().open();

           isRaised = false;
           $(document.documentElement).trigger('mousedown');

           ok(isRaised);
       });

       test('clicking item should raise close event', function() {
           var timepicker = getTimePicker();
           timepicker.open();

           var $item = $(timepicker.timeView.dropDown.$items[0]);

           isRaised = false;
           $item.trigger('click');

           ok(isRaised);
       });
       
       test('clicking enter should raise onChange after navigate', function() {

           var timepicker = getTimePicker();
           timepicker.open();

           var input = $('#TimePicker');
           input[0].focus();

           isChanged = false;
           input.trigger({ type: "keydown", keyCode: 40 });
           input.trigger({ type: "keydown", keyCode: 13 });

           ok(isChanged);
       });

       test('clicking tab should raise onChange after navigate', function () {

           var timepicker = getTimePicker();
           timepicker.open();

           var input = $('#TimePicker');
           input[0].focus();

           isChanged = false;
           input.trigger({ type: "keydown", keyCode: 40 });
           input.trigger({ type: "keydown", keyCode: 9 });

           ok(isChanged);
       });

       test('clicking escape should raise onChange', function() {

           var timepicker = getTimePicker();
           timepicker.open();

           var input = $('#TimePicker');
           input[0].focus();

           isChanged = false;
           input.trigger({ type: "keydown", keyCode: 40 });
           input.trigger({ type: "keydown", keyCode: 27 });

           ok(isChanged);
       });

       test('clicking up arrow should raise onChange if dropDown is closed', function() {
           var input = $('#TimePicker');
           input[0].focus();
           getTimePicker().close();
           
           isChanged = false;
           input.trigger({ type: "keydown", keyCode: 38 });

           ok(isChanged);
       });
       
       test('not able to parse text should add error state', function() {
           var input = $('#TimePicker');
           input[0].focus();
           input.val('Not valid time');

           input.trigger({ type: "keydown", keyCode: 13 });

           ok(input.hasClass('t-state-error'), "t-error-state was not added!");
           
       });
       
       test('change event should not raise if value is set with value() and document is clicked', function () {
           isChanged = false;
           var timepicker = getTimePicker();
           timepicker.value(new Date());

           $(document.documentElement).mousedown();

           ok(!isChanged, "change event was raised incorrectly");
       });

       test('change event should not raise when call min() method', function () {
           isChanged = false;

           var timepicker = getTimePicker();
           timepicker.min(new Date());

           ok(!isChanged, "change event was raised incorrectly");
       });

</script>

</asp:Content>