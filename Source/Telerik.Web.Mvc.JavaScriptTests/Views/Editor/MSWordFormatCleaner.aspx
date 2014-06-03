<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MSWordFormatCleaner</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>

    <script type="text/javascript">
    
    var cleaner;
    
    function clean(html) {
       var value = cleaner.clean(html);
       return value.replace(/(<\/?[^>]*>)/g, function (_, tag) {
        return tag.toLowerCase();
       }).replace(/[\r\n]+/g, '');
    }

    function setUp() {
        cleaner = new $.telerik.editor.MSWordFormatCleaner();
    }
    
    function test_cleaning_meta_tags() {
        assertEquals('', clean('<meta content="text/html"><meta content="Word.Document">'));
    }

    function test_cleaning_link_tags() {
        assertEquals('', clean('<link href="file://clip_filelist.xml"><link rel="colorSchemeMapping"></link>'));
    }

    function test_cleaning_style_tags() {
        assertEquals('', clean('<style>foo<\/style>'));
    }
    
    function test_cleaning_invalid_tag_contents_style_tags() {
        assertEquals('', clean('<style>foo<\/style>'));
    }
    
    function test_ordered_list() {
        assertEquals('<ol><li>foo</li></ol>', clean('<p class="MsoListParagraphCxSpFirst" style="text-indent: -0.25in;"><!--[if !supportLists]--><span style=""><span style="">1.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span><!--[endif]-->foo</p>'));
    }

    function test_strip_comments() {
        assertEquals('', clean('<!--[if gte mso 9]>\n<xml>foo</xml><![endif]--><!--[if gte mso 9]><xml>foo</xml><![endif]-->'));
    }
    
    function test_strip_comments_regardles_of_version() {
        assertEquals('', clean('<!--[if gte mso 10]>\n<xml>foo</xml><![endif]--><!--[if gte mso 49]><xml>foo</xml><![endif]-->'));
    }

    function test_unordered_list() {
        assertEquals('<ul><li>foo</li></ul>', clean('<p class="MsoListParagraphCxSpFirst" style="text-indent: -0.25in;"><span style="font-family: Symbol;"><span style="">o<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>foo</p>'));
    }
    
    function test_class_removed() {
        assertEquals('<p>foo</p>', clean('<p class="foo">foo</p>'));
    }

    function test_class_without_quote_removed() {
        assertEquals('<p>foo</p>', clean('<p class=MsoTest>foo</p>'));
    }
    
    function test_class_without_quote_and_with_other_attributes_removed() {
        assertEquals(-1, clean('<p class=MsoTest id="foo-bar">foo</p>').indexOf('MsoTest'));
    }

    function test_remove_o_namespace() {
        assertEquals('', clean('<o:p>foo</o:p>'));
    }

    function test_remove_v_namespace() {
        assertEquals('', clean('<v:p>foo</v:p>'));
    }

    function test_remove_mso_style_attributes() {
        assertEquals(-1, clean('<p style="mso-fareast-font:Symbol;color:red;">foo</p>').indexOf('mso'));
    }
    
    function test_opening_list_when_there_is_no_comment() {
        assertEquals('<ul><li>foo</li></ul>', clean('<p style="text-indent: -0.25in;" class="MsoListParagraphCxSpFirst"><span style="font-family: Symbol;"><span style="">o<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>foo</p>'));
    }

    function test_comments_removed() {
        assertEquals('', clean('<!--[if gte vml 1]>foo<![endif]-->'));
    }

    function test_nested_lists_with_fractional_margin() {
        assertEquals('<ol><li>One<ol><li>Two</li></ol></li></ol>', clean('<p class="MsoListParagraphCxSpFirst" style="text-indent: -0.25in;"><span style=""><span style="">1.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>One</p><p class="MsoListParagraphCxSpFirst" style="margin-left: 0.75in; text-indent: -0.25in;"><span style=""><span style="">1.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>Two</p>'));
    }

    function test_nested_list_with_more_than_one_root_node() {
        assertEquals('<ol><li>One<ol><li>Two</li></ol></li><li>Three</li></ol>', clean('<p class="MsoListParagraphCxSpFirst" style="text-indent: -0.25in;"><span style=""><span style="">1.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>One</p><p class="MsoListParagraphCxSpFirst" style="margin-left: 0.75in; text-indent: -0.25in;"><span style=""><span style="">1.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>Two</p><p class="MsoListParagraphCxSpFirst" style="text-indent: -0.25in;"><span style=""><span style="">2.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>Three</p>'));
    }

    function test_paragraph_cleaned() {
        assertEquals('<p><span>foo</span></p>', clean('<p class="MsoTitle"><span>foo</span></p>'));
    }

    function test_list_paragraph() {
        assertEquals('<ul><li>foo</li></ul><p>bar</p>', clean('<p class="MsoListParagraphCxSpFirst"><span><span><span>o</span>&nbsp;</span></span>foo</p><p>bar</p>'));
    }
    
    function test_list_paragraph_list() {
        assertEquals('<ul><li>foo</li></ul><p>bar</p><ul><li>baz</li></ul>', clean('<p class="MsoListParagraphCxSpFirst"><span><span><span>o</span>&nbsp;</span></span>foo</p><p>bar</p><p class="MsoListParagraphCxSpFirst"><span><span><span>o</span>&nbsp;</span></span>baz</p>'));
    }

    function test_nested_list_paragraph_list() {
        assertEquals('<ul><li>foo<ul><li>moo</li></ul></li></ul><p>bar</p><ul><li>baz</li></ul>', clean('<p class="MsoListParagraphCxSpFirst"><span><span><span>o</span>&nbsp;</span></span>foo</p><p class="MsoListParagraphCxSpFirst" style="margin-left:1in;"><span><span><span>o</span>&nbsp;</span></span>moo</p><p>bar</p><p class="MsoListParagraphCxSpFirst"><span><span><span>o</span>&nbsp;</span></span>baz</p>'));
    }
    
    function test_list_block_element_list() {
        assertEquals('<ul><li>foo</li></ul><h1>bar</h1><ul><li>baz</li></ul>', clean('<p class="MsoListParagraphCxSpFirst"><span><span><span>o</span>&nbsp;</span></span>foo</p><h1>bar</h1><p class="MsoListParagraphCxSpFirst"><span><span><span>o</span>&nbsp;</span></span>baz</p>'));
    }

    function test_list_when_there_is_no_class_just_margin() {
        assertEquals('<ul><li>foo</li></ul>', clean('<p style="margin-left:1in;text-indent: -0.25in;"><span style="font-family: Symbol;"><span style="">o<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>foo</p>'));        
    }

    function test_empty_block_elements_skipped() {
        assertEquals('<ul><li>foo</li><li>baz</li></ul>', clean('<p class="MsoListParagraphCxSpFirst"><span><span><span>o</span>&nbsp;</span></span>foo</p><h1></h1><p class="MsoListParagraphCxSpFirst"><span><span><span>o</span>&nbsp;</span></span>baz</p>'));
    }

    function test_paragraph_which_contains_o_but_not_first_is_not_suitable() {
        assertEquals('<p><span><span>foo</span>&nbsp;</span>foo</p>', clean('<p class="MsoListParagraphCxSpFirst"><span><span>foo</span>&nbsp;</span>foo</p>'));
    }
    
    function test_paragraph_which_contains_number_but_not_first_is_not_suitable() {
        assertEquals('<p><span><span>foo1.</span>&nbsp;</span>foo</p>', clean('<p class="MsoListParagraphCxSpFirst"><span><span>foo1.</span>&nbsp;</span>foo</p>'));
    }

    function test_nested_lists() {
        assertEquals('<ul><li>foo<ul><li>bar<ul><li>baz</li></ul></li></ul></li><li>moo</li></ul>', clean('<p class="MsoListParagraphCxSpFirst" style="text-indent: -0.25in;"><span style="font-family: Symbol;"><span style="">·<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>foo</p><p class="MsoListParagraphCxSpMiddle" style="margin-left: 1in; text-indent: -0.25in;"><span style="font-family: &quot;Courier New&quot;;"><span style="">o<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;</span></span></span>bar</p><p class="MsoListParagraphCxSpMiddle" style="margin-left: 1.5in; text-indent: -0.25in;"><span style="font-family: Wingdings;"><span style="">§<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;</span></span></span>baz</p><p class="MsoListParagraphCxSpLast" style="text-indent: -0.25in;"><span style="font-family: Symbol;"><span style="">·<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>moo</p>'));
    }

    function test_unordered_list_with_two_nested_spans() {
        assertEquals('<ul><li>foo</li></ul>', clean('<p class="MsoListParagraphCxSpFirst" style="text-indent: -0.25in;"><span style="">o<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span>foo</p>'));
    }
    
    function test_unordered_list_from_dash() {
        assertEquals('<ul><li>foo</li></ul>', clean('<p class="MsoListParagraphCxSpFirst"><span><span>-</span>&nbsp;</span>foo</p>'));
    }
    
    function test_nested_lists_of_different_type_and_same_margin() {
        assertEquals('<ol><li>foo<ul><li>bar</li></ul></li></ol>', clean('<p><span><span>1.</span>&nbsp;&nbsp;</span>foo</p><p><span><span>o</span>&nbsp;</span>bar</p>'));
    }

    function test_mixed_multi_level_lists_setup_1() {
        assertEquals('<ul><li>foo<ol><li>bar<ol><li>moo</li></ol></li><li>baz</li></ol></li></ul>', clean('<p class="MsoListParagraphCxSpFirst" style="text-indent: -0.25in;"><span style="font-family: Symbol;"><span style="">·<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>foo</p><p class="MsoListParagraphCxSpMiddle" style="margin-left: 1in; text-indent: -0.25in;"><span style=""><span style="">1.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>bar</p><p class="MsoListParagraphCxSpMiddle" style="margin-left: 1.5in; text-indent: -0.25in;"><span style=""><span style="">1.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>moo</p><p class="MsoListParagraphCxSpLast" style="margin-left: 1in; text-indent: -0.25in;"><span style=""><span style="">2.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>baz</p>'));
    }

    function test_mixed_multi_level_lists_setup_2() {
        assertEquals('<ul><li>foo<ol><li>bar</li></ol></li><li>moo</li></ul>', clean('<p class="MsoListParagraphCxSpFirst" style="text-indent: -0.25in;"><span style="font-family: Symbol;"><span style="">·<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>foo</p><p class="MsoListParagraphCxSpMiddle" style="margin-left: 1in; text-indent: -0.25in;"><span style=""><span style="">1.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>bar</p><p class="MsoListParagraphCxSpLast" style="text-indent: -0.25in;"><span style="font-family: Symbol;"><span style="">·<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>moo</p>'));
    }

    function test_three_level_lists() {
        assertEquals('<ul><li>foo<ul><li>foo1</li></ul></li><li>bar<ul><li>bar1<ul><li>bar11</li></ul></li><li>bar2</li></ul></li></ul>', clean('<p class="MsoListParagraphCxSpFirst" style="text-indent: -0.25in;"><span style="font-family: Symbol;"><span style="">·<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>foo</p><p class="MsoListParagraphCxSpMiddle" style="margin-left: 1in; text-indent: -0.25in;"><span style="font-family: &quot;Courier New&quot;;"><span style="">o<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;</span></span></span>foo1</p><p class="MsoListParagraphCxSpMiddle" style="text-indent: -0.25in;"><span style="font-family: Symbol;"><span style="">·<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>bar</p><p class="MsoListParagraphCxSpMiddle" style="margin-left: 1in; text-indent: -0.25in;"><span style="font-family: &quot;Courier New&quot;;"><span style="">o<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;</span></span></span>bar1</p><p class="MsoListParagraphCxSpMiddle" style="margin-left: 1.5in; text-indent: -0.25in;"><span style="font-family: Wingdings;"><span style="">§<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;</span></span></span>bar11</p><p class="MsoListParagraphCxSpLast" style="margin-left: 1in; text-indent: -0.25in;"><span style="font-family: &quot;Courier New&quot;;"><span style="">o<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;</span></span></span>bar2</p>'));
    }
    
    function test_three_level_mixed_lists() {
        assertEquals('<ul><li>foo<ol><li>foo1</li></ol></li><li>bar<ol><li>bar1<ol><li>bar11</li></ol></li><li>bar2</li></ol></li></ul>', clean('<p class="MsoListParagraphCxSpFirst" style="text-indent: -0.25in;"><span style="font-family: Symbol;"><span style="">·<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>foo</p><p class="MsoListParagraphCxSpMiddle" style="margin-left: 1in; text-indent: -0.25in;"><span style=""><span style="">1.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>foo1</p><p class="MsoListParagraphCxSpMiddle" style="text-indent: -0.25in;"><span style="font-family: Symbol;"><span style="">·<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>bar</p><p class="MsoListParagraphCxSpMiddle" style="margin-left: 1in; text-indent: -0.25in;"><span style=""><span style="">1.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>bar1</p><p class="MsoListParagraphCxSpMiddle" style="margin-left: 1.5in; text-indent: -0.25in;"><span style=""><span style="">1.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>bar11</p><p class="MsoListParagraphCxSpLast" style="margin-left: 1in; text-indent: -0.25in;"><span style=""><span style="">2.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>bar2</p>'));
    }

    function test_mixed_lists_same_margin() {
        assertEquals('<ol><li>foo<ul><li>bar</li></ul></li><li>baz</li></ol>', clean('<p class="MsoNormal" style="margin: 3pt 0in 3pt 0.25in; text-align: justify; text-indent: -0.25in;"><span style="font-size: 10pt; font-family: &quot;Verdana&quot;,&quot;sans-serif&quot;;"><span style="">1.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;</span></span></span>foo</p><p class="MsoNormal" style="margin-left: 0.25in; text-align: justify; text-indent: -0.25in;"><span style="font-size: 10pt; font-family: Symbol;"><span style="">·<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>bar</p><p class="MsoNormal" style="margin: 3pt 0in 3pt 0.25in; text-align: justify; text-indent: -0.25in;"><span style="font-size: 10pt; font-family: &quot;Verdana&quot;,&quot;sans-serif&quot;;"><span style="">2.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;</span></span></span>baz</p>'));
    }
    
    function test_mixed_nested_lists_same_margin() {
        assertEquals('<ol><li>foo<ul><li>bar</li></ul></li><li>baz<ul><li>bar</li></ul></li></ol>', clean('<p class="MsoNormal" style="margin: 3pt 0in 3pt 0.25in; text-align: justify; text-indent: -0.25in;"><span style="font-size: 10pt; font-family: &quot;Verdana&quot;,&quot;sans-serif&quot;;"><span style="">1.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;</span></span></span>foo</p><p class="MsoNormal" style="margin-left: 0.25in; text-align: justify; text-indent: -0.25in;"><span style="font-size: 10pt; font-family: Symbol;"><span style="">·<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>bar</p><p class="MsoNormal" style="margin: 3pt 0in 3pt 0.25in; text-align: justify; text-indent: -0.25in;"><span style="font-size: 10pt; font-family: &quot;Verdana&quot;,&quot;sans-serif&quot;;"><span style="">2.<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;</span></span></span>baz</p><p class="MsoNormal" style="margin-left: 0.25in; text-align: justify; text-indent: -0.25in;"><span style="font-size: 10pt; font-family: Symbol;"><span style="">·<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>bar</p>'));
    }

    function test_ordered_list_from_parenthesis() {
        assertEquals('<ol><li>foo</li></ol>', clean('<p class="MsoListParagraphCxSpFirst" style="text-indent: -0.25in;"><span style="font-family: Symbol;"><span style="">1)<span style="font: 7pt &quot;Times New Roman&quot;;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span></span>foo</p>'));
    }    

    function test_handling_unclosed_tags() {
        assertEquals('<h2>foo<br></h2><div>bar</div>', clean('<h2>foo<br></h2><div>bar</div>'))
    }
    </script>
</asp:Content>
