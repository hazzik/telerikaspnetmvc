<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%

    string culture = Request.QueryString["culture"] ?? "en-US";
    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);

    Response.Write("<script type='text/javascript'>var culture='");
    Response.Write(culture);
    Response.Write("';</script>");
%>

<ul>
    <li><%= .5.ToString("#,###.00") %></li>
    <li><%= 0.5.ToString("#,###.00") %></li>
    <li><%= 0.ToString("#,###.00") %></li>
</ul>

    <h2>
        NumberFormatting</h2>

    <script type="text/javascript">
        var $t;

        function setUp() {
            $t = $.telerik;
        }

        function test_number_formatting_supports_decimal_format() { //D
            var number = 12123.12;
            assertEquals(culture, '<%= (12123).ToString("D") %>', $t.textbox.formatNumber(number, "D"));
        }

        function test_number_formatting_supports_decimal_format_with_small_d() { //d
            var number = 12123.12;
            assertEquals(culture, '<%= (12123).ToString("d") %>', $t.textbox.formatNumber(number, "d"));
        }

        function test_number_formatting_supports_currency_format() { //C
            var number = 12123.129;
            assertEquals(culture, '<%= (12123.13).ToString("C") %>', $t.textbox.formatNumber(number, "C"));
        }

        function test_number_formatting_supports_currency_format_with_small_c() { //C

            var number = 12123.129;
            assertEquals(culture, '<%= (12123.13).ToString("c") %>', $t.textbox.formatNumber(number, "c"));
        }

        function test_number_formatting_supports_Number_format() { //N
            var number = 12123.129;
            assertEquals(culture, '<%= (12123.13).ToString("N") %>', $t.textbox.formatNumber(number, "N"));
        }

        function test_number_formatting_supports_Number_format_with_small_n() { //n
            var number = 12123.129;
            assertEquals(culture, '<%= (12123.13).ToString("n") %>', $t.textbox.formatNumber(number, "n"));
        }

        function test_number_formatting_supports_Percent_format() { //P
            var number = 12123.129;
            assertEquals(culture, '<%= (12123.129).ToString("P") %>', $t.textbox.formatNumber(number, "P"));
        }

        function test_number_formatting_supports_Percent_format_with_small_p() { //p
            var number = 12123.129;
            assertEquals(culture, '<%= (12123.129).ToString("p") %>', $t.textbox.formatNumber(number, "p"));
        }

        function test_number_formatting_supports_decimal_format_used_in_String_Format() { //D
            var number = 12123.12;
            assertEquals(culture, '<%= (12123).ToString("D") %>', $t.textbox.formatNumber(number, "{0:D}"));
        }

        function test_number_formatting_supports_decimal_format_with_small_d_used_in_String_Format() { //d
            var number = 12123.12;
            assertEquals(culture, '<%= (12123).ToString("d") %>', $t.textbox.formatNumber(number, "{0:d}"));
        }

        function test_number_formatting_supports_currency_format_used_in_String_Format() { //C
            var number = 12123.129;
            assertEquals(culture, '<%= (12123.13).ToString("C") %>', $t.textbox.formatNumber(number, "{0:C}"));
        }

        function test_number_formatting_supports_currency_format_with_small_c_used_in_String_Format() { //C

            var number = 12123.129;
            assertEquals(culture, '<%= (12123.13).ToString("c") %>', $t.textbox.formatNumber(number, "{0:c}"));
        }

        function test_number_formatting_supports_Number_format_used_in_String_Format() { //N
            var number = 12123.129;
            assertEquals(culture, '<%= (12123.13).ToString("N") %>', $t.textbox.formatNumber(number, "{0:N}"));
        }

        function test_number_formatting_supports_Number_format_with_small_n_used_in_String_Format() { //n
            var number = 12123.129;
            assertEquals(culture, '<%= (12123.13).ToString("n") %>', $t.textbox.formatNumber(number, "{0:n}"));
        }

        function test_number_formatting_supports_Percent_format_used_in_String_Format() { //P
            var number = 12123.129;
            assertEquals(culture, '<%= (12123.129).ToString("P") %>', $t.textbox.formatNumber(number, "{0:P}"));
        }

        function test_number_formatting_supports_Percent_format_with_small_p_used_in_String_Format() { //p
            var number = 12123.129;
            assertEquals(culture, '<%= (12123.129).ToString("p") %>', $t.textbox.formatNumber(number, "{0:p}"));
        }

        //custom formating;
        function test_custom_number_formatting_with_normal_number_format() {
            var number = 12123.12;
            assertEquals(culture, '<%= (12123.12).ToString("###.##") %>', $t.textbox.formatNumber(number, "###.##"));
        }

        function test_custom_number_formatting_with_three_digits_after_decimal_point() {
            var number = 12123.12;
            assertEquals(culture, '<%= (12123.12).ToString("###.##0") %>', $t.textbox.formatNumber(number, "###.##0"));
        }

        function test_custom_number_formatting_with_decimal_digits_and_int_number() {
            var number = 12123;
            assertEquals(culture, '<%= (12123).ToString("###.##") %>', $t.textbox.formatNumber(number, "###.##"));
        }

        function test_custom_number_formatting_with_one_decimal_digits_and_double_number() {
            var number = 12123.17;
            assertEquals(culture, '<%= (12123.17).ToString("###.0") %>', $t.textbox.formatNumber(number, "###.0"));
        }

        function test_custom_number_formatting_with_one_decimal_digits_and_obligotory_zeros() {
            var number = 23.17;
            assertEquals(culture, '<%= (23.17).ToString("#00##.0") %>', $t.textbox.formatNumber(number, "#00##.0"));
        }

        function test_custom_number_formatting_with_currency_symbol() {
            var number = 23.17;
            assertEquals(culture, '<%= (23.17).ToString("$ #00##.0") %>', $t.textbox.formatNumber(number, "$ #00##.0"));
        }

        function test_custom_number_formatting_with_currency_symbol_and_string_format() {
            var number = 23.17;
            assertEquals(culture, '<%= String.Format("{0:$ #00##.0}", 23.17) %>', $t.textbox.formatNumber(number, "{0:$ #00##.0}"));
        }


        function test_custom_number_formatting_with_other_characteres() {
            var number = 23.17;
            assertEquals(culture, '<%= (23.17).ToString("(#) - ##") %>', $t.textbox.formatNumber(number, "(#) - ##"));
        }

        function test_custom_number_formatting_with_other_characteres_and_obligatory_zeros() {
            var number = 23.17;
            assertEquals(culture, '<%= (23.17).ToString("(0) - 00") %>', $t.textbox.formatNumber(number, "(0) - 00"));
        }

        function test_custom_number_formatting_with_negative_pattern() {
            var number = -23.17;
            assertEquals(culture, '<%= (-23.17).ToString("000.00; -###.00") %>', $t.textbox.formatNumber(number, "000.00; -###.00"));
        }

        function test_custom_number_formatting_with_negative_pattern_and_string_format() {
            var number = -23.17;
            assertEquals(culture, '<%= String.Format("{0:000.00;- 0###.00}", -23.17) %>', $t.textbox.formatNumber(number, "{0:000.00;- 0###.00}"));
        }

        function test_custom_number_formatting_with_zero_pattern() {
            var number = 0;
            assertEquals(culture, '<%= (0).ToString("000.00; -###.00;zero") %>', $t.textbox.formatNumber(number, "000.00; -###.00;zero"));
        }

        function test_custom_number_formatting_with_one_zero() {
            var number = 123.12;
            assertEquals(culture, '<%= (123.12).ToString("0$") %>', $t.textbox.formatNumber(number, "0$"));
        }

        function test_custom_number_format_with_less_digits_than_specified_in_format() {
            var value = $t.textbox.formatNumber(666, "#,###.00");
            assertEquals(culture, '<%= 666.ToString("#,###.00") %>', value);
        }

        function test_custom_number_format_with_less_digits_than_specified_in_format1() {
            var value = $t.textbox.formatNumber(666, "#,##,###.00");
            assertEquals(culture, '<%= 666.ToString("#,##,###.00") %>', value);
        }

        function test_custom_number_format_with_less_digits_than_specified_in_format_which_has_more_than_two_group_separators() {
            var value = $t.textbox.formatNumber(6666, "#,##,###.00");
            assertEquals(culture, '<%= 6666.ToString("#,##,###.00") %>', value);
        }

        function test_custom_number_format_exact_digits_count_as_format() {
            var value = $t.textbox.formatNumber(6666666, "#,###,###.00");
            assertEquals(culture, '<%= 6666666.ToString("#,###,###.00") %>', value);
        }

        function test_custom_number_format_with_less_digits_than_specified_in_format_less_than_zero() {
            var value = $t.textbox.formatNumber(.5, "#,###.00");
            assertEquals(culture, '<%= .5.ToString("#,###.00") %>', value);
        }
    </script>

    <ul>
        <li>Current culture:
            <%= System.Threading.Thread.CurrentThread.CurrentCulture.Name %></li>
        <li>Current UI culture:
            <%= System.Threading.Thread.CurrentThread.CurrentUICulture.Name %></li>
        <li>
            Currency info!
        </li>
        <li>CurrencyDecimalDigits:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalDigits %></li>
        <li>CurrencyDecimalSeparator:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator%></li>
        <li>CurrencyGroupSeparator:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyGroupSeparator%></li>
        <li>CurrencyGroupSizes:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyGroupSizes%></li>
        <li>CurrencyNegativePattern:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyNegativePattern%></li>
        <li>CurrencyPositivePattern:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyPositivePattern%></li>
        <li>
            CurrencySymbol:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.CurrencySymbol%>
        </li>
        <li>
            Number info!
        </li>
        <li>NumberDecimalDigits:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalDigits %></li>
        <li>NumberDecimalSeparator:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator%></li>
        <li>NumberGroupSeparator:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator%></li>
        <li>NumberGroupSizes:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSizes%></li>
        <li>NumberNegativePattern:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.NumberNegativePattern%></li>
        <li>
            Percent info!
        </li>
        <li>PercentDecimalDigits:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.PercentDecimalDigits %></li>
        <li>PercentDecimalSeparator:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.PercentDecimalSeparator%></li>
        <li>PercentGroupSeparator:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.PercentGroupSeparator%></li>
        <li>PercentGroupSizes:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.PercentGroupSizes%></li>
        <li>PercentNegativePattern:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.PercentNegativePattern%></li>
        <li>PercentPositivePattern:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.PercentPositivePattern%></li>
        <li>
            PercentSymbol:
            <%= System.Globalization.NumberFormatInfo.CurrentInfo.PercentSymbol%>
        </li>
        </ul>
    <%
        Html.Telerik().ScriptRegistrar().Globalization(true)
                            .DefaultGroup(group => group
                                .Add("telerik.common.js")
                                .Add("telerik.textbox.js"));

    %>
</asp:Content>
