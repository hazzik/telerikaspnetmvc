<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>IndentFormatter</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    
    <script type="text/javascript">

        var editor, IndentFormatter, TextNodeEnumerator;

        function setUp() {
            editor = getEditor();
            IndentFormatter = $.telerik.editor.IndentFormatter;
            TextNodeEnumerator = $.telerik.editor.TextNodeEnumerator;
        }

        function test_apply_to_text_node() {
            editor.value('foo');
            formatter = new IndentFormatter();
            formatter.apply([editor.body.firstChild]);
            assertEquals('<div style="margin-left:30px;">foo</div>', editor.value());
        }

        function test_apply_to_inline_node() {
            editor.value('<span>foo</span>');
            formatter = new IndentFormatter();
            formatter.apply([editor.body.firstChild.firstChild]);
            assertEquals('<div style="margin-left:30px;"><span>foo</span></div>', editor.value());
        }
        
        function test_apply_to_block_node() {
            editor.value('<div>foo</div>');
            formatter = new IndentFormatter();
            formatter.apply([editor.body.firstChild.firstChild]);
            assertEquals('<div style="margin-left:30px;">foo</div>', editor.value());
        }
        
        function test_apply_to_selection() {
            editor.value('<div>foo</div><div>bar</div>');
            formatter = new IndentFormatter();
            formatter.apply([editor.body.firstChild.firstChild,editor.body.lastChild.firstChild]);
            assertEquals('<div style="margin-left:30px;">foo</div><div style="margin-left:30px;">bar</div>', editor.value());
        }

        function test_remove_from_selection() {
            editor.value('<div style="margin-left:30px;">foo</div><div style="margin-left:30px;">bar</div>');
            formatter = new IndentFormatter();
            formatter.remove([editor.body.firstChild.firstChild,editor.body.lastChild.firstChild]);
            assertEquals('<div>foo</div><div>bar</div>', editor.value());
        }

        function test_apply_increases_margin() {
            editor.value('<div style="margin-left:30px">foo</div>');
            formatter = new IndentFormatter();
            formatter.apply([editor.body.firstChild.firstChild]);
            assertEquals('<div style="margin-left:60px;">foo</div>', editor.value());
        }

        function test_remove_removes_margin() {
            editor.value('<div style="margin-left:30px">foo</div>');
            formatter = new IndentFormatter();
            formatter.remove([editor.body.firstChild.firstChild]);
            assertEquals('<div>foo</div>', editor.value());
        }

        function test_remove_decreases_margin() {
            editor.value('<div style="margin-left:60px">foo</div>');
            formatter = new IndentFormatter();
            formatter.remove([editor.body.firstChild.firstChild]);
            assertEquals('<div style="margin-left:30px;">foo</div>', editor.value());
        }

        function test_apply_first_li_adds_margin_to_ul() {
            editor.value('<ul><li>foo</li></ul>');
            formatter = new IndentFormatter();
            formatter.apply([editor.body.firstChild.firstChild.firstChild]);
            assertEquals('<ul style="margin-left:30px;"><li>foo</li></ul>', editor.value());
        }
        
        function test_apply_selection_of_li_including_first_adds_margin_to_ul() {
            editor.value('<ul><li>foo</li><li>bar</li></ul>');
            formatter = new IndentFormatter();
            formatter.apply([editor.body.firstChild.firstChild.firstChild,editor.body.firstChild.lastChild.firstChild]);
            assertEquals('<ul style="margin-left:30px;"><li>foo</li><li>bar</li></ul>', editor.value());
        }

        function test_apply_second_li_nests_in_previous_li() {
            editor.value('<ul><li>foo</li><li>bar</li></ul>');
            formatter = new IndentFormatter();
            formatter.apply([editor.body.firstChild.lastChild.firstChild]);
            assertEquals('<ul><li>foo<ul><li>bar</li></ul></li></ul>', editor.value());
        }

        function test_apply_reuses_nested_list() {
            editor.value('<ul><li>foo<ul><li>baz</li></ul></li><li>bar</li></ul>');
            formatter = new IndentFormatter();
            formatter.apply([editor.body.firstChild.lastChild.firstChild]);
            assertEquals('<ul><li>foo<ul><li>baz</li><li>bar</li></ul></li></ul>', editor.value());
            
        }

        function test_apply_nests_selected_lis_in_previous_li() {
            editor.value('<ul><li>foo</li><li>bar</li><li>baz</li></ul>');
            formatter = new IndentFormatter();
            formatter.apply([editor.body.firstChild.firstChild.nextSibling.firstChild, editor.body.firstChild.lastChild.firstChild]);
            assertEquals('<ul><li>foo<ul><li>bar</li><li>baz</li></ul></li></ul>', editor.value());
        }

        function test_remove_first_li_removes_margin_from_ul() {
            editor.value('<ul style="margin-left:30px;"><li>foo</li></ul>');
            formatter = new IndentFormatter();
            formatter.remove([editor.body.firstChild.firstChild.firstChild]);
            assertEquals('<ul><li>foo</li></ul>', editor.value());
        }
        
        function test_remove_nested_li_unnests_it() {
            editor.value('<ul><li>foo<ul><li>bar</li></ul></li></ul>');
            formatter = new IndentFormatter();
            formatter.remove([$(editor.body).find('ul ul li:first')[0].firstChild]);
            assertEquals('<ul><li>foo</li><li>bar</li></ul>', editor.value());
        }

        function test_remove_nested_li_unnests_it_and_nests_siblings() {
            editor.value('<ul><li>foo<ul><li>bar</li><li>baz</li></ul></li></ul>');
            formatter = new IndentFormatter();
            formatter.remove([$(editor.body).find('ul ul li:first')[0].firstChild]);
            assertEquals('<ul><li>foo</li><li>bar<ul><li>baz</li></ul></li></ul>', editor.value());
        }

        function test_double_nested_li_removes_margin() {
            editor.value('<ul><li>foo<ul style="margin-left:30px;"><li>bar</li></ul></li></ul>');
            formatter = new IndentFormatter();
            formatter.remove([$(editor.body).find('ul ul li:first')[0].firstChild]);
            assertEquals('<ul><li>foo<ul><li>bar</li></ul></li></ul>', editor.value());
        }
    </script>
</asp:Content>
