<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Accordion Events Example
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.jQuery().Accordion()
                    .Name("myAccordion")
                    .Items(parent =>
                                   {
                                       parent.Add()
                                           .Text("Section 1")
                                           .Content(() =>
                                                            {%>
                                                                <p>
                                                                    Mauris mauris ante, blandit et, ultrices a, 
                                                                    suscipit eget, quam. Integer ut neque. 
                                                                    Vivamus nisi metus, molestie vel, gravida in, 
                                                                    condimentum sit amet, nunc. Nam a nibh. 
                                                                    Donec suscipit eros. Nam mi. Proin viverra 
                                                                    leo ut odio. Curabitur malesuada. Vestibulum 
                                                                    a velit eu ante scelerisque vulputate.
                                                                </p>
                                                            <%}
                                                    );

                                       parent.Add()
                                           .Text("Section 2")
                                           .Content(() =>
                                                            {%>
                                                                <p>
                                                                    Sed non urna. Donec et ante. Phasellus eu 
                                                                    ligula. Vestibulum sit amet purus. Vivamus 
                                                                    hendrerit, dolor at aliquet laoreet, mauris 
                                                                    turpis porttitor velit, faucibus interdum 
                                                                    tellus libero ac justo. Vivamus non quam. 
                                                                    In suscipit faucibus urna.
                                                                </p>
                                                            <%}
                                                    );

                                       parent.Add()
                                           .Text("Section 3")
                                           .Content(() =>
                                                            {%>
                                                                <p>
                                                                    Nam enim risus, molestie et, porta ac, 
                                                                    aliquam ac, risus. Quisque lobortis. 
                                                                    Phasellus pellentesque purus in massa. 
                                                                    Aenean in pede. Phasellus ac libero ac 
                                                                    tellus pellentesque semper. Sed ac felis. 
                                                                    Sed commodo, magna quis lacinia ornare, 
                                                                    quam ante aliquam nisi, eu iaculis leo purus 
                                                                    venenatis dui.
                                                                </p>
                                                                <ul>
                                                                    <li>List item one</li>
                                                                    <li>List item two</li>
                                                                    <li>List item three</li>
                                                                </ul>
                                                            <%}
                                                    );

                                       parent.Add()
                                           .Text("Section 4")
                                           .Content(() =>
                                                            {%>
                                                                <p>
                                                                    Cras dictum. Pellentesque habitant morbi 
                                                                    tristique senectus et netus et malesuada 
                                                                    fames ac turpis egestas. Vestibulum ante 
                                                                    ipsum primis in faucibus orci luctus et 
                                                                    ultrices posuere cubilia Curae; Aenean 
                                                                    lacinia mauris vel est.
                                                                </p>
                                                                <p>
                                                                    Suspendisse eu nisl. Nullam ut libero. 
                                                                    Integer dignissim consequat lectus. Class 
                                                                    aptent taciti sociosqu ad litora torquent 
                                                                    per conubia nostra, per inceptos himenaeos.
                                                                </p>
                                                            <%}
                                                    );
                                   }
                          )
                    .OnChange(() =>
                                    {%>
                                        function(event, ui)
                                        {
                                            $('#trace').append('Change fired: ' + new Date() + '<br/>');
                                        }
                                    <%}
                              )
                    .Render(); %>
    <br/>
    <div id="trace" style="font-family:Courier New;background-color:#eee"></div>
</asp:Content>