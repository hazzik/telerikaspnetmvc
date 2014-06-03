<%@ Page Title="SingleExpandItem ClientAPI tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<style type="text/css">
                
        .t-state-focus
        {
            border-color: Red !important;
            border-width: 2px !important;
        }
    </style>


    <h2>Client API Tests</h2>
    
   <script type="text/javascript">

       function getDateTimePicker() {
           return $('#DateTimePicker').data('tDateTimePicker');
       }

       function isValidDate(date1, date2) {
           var isValid = true;

           if (date1.getFullYear() != date2.getFullYear())
               isValid = false;
           else if (date1.getMonth() != date2.getMonth())
               isValid = false;
           else if (date1.getDate() != date2.getDate())
               isValid = false;

           return isValid;
       }

       var isChanged;
       var isRaised;
       var count;
       //handlers

       function onChange(e) {
           isChanged = !isChanged;

           start();
       }

       function onClose() {
           isRaised = true;
       }

       function onOpen() {
           isRaised = true;
       }

       var onLoadDatePicker;

       function onLoad() {
           onLoadDatePicker = $(this).data('tDateTimePicker');
       }
   </script>

 <%= Html.Telerik().DateTimePicker().Name("DateTimePicker")
                   .Effects(e => e.Toggle())
                   .Min(new DateTime(1600, 1,1))
                   .Max(new DateTime(2400, 1, 1))
                   .ClientEvents(events => events.OnLoad("onLoad")
                                                 .OnChange("onChange")
                                                 .OnClose("onClose")
                                                 .OnOpen("onOpen"))
                                                 
 %>

</asp:Content>

<asp:Content ContentPlaceHolderID="TestContent" runat="server">

<script type="text/javascript">

       test('client object is available in on load', function() {
           ok(null !== onLoadDatePicker);
           ok(undefined !== onLoadDatePicker);
       });

       test('value should return selected date', function() {
           var dateTimepicker = getDateTimePicker();
           dateTimepicker.open();

           var $calendar = dateTimepicker.dateView.$calendar;

           var today = new $.telerik.datetime();

           var days = $calendar.find('td:not(.t-other-month)');

           var day = $.grep(days, function(n) {
               return $('.t-link', n).html() == today.date();
           });

           $(day).click();

           var result = new $.telerik.datetime(dateTimepicker.value());

           equal(result.year(), today.year(), 'year');
           equal(result.month(), today.month(), 'month');
           equal(result.date(), today.date(), 'date');
       });

       test('focusing input should raise onOpen event', function() {
           var dateTimepicker = getDateTimePicker();
           dateTimepicker.close();

           isRaised = false;

           dateTimepicker.$wrapper.find('.t-icon-calendar').click();         

           ok(isRaised);
       });

       test('clicking tab should raise onClose', function() {
           var dateTimepicker = getDateTimePicker();
           dateTimepicker.open();

           isRaised = false;
           var input = dateTimepicker.$element;
           input.trigger({ type: "keydown", keyCode: 9});        

           ok(isRaised);
       });

       test('clicking escape should raise onClose', function() {
           var dateTimepicker = getDateTimePicker();
           dateTimepicker.open();

           isRaised = false;

           var input = dateTimepicker.$element;
           input.trigger({ type: "keydown", keyCode: 27 });

           ok(isRaised);
       });

       test('clicking enter should raise onClose', function() {
           var dateTimepicker = getDateTimePicker();
           dateTimepicker.open();

           isRaised = false;

           var input = dateTimepicker.$element;
           input.trigger({ type: "keydown", keyCode: 13 });

           ok(isRaised);
       });

       test('change event should not raise if value is set with value() and document is clicked', function () {
           isChanged = false;
           var datetimepicker = getDateTimePicker();
           datetimepicker.value(new Date());

           $(document.documentElement).mousedown();

           ok(!isChanged, "change event was raised incorrectly");
       });

       asyncTest('change event should raise when DV is opened and Enter is clicked', function () {         
           var datetimepicker = getDateTimePicker();
           datetimepicker.close('time')
           datetimepicker.close('date')
           datetimepicker.value(null);

           datetimepicker.element.focus();
           datetimepicker.open('date');
           isChanged = false;
           datetimepicker.$element.trigger({ type: "keydown", keyCode: 13 });

           ok(isChanged);
       });

       asyncTest('change event should not raise when TV is opened and Enter is clicked, user did not change anything', function () {
           isChanged = false;
           var datetimepicker = getDateTimePicker();
           datetimepicker.close('time')
           datetimepicker.close('date')
           datetimepicker.value(null);

           datetimepicker.element.focus();
           datetimepicker.open('time');
           datetimepicker.$element.trigger({ type: "keydown", keyCode: 13 });

           ok(!isChanged);
       });
</script>

</asp:Content>