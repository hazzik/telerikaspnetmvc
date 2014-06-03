<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<DynamicTabContent>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Tab Dynamic Item Example
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.jQuery().Tab().Name("tabs")
                          .Items(parent =>
                                         {
                                             for(var i = 0; i < Model.Count; i++)
                                             {
                                                 var dynamicContent = Model[i];

                                                 parent.Add()
                                                     .Text(dynamicContent.Text)
                                                     .Content(() =>
                                                                      {%>
                                                                        <p><%= Html.Encode(dynamicContent.Content) %></p>
                                                                      <%}
                                                             )
                                                     .Selected(dynamicContent.IsSelected)
                                                     .Disabled(dynamicContent.IsDisabled);
                                             }
                                         }
                                )
                          .Render(); %>
</asp:Content>