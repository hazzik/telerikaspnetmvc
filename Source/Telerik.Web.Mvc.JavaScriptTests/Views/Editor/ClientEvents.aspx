<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        ClientEvents</h2>
    <%= Html.Telerik().Editor()
              .Name("Editor")
              .Value("foo")
              .ClientEvents(events => events
                  .OnLoad("onLoad")
                  .OnSelectionChange("onSelectionChange")
                  .OnChange("onChange")
                  .OnExecute("onExecute"))
    %>
    <script type="text/javascript">
        var editor;

        function getEditor() {
            return $('#Editor').data("tEditor");
        }

        function onLoad() {
            loadRaised = true;
        }

        function onSelectionChange() {
            onSelectionChangeRaised = true;
        }

        function onChange() {
            onChangeRaised = true;
        }

        function onExecute() {
            onExecuteRaised = true;
        }

        var changeRaised = false;
        var loadRaised = false;
        var onSelectionChangeRaised = false;
        var onChangeRaised = false;
        var onExecuteRaised = false;

        function setUp() {
            changeRaised = false;
            editor = getEditor();

            $(editor.element).bind('selectionChange', function () {
                changeRaised = true;
            });

            editor.focus();
        }

        function type(keyCode, ctrl, alt, shift) {
            var e = $.Event();
            e.keyCode = keyCode;
            e.ctrlKey = !!ctrl;
            e.altKey = !!alt;
            e.shiftKey = !!shift;
            e.type = 'keyup';

            var editor = getEditor();
            $(editor.document).trigger(e);
        }

        function test_onChange_executed() {
            editor.value('bar');
            editor.body.innerHTML = 'foo';
            $(editor.window).trigger('blur');
            assertTrue(onChangeRaised);
        }

        function test_onload_executed() {
            assertTrue(loadRaised);
        }

        function test_on_selection_change_executed() {
            onSelectionChangeRaised = false;
            $(getEditor().element).trigger('selectionChange');
            assertTrue(onSelectionChangeRaised);
        }

        function test_up_arrow_raises_selection_change() {
            type(38);

            assertTrue(changeRaised);
        }

        function test_exec_raises_onExecute() {
            editor.exec('undo');
            assertTrue(onExecuteRaised);
        }

        function test_exec_supplies_command_name_and_object() {
            var e;
            $(editor.element).bind('execute', function() {
                e = arguments[0];
            });

            editor.value('foo');
            var range = editor.createRange();
            range.selectNodeContents(editor.body.firstChild);
            editor.getSelection().removeAllRanges();
            editor.getSelection().addRange(range);

            editor.exec('bold');

            assertEquals('bold', e.name);
            assertTrue(e.command instanceof $.telerik.editor.FormatCommand);
        }

        function test_down_arrow_raises_selection_change() {
            type(40);

            assertTrue(changeRaised);
        }

        function test_left_arrow_raises_selection_change() {
            type(37);

            assertTrue(changeRaised);
        }

        function test_right_arrow_raises_selection_change() {
            type(39);

            assertTrue(changeRaised);
        }

        function test_home_raises_selection_change() {
            type(36);

            assertTrue(changeRaised);
        }

        function test_end_raises_selection_change() {
            type(35);

            assertTrue(changeRaised);
        }

        function test_pgup_raises_selection_change() {
            type(33);

            assertTrue(changeRaised);
        }

        function test_pgdown_raises_selection_change() {
            type(34);

            assertTrue(changeRaised);
        }

        function test_insert_raises_selection_change() {
            type(45);

            assertTrue(changeRaised);
        }

        function test_backspace_raises_selection_change() {
            type(9);

            assertTrue(changeRaised);
        }

        function test_enter_raises_selection_change() {
            var editor = getEditor();
            editor.value('foo');
            type(13);

            assertTrue(changeRaised);
        }

        function test_mouseup_raises_selection_change() {
            $(getEditor().document).mouseup();
            assertTrue(changeRaised);
        }

        function test_exec_raises_selection_change() {
            var editor = getEditor();
            editor.value('foo');
            var range = editor.createRange();
            range.selectNodeContents(editor.body.firstChild);
            editor.getSelection().removeAllRanges();
            editor.getSelection().addRange(range);

            editor.exec('bold');
            assertTrue(changeRaised);
        }

        function test_undo_raises_selection_change() {
            var editor = getEditor();
            editor.exec('undo');

            assertTrue(changeRaised);
        }

        function test_redo_raises_selection_change() {
            var editor = getEditor();
            editor.exec('redo');

            assertTrue(changeRaised);
        }
    </script>
</asp:Content>
