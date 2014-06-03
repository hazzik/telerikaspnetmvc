<%@ Page Title="CollapseDelay Tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>ClientSideRendering</h2>

    <% Html.Telerik().ScriptRegistrar()
                        .DefaultGroup(group => group
                            .Add("telerik.common.js")
                            .Add("telerik.treeview.js"));

    %>

    <script type="text/javascript">

        var treeviewStatic;

        function setUp() {
            treeviewStatic = $.telerik.treeview;
        }

        function test_getGroupHtml_with_no_data_renders_empty_group() {
            var html = new $.telerik.stringBuilder();
            
            treeviewStatic.getGroupHtml([], html);

            assertEquals('<ul class="t-group"></ul>', html.string());
        }

        function test_getGroupHtml_for_first_level_renders_lines_class() {
            var html = new $.telerik.stringBuilder();
            
            treeviewStatic.getGroupHtml([], html, null, true);

            assertEquals('<ul class="t-group t-treeview-lines"></ul>', html.string());
        }

        function test_getGroupHtml_should_render_hidden_group_if_it_is_not_expanded() {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getGroupHtml([], html, null, false, false, false /*is not expanded*/);

            assertEquals('<ul class="t-group" style="display:none"></ul>', html.string());
        }

        function test_getGroupHtml_calls_getItemHtml_when_data_is_available() {
            var oldGetItemHtml = treeviewStatic.getItemHtml;
            
            try {
                var html = new $.telerik.stringBuilder();

                var called = false;

                treeviewStatic.getItemHtml = function() { called = true; }
            
                treeviewStatic.getGroupHtml([{}], html, null, true);

                assertTrue(called);

            } finally {
                treeviewStatic.getItemHtml = oldGetItemHtml;
            }
        }

        function test_getGroupHtml_calls_getItemHtml_for_each_data_item() {
            var oldGetItemHtml = treeviewStatic.getItemHtml;
            
            try {
                var html = new $.telerik.stringBuilder();

                var calls = 0;

                treeviewStatic.getItemHtml = function() { calls++; }
            
                treeviewStatic.getGroupHtml([{}, {}, {}], html, null, true);

                assertEquals(3, calls);

            } finally {
                treeviewStatic.getItemHtml = oldGetItemHtml;
            }
        }

        function test_getItemHtml_renders_simple_items() {
            var html = new $.telerik.stringBuilder();
            
            treeviewStatic.getItemHtml({
                    Text: "text"
                }, html, false, false, false, 0, 1);

            assertEquals('<li class="t-item t-last"><div class="t-bot"><span class="t-in">text</span></div></li>', html.string());
        }

        function test_getItemHtml_renders_links_for_items_with_NavigateUrl() {
            var html = new $.telerik.stringBuilder();
            
            treeviewStatic.getItemHtml({
                    Text: "text",
                    NavigateUrl: "http://google.com/"
                }, html, false, false, false, 0, 1);

            assertEquals('<li class="t-item t-last"><div class="t-bot"><a href="http://google.com/" class="t-link t-in">text</a></div></li>', html.string());
        }

        function test_getItemHtml_renders_disabled_state_on_disabled_items() {
            var html = new $.telerik.stringBuilder();
            
            treeviewStatic.getItemHtml({
                    Text: "text",
                    Enabled: false
                }, html, false, false, false, 0, 1);

            assertEquals('<li class="t-item t-last"><div class="t-bot"><span class="t-in t-state-disabled">text</span></div></li>', html.string());
        }

    </script>
</asp:Content>
