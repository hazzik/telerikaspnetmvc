<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%

    string culture = Request.QueryString["culture"] ?? "en-US";
    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);

    Response.Write("<script type='text/javascript'>var culture='");
    Response.Write(culture);
    Response.Write("';</script>");
%>
    <h2>
        NumberFormatting</h2>

    <script type="text/javascript">
        var $t;

        function setUp() {
            $t = $.telerik;
        }
        
        function isValidDate(expected, result) {
            var isValid = true;

            if (expected.year() != result.year())
                isValid = false;
            else if (expected.month() != result.month())
                isValid = false;
            else if (expected.date() != result.date())
                isValid = false;

            return isValid;
        }

        function test_time_parsing_supports_short_time_format() {
            var dateToModify = new $t.datetime(2000, 1, 20, 10, 0, 20);
            var value = '<%= new DateTime(2000, 1, 20, 22, 20, 20).ToString("t") %>';

            var result = $t.datetime.parse({ value: value, format: "t", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 22, result.hours());
            assertEquals(culture + "-minutes", 20, result.minutes());
            if($t.cultureInfo.shortTime.indexOf('ss') == -1)
                assertEquals(culture + "-seconds", 0, result.seconds());
        }

        function test_time_parsing_should_parse_12_PM_with_short_time_format() {
            var dateToModify = new $t.datetime(2000, 1, 10, 15, 15, 0);
            var value = '<%= new DateTime(2000, 1, 10, 12, 15, 0).ToString("t") %>';

            var result = $t.datetime.parse({ value: value, format: "t", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 12, result.hours());
            assertEquals(culture + "-minutes", 15, result.minutes());
        }

        function test_time_parsing_should_parse_12_AM_with_short_time_format() {
            var dateToModify = new $t.datetime(2000, 1, 10, 15, 15, 0);
            var value = '<%= new DateTime(2000, 1, 10, 0, 15, 0).ToString("t") %>';
            
            var result = $t.datetime.parse({ value: value, format: "t", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 0, result.hours());
            assertEquals(culture + "-minutes", 15, result.minutes());
        }

        function test_time_parsing_supports_long_time_format() {
            var dateToModify = new $t.datetime(2000, 1, 20, 10, 0, 20);
            var value = '<%= new DateTime(2000, 1, 20, 18, 0, 30).ToString("T") %>';

            var result = $t.datetime.parse({ value: value, format: "T", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 18, result.hours());
            assertEquals(culture + "-minutes", 0, result.minutes());
            assertEquals(culture + "-seconds", 30, result.seconds());
        }

        function test_time_parsing_should_parse_morning_hours_using_short_time_format() {
            var dateToModify = new $t.datetime(2000, 1, 20, 10, 0, 20);
            var value = '<%= new DateTime(2000, 1, 20, 9, 30, 0).ToString("t") %>';

            var result = $t.datetime.parse({ value: value, format: "t", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 9, result.hours());
            assertEquals(culture + "-minutes", 30, result.minutes());
            assertEquals(culture + "-seconds", 0, result.seconds());
        }

        function test_time_parsing_should_parse_morning_hours_using_long_time_format() {
            var dateToModify = new $t.datetime(2000, 1, 20, 10, 0, 20);
            var value = '<%= new DateTime(2000, 1, 20, 9, 0, 30).ToString("T") %>';

            var result = $t.datetime.parse({ value: value, format: "T", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 9, result.hours());
            assertEquals(culture + "-minutes", 0, result.minutes());
            assertEquals(culture + "-seconds", 30, result.seconds());
        }

        function test_time_parsing_should_parse_24_and_return_0_hour() {
            var dateToModify = new $t.datetime(2000, 1, 20, 10, 0, 20);
            var value = "24:00"
            
            var result = $t.datetime.parse({ value: value, format: "H:mm", baseDate: dateToModify });

            assertEquals(culture, null, result);
        }

        function test_time_parsing_should_parse_23_59() {
            var dateToModify = new $t.datetime(2000, 1, 20, 10, 0, 20);
            var value = "23:59"
            
            var result = $t.datetime.parse({ value: value, format: "H:mm", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 23, result.hours());
            assertEquals(culture + "-minutes", 59, result.minutes());
            assertEquals(culture + "-seconds", 0, result.seconds());
        }

        function test_time_parsing_should_parse_using_H_mm() {
            var dateToModify = new $t.datetime(2000, 1, 20, 10, 0, 20);
            var value = "13:22";

            var result = $t.datetime.parse({ value: value, format: "H:mm", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 13, result.hours());
            assertEquals(culture + "-minutes", 22, result.minutes());
            assertEquals(culture + "-seconds", 0, result.seconds());
        }

        function test_time_parsing_should_parse_with_seconds_using_H_mm_tt() {
            var dateToModify = new $t.datetime(2000, 1, 20, 10, 0, 20);
            var value = "4:22:22 PM";

            var result = $t.datetime.parse({ value: value, format: "H:mm tt", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 16, result.hours());
            assertEquals(culture + "-minutes", 22, result.minutes());
            assertEquals(culture + "-seconds", 0, result.seconds());
        }

        function test_time_parsing_should_parse_without_seconds_using_H_mm_tt() {
            var dateToModify = new $t.datetime(2000, 1, 20, 10, 0, 20);
            var value = "4:22 PM";

            var result = $t.datetime.parse({ value: value, format: "H:mm tt", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 16, result.hours());
            assertEquals(culture + "-minutes", 22, result.minutes());
            assertEquals(culture + "-seconds", 0, result.seconds());
        }

        function test_time_parsing_should_parse_with_seconds_using_H_mm_ss_tt() {
            var dateToModify = new $t.datetime(2000, 1, 20, 10, 0, 20);
            var value = "4:22:22 PM";

            var result = $t.datetime.parse({ value: value, format: "H:mm:ss tt", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 16, result.hours());
            assertEquals(culture + "-minutes", 22, result.minutes());
            assertEquals(culture + "-seconds", 22, result.seconds());
        }

        function test_time_parsing_should_parse_without_seconds_using_H_mm_ss_tt() {
            var dateToModify = new $t.datetime(2000, 1, 20, 10, 0, 20);
            var value = "4:22 " + $t.cultureInfo.pm;

            var result = $t.datetime.parse({ value: value, format: "H:mm:ss tt", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 16, result.hours());
            assertEquals(culture + "-minutes", 22, result.minutes());
            assertEquals(culture + "-seconds", 0, result.seconds());
        }

        function test_time_parsing_should_parse_H_mm_ss() {
            var dateToModify = new $t.datetime(2000, 1, 20, 10, 0, 20);
            var value = "22:10:22";
            
            var result = $t.datetime.parse({ value: value, format: "H:mm:ss", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 22, result.hours());
            assertEquals(culture + "-minutes", 10, result.minutes());
            assertEquals(culture + "-seconds", 22, result.seconds());
        }

        function test_time_parsing_not_fully_typed_hours_and_minutes() {
            var dateToModify = new $t.datetime(2000, 1, 20, 10, 0, 20);
            var value = "::22";

            var result = $t.datetime.parse({ value: value, format: "H:mm:ss", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 0, result.hours());
            assertEquals(culture + "-minutes", 0, result.minutes());
            assertEquals(culture + "-seconds", 22, result.seconds());
        }

        function test_time_parsing_should_parse_using_H_mm_ss_f_time_format() {
            var dateToModify = new $t.datetime(2000, 1, 20, 10, 0, 20);
            var value = "10:10:22.6";

            var result = $t.datetime.parse({ value: value, format: "H:mm:ss.f", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 10, result.hours());
            assertEquals(culture + "-minutes", 10, result.minutes());
            assertEquals(culture + "-seconds", 22, result.seconds());
            assertEquals(culture + "-milliseconds", 6, result.milliseconds());
        }

        function test_time_parsing_should_parse_using_H_mm_ss_ff_time_format() {
            var dateToModify = new $t.datetime(2000, 1, 20, 10, 0, 20);
            var value = "10:10:22.67";

            var result = $t.datetime.parse({ value: value, format: "H:mm:ss.ff", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 10, result.hours());
            assertEquals(culture + "-minutes", 10, result.minutes());
            assertEquals(culture + "-seconds", 22, result.seconds());
            assertEquals(culture + "-milliseconds", 67, result.milliseconds());
        }

        function test_time_parsing_should_parse_using_H_mm_ss_fff_time_format() {
            var dateToModify = new $t.datetime(2000, 1, 20, 10, 0, 20);
            var value = "10:10:22.6741";

            var result = $t.datetime.parse({ value: value, format: "H:mm:ss.fff", baseDate: dateToModify });

            assertTrue('date is not same', isValidDate(dateToModify, result));
            assertEquals(culture + "-hours", 10, result.hours());
            assertEquals(culture + "-minutes", 10, result.minutes());
            assertEquals(culture + "-seconds", 22, result.seconds());
            assertEquals(culture + "-milliseconds", 674, result.milliseconds());
        }

    </script>

    <ul>
        <li>Current culture:
            <%= System.Threading.Thread.CurrentThread.CurrentCulture.Name %></li>
        <li>Current UI culture:
            <%= System.Threading.Thread.CurrentThread.CurrentUICulture.Name %></li>
        </ul>
    <%
        Html.Telerik().ScriptRegistrar().Globalization(true)
                            .DefaultGroup(group => group
                                .Add("telerik.common.js"));

    %>
</asp:Content>
