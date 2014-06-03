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

       function getDatePicker() {
           return $('#DatePicker').data('tDatePicker');
       }

       function test_client_object_is_available_in_on_load() {
           assertNotNull(onLoadDatePicker);
           assertNotUndefined(onLoadDatePicker);
       }

       function test_value_should_return_selected_date() {
           getDatePicker().open();

           var $calendar = getDatePicker().dateView.$calendar;

           var today = new Date();

           var days = $calendar.find('td:not(.t-other-month)');

           var day = $.grep(days, function(n) {
               return $('.t-link', n).html() == today.getDate();
           });

           $(day).click();

           assertTrue(isValidDate(today, getDatePicker().value()));
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
              
       function test_focusing_input_should_raise_onOpen_event() {
           getDatePicker().close();
           var input = $('#DatePicker .t-input');

           isRaised = false;

           input.focus();

           assertTrue(isRaised);
       }

       function test_clicking_tab_should_raise_onClose() {

           getDatePicker().open();

           isRaised = false;
           var input = $('#DatePicker .t-input');
           input.trigger({ type: "keydown", keyCode: 9});        

           assertTrue(isRaised);
       }

       function test_clicking_escape_should_raise_onClose() {

           getDatePicker().open();

           isRaised = false;
           
           var input = $('#DatePicker .t-input');
           input.trigger({ type: "keydown", keyCode: 27 });

           assertTrue(isRaised);
       }

       function test_clicking_enter_should_raise_onClose() {

           getDatePicker().open();

           isRaised = false;

           var input = $('#DatePicker .t-input');
           input.trigger({ type: "keydown", keyCode: 13 });

           assertTrue(isRaised);
       }

       //handlers

       function onChange(e) {
           isChanged = true;
       }

       function onClose() {
           isRaised = true;
       }

       function onOpen() {
           isRaised = true;
       }

       var onLoadDatePicker;

       function onLoad() {
           onLoadDatePicker = $(this).data('tDatePicker');
       }
   </script>

 <%= Html.Telerik().DatePicker().Name("DatePicker")
                   .Effects(e => e.Toggle())
                   .MinDate(new DateTime(1600, 1,1))
                   .MaxDate(new DateTime(2400, 1, 1))
                   .ClientEvents(events => events.OnLoad("onLoad")
                                                 .OnChange("onChange")
                                                 .OnClose("onClose")
                                                 .OnOpen("onOpen"))
                                                 
 %>

</asp:Content>