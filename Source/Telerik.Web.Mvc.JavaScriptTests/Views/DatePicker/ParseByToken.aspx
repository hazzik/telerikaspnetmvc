<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Date by token</h2>

    <script type="text/javascript">
        var $t;

        function setUp() {
            $t = $.telerik;
        }

        function compareDates(expected, returned) {
            var isValid = true;

            if (expected.getFullYear() != returned.year())
                isValid = false;
            else if (expected.getMonth() != returned.month())
                isValid = false;
            else if (expected.getDate() != returned.date())
                isValid = false;

            return isValid;
        }

        function test_parseByToken_should_return_today_date_token_today() {
           
            var expectedDate = new Date();

            var returnedDate = $t.datetime.parseByToken("today");

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_yesterday_date_token_yesterday() {
            var expectedDate = new Date();
            expectedDate.setDate(expectedDate.getDate() -1);

            var returnedDate = $t.datetime.parseByToken("yesterday");

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_tomorrow_date_token_tomorrow() {
            var expectedDate = new Date();
            expectedDate.setDate(expectedDate.getDate() + 1);

            var returnedDate = $t.datetime.parseByToken("tomorrow");

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_monday_of_current_week() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27); //friday
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = $t.datetime.parseByToken("monday", tmpDate);

            expectedDate.setDate(expectedDate.getDate() - 4); //set the expected date

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_friday_of_current_week() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //friday
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = $t.datetime.parseByToken("friday", tmpDate);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_sunday_of_current_week() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27); //friday
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = $t.datetime.parseByToken("sunday", tmpDate);

            expectedDate.setDate(expectedDate.getDate() + 2); //set the expected date

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_march_of_current_year() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = $t.datetime.parseByToken("March", tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() - 8);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_December_of_current_year() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = $t.datetime.parseByToken("december", tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() + 1);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_November_of_current_year() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = $t.datetime.parseByToken("november", tmpDate);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_friday() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = $t.datetime.parseByToken("next friday", tmpDate);

            expectedDate.setDate(expectedDate.getDate() + 7);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_friday() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = $t.datetime.parseByToken("last friday", tmpDate);

            expectedDate.setDate(expectedDate.getDate() - 7);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_monday() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = $t.datetime.parseByToken("next monday", tmpDate);

            expectedDate.setDate(expectedDate.getDate() + 3);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_monday() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = $t.datetime.parseByToken("last monday", tmpDate);

            expectedDate.setDate(expectedDate.getDate() - 11);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_sunday() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = $t.datetime.parseByToken("next sunday", tmpDate);

            expectedDate.setDate(expectedDate.getDate() + 9);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_November() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = $t.datetime.parseByToken("next november", tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() + 12);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_november() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = $t.datetime.parseByToken("last november", tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() - 12);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_february() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = $t.datetime.parseByToken("next february", tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() + 3);
            
            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_february() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = $t.datetime.parseByToken("last february", tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() - 21);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_december() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = $t.datetime.parseByToken("next december", tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() + 13);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        //short names
        function test_parseByToken_should_return_next_fri() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = $t.datetime.parseByToken("next fri", tmpDate);

            expectedDate.setDate(expectedDate.getDate() + 7);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_fri() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = $t.datetime.parseByToken("last fri", tmpDate);

            expectedDate.setDate(expectedDate.getDate() - 7);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_mon() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = $t.datetime.parseByToken("next mon", tmpDate);

            expectedDate.setDate(expectedDate.getDate() + 3);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_mon() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = $t.datetime.parseByToken("last mon", tmpDate);

            expectedDate.setDate(expectedDate.getDate() - 11);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_sun() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //friday

            var returnedDate = $t.datetime.parseByToken("next sun", tmpDate);

            expectedDate.setDate(expectedDate.getDate() + 9);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_Nov() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = $t.datetime.parseByToken("next Nov", tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() + 12);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_Nov() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = $t.datetime.parseByToken("last Nov", tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() - 12);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_feb() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = $t.datetime.parseByToken("next feb", tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() + 3);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_feb() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = $t.datetime.parseByToken("last feb", tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() - 21);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_dec() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27); //November
            var expectedDate = new Date(2009, 10, 27); //November

            var returnedDate = $t.datetime.parseByToken("next dec", tmpDate);

            expectedDate.setMonth(expectedDate.getMonth() + 13);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_year() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27);
            var expectedDate = new Date(2009, 10, 27);

            var returnedDate = $t.datetime.parseByToken("next year", tmpDate);

            expectedDate.setFullYear(expectedDate.getFullYear() + 1, expectedDate.getMonth(), expectedDate.getDate());

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_year() {
            var tmpDate = new $.telerik.datetime(2009, 10, 27);
            var expectedDate = new Date(2009, 10, 27);

            var returnedDate = $t.datetime.parseByToken("last year", tmpDate);

            expectedDate.setFullYear(expectedDate.getFullYear() - 1, expectedDate.getMonth(), expectedDate.getDate());

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_month() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27);
            var expectedDate = new Date(2009, 10, 27);

            var returnedDate = $t.datetime.parseByToken("next month", tmpDate);

            expectedDate.setFullYear(expectedDate.getFullYear(), expectedDate.getMonth() + 1, expectedDate.getDate());

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_month() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27);
            var expectedDate = new Date(2009, 10, 27);

            var returnedDate = $t.datetime.parseByToken("last month", tmpDate);

            expectedDate.setFullYear(expectedDate.getFullYear(), expectedDate.getMonth() - 1, expectedDate.getDate());

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_week() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27);
            var expectedDate = new Date(2009, 10, 27);

            var returnedDate = $t.datetime.parseByToken("next week", tmpDate);

            expectedDate.setFullYear(expectedDate.getFullYear(), expectedDate.getMonth(), expectedDate.getDate() + 7);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_last_week() {

            var tmpDate = new $.telerik.datetime(2009, 10, 27);
            var expectedDate = new Date(2009, 10, 27);

            var returnedDate = $t.datetime.parseByToken("last week", tmpDate);

            expectedDate.setFullYear(expectedDate.getFullYear(), expectedDate.getMonth(), expectedDate.getDate() - 7);

            assertTrue(compareDates(expectedDate, returnedDate));
        }

        function test_parseByToken_should_return_next_day() { //like tomorrow

            var tmpDate = new $.telerik.datetime(2009, 10, 27);
            var expectedDate = new Date(2009, 10, 27);

            var returnedDate = $t.datetime.parseByToken("next day", tmpDate);

            expectedDate.setFullYear(expectedDate.getFullYear(), expectedDate.getMonth(), expectedDate.getDate() + 1);

            assertTrue(compareDates(expectedDate, returnedDate));
        }


        function test_parseByToken_should_return_last_day() { //like tomorrow

            var tmpDate = new $.telerik.datetime(2009, 10, 27);
            var expectedDate = new Date(2009, 10, 27);

            var returnedDate = $t.datetime.parseByToken("last day", tmpDate);

            expectedDate.setFullYear(expectedDate.getFullYear(), expectedDate.getMonth(), expectedDate.getDate() - 1);

            assertTrue(compareDates(expectedDate, returnedDate));
        }
        
        function test_parseByToken_should_return_null_if_cannot_parse_first_token()
        {
            var returnedDate = $t.datetime.parseByToken("undefined");
            assertNull(returnedDate);
        }

        function test_parseByToken_should_return_null_if_second_token_is_not_provided() {
            var returnedDate = $t.datetime.parseByToken("next ");
            assertNull(returnedDate);
        }

        function test_parseByToken_should_return_null_if_second_token_is_not_correct() {
            var returnedDate = $t.datetime.parseByToken("next undeffined");
            assertNull(returnedDate);
        }
       
    </script>
    
    
    
    <%= Html.Telerik().DatePicker()
            .Name("DatePicker")
            .MinDate(new DateTime(1900, 01, 01))
            .MaxDate(new DateTime(2100, 01, 01))
    %>
    
    <% Html.Telerik().ScriptRegistrar()
           .Scripts(scripts => scripts
               .Add("telerik.common.js")
               .Add("telerik.calendar.js")
               .Add("telerik.datepicker.js")); %>

</asp:Content>
