<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ClientSideApi</h2>
    
    <%= Html.Telerik().Window()
            .Name("Window")
            .Title("Title")
            .Content("Content")
            .Effects(fx => fx.Toggle()) 
    %>

    <%= Html.Telerik().Window()
            .Name("ModalWindow")
            .Title("Modal Window")
            .Content("No content to display")
            .Modal(true)
            .Visible(false)
            .Effects(fx => fx.Toggle()) 
    %>
    
    <script type="text/javascript">

    var oWindow;

    function getWindow(selector) {
        return $(selector || '#Window').data('tWindow');
    }

    function setUp() {
        oWindow = getWindow();
    }
    
    function test_title_gets_title() {
        assertEquals('Title', oWindow.title());
    }
    
    function test_title_sets_title() {
        var oldTitle = oWindow.title();
        var titleElement = $('.t-window-title', oWindow.element);

        oWindow.title('Title is the new title!');

        assertEquals('Title is the new title!', titleElement.text());

        oWindow.title(oldTitle);

        assertEquals(oldTitle, titleElement.text());
    }
    
    function test_content_gets_content() {
        assertEquals('Content', oWindow.content());
    }
    
    function test_content_sets_content() {
        var oldContent = oWindow.content();
        var contentElement = $('.t-window-content', oWindow.element);

        oWindow.content('Content is the new content!');

        assertEquals('Content is the new content!', contentElement.text());

        oWindow.content(oldContent);
        
        assertEquals(oldContent, contentElement.text());
    }

    function test_open_of_modal_window_adds_overlay_if_it_does_not_exist() {
        $('body > .t-overlay').remove();

        getWindow('#ModalWindow').open();

        assertEquals(1, $('body > .t-overlay').length);
    }

    function test_dblclick_on_resizable_window_title_maximizes_window() {
        var $window = $('<div />').tWindow();

        $window.find('.t-window-titlebar').trigger('dblclick');

        assertTrue($window.data('tWindow').isMaximized);
    }

    function test_dblclick_on_non_resizable_window_title_does_not_maximize_window() {
        var $window = $('<div />').tWindow({ resizable: false });

        $window.find('.t-window-titlebar').trigger('dblclick');

        assertTrue(!$window.data('tWindow').isMaximized);
    }
    
    </script>
</asp:Content>
