<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Tab Load Via Ajax Example
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.jQuery().Tab()
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
                                           .Text("Proin dolor")
                                           .LoadContentFrom(Url.Action("AjaxView1", "Tab"));

                                       parent.Add()
                                           .Text("Aenean lacinia")
                                           .LoadContentFrom(Url.Action("AjaxView2", "Tab"));
                                  }
                           )
                    .Render(); %>
</asp:Content>