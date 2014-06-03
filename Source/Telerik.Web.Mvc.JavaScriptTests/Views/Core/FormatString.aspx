<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>FormatString</h2>
    <script type="text/javascript">
      var $t;

        function setUp() {
            $t = $.telerik;
        }

        function test_string_format_with_less_arguments() {
            assertEquals('foo {0}', $t.formatString('foo {0}'));
        }
        
        function test_string_format_with_more_arguments() {
            assertEquals('foo', $t.formatString('foo', 1));
        }
        
        function test_string_format_with_missing_format_argument() {
            assertEquals('foo 2 3', $t.formatString('foo {1} {2}', 1, 2, 3));
        }

    </script>
    <% Html.Telerik().ScriptRegistrar()
                            .DefaultGroup(group => group
                                    .Add("telerik.common.js")
                                    .Add("telerik.textbox.js")
        );
    %>
</asp:Content>
