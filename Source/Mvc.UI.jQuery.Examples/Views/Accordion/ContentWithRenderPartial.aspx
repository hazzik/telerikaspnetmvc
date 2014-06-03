<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Accordion Content With Render Partial
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.jQuery().Accordion()
                    .Name("myAccordionRenderPartial")
                    .Items(parent =>
                                   {
                                       parent.Add()
                                           .Text("Section 1")
                                           .Content(() =>
                                                            {%>
                                                                <p>
                                                                    <% Html.RenderPartial("DummyControl"); %>
                                                                </p>
                                                            <%}
                                                    );
                                   }
                          )
                    .Render(); %>
</asp:Content>