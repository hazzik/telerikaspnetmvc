<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Keyboard support</h2>
    
    <script type="text/javascript">

        var $t;
        var combobox;
        var filtering;

        function getComboBox(id) {
            return $(id || '#ComboBox').data('tComboBox');
        }

        function setUp() {
            $t = $.telerik;
            combobox = getComboBox();
            filtering = combobox.filtering;
            combobox.scrollTo = function () { };
        }

        function test_filter_method_should_pass_first_item_text_to_the_autoFill_method() {

            var oldAutoFill = filtering.autoFill;
            var text;

            try {
                combobox.$text.val('boy');
                filtering.autoFill = function (component, itemText) { text = itemText; };
                filtering.filter(combobox);

                assertEquals("Boysenberry", text);
            } finally {
                filtering.autoFill = oldAutoFill;
                combobox.$text.val('');
                filtering.filter(combobox);
            }
        }

        function test_filter_method_should_pass_Tofu_text_to_the_autoFill_method_if_filter_mode_is_None() {

            var oldAutoFill = filtering.autoFill;
            var text;
            
            try {
                combobox.filter = 0;
                filtering.autoFill = function (component, itemText) { text = itemText; };
                combobox.$text.val('tofu');

                filtering.filter(combobox);

                assertEquals("Tofu", text);
            } finally {
                filtering.autoFill = oldAutoFill;
                combobox.filter = 1;
            }
        }

        function test_filter_method_should_return_if_text_length_is_less_than_minChars() {
            var oldFilter = combobox.filters[combobox.filter];
            var isCalled = false;
            
            try {
                combobox.minChars = 10;
                combobox.$text.val('12345');
                combobox.filters[combobox.filter] = function () { isCalled = true; };
                
                filtering.filter(combobox);

                assertFalse(isCalled);
            } finally {
                combobox.minChars = 0;
                combobox.$text.val('');
                combobox.filters[combobox.filter] = oldFilter;
            }
        }

        function test_filter_should_call_startsWith_filter_method() {
            var oldFilter = combobox.filters[combobox.filter];
            var isCalled = false;

            combobox.dropDown.$items = null;
            combobox.fill();
            
            try {
                combobox.filters[combobox.filter] = function () { isCalled = true; };
                combobox.selectedIndex = -1;

                filtering.filter(combobox);

                assertTrue(isCalled);
            } finally {
                combobox.filters[combobox.filter] = oldFilter;
            }
        }

        function test_filter_method_should_call_ajaxRequest_method_if_isAjax_and_no_cache_or_no_items_after_filtering() {
            var oldAjaxRequest = combobox.ajaxRequest;
            var oldAjax = combobox.loader.isAjax;
            var isCalled = false;
            
            combobox.filteredDataIndexes = [];
            combobox.cache = false;

            try {
                combobox.loader.ajaxRequest = function () { isCalled = true; };
                combobox.loader.isAjax = function () { return true; };
            
                filtering.filter(combobox);

                assertTrue(isCalled);
            } finally {
                combobox.loader.isAjax = oldAjax;
                combobox.loader.ajaxRequest = oldAjaxRequest;
            }
        }

        function test_filter_method_should_call_startsWith_method_if_isAjax_and_cache_is_enabled() {

            var oldFilter = combobox.filters[combobox.filter];
            var oldAjax = combobox.loader.isAjax;
            var oldData = combobox.data;
            var oldCache = combobox.cache;
            var oldFilteredDataIndexes = combobox.filteredDataIndexes;
            var isCalled = false;
            
            try {
                combobox.data = [{ "Text": "Chai", "Value": "1", "Selected": true}];
                combobox.filteredDataIndexes = [];
                combobox.cache = true;

                combobox.filters[combobox.filter] = function () { isCalled = true; };
                combobox.loader.isAjax = function () { return true; }; //predefining isAjax method to return 'true';
            
                filtering.filter(combobox);

                assertTrue(isCalled);
            } catch(e) {
            } finally {
                combobox.data = oldData;
                combobox.cache = oldCache;
                combobox.loader.isAjax = oldAjax;
                combobox.filteredDataIndexes = oldFilteredDataIndexes;
                combobox.filters[combobox.filter] = oldFilter;
            }
        }

        function test_filter_method_should_rebind_and_close_dropDown_if_no_data_after_ajax_request() {

            var oldAjax = combobox.loader.isAjax;
            var oldData = combobox.data;
            var ajaxRequest = combobox.loader.ajaxRequest;

            combobox.open();

            combobox.cache = false;

            try {
                combobox.loader.isAjax = function () { return true; };
                combobox.loader.ajaxRequest = function (callback) { callback([]); };
            
                filtering.filter(combobox);

                assertEquals(0, combobox.dropDown.$items.length);
                assertTrue(combobox.dropDown.$element.attr('style').indexOf('none') != -1);
                
            } finally {
                combobox.data = oldData;
                combobox.loader.isAjax = oldAjax;
                combobox.loader.ajaxRequest = ajaxRequest;
            }
        }

        function test_filter_method_should_rebind_and_close_items_list_and_open_after_filtering() {
            var oldAjax = combobox.loader.isAjax;
            var oldData = combobox.data;
            var ajaxRequest = combobox.loader.ajaxRequest;

            combobox.close();

            combobox.cache = false;

            try {
                combobox.loader.isAjax = function () { return true; };
                combobox.loader.ajaxRequest = function (callback) { callback([{ "Text": "Chai", "Value": "1" }, { "Text": "Chang", "Value": "2"}]); };

                combobox.dropDown.$items = [];
            
                filtering.filter(combobox);

                assertEquals(2, combobox.dropDown.$items.length);
                assertTrue(combobox.dropDown.isOpened());

            } finally {
                combobox.data = oldData;
                combobox.loader.isAjax = oldAjax;
                combobox.loader.ajaxRequest = ajaxRequest;
            }
        }

        function test_selecting_items_after_filtering_selects_correct_item() {
            var oldAjax = combobox.loader.isAjax;
            var oldData = combobox.data;
            var ajaxRequest = combobox.loader.ajaxRequest;

            combobox.close();

            combobox.cache = false;

            try {
                combobox.loader.isAjax = function () { return true; };
                combobox.loader.ajaxRequest = function (callback) { callback([
                    { "Text": "Chai", "Value": "1" },
                    { "Text": "Tofu", "Value": "3" },
                    { "Text": "Chang", "Value": "2"}]); };

                combobox.dropDown.$items = [];
                combobox.$text.val('C');
            
                filtering.filter(combobox);

                combobox.dropDown.$element.find('.t-item').eq(1).click();

                assertEquals('Chang', combobox.$text.val());

            } finally {
                combobox.data = oldData;
                combobox.$text.val('');
                combobox.loader.isAjax = oldAjax;
                combobox.loader.ajaxRequest = ajaxRequest;
            }
        }

    </script>
           
    <%= Html.Telerik().ComboBox()
        .Name("ComboBox")
        .Items(items =>
        {
            items.Add().Text("Chai").Value("1");
            items.Add().Text("Chang").Value("2");
            items.Add().Text("Tofu").Value("3");
            items.Add().Text("Boysenberry").Value("4");
            items.Add().Text("Uncle").Value("5");
            items.Add().Text("Northwoods").Value("6");
            items.Add().Text("Ikura").Value("7");
            items.Add().Text("Queso").Value("8");
            items.Add().Text("Manchego").Value("9");
            items.Add().Text("Dried").Value("10");
            items.Add().Text("тtem20").Value("11");
        })
        .Effects(fx => fx.Toggle())
        .Filterable(filtering => filtering.FilterMode(AutoCompleteFilterMode.StartsWith))
    %>
</asp:Content>