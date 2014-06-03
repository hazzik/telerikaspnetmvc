<%@ Page Title="Ajax Loading Of Content Tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Ajax Loading Of Content Tests</h2>
        
 <% Html.Telerik().TabStrip()
            .Name("myTab")
            .Items(parent =>
                          {
                              parent.Add()
                                  .Text("Nunc tincidunt")
                                  .Content(() =>
                                         {%>
                                             <p>
                                                 Proin elit arcu, rutrum commodo, vehicula tempus, 
                                                 commodo a, risus. Curabitur nec arcu. Donec 
                                                 sollicitudin mi sit amet mauris. Nam elementum 
                                                 quam ullamcorper ante. Etiam aliquet massa et 
                                                 lorem. Mauris dapibus lacus auctor risus. Aenean 
                                                 tempor ullamcorper leo. Vivamus sed magna quis 
                                                 ligula eleifend adipiscing. Duis orci. Aliquam 
                                                 sodales tortor vitae ipsum. Aliquam nulla. Duis 
                                                 aliquam molestie erat. Ut et mauris vel pede 
                                                 varius sollicitudin. Sed ut dolor nec orci 
                                                 tincidunt interdum. Phasellus ipsum. Nunc 
                                                 tristique tempus lectus.
                                            </p>
                                         <%}
                                    );
							  
                              parent.Add()
                                  .Text("Nunc tincidunt 2")
                                  .Content(() =>
                                         {%>
                                             <p>
                                                 Lipsum!!!
                                            </p>
                                         <%}
                                    );

                               parent.Add()
                                   .Text("Proin dolor")
                                   .LoadContentFrom(Url.Action("AjaxView1", "TabStrip"));

                               parent.Add()
                                   .Text("Aenean lacinia")
								   .LoadContentFrom(Url.Action("AjaxView2", "TabStrip"));
                          }
                   )
            .Render(); %>

    <script type="text/javascript">

        function getRootItem(index) {
            return $('#myTab').find('.t-item').eq(index)
        }

        function getContentItem(index) {
            return $('#myTab').find('.t-content').eq(index)
        }

//        function test_clicking_should_make_clicked_item_active() {
//            var item = getRootItem(1);

//            item.trigger('click');
//            
//            assertTrue(item.hasClass('t-state-active'));
//        }
//        
//        function test_clicking_should_make_all_items_except_clicked_unactive()
//        {
//            var item = getRootItem(0);
//            
//            item.trigger('click');

//            assertEquals(1, item.parent().find('.t-state-active').length);
//        }

//        function test_clicked_item_should_make_tab_content_active() {

//            var item = getRootItem(1);
//            var content = getContentItem(1);            

//            item.trigger('click');

//            assertTrue(content.hasClass('t-state-active'));
//        }

//        function test_clicking_should_make_all_items_except_clicked_unactive() {
//        
//            var item = getRootItem(0);

//            item.trigger('click');

//            assertEquals(1, $('#myTab').find('.t-content').find('.t-state-active').length);
//        }
//        
//        function test_clicking_unactive_contentUrl_items_should_push_its_index_to_contentTabs() {

//            var item = getRootItem(2);
//            
//            var index = item.parent().children().index(item);

//            item.trigger('click');

//            var isTrue = $.inArray(index, jQuery('#myTab').data('tTabStrip').contentTabs) >= 3;
//            assertTrue(isTrue);
//        }
        
        //how to test: once the data is async loaded it should not be requestes again?
        
        //current clicked item should be active and all other should be unactive.
    </script>

</asp:Content>