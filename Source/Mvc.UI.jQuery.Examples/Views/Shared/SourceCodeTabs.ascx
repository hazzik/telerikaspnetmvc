<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

    <% Html.jQuery().Tab()
                    .Name("sourceCodes")
                    .Items(parent =>
                                  {
                                      parent.Add()
                                          .Text("Description")
                                          .Content(() =>
                                                         {%>
                                                             <p>
                                                                <%= ViewData.Get<string>("description") %>
                                                            </p>
                                                         <%}
                                                  );
                                      
                                      parent.Add()
                                          .Text("View Page")
                                          .Content(() =>
                                                         {%>
                                                             <p>
                                                                <pre name="code" class="xml" cols="20" rows="80" style="width:100%;height:100%"><%= Html.Encode(ViewData.Get<string>("viewSourceCode")) %></pre>
                                                            </p>
                                                         <%}
                                                  );

                                       parent.Add()
                                           .Text("Controller")
                                           .Content(() =>
                                                         {%>
                                                             <p>
                                                                <pre name="code" class="c-sharp" style="width:100%;height:100%"><%= Html.Encode(ViewData.Get<string>("controllerSourceCode"))%></pre>
                                                            </p>
                                                         <%}
                                                    );

                                      parent.Add()
                                          .Text("Master Page")
                                          .Content(() =>
                                                         {%>
                                                             <p>
                                                                <pre name="code" class="xml" cols="20" rows="80" style="width:100%;height:100%"><%= Html.Encode(ViewData.Get<string>("masterPageSourceCode")) %></pre>
                                                            </p>
                                                         <%}
                                                    );
                                  }
                           )
                    .Render(); %>