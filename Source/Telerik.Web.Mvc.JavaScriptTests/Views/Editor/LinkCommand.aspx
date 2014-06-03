<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        LinkCommand</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    <script type="text/javascript">
        var editor;
        var LinkCommand;

        function setUp() {
            editor = getEditor();
            LinkCommand = $.telerik.editor.LinkCommand;
        }

        function tearDown() {
            var window = $('.t-window').data('tWindow');
            if (window) window.destroy();
        }

        function execLinkCommandOnRange(range) {
            var command = new LinkCommand({ range: range });
            command.editor = editor;
            command.exec();

            return command;
        }

        function test_exec_creates_window() {
            var range = createRangeFromText(editor, '|foo|');
            execLinkCommandOnRange(range);

            assertEquals(1, $('.t-window').length)
        }

        function test_clicking_close_closes_the_window() {
            var range = createRangeFromText(editor, '|foo|');
            execLinkCommandOnRange(range);

            $('.t-dialog-close').click();
            assertEquals(0, $('.t-window').length)
        }

        function test_clicking_insert_closes_the_window() {
            var range = createRangeFromText(editor, '|foo|');
            execLinkCommandOnRange(range);

            $('.t-dialog-insert').click();
            assertEquals(0, $('.t-window').length)
        }

        function test_text_is_filled_when_only_text_is_selected() {
            var range = createRangeFromText(editor, '|foo bar|');
            execLinkCommandOnRange(range);

            assertEquals('foo bar', $('#t-editor-link-text').val())
        }

        function test_text_is_removed_if_more_than_one_text_node_is_selected() {
            var range = createRangeFromText(editor, '|foo <strong>bar</strong>|');
            execLinkCommandOnRange(range);

            assertEquals(0, $('label[for=t-editor-link-text]').length)
            assertEquals(0, $('#t-editor-link-text').length)
        }

        function test_collapsed_range_is_expanded() {
            editor.value('foo');
            var range = editor.createRange();
            
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 1);
            
            execLinkCommandOnRange(range);

            assertEquals('foo', $('#t-editor-link-text').val())
        }

        function test_clicking_insert_inserts_link_if_url_is_set() {
            var range = createRangeFromText(editor, '|foo|');
            execLinkCommandOnRange(range);

            $('#t-editor-link-url').val('foo');
            $('.t-dialog-insert').click();
            assertEquals('<a href="foo">foo</a>', editor.value())
        }

        function test_clicking_insert_does_not_inserts_link_if_url_is_not_set() {
            var range = createRangeFromText(editor, '|foo|');
            execLinkCommandOnRange(range);

            $('#t-editor-link-url').val('');
            $('.t-dialog-insert').click();
            assertEquals('foo', editor.value())
        }

        function test_clicking_insert_updates_existing_url() {
            var range = createRangeFromText(editor, '<a href="bar">|foo|</a>');
            execLinkCommandOnRange(range);

            $('#t-editor-link-url').val('foo');
            $('.t-dialog-insert').click();
            assertEquals('<a href="foo">foo</a>', editor.value())
        }

        function test_url_text_is_set() {
            var range = createRangeFromText(editor, '<a href="bar">|foo|</a>');
            execLinkCommandOnRange(range);

            assertEquals('bar', $('#t-editor-link-url').val());
        }

        function test_hitting_enter_in_url_inserts_link() {
            var range = createRangeFromText(editor, '|foo|');
            execLinkCommandOnRange(range);

            var e = new $.Event();
            e.type = 'keydown';
            e.keyCode = 13;
            
            $('#t-editor-link-url')
                .val('foo')
                .trigger(e);
            
            assertEquals('<a href="foo">foo</a>', editor.value())
            assertEquals(0, $('.t-window').length);
        }
        function test_hitting_esc_in_url_cancels() {
            var range = createRangeFromText(editor, '|foo|');
            execLinkCommandOnRange(range);

            var e = new $.Event();
            e.type = 'keydown';
            e.keyCode = 27;

            $('#t-editor-link-url')
                .val('foo')
                .trigger(e);

            assertEquals('foo', editor.value())
            assertEquals(0, $('.t-window').length);
        }

        function test_hitting_enter_in_name_field_inserts_link() {
            var range = createRangeFromText(editor, '|foo|');
            execLinkCommandOnRange(range);

            var e = new $.Event();
            e.type = 'keydown';
            e.keyCode = 13;
            
            $('#t-editor-link-url')
                .val('foo')
            $('#t-editor-link-text').trigger(e);

            assertEquals('<a href="foo">foo</a>', editor.value())
            assertEquals(0, $('.t-window').length);
        }
        
        function test_hitting_enter_in_title_field_inserts_link() {
            var range = createRangeFromText(editor, '|foo|');
            execLinkCommandOnRange(range);

            var e = new $.Event();
            e.type = 'keydown';
            e.keyCode = 13;

            $('#t-editor-link-url')
                .val('foo')
            $('#t-editor-link-title').trigger(e);

            assertEquals('<a href="foo">foo</a>', editor.value())
            assertEquals(0, $('.t-window').length);
        }

         function test_hitting_esc_in_text_cancels() {
            var range = createRangeFromText(editor, '|foo|');
            execLinkCommandOnRange(range);

            var e = new $.Event();
            e.type = 'keydown';
            e.keyCode = 27;

            $('#t-editor-link-url')
                .val('foo')

            $('#t-editor-link-text').trigger(e);
            
            assertEquals('foo', editor.value())
            assertEquals(0, $('.t-window').length);
        }
        
        function test_hitting_esc_in_title_cancels() {
            var range = createRangeFromText(editor, '|foo|');
            execLinkCommandOnRange(range);

            var e = new $.Event();
            e.type = 'keydown';
            e.keyCode = 27;

            $('#t-editor-link-url')
                .val('foo')

            $('#t-editor-link-title').trigger(e);

            assertEquals('foo', editor.value())
            assertEquals(0, $('.t-window').length);
        }

        function test_closing_the_window_restores_content() {
            var range = createRangeFromText(editor, '|foo|');
            execLinkCommandOnRange(range);

            $('.t-window .t-close').click();

            assertEquals('foo', editor.value())
            assertEquals(0, $('.t-window').length);
        }

        function test_setting_title() {
            var range = createRangeFromText(editor, '|foo|');
            execLinkCommandOnRange(range);

            $('#t-editor-link-url')
                .val('foo');

            $('#t-editor-link-title')
                .val('bar')

            $('.t-dialog-insert').click();
            assertEquals('<a href="foo" title="bar">foo</a>', editor.value())
        }

        function test_title_text_box_is_updated() {
            var range = createRangeFromText(editor, '<a href="#" title="bar">|foo|</a>');
            execLinkCommandOnRange(range);

            assertEquals('bar', $('#t-editor-link-title').val());
        }

        function test_target_checkbox_is_updated() {
            var range = createRangeFromText(editor, '<a href="#" target="_blank">|foo|</a>');
            execLinkCommandOnRange(range);

            assertEquals('true', $('#t-editor-link-target').val());
        }

        function test_updatung_link_text() {
            var range = createRangeFromText(editor, '|foo|');
            execLinkCommandOnRange(range);

            $('#t-editor-link-url')
                .val('foo');

            $('#t-editor-link-text')
                .val('bar')

            $('.t-dialog-insert').click();
            assertEquals('<a href="foo">bar</a>', editor.value())
        }
        
        function test_updating_link_text_from_caret() {
            editor.value('foo');
            var range = editor.getRange();
            range.setStart(editor.body.firstChild,1);
            range.setEnd(editor.body.firstChild,1);
            
            execLinkCommandOnRange(range);

            $('#t-editor-link-url')
                .val('foo');

            $('#t-editor-link-text')
                .val('bar')

            $('.t-dialog-insert').click();
            assertEquals('<a href="foo">bar</a>', editor.value())
        }

        function test_undo_restores_content() {
            editor.value('foo');
            var range = editor.getRange();
            range.setStart(editor.body.firstChild,1);
            range.setEnd(editor.body.firstChild,1);
            
            var command = execLinkCommandOnRange(range);

            $('#t-editor-link-url')
                .val('foo');

            $('#t-editor-link-text')
                .val('bar')

            $('.t-dialog-insert').click();
            command.undo();
            assertEquals('foo', editor.value());
        }

        function test_redo_creates_link() {
            editor.value('foo');
            var range = editor.getRange();
            range.setStart(editor.body.firstChild,1);
            range.setEnd(editor.body.firstChild,1);
            
            var command = execLinkCommandOnRange(range);

            $('#t-editor-link-url')
                .val('foo');

            $('#t-editor-link-text')
                .val('bar')

            $('.t-dialog-insert').click();
            command.undo();
            command.redo();
            assertEquals('<a href="foo">bar</a>', editor.value())            
        }

        function test_link_is_not_created_if_url_is_http_slash_slash() {
            var range = createRangeFromText(editor, '|foo|');
            
            execLinkCommandOnRange(range);

            $('.t-dialog-insert').click();
            assertEquals('foo', editor.value())            
        }
        
        function test_exec_inserts_link_with_empty_range() {
            editor.value('foo ');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 4);
            range.setEnd(editor.body.firstChild, 4);
            
            execLinkCommandOnRange(range);
            
            $('#t-editor-link-url')
                .val('bar');

            $('.t-dialog-insert').click();
            assertEquals('foo <a href="bar"></a>', editor.value())            
        }

    </script>
</asp:Content>
