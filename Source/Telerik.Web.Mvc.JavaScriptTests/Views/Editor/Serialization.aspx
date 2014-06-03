<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Serialization</h2>

    
    <%= Html.Telerik().Editor().Name("Editor") %>

    <script type="text/javascript">
        function getEditor() {
            return $('#Editor').data('tEditor');
        }
        
        var editor;
        
        function setUp() {
            editor = getEditor();
        }

        function test_value_reciprocity() {
            var editor = getEditor();

            editor.value("<p>and now, for something completely different</p>");

            assertEquals("<p>and now, for something completely different</p>", editor.value());
        }
        
        function test_closes_empty_tags() {
            editor.value('<br>');
            assertEquals('<br />', editor.value());
        }

        function test_converts_to_lower_case() {
            editor.value('<BR>');
            assertEquals('<br />', editor.value());
        }

        function test_converts_mixed_case_to_lower_case() {
            editor.value('<Br>');
            assertEquals('<br />', editor.value());
        }

        function test_returns_child_tags() {
            editor.value('<div><span></div>');
            assertEquals('<div><span></span></div>', editor.value());
        }
        
        function test_returns_root_text_nodes() {
            editor.value('test');
            assertEquals('test', editor.value());
        }
        
        function test_returns_child_text_nodes() {
            editor.value('<span>test</span>');
            assertEquals('<span>test</span>', editor.value());
        }

        function test_fills_attributes() {
            editor.value('<input type=hidden>');
            assertEquals('<input type="hidden" />', editor.value());
        }

        function test_expands_attributes() {
            editor.value('<br disabled>');
            assertEquals('<br disabled="disabled" />', editor.value());
        }

        function test_fills_custom_attributes() {
            editor.value('<br test="test">');
            assertEquals('<br test="test" />', editor.value());
        }

        function test_caps_attributes() {
            editor.value('<br CLASS="test">');
            assertEquals('<br class="test" />', editor.value());
        }

        function test_attributes_containing_dashes() {
            editor.value('<br class=t-input />');
            assertEquals('<br class="t-input" />', editor.value());
        }

        function test_adds_closing_tag() {
            editor.value('<strong>');
            assertEquals('<strong></strong>', editor.value());
        }

        function test_fixes_improperly_nested_inline_tags() {
            editor.value('<strong><span></strong></span>');
            assertEquals('<strong><span></span></strong>', editor.value());
        }
        
        function test_fixes_improperly_nested_inline_and_block_tags() {
//How to fix this without string parsing ???
//            editor.value('<strong><div></strong></div>');
//            assertEquals('<strong></strong><div></div>', editor.value());
        }

        function test_handles_style_attribute_values() {
            editor.value('<div style="text-align:left"></div>');
            
            assertEquals('<div style="text-align:left;"></div>', editor.value());
        }

        function test_handles_style_color_values() {
            editor.value('<div style="color:#000000"></div>');
            assertEquals('<div style="color:#000000;"></div>', editor.value());
        }

        function test_comments() {
            editor.value('<!-- comment -->');
            assertEquals('<!-- comment -->', editor.value());
        }
        
        function test_cdata_encoding() {
            editor.value('<![CDATA[test]]>');
            assertEquals('<!--[CDATA[test]]-->', editor.body.innerHTML);
        }

        function test_cdata() {
            editor.value('<![CDATA[test]]>');
            assertEquals('<![CDATA[test]]>', editor.value());
        }

        function test_b_converted_to_strong() {
            editor.value('<b></b>');
            assertEquals('<strong></strong>', editor.value());
        }

        function test_i_converted_to_em() {
            editor.value('<i></i>');
            assertEquals('<em></em>', editor.value());
        }
        
        function test_u_converted_to_span_with_underline_style() {
            editor.value('<u></u>');
            assertEquals('<span style="text-decoration:underline;"></span>', editor.value());
        }

        function test_font_converted_to_span() {
            editor.value('<font color="#ff0000" face="verdana" size="5">foo</font>');
            assertEquals('<span style="color:#ff0000;font-face:verdana;font-size:x-large;">foo</span>', editor.value());
        }

        function test_script_tag_preserved() {
            editor.value('<script>var answer=42;<\/script>');
            assertEquals('<script>var answer=42;<\/script>', editor.value());
        }
        
        function test_script_tag_not_executed() {
            editor.value('<script>var answer=42;<\/script>');
            assertUndefined(window.answer);
        }

        function test_br_moz_dirty_removed() {
            editor.value('<br _moz_dirty="">');
            assertEquals('', editor.value());
        }
        
        function test_moz_dirty_removed() {
            editor.value('<hr _moz_dirty="">');
            assertEquals('<hr />', editor.value());
        }

        function test_multiple_attributes_sorted_alphabetically() {
            editor.value('<input type="button" class="t-button" style="display:none;" />');
            assertEquals('<input class="t-button" style="display:none;" type="button" />', editor.value());
        }

        function test_javascript_attributes() {
            editor.value('<input type="button" onclick="alert(1)" />');
            assertEquals('<input onclick="alert(1)" type="button" />', editor.value());
        }

        function test_value_attribute() {
            editor.value('<input type="button" value="test" />');
            assertEquals('<input type="button" value="test" />', editor.value());
        }

        function test_type_text_attribute() {
            editor.value('<input type="text" value="test" />');
            assertEquals('<input type="text" value="test" />', editor.value());
        }
        
        function test_style_ending_with_whitespace() {
            editor.value('<br style="display:none; " />');
            assertEquals('<br style="display:none;" />', editor.value());
        }

        function test_a_href_is_not_made_absolute() {
            editor.value('<a href="foo">a</a>');
            assertEquals('<a href="foo">a</a>', editor.value());
        }
        
        function test_link_href_is_not_made_absolute() {
            editor.value('<link href="foo" />');
            assertEquals('<link href="foo" />', editor.value());
        }
        
        function test_img_src_is_not_made_absolute() {
            editor.value('<img src="foo" />');
            assertEquals('<img src="foo" />', editor.value());
        }
        function test_script_src_is_not_made_absolute() {
            editor.value('<script src="foo" ><\/script>');
            assertEquals('<script src="foo"><\/script>', editor.value());
        }

        function test_href_without_quotes() {
            editor.value('<a href=foo>a</a>');
            assertEquals('<a href="foo">a</a>', editor.value());
        }
        
        function test_href_without_quotes_and_with_whitespace() {
            editor.value('<a href= foo >a</a>');
            assertEquals('<a href="foo">a</a>', editor.value());
        }
        
        function test_href_without_quotes_and_whith_other_attrubutes() {
            editor.value('<a href= foo class=test>a</a>');
            assertEquals('<a class="test" href="foo">a</a>', editor.value());
        }

        function test_href_with_single_quotes() {
            editor.value('<a href=\'foo\'>a</a>');
            assertEquals('<a href="foo">a</a>', editor.value());
        }
        
        function test_href_with_hash() {
            editor.value('<a href="#hash">a</a>');
            assertEquals('<a href="#hash">a</a>', editor.value());
        }
        
        function test_href_with_absolute() {
            editor.value('<a href="http://www.example.com">a</a>');
            assertEquals('<a href="http://www.example.com">a</a>', editor.value());
        }

        function test_href_with_absolute_and_url_content() {
            editor.value('<a href="http://www.example.com">www.example.com</a>');
            assertEquals('<a href="http://www.example.com">www.example.com</a>', editor.value());
        }

        function test_attributes_starting_with_underscore_moz_are_removed() {
            editor.value('<br _moz_resizing="true" />');
            assertEquals('<br />', editor.value());
        }

        function test_empty_whitespace_whitespace_trimmed() {
            editor.value('<br />      ');
            assertEquals('<br />', editor.value());
        }

        function test_whitespace_empty_whitespace_trimmed() {
            editor.value('           <br />');
            assertEquals('<br />', editor.value());
        }
        
        function test_whitespace_empty_inline_whitespace_trimmed() {
            editor.value('           <a></a>');
            assertEquals('<a></a>', editor.value());
        }

        function test_whitespace_inline_whitespace_trimmed() {
            editor.value('           <a>foo</a>');
            assertEquals('<a>foo</a>', editor.value());
        }

        function test_empty_inline_whitespace_whitespace_trimmed() {
            editor.value('<a></a>     ');
            assertEquals('<a></a>', editor.value());
        }

        function test_inline_whitespace_whitespace_collapsed() {
            editor.value('<a>foo</a>     ');
            assertEquals('<a>foo</a> ', editor.value());
        }
        
        function test_whitespace_empty_block_whitespace_trimmed() {
            editor.value('           <div></div>');
            assertEquals('<div></div>', editor.value());
        }
        
        function test_whitespace_block_whitespace_trimmed() {
            editor.value('           <div>foo</div>');
            assertEquals('<div>foo</div>', editor.value());
        }

        function test_empty_block_whitespace_whitespace_trimmed() {
            editor.value('<div></div>     ');
            assertEquals('<div></div>', editor.value());
        }

        function test_block_whitespace_whitespace_trimmed() {
            editor.value('<div>foo</div>     ');
            assertEquals('<div>foo</div>', editor.value());
        }

        function test_trimming_whitespace_within_content() {
            editor.value('<span>foo   bar</span>');
            assertEquals('<span>foo bar</span>', editor.value());
        }

        function test_keeping_white_space_in_pre() {
            editor.value('<pre>foo   bar</pre>');
            assertEquals('<pre>foo   bar</pre>', editor.value());
        }
        
        function test_keeping_white_space_in_pre_children() {
            editor.value('<pre><span>   foo  </span></pre>');
            assertEquals('<pre><span>   foo  </span></pre>', editor.value());
        }

        function test_text_whitespace_inline_whitespace_collapsed() {
            editor.value('foo  <strong>bar</strong>');
            assertEquals('foo <strong>bar</strong>', editor.value());
        }

        function test_text_whitespace_block_whitespace_preserved() {
            editor.value('foo <div>bar</div>');
            assertEquals('foo <div>bar</div>', editor.value());
        }

        function test_text_whitespace_empty_element_whitespace_preserved() {
            editor.value('foo <br />');
            assertEquals('foo <br />', editor.value());
        }

        function test_empty_element_whitespace_text_whitespace_trimmed() {
            editor.value('<br /> foo');
            assertEquals('<br />foo', editor.value());
        }

        function test_whitespace_at_end_of_inline_element_preserved() {
            editor.value('<strong>foo </strong>');
            assertEquals('<strong>foo </strong>', editor.value());
        }

        function test_whitespace_at_beginning_of_inline_element_after_text() {
            editor.value('foo bar<strong> baz</strong>');
            assertEquals('foo bar<strong> baz</strong>', editor.value());
        }          
        
        function test_whitespace_at_end_of_inline_element_after_text() {
            editor.value('foo bar<strong>baz </strong>');
            assertEquals('foo bar<strong>baz </strong>', editor.value());
        }        
        
        function test_whitespace_at_end_of_inline_element() {
            editor.value('<strong>baz </strong>');
            assertEquals('<strong>baz </strong>', editor.value());
        }         
        
        function test_whitespace_at_beginning_of_inline_element() {
            editor.value('<strong> baz</strong>');
            assertEquals('<strong>baz</strong>', editor.value());
        }        
        
        function test_whitespace_at_beginning_of_inline_element_before_text() {
            editor.value('<p><strong>foo</strong> bar</p>');
            assertEquals('<p><strong>foo</strong> bar</p>', editor.value());
        }

        function test_complete_attribute_ignored() {
            editor.value('<img complete="complete" />');
            assertEquals('<img />', editor.value());
        }
        
        function test_nbsp() {
            editor.value('&nbsp;&nbsp;&nbsp;');
            assertEquals('&nbsp;&nbsp;&nbsp;', editor.value());
        }

        function test_nbsp_and_whitespace() {
            editor.value('            &nbsp;&nbsp;&nbsp;');
            assertEquals('&nbsp;&nbsp;&nbsp;', editor.value());
        }
        function test_amp() {
            editor.value('&amp;');
            assertEquals('&amp;', editor.value());
        }
        
        function test_lt() {
            editor.value('&lt;');
            assertEquals('&lt;', editor.value());
        }
        
        function test_gt() {
            editor.value('&gt;');
            assertEquals('&gt;', editor.value());
        }

        function test_amp_escaped() {
            editor.value('&amp;');
            assertEquals('&amp;amp;', editor.encodedValue());
        }

        function test_gt_escaped() {
            editor.value('&gt;');
            assertEquals('&amp;gt;', editor.encodedValue());
        }

        function test_nbsp_escaped() {
            editor.value('&nbsp;');
            assertEquals('&amp;nbsp;', editor.encodedValue());
        }
    </script>
</asp:Content>
